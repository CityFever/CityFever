using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityObject : MonoBehaviour
{
    public GameObjectType Type { get; private set; }

    public int SizeInTiles { get; set; }
}

public enum GameObjectType
{
    Tree,
    Bench, 
    Building
}
