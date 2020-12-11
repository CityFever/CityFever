using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class GameObject
    {
        public int sizeInTiles;
        public string id;
        public double price;
        public double temperatureImpact;

        public GameObject(int sizeInTiles, string id, double price, double temperatureImpact) {
            this.sizeInTiles = sizeInTiles;
            this.id = id;
            this.price = price;
            this.temperatureImpact = temperatureImpact;
        }

        public double EmitHeat()
        {
            throw new NotImplementedException();
        }
    }
}
