using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityObject : MonoBehaviour
{
    public GameObjectType Type { get; private set; }

    public CanBePlacedOn CanBePlacedOn { get; set; }

    abstract public Vector3 SizeInTiles();


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

public enum CanBePlacedOn
{
    Grass,
    Asphalt,
    Water
}

