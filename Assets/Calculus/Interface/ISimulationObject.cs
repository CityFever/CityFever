namespace Calculus
{
    public interface ISimulationObject
    {
        int GetHeight();
        int GetRowSize();
        int GetColSize();
        GameObjectType GetObjectType();
    }
}
