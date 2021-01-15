using UnityEngine;

public abstract class UnityObject : MonoBehaviour
{    
    //height is determined by the 2nd parameter in Vector3 
    abstract public GameObjectType Type();

    public CanBePlacedOn CanBePlacedOn { get; set; }

    abstract public Vector3 SizeInTiles();

    public void DestroyUnityObject()
    {
        Destroy(gameObject);
    }
}

public enum GameObjectType
{
    Bush,
    Tree,
    Flower,
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

