using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : UnityObject
{
    private static Vector2 sizeSpan = new Vector2(25, 17);
    public override Vector2 SizeInTiles()
    {
        return sizeSpan;
    }
}
