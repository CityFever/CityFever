using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : UnityObject
{
    //height is determined by the 2nd parameter 
    private static Vector3 sizeSpan = new Vector3(25, 16, 17);

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.House;
    public override GameObjectType Type()
    {
        return type;
    }
}
