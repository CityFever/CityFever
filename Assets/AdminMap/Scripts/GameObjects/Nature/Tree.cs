using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using UnityEngine;

class Tree : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(3, 8, 3);

    public override GameObjectType Type()
    {
        return GameObjectType.Tree;
    }

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    public override List<TileType> CanBePlacedOn()
    {
        return new List<TileType>()
        {
            TileType.Grass
        };
    }
}
