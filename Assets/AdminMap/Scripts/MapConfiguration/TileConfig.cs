using UnityEngine;

namespace Assets.AdminMap.Scripts.MapConfiguration
{
    public class TileConfig
    {
        public TileType type { get; set; }
        public State state { get; set; }
        public Vector2 coordinate { get; set; }
        public GameObjectType ObjectType { get; set; }

        public TileConfig(TileType type, State state, Vector2 coordinate, GameObjectType objectType)
        {
            this.type = type;
            this.state = state;
            this.coordinate = coordinate;
            ObjectType = objectType;
        }
    }

    public enum TileType
    {
        Water,
        Asphalt,
        Grass
    }
}
