using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public List<Player> players;
    public Player activePlayer;
    public EntitySpawner entitySpawner;
    public TileSpawner tileSpawner;
    
    public List<Entity> entities;
    public Text entityMovedInfo;
    
    private CombatSimulator _combatSimulator;
    private List<List<Tile>> _tiles;
    
    private void Awake()
    {       
        _tiles=tileSpawner.Initialize();
        _combatSimulator = new CombatSimulator(this);

    }
    public void TileClick(Vector2 tilePosition)
    {
        var entityMoved = (Combatant)activePlayer.entityMoved;
        var onTileCombatant = GetCombatant(tilePosition);

        if (entityMoved!=null)
        {

            if (entityMoved is not { DidAction: false })
            {
                activePlayer.entityMoved = null;
                return;
            }
            CombatantAction(tilePosition,onTileCombatant, entityMoved);
            activePlayer.entityMoved = null;
        }
        else
        {
            if (onTileCombatant!=null)
            {
                CombatantActive(tilePosition,onTileCombatant);
            }
            else
            {
                CreateEntity(tilePosition, Random.Range(1, 2));
            }
        }
    }
    private void CreateEntity(Vector2 tileId,int entityType)
    {
        entityMovedInfo.text = "";
        activePlayer.entityMoved = null;
        
        if (activePlayer.money < entitySpawner.GetEntityPrice(entityType)) return;
        
        var tile = _tiles[(int)tileId.x][(int)tileId.y];
        var entity = GetEntity(tileId);
        
        if (!IsColorsSame(tile) || entity!=null) return;
        entity = entitySpawner.CreateEntity(tile, entityType, activePlayer.color, entities.Count);
        entities.Add(entity);
        activePlayer.money -= entity.Price;
    }

    public void NextPlayer()
    {
        var id = activePlayer.playersId+1;
        if (activePlayer.playersId>=players.Count-1)
        {
            id = 0;
        }
        activePlayer = players[id];
        activePlayer.NewMove(entities.FindAll(x=>x.Color==activePlayer.color));
        foreach (var player in players)
        {
            player.entityMoved = null;
        }
    }
    private void FactoryClick(Vector2 tilePosition)
    {
        
    }

    private void CombatantClick(Vector2 tilePosition)
    {
       
    }

    private void CombatantActive(Vector2 tilePosition,Combatant combatant)
    {
        if (activePlayer.color != combatant.Color) return;
        activePlayer.entityMoved = combatant;
        entityMovedInfo.text =
            $"Entity id:{activePlayer.entityMoved.ID} Entity power:{combatant.Power} MovePoint:{combatant.DidAction}";
    }

    private void CombatantAction(Vector2 tileId,Combatant enemyEntity,Combatant playerEntity)
    {
        if (enemyEntity == null)
        {
            if (!IsColorsSame(playerEntity) || !IsEntityMoveDistance(playerEntity, tileId))return;
            NewTileColor(tileId, playerEntity);
            playerEntity.Move(GetTile(tileId).transform.position);
        }
        else
        {
            if (!IsColorsSame(playerEntity, enemyEntity) || !IsEntityOnAttackDistance(playerEntity, tileId) ||
                _combatSimulator.IsEntityLoseBattle(playerEntity, enemyEntity) != true ||
                _combatSimulator.IsEntityArcher(playerEntity)) return;
            NewTileColor(tileId, playerEntity);
            playerEntity.Move(GetTile(tileId).transform.position);
            activePlayer.entityMoved = null;

        }
    }

    private void NewTileColor(Vector2 tileId, Entity entity)
    {
        var tile = GetTile(tileId);
        entity.Position = tile.PositionTile;
        if (tile == null) return;
        tile.TileData = tileSpawner.tilesColor.Find(x => x.tileColor == activePlayer.color);
        tile.UpdateSprite();
    }

    private Tile GetTile(Vector2 position)
    {
        return (from tile in _tiles from _tile in tile where position == _tile.PositionTile select _tile).First();
    }
    private Combatant GetCombatant(Vector2 position)
    {
        return (Combatant)entities.Find(x => x as Combatant&& x.Position == position);
    }
    private Entity GetEntity(Vector2 position)
    {
        return entities.Find(x => x.Position == position);
    }
    private bool IsColorsSame(Entity playerEntity, Entity enemyEntity)
    {
        return enemyEntity.Color != playerEntity.Color;
    }
    private bool IsColorsSame(Entity entity)
    {
        return entity.Color == activePlayer.color;
    }
    private bool IsColorsSame(Tile tile)
    {
        return tile.TileData.tileColor == activePlayer.color;
    }
    private bool IsEntityOnAttackDistance(Combatant playerEntity,Vector2 tileId)
    {
        return playerEntity.attackDistance.Contains(tileId);
    }
    private bool IsEntityMoveDistance(Combatant playerEntity,Vector2 tileId)
    {
        return playerEntity.moveDistance.Contains(tileId);
    }
}