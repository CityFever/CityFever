using System.Collections;
using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using UnityEngine;

public class Flower : UnityObject
{
    public override GameObjectType Type()
    {
        return GameObjectType.Flower;
    }

    public override Vector3 SizeInTiles()
    {
        return new Vector3(1, 1, 1);
    }

    public override List<TileType> CanBePlacedOn()
    {
        return new List<TileType>()
        {
            TileType.Grass
        };
    }
}

