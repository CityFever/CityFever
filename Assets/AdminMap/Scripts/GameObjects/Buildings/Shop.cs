using System.Collections;
using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using UnityEngine;

public class Shop : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(10, 10, 10);

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.Shop;

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
