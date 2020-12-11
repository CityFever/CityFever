using System.Collections.Generic;

namespace Calculation
{
    public interface ISimulationMap
    {
        List<ISimulationObject> GetAllGameObjects();
        ISimulationTile GetTile(int row, int col);
        int GetMapSize();       
    }
}
