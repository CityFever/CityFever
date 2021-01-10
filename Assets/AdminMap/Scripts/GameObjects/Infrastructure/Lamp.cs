﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(4, 0, 1);

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.Lamp;
    public override GameObjectType Type()
    {
        return type;
    }
}
