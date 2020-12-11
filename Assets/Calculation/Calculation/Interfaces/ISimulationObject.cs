namespace Calculation
{
    public interface ISimulationObject
    {
        int GetHeight();
        int GetSize();
        ISimulationTile GetCentralTile();
        Type GetObjectType();
    }
}
