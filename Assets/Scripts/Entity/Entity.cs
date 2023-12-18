using System.Collections.Generic;
using System.Linq;
using GoldenRations;
using UnityEngine;

namespace Core
{
    public class Entity : MonoBehaviour
    {
        public EntityData Data{ get; private set; }
        public Colors Color{ get; private set; }
        public int ID{ get; private set; }
        public int Spent{ get; private set; }
        public int Reward{ get; private set; }
        public int Price{ get; private set; }
        public Vector2 Position { get; set; }
        
        private SpriteRenderer _renderer;

        public virtual void Initialization(EntityData data,Vector2 position,Vector3 transformPosition,Colors colors,int _id)
        {
            Price = 0;
            Data = data;
            Color = colors;
            ID = _id;
            Position = position;
            transform.position = new Vector3(transformPosition.x,transformPosition.y,transformPosition.z-1);
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _renderer.sprite = data.sprite;
            transform.localScale = new Vector3(GoldenRatio.TileRation/4,GoldenRatio.TileRation/4);
        }
        public void EntityDestroy()
        {
           Destroy(gameObject);
        }
        public virtual int NewPlayerMove()
        {
            return Reward - Spent;
        }
    }
}
