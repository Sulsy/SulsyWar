using System.Collections.Generic;
using System.Linq;
using GoldenRations;
using UnityEngine;

namespace Core
{
    public class TileSpawner : MonoBehaviour
    {
        public List<TileData> tilesColor;
        public List<List<Tile>> tiles;
        public GameObject prefab;
        public Vector2 size;
        public float rowPositionCoefficient;
        private void Awake()
        {
            tiles = new List<List<Tile>>();
            for (int i = 0; i < size.x; i++)
            {
                tiles.Add(new List<Tile>()); 
            }

            for (int i = 0; i < tiles.Count; i++)
            {
                GetTilesList(i);
                foreach (var tile in tiles[i])
                {
                    FillTile(tile,tilesColor[Random.Range(0,tilesColor.Count)]);
                }
                MoveTile(i);
                rowPositionCoefficient += 0.76f;
            }

        }

        private void GetTilesList(int row)
        {
            for (int i = 0; i < size.y; i++)
            {
                var tile= Instantiate(prefab);
                tiles[row].Add(tile.GetComponent<Tile>());
                tiles[row][i].id = new Vector2(row,i);
            }
        }
        private void FillTile(Tile tile,TileData data)
        {
            tile.Initialization(data);
        }

        private void MoveTile(int row)
        {
            for (int i = 0; i < tiles[row].Count; i++)
            {
                if (i==0)
                {
                    tiles[row][i].GetComponent<Transform>().position =
                        new Vector3(0+rowPositionCoefficient, 0);
                    continue;
                }
                
                var position = tiles[row][i-1].GetComponent<Transform>().position;
                tiles[row][i].GetComponent<Transform>().position =
                    new Vector3(position.x + 0.38f, position.y + 0.61f);
            }
        }

        public Tile GetTile(Vector2 position)
        {
            return (from tile in tiles from _tile in tile where position == _tile.id select _tile).FirstOrDefault();
        }
        
    }
}

