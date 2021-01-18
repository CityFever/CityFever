namespace Calculus
{
    public interface ISimulationTile
    {
        int GetRow();
        int GetColumn();
        ISimulationObject GetGameObject();
        Type GetTileType();
    }
}
