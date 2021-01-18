using Calculus;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour, ISimulationTile
{
    public State State { get; set; } = State.Available;
    public Vector2 Coordinate { get; set; }
    public UnityObject unityObject { get; set;}

    public int GetColumn()
    {
        return (int)Coordinate.x;
    }

    public ISimulationObject GetGameObject()
    {
        return unityObject;
    }

    public int GetRow()
    {
        return 99 - (int)Coordinate.y;

    }

    abstract public Type GetTileType();
    

    public BaseTile Initialize(Vector2 coordinate)
    {
        Coordinate = coordinate;
        return this;
    }
}

public enum State
{
    Available,
    Unavailable,
    Off,
    Hovered
}


