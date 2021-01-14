using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityObject : MonoBehaviour
{
    abstract public GameObjectType Type();

    public CanBePlacedOn CanBePlacedOn { get; set; }

    abstract public Vector3 SizeInTiles();

    public void DestroyUnityObject()
    {
        Destroy(gameObject);
    }

    abstract public CanBePlacedOn CanBePlaced();
        
}

public enum GameObjectType
{
    Bush,
    Tree,
    House,
    Church,
    Car, 
    Fountain,
    Gazebo,
    Lamp,
    Sandpit,
    TrashBin,
    Default
}

public enum CanBePlacedOn
{
    Grass,
    Asphalt,
    Water
}


