using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class Map
    {
        public Tile[][] tiles;
        public double budget;
        public string mapId;
        public List<GameObject> baseObjects;
        public string owner;

        public Map(double budget, string mapId, string owner)
        {
            this.budget = budget;
            this.mapId = mapId;
            this.owner = owner;
        }
    }
}
