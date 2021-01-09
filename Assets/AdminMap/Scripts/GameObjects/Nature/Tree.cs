﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class Tree : UnityObject
{

    private static Vector3 sizeSpan = new Vector3(3, 0, 3);

    public override Vector3 SizeInTiles()
    {
        return sizeSpan;
    }

   public Tree()
    {
       
    }

    private void Awake()
    {
        CanBePlacedOn = CanBePlacedOn.Grass;
    }
}

