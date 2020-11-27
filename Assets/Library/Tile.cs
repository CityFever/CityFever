using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class Tile
    {
        public int positionX { get; }
        public int positionY { get; }
        public bool isOccupied { get; set; }
        public bool isActive { get; set; }

        public void AbsorbHeat(double heat)
        {
            throw new NotImplementedException();
        }

    }
}
