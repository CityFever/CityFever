﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fontain : UnityObject
{
    private static Vector2 sizeSpan = new Vector2(7, 7);

    public override Vector2 SizeInTiles()
    {
        return sizeSpan;
    }
}
