using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : UnityObject
{
    private static Vector2 sizeSpan = new Vector2(3, 3);

    public override Vector2 SizeInTiles()
    {
        return sizeSpan;
    }
}
