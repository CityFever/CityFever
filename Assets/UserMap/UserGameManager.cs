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
        foreach (var tileConfig in MapConfig.mapConfig.tileCongigurations)
        {
            UnityObject configPrefab = null;

            if (!tileConfig.ObjectType.Equals(GameObjectType.Default))
            {
                 configPrefab = prefabs.FirstOrDefault(prefab => prefab.Type().Equals(tileConfig.ObjectType));
            }

            map.CreateTilesFromConfiguration(tileConfig, configPrefab);
        }
    }
}
