using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using Calculus;
using UnityEngine;

public abstract class UnityObject : MonoBehaviour, ISimulationObject
{    
    //height is determined by the 2nd parameter in Vector3 
    abstract public GameObjectType Type();

    abstract public List<TileType> CanBePlacedOn();

    abstract public Vector3 SizeInTiles();

    public void DestroyUnityObject()
    {
        Destroy(gameObject);
    }

    public int GetHeight()
    {
        return (int)SizeInTiles().y;
    }

    public int GetRowSize()
    {
        return 99 - (int)SizeInTiles().z;
    }

    public int GetColSize()
    {
        return (int)SizeInTiles().x;
    }

    public GameObjectType GetObjectType()
    {
        return Type();
    }
}

public enum GameObjectType
{
    Bush,
    Tree,
    Flower,
    House,
    Church,
    Shop,
    Car, 
    Fountain,
    Gazebo,
    Lamp,
    Sandpit,
    TrashBin,
    FancyBush,
    ModernHouse,
    SolarHouse,
    Bench,
    FirTree,
    Slide,
    AnotherBench,
    Default
}

public enum CanBePlacedOn
{
    Grass,
    Asphalt,
    Water
}

