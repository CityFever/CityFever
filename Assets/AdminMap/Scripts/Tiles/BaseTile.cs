using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseTile : MonoBehaviour
{
    [SerializeField] private State state = State.Available;
    [SerializeField] private Vector2 coordinate;

    public State State
    {
        get { return state; }
        set { state = value; }
    }

    public Vector2 Coordinate
    {
        get { return coordinate; }
        set { coordinate = value; }
    }


    void Update()
    {
        if (State == State.Off)
        { 
            //GetComponentInChildren<Renderer>().material.color = Color.gray;
        }
    }
    public BaseTile Initialize(Vector2 coordinate)
    {
        Coordinate = coordinate;
        return this;
    }
}

[Serializable]
public enum State
{
    Available,
    Unavailable,
    Off
}


