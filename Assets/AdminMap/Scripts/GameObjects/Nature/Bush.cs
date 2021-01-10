using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(3, 2, 3);
    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.Bush;
    public override GameObjectType Type()
    {
        return type;
    }
}
