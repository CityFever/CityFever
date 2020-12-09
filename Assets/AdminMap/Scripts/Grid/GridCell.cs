using System;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool IsAvailable { get; set; } = true;
    public Vector2 GridCoordinate { get; set; }
}

