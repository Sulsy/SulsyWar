using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<Player> players;
    public Player activePlayer;
    public EntitySpawner entitySpawner;
    public TileSpawner tileSpawner;
    private List<List<Tile>> _tiles;
    public List<Entity> entities;
    public Text entityMovedInfo;
    private void Awake()
    {       
        _tiles=tileSpawner.Initialize();              
                                                        
    }
    private void CreateEntity(Vector2 tileId,int entityId)
    {
        entityMovedInfo.text = "";
        activePlayer.entityMoved = null;
        var tile = _tiles[(int)tileId.x][(int)tileId.y];
        var entity = entities.Find(x => x.tile.positionTile == tile.positionTile);
        if (tile.tileData.tileColor != activePlayer.color || entity!=null) return;
        entities.Add(entitySpawner.CreateEntity(tile, entityId, activePlayer.color, entities.Count));

    }

    public void TileClick(Vector2 tileId)
    {
        var entity = activePlayer.entityMoved;
        if (GetEntity(tileId) != null && entity == null)
        {
            EntityClick(tileId);
            return;
        }
        if (entity is { didAction: false } && entity.moveDistance.Contains(tileId))
        {
            MoveEntity(tileId, entity);
            return;
        }
        
        CreateEntity(tileId, 0);
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

    private void MoveEntity(Vector2 tileId,Entity entity)
    {
        foreach (var gEntity in from _entity in entities
                 let tile = GetTile(tileId)
                 where tile.tileData.tileColor != entity.color
                 from position in _entity.moveDistance
                 where tileId == position
                 select GetEntity(tile)
                 into gEntity
                 where gEntity != null
                 select gEntity)
        {
            if (EntityBattle(entity, gEntity))
                return;
            else
                break;
        }
       
        NewTileColor(tileId, entity);
        entity.Move();
        activePlayer.entityMoved = null;
    }

    private bool EntityBattle(Entity entity, Entity gEntity)
    {
        if (gEntity == null) return true;
        if (Math.Abs(entity.power - gEntity.power) < 1)
        {
            entity.EntityDestroy();
            gEntity.EntityDestroy();
            return true;
        }

        if (entity.power > gEntity.power)
        {
            EntityLoseBattle(gEntity, entity);
            return false;
        }

        EntityLoseBattle(entity, gEntity);
        return true;
    }

    private void EntityLoseBattle(Entity entityLose, Entity entityWin)
    {
        entities.Remove(entityLose);
        entityLose.EntityDestroy();
        activePlayer.entityMoved = null;
        entityWin.power -= entityLose.power;
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
        return (from tile in _tiles from _tile in tile where position == _tile.positionTile select _tile).FirstOrDefault();
    }
    private Entity GetEntity(Vector2 position)
    {
        return entities.Find(x => x.tile.positionTile == position);
    }
    private Entity GetEntity(Tile tile)
    {
        return entities.Find(x => x.tile == tile);
    }
}