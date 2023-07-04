using System.Collections.Generic;
using System.Linq;
using GoldenRations;
using UnityEngine;

namespace Core
{
    public class TileSpawner : MonoBehaviour
    {
        public List<TileData> tilesColor;
        public GameObject prefab;
        public Vector2 size;
        public float rowPositionCoefficient;
        private List<List<Tile>> _tiles;
        
        public List<List<Tile>> Initialize()
        {
            _tiles = new List<List<Tile>>();
            for (int i = 0; i < size.x; i++)
            {
                _tiles.Add(new List<Tile>());
            }

            for (int i = 0; i < _tiles.Count; i++)
            {
                GetTilesList(i);
                FillTile(i);
                MoveTile(i);
                rowPositionCoefficient += 0.76f;
            }

            return _tiles;
        }

        private void GetTilesList(int row)
        {
            for (int i = 0; i < size.y; i++)
            {
                var tile = Instantiate(prefab);
                _tiles[row].Add(tile.GetComponent<Tile>());
                _tiles[row][i].positionTile = new Vector2(row, i);
            }
            
        }
        
        private void FillTile(int row)
        {
            foreach (var tile in _tiles[row])
            {
                tile.Initialization(tilesColor[Random.Range(0, tilesColor.Count)]);
            }
            
        }

        private void MoveTile(int row)
        {
            for (var i = 0; i < _tiles[row].Count; i++)
            {
                if (i == 0)
                {
                    _tiles[row][i].GetComponent<Transform>().position =
                        new Vector3(0 + rowPositionCoefficient, 0);
                    continue;
                }

                var position = _tiles[row][i - 1].GetComponent<Transform>().position;
                _tiles[row][i].GetComponent<Transform>().position =
                    new Vector3(position.x + 0.38f, position.y + 0.61f);
            }
        }

    }
}

