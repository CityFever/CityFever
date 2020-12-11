namespace Calculation
{
    public interface ISimulationTile
    {
        int GetRow();
        int GetColumn();
        Type GetTileType();
        //only for testing
        void SetTileType(Type type);
    }
}
