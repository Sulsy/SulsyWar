using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData", order = 2)]
    public class EntityData : ScriptableObject
    {
        public Sprite sprite;
        public EntityType type;
    }
}