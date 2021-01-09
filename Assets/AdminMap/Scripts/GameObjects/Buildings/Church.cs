using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(25,0, 19);

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }
}
