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
        activePlayer.money -= entity.price;
    }

    public void TileClick(Vector2 tileId)
    {
        var entity = activePlayer.entityMoved;
        var enemyEntity = GetEntity(tileId);
        if (enemyEntity != null && entity == null)
        {
            EntityClick(tileId);
            return;
        }
        if (entity is { didAction: false })
        {
            ActionEntity(tileId,enemyEntity, entity);
            return;
        }

        CreateEntity(tileId,Random.Range(0,2));
        
    }

    public void NextPlayer()
    {
        var id = activePlayer.playersId+1;
        if (activePlayer.playersId>=players.Count-1)
        {
            id = 0;
        }
        activePlayer = players[id];
        activePlayer.NewMove(entities.FindAll(x=>x.color==activePlayer.color));
        foreach (var player in players)
        {
            player.entityMoved = null;
        }
    }
    private void EntityClick(Vector2 tilePosition)
    {
        var entity = GetEntity(tilePosition);
        if (activePlayer.color!=entity.color)return;
        activePlayer.entityMoved =entity ;
        entityMovedInfo.text = $"Entity id:{activePlayer.entityMoved.id} Entity power:{activePlayer.entityMoved.power} MovePoint:{activePlayer.entityMoved.didAction}";
        entityMovedInfo.color = activePlayer.entityMoved.tile.tileData.color;
    }

    private void ActionEntity(Vector2 tileId,Entity enemyEntity,Entity playerEntity)
    {
        if (enemyEntity == null)
        {
            if (!IsColorsSame(playerEntity) || !IsEntityMoveDistance(playerEntity, tileId))return;
            NewTileColor(tileId, playerEntity);
            playerEntity.Move();
        }
        else
        {
            if (!IsColorsSame(playerEntity, enemyEntity) || !IsEntityOnAttackDistance(playerEntity, tileId) ||
                _combatSimulator.IsEntityLoseBattle(playerEntity, enemyEntity) != true ||
                _combatSimulator.IsEntityArcher(playerEntity)) return;
            NewTileColor(tileId, playerEntity);
            playerEntity.Move();
            activePlayer.entityMoved = null;

        }
    }

    private void NewTileColor(Vector2 tileId, Entity entity)
    {
        var tile = GetTile(tileId);
        entity.tile = tile;
        if (tile == null) return;
        tile.tileData = tileSpawner.tilesColor.Find(x => x.tileColor == activePlayer.color);
        tile.UpdateSprite();
    }

    private Tile GetTile(Vector2 position)
    {
        return (from tile in _tiles from _tile in tile where position == _tile.positionTile select _tile).First();
    }
    private Entity GetEntity(Vector2 position)
    {
        return entities.Find(x => x.tile.positionTile == position);
    }
    private Entity GetEntity(Tile tile)
    {
        return entities.Find(x => x.tile == tile);
    }

    private bool IsColorsSame(Entity playerEntity, Entity enemyEntity)
    {
        return enemyEntity.color != playerEntity.color;
    }
    private bool IsColorsSame(Entity entity)
    {
        return entity.color == activePlayer.color;
    }
    private bool IsColorsSame(Tile tile)
    {
        return tile.tileData.tileColor == activePlayer.color;
    }
    private bool IsEntityOnAttackDistance(Entity playerEntity,Vector2 tileId)
    {
        return playerEntity.attackDistance.Contains(tileId);
    }
    private bool IsEntityMoveDistance(Entity playerEntity,Vector2 tileId)
    {
        return playerEntity.moveDistance.Contains(tileId);
    }
}