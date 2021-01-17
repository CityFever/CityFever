using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using UnityEngine;

public class TrashBin : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(1, 1, 1);
    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.TrashBin;
    public override GameObjectType Type()
    {
        return type;
    }

    public override List<TileType> CanBePlacedOn()
    {
        return new List<TileType>()
        {
            TileType.Grass,
            TileType.Asphalt
        };
    } 
}
