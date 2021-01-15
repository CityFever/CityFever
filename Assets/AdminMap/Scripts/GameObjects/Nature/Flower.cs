using System.Collections;
using System.Collections.Generic;
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
}
