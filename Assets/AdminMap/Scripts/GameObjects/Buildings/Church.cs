using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : UnityObject
{
    private static Vector2 sizeSpan = new Vector2(30, 23);

    public override Vector2 SizeInTiles()
    {
        return sizeSpan;
    }
}
