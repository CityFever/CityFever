using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class Gazebo : UnityObject
{
    private static Vector2 sizeSpan = new Vector2(3, 5);

    public override Vector2 SizeInTiles()
    {
        return sizeSpan;
    }
       
}

