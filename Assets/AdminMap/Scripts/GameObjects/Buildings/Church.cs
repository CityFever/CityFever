using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using UnityEngine;

public class Church : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(25, 30, 19);

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.Church;

    public override GameObjectType Type()
    {
        return type;
    }

    public override List<TileType> CanBePlacedOn()
    {
        return new List<TileType>()
        {
            TileType.Grass
        };
    }
}
