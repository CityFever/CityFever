using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class Tree : UnityObject
{
    private static Vector2 sizeSpan = new Vector2(3, 3);

    public override Vector2 SizeInTiles()
    {
        return sizeSpan;
    }
       
}

