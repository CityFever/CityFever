using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : UnityObject
{
    private static Vector2 sizeSpan = new Vector2(36, 30);

    public override Vector2 SizeInTiles()
    {
        return sizeSpan;
    }
}
