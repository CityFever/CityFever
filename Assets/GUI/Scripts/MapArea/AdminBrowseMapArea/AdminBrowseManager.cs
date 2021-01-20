using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.AdminMap.Scripts;
using Database;
using Library;
using UnityEngine;
using Application = Assets.AdminMap.Scripts.Application;

public class AdminBrowseManager : MonoBehaviour
{
    public string mapId { get; set; }
    [SerializeField] private List<UnityObject> prefabs;
    [SerializeField] private Map map; 

    void Start()
    {
        mapId = Application.application.SelectedAdminMapId;
        Debug.Log(Application.application.SelectedAdminMapId);

        foreach (var tile in MapConfig.mapConfig.tileConfigs)
        {
            Debug.Log("Tile: "+ tile.type);
        }

        CreateMap();
    }

    private void CreateMap()
    {
        map = Instantiate(map, transform).Initialize(Constants.MAP_SIZE);
        ConfigureTiles();
    }

    private void ConfigureTiles()
    {
        foreach (var tileConfig in MapConfig.mapConfig.tileConfigs)
        {
            UnityObject configPrefab = null;

            if (!tileConfig.ObjectType.Equals(GameObjectType.Default))
            {
                configPrefab = prefabs.FirstOrDefault(prefab => prefab.Type().Equals(tileConfig.ObjectType));
            }
            map.CreateTilesFromConfiguration(tileConfig, configPrefab);
        };
    }

    /*private void FetcMapFromDatabase()
    {
        UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password, () => {
            Debug.Log("Started fetching a map");
            MapsRepository.GetAdminMap(mapId, (fetchedMap) =>
            {
                CreateMap();
                ConfigureTiles(fetchedMap);
                MapConfig.mapConfig.mapBudget = fetchedMap.mapBudget;
            });
        });
    }

    private void ConfigureTiles(MapConfig mapConfig)
    {
        foreach (var tileConfig in mapConfig.tileConfigs)
        {
            UnityObject configPrefab = null;

            if (!tileConfig.ObjectType.Equals(GameObjectType.Default))
            {
                configPrefab = prefabs.FirstOrDefault(prefab => prefab.Type().Equals(tileConfig.ObjectType));
            }
            map.CreateTilesFromConfiguration(tileConfig, configPrefab);
        }
    }*/

    

}
