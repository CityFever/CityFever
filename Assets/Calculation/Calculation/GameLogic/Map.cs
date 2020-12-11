using System.Collections.Generic;

namespace Calculation.GameLogic
{
    public class Map : ISimulationMap
    {
        int size;
        public ISimulationTile[,] tiles;
        public List<ISimulationObject> baseObjects = new List<ISimulationObject>();

        public Map(int size)
        {
            this.size = size;
            tiles = new Tile[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    tiles[i,j] = new Tile(i, j);
                }
        }

        public ISimulationTile GetTile(int row, int col)
        {
            return tiles[row,col];
        }

        public List<ISimulationObject> GetAllGameObjects()
        {
            return baseObjects;
        }

        public int GetMapSize()
        {
            return size;
        }
    }
}
