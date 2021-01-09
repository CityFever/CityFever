using System;
using System.Collections;
using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using Library;
using UnityEngine;

public class UserGameManager : MonoBehaviour
{
    private readonly int mapSize = 100; 

    private Map map;

    [SerializeField] private Map mapPrefab;
    [SerializeField] private WaterTile waterPrefab;
    [SerializeField] private AsphaltTile asphalPrefab;
    [SerializeField] private GrassTile grassPrefab;

    void Start()
    {
        CreateMap();

        Debug.Log(MapConfig.mapConfig.mapBudget);

        foreach (var tileConfig in MapConfig.mapConfig.tileCongigurations)
        {
            Debug.Log("Tile: " + tileConfig.type + ", state: " + tileConfig.state + ", coordinate: " + tileConfig.coordinate);
        }
    }

    private void CreateMap()
    {
        map = Instantiate(mapPrefab, transform).Initialize(mapSize);
        map.budget = MapConfig.mapConfig.mapBudget;
        ConfigureTiles();
    }

    private void ConfigureTiles()
    {
        foreach (var tileConfig in MapConfig.mapConfig.tileCongigurations)
        { 
            Debug.Log(tileConfig.coordinate.x + ", " + tileConfig.coordinate.y);
           map.RecreateTiles(tileConfig.type, tileConfig.coordinate, tileConfig.state);
        }
    }
}
