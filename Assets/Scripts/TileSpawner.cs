using System.Collections.Generic;
using System.Linq;
using GoldenRations;
using Unity.VisualScripting;
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

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    GetTilesList(i,j);
                    FillTile(i,j,0);
                    MoveTile(i,j);
                }
                rowPositionCoefficient += 0.76f;
            }

            for (int i = 1; i < tilesColor.Count; i++)
            {
                GenerateHomes(i);
            }
            return _tiles;
        }

        private void GetTilesList(int row,int colum)
        {
            var tile = Instantiate(prefab);
            _tiles[row].Add(tile.GetComponent<Tile>());
            _tiles[row][colum].positionTile = new Vector2(row, colum);
        }
        
        private void FillTile(int row,int colum,int colorType)
        {
            _tiles[row][colum].Initialization(tilesColor[colorType]);
        }

        private void MoveTile(int row,int colum)
        {
            if (colum == 0)
            {
                _tiles[row][colum].GetComponent<Transform>().position =
                    new Vector3(transform.position.x + rowPositionCoefficient, transform.position.y);
                return;
            }

            var position = _tiles[row][colum - 1].GetComponent<Transform>().position;
            _tiles[row][colum].GetComponent<Transform>().position =
                new Vector3(position.x + 0.38f, position.y + 0.61f);
        }

        private void GenerateHomes(int tileIndex)
        {
            var start = new Vector2();
            var colorRadius = new List<Vector2>();
            
            for (var i = 0; i < 7; i++)
            {
                colorRadius.Add((new Vector2((start.x), (start.y))));
            }
            
            while (true)
            {
                var row = Random.Range(1,(int)size.x-2);
                var colum = Random.Range(1,(int)size.y-2);
                start = new Vector2(row,colum);

                colorRadius[0]=((new Vector2((start.x), (start.y))));
                colorRadius[1]=((new Vector2((start.x + 1), (start.y))));
                colorRadius[2]=((new Vector2((start.x - 1), (start.y))));
                colorRadius[3]=((new Vector2((start.x), (start.y + 1))));
                colorRadius[4]=((new Vector2((start.x), (start.y - 1))));
                colorRadius[5]=((new Vector2((start.x - 1), (start.y + 1))));
                colorRadius[6]=((new Vector2((start.x + 1), (start.y - 1))));


                var index = colorRadius.Select(tileRadius => _tiles[(int)tileRadius.x][(int)tileRadius.y]).Count(tile => XColor.ToHexString(tile.tileData.color) == "FFFFFFFF");

                if (index>=7)
                {
                    break;   
                }
            }

            for (var i = 0; i < colorRadius.Count; i++)
            {
                FillTile((int)colorRadius[i].x,(int)colorRadius[i].y,tileIndex);
            }
        }

    }
}

