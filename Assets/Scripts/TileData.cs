using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/TileData", order = 1)]
    public class TileData : ScriptableObject
    {
        public Colors tileColor;
        public string countryName;
        public Sprite  sprite;
        public Color color;
    }
}