using System.Collections.Generic;
using System.Linq;
using GoldenRations;
using UnityEngine;

namespace Core
{
    public class Entity : MonoBehaviour
    {
        public EntityData Data;
        public Colors color;
        public int id;
        private SpriteRenderer _renderer;
        public Tile tile;
        public List<Vector2> moveDistance;

        public void Initialization(EntityData data,Tile tile,Colors colors,int _id)
        {
            Data = data;
            color = colors;
            id = _id;
            this.tile = tile;
            for (int i = 0; i < 6; i++)
            {
                moveDistance.Add(new Vector2());
            }
            Move();
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _renderer.sprite = data.sprite;
            transform.localScale = new Vector3(GoldenRatio.TileRation/4,GoldenRatio.TileRation/4);
        }

        public void Move()
        {
            var position = tile.transform.position;
            transform.position = new Vector3(position.x,position.y,position.z-1);
            MoveDistance();
        }

        private void MoveDistance()
        {
            moveDistance[0]=(new Vector2((tile.id.x+1),(tile.id.y)));
            moveDistance[1]=(new Vector2((tile.id.x-1),(tile.id.y)));
            moveDistance[2]=(new Vector2((tile.id.x),(tile.id.y+1)));
            moveDistance[3]=(new Vector2((tile.id.x),(tile.id.y-1)));
            moveDistance[4]=(new Vector2((tile.id.x-1),(tile.id.y+1)));
            moveDistance[5]=(new Vector2((tile.id.x+1),(tile.id.y-1)));
        }
        private void OnMouseDown()
        {
            var controller = FindObjectOfType<GameController>();
            var king = controller.activePlayer;
            if (king!=null)    
            { 
                king.EntityClick(id);
            }

        }
    }
}
