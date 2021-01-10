﻿using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using UnityEngine;

public class MapConfig : MonoBehaviour
{
    public static MapConfig mapConfig;

    public List<TileConfig> tileCongigurations;
    public float mapBudget { get; set; }
    public int mapSize { get; set; } = 100;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (mapConfig == null)
        {
            mapConfig = this;
        }
        else if (mapConfig != this)
        {
            Destroy(gameObject);
        }
    }
}
