using UnityEngine;

public class Fountain : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(7, 6, 7);
    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    private static GameObjectType type = GameObjectType.Fountain;
    public override GameObjectType Type()
    {
        return type;
    }
}
