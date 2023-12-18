using System;
using System.Linq;
using GoldenRations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class Tile : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        public Vector2 PositionTile { get; set; }
        public TileData TileData { get; set; }
        public int Reward { get; set; }
        public void Initialization(TileData data)
        {
            TileData = data;
            Reward = 1;
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _renderer.sprite = TileData.sprite;
            UpdateSprite();
        }

        public void UpdateSprite()
        {
            _renderer.color = TileData.color;
        }
        private void OnMouseDown()
        {
            var controller = FindObjectOfType<GameController>();
            controller.TileClick(PositionTile);

        }
    }
}