using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdminMap.Scripts.MapConfiguration
{
    public class TileConfig
    {
        public TileType type { get; set; }
        public State state { get; set; }
        public Vector2 coordinate { get; set; }

        public TileConfig(int type, State state, Vector2 coordinate)
        {
            this.type = (TileType) type;
            this.state = state;
            this.coordinate = coordinate;
        }
    }
    public enum TileType
    {
        Water,
        Asphalt,
        Grass
    }
}
