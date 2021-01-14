using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class Gazebo : UnityObject
{
    private static Vector3 sizeSpan = new Vector3(3, 0, 5);
    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

    public override CanBePlacedOn CanBePlaced()
    {
        return CanBePlacedOn.Asphalt;
    }

    public Gazebo()
    {
       
    }

    private static GameObjectType type = GameObjectType.Gazebo;
    public override GameObjectType Type()
    {
        return type;
    }

  
}

