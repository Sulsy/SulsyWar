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
        public Tile tile;
        public List<Vector2> moveDistance;
        public List<Vector2> attackDistance;
        public bool didAction;
        public float power;
        public bool distanceDamage;
        public int price;

        private SpriteRenderer _renderer;

        public void Initialization(EntityData data,Tile tile,Colors colors,int _id)
        {
            Data = data;
            color = colors;
            id = _id;
            power = Random.Range(1, 100);
            this.tile = tile;
            for (int i = 0; i < 20; i++)
            {
                moveDistance.Add(new Vector2());
                attackDistance.Add(new Vector2());
            }
            Move(true);
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _renderer.sprite = data.sprite;
            transform.localScale = new Vector3(GoldenRatio.TileRation/4,GoldenRatio.TileRation/4);
        }

        public void Move(bool created=false)
        {
            var position = tile.transform.position;
            transform.position = new Vector3(position.x,position.y,position.z-1);
            if (!created)
            {
                EntityDidAction();
            }
            MoveDistance();
        }
        
        public void NewPlayerMove()
        {
            didAction = false;
        }
        public void EntityDidAction()
        {
            didAction = true;
        }
        public void EntityDestroy()
        {
           Destroy(gameObject);
        }

        protected virtual void AttackDistance()
        {
            attackDistance[0]=(new Vector2((tile.positionTile.x+1),(tile.positionTile.y)));
            attackDistance[1]=(new Vector2((tile.positionTile.x-1),(tile.positionTile.y)));
            attackDistance[2]=(new Vector2((tile.positionTile.x),(tile.positionTile.y+1)));
            attackDistance[3]=(new Vector2((tile.positionTile.x),(tile.positionTile.y-1)));
            attackDistance[4]=(new Vector2((tile.positionTile.x-1),(tile.positionTile.y+1)));
            attackDistance[5]=(new Vector2((tile.positionTile.x+1),(tile.positionTile.y-1)));
        }

        protected virtual void MoveDistance()
        {
            moveDistance[0]=(new Vector2((tile.positionTile.x+1),(tile.positionTile.y)));
            moveDistance[1]=(new Vector2((tile.positionTile.x-1),(tile.positionTile.y)));
            moveDistance[2]=(new Vector2((tile.positionTile.x),(tile.positionTile.y+1)));
            moveDistance[3]=(new Vector2((tile.positionTile.x),(tile.positionTile.y-1)));
            moveDistance[4]=(new Vector2((tile.positionTile.x-1),(tile.positionTile.y+1)));
            moveDistance[5]=(new Vector2((tile.positionTile.x+1),(tile.positionTile.y-1)));
            AttackDistance();
        }
    }
}
