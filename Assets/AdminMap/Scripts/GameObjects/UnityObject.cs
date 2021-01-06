﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityObject : MonoBehaviour
{
    public GameObjectType Type { get; private set; }

    abstract public Vector2 SizeInTiles();


    public void DestroyUnityObject()
    {
        Destroy(gameObject);
    }
}

public enum GameObjectType
{
    Tree,
    Bench, 
    Building
}

public enum Restrictions
{
    Grass,
    Asphalt,
    Water
}
