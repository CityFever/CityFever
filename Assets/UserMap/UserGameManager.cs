using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.AdminMap.Scripts.MapConfiguration;
using Library;
using UnityEditor;
using UnityEngine;

public class UserGameManager : MonoBehaviour
{
    private Map map;

    [SerializeField] private Map mapPrefab;
    [SerializeField] private List<UnityObject> prefabs;
    void Start()
    {
        CreateMap();

        Debug.Log(MapConfig.mapConfig.mapBudget);

        foreach (var tileConfig in MapConfig.mapConfig.tileCongigurations)
        {
            Debug.Log("Tile: " + tileConfig.type + ", state: " + tileConfig.state + ", coordinate: " + tileConfig.coordinate + " GAME OBJECT: " + tileConfig.ObjectType);
        }
    }

    private void CreateMap()
    {
        float savedBudget = MapConfig.mapConfig.mapBudget;
        int savedMapSize = MapConfig.mapConfig.mapSize;

        map = Instantiate(mapPrefab, transform).Initialize(savedMapSize);
        map.budget = savedBudget;
        ConfigureTiles();
    }

    private void ConfigureTiles()
    {
        Debug.Log("-------------------------------------");
        foreach (var prefab in prefabs)
        {
            Debug.Log(prefab);
        }
        Debug.Log("-------------------------------------");


        foreach (var tileConfig in MapConfig.mapConfig.tileCongigurations)
        {
            UnityObject configPrefab = null;

            if (!tileConfig.ObjectType.Equals(GameObjectType.Default))
            {
                 configPrefab = prefabs.FirstOrDefault(prefab => prefab.Type().Equals(tileConfig.ObjectType));

                Debug.Log(configPrefab);
            }
            // Debug.Log(tileConfig.coordinate.x + ", " + tileConfig.coordinate.y);
            map.CreateTilesFromConfiguration(tileConfig.type, tileConfig.coordinate, tileConfig.state, tileConfig.ObjectType, configPrefab);
        }
    }
}
