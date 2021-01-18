using System.Collections.Generic;

namespace Calculus
{
    public interface ISimulationMap
    {
        List<ISimulationTile> GetTilesWithObjects();
        ISimulationTile GetTile(int row, int col);
        int GetMapSize();
    }
}
