using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(25, 0, 17);

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.House;
    public override GameObjectType Type()
    {
        return type;
    }

    public override CanBePlacedOn CanBePlaced()
    {
        return CanBePlacedOn.Grass;
    }
}
