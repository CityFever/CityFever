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
        FetcMapFromDatabase();
    }

    private void CreateMap()
    {
        map = Instantiate(map, transform).Initialize(Constants.MAP_SIZE);

    }

    private void FetcMapFromDatabase()
    {
        UsersRepository.Login("226435@edu.p.lodz.pl", "password", () => {
            Debug.Log("Started fetching a map");
            MapsRepository.GetAdminMap(mapId, (fetchedMap) =>
            {
                CreateMap();
                ConfigureTiles(fetchedMap);
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
    }
}
