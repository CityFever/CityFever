using System.Collections.Generic;

namespace Calculus
{
    public interface ISimulationMap
    {
        ISimulationTile GetTile(int row, int col);
        int GetMapSize();
    }
}
