using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(1, 0, 1);
    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.TrashBin;
    public override GameObjectType Type()
    {
        return type;
    }
}
