using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : BaseTile
{
    private void Awake()
    {
        State = State.Unavailable;
    }
}
