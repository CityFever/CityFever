using System;
using UnityEngine;

namespace Assets.AdminMap.Scripts.MapConfiguration
{
    [Serializable]
    public class TileConfig
    {
        public TileType type;
        public State state;
        public Vector2 coordinate;
        public GameObjectType ObjectType;

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
