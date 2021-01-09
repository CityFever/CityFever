﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandpit : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(3, 0, 3);

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }
}
