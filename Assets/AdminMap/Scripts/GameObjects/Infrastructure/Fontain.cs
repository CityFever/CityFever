using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fontain : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(7, 0, 7);

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }
}
