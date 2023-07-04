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
        public Vector2 positionTile;
        public TileData tileData;
        public void Initialization(TileData data)
        {
            tileData = data;

            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _renderer.sprite = tileData.sprite;
            UpdateSprite();
        }

        public void UpdateSprite()
        {
            _renderer.color = tileData.color;
        }
        private void OnMouseDown()
        {
            var controller = FindObjectOfType<GameController>();
            controller.TileClick(positionTile);

        }
    }
}