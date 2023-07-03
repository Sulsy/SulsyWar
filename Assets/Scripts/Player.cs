using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

public class Player : MonoBehaviour
{
    public EntitySpawner entitySpawner;
    public TileSpawner tileSpawner;
    public Colors color;
    public int playersId;
    public Entity entityMoved;
    public event Action MoveLimited;
    public int moveLimit;
    public int moves;

    private void CreateEntity(Vector2 tileId,int entityId)
    {
        var tile = tileSpawner.tiles[(int)tileId.x][(int)tileId.y];
        if (tile.tileData.tileColor==color)
        {
            entitySpawner.CreateEntity(tile,entityId,color);
            NextPlayer();
        }
       
    }

    public void TileClick(Vector2 tileId)
    {
        if (entityMoved!=null)
        {
            foreach (var tile in entityMoved.moveDistance)
            {
                if (tile == tileId)
                {
                    MoveEntity(tileId, entityMoved);
                    return;
                }
            }
        }
        else
        { 
            CreateEntity(tileId, 0);
        }

    }

    public void EntityClick(int entityId)
    {
        if (entityMoved!=null)
        {
            entityMoved = null;
            return;
        }
        entityMoved = entitySpawner.entities.Find(x=>x.id==entityId);
    }

    private void MoveEntity(Vector2 tileId,Entity entity)
    {
        foreach (var _entity in entitySpawner.entities)
        {
            if (_entity.color!=entity.color && tileSpawner.GetTile(tileId).tileData.tileColor!=entity.color)
            {
                foreach (var position in _entity.moveDistance)
                {
                    if (tileId == position)
                    {
                        return;
                    }
                }
            }
            
        }
        var tile= tileSpawner.tiles[(int)tileId.x].FirstOrDefault(x => x.id == tileId);
        entity.tile = tile;
        if (tile != null)
        {
            tile.tileData = tileSpawner.tilesColor.Find(x => x.tileColor == color);
            tile.UpdateSprite();
        }

        entity.Move();
        NextPlayer();
    }

    private void NextPlayer()
    {
        moves++;
        if (moves>=moveLimit)
        {
            MoveLimited?.Invoke();
            moves = 0;
        }
    }
    
}
