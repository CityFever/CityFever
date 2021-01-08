using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityObject : MonoBehaviour
{
    abstract public Vector2 SizeInTiles();

    public void DestroyUnityObject()
    {
        Destroy(gameObject);
    }
}
