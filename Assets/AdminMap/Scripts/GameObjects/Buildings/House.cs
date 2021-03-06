﻿using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using UnityEngine;

public class House : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(25, 16, 17);
    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.House;
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
