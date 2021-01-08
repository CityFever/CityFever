using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : UnityObject
{
    private static Vector2 sizeSpan = new Vector2(1, 1);

    public override Vector2 SizeInTiles()
    {
        return sizeSpan;
    }
}
