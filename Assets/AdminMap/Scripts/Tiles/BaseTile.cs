using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public State State { get; set; } = State.Available;
    public Vector2 Coordinate { get; set; }
    public UnityObject unityObject { get; set;}

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


