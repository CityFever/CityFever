﻿using UnityEngine;

public class Car : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(5, 2, 3);
    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.Car;
    public override GameObjectType Type()
    {
        return type;
    }
}
