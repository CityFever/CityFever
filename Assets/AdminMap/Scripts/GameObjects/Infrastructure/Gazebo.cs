using UnityEngine;

class Gazebo : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(3, 3, 5);
    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.Gazebo;
    public override GameObjectType Type()
    {
        return type;
    }
}

