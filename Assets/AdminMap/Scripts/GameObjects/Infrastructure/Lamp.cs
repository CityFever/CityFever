using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : UnityObject
{
    private static Vector2 sizeSpan = new Vector2(4,1);

    public override Vector2 SizeInTiles()
    {
        return sizeSpan;
    }
}
