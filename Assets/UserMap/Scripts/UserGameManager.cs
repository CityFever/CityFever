﻿using Assets.AdminMap.Scripts.Controllers;
using Assets.AdminMap.Scripts.MapConfiguration;
using Library;
using System.Collections.Generic;
using System.Linq;
using Assets.AdminMap.Scripts;
using Database;
using UnityEngine;
using UnityEngine.EventSystems;
using Application = Assets.AdminMap.Scripts.Application;
using Calculus;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;

public class UserGameManager : MonoBehaviour
{
    private Map map;

    [SerializeField] private Map mapPrefab;
    [SerializeField] private List<UnityObject> prefabs;

    private UnityObject unityObject;
    private BaseTile selectedTile;

    private bool showHover = true;

    private GameMode mode = GameMode.Default;

    private List<ObjectConfig> availableObjects;
    private UHISimulation simulation;
    private TMP_Text tValue;
    private TMP_Text tSun;
    private Light light1;
    private Light light2;

    void Start()
    {
        CreateMap();
        map.adminAccess = false;

        tValue = GameObject.Find("TemperatureButton").GetComponentInChildren<TMP_Text>();
        tSun = GameObject.Find("SunPosition").GetComponentInChildren<TMP_Text>();
        light1 = GameObject.Find("DirectionalLight1").GetComponent<Light>();
        light2 = GameObject.Find("DirectionalLight2").GetComponent<Light>();        
    }

    private void CreateMap()
    {
        ConfigureTiles();
        SetAvailableObjects();
        simulation = new UHISimulation(map);
    }

    private void SetAvailableObjects()
    {
        availableObjects = MapConfig.mapConfig.placeableObjectConfigs;
    }

    private void ConfigureTiles()
    {
        map = Instantiate(mapPrefab, transform).Initialize(Constants.MAP_SIZE);
        map.budget = MapConfig.mapConfig.mapBudget;

        foreach (var tile in MapConfig.mapConfig.tileConfigs)
        {
            UnityObject configPrefab = null;

            if (!tile.ObjectType.Equals(GameObjectType.Default))
            {
                configPrefab = prefabs.FirstOrDefault(prefab => prefab.Type().Equals(tile.ObjectType));
            }
            map.CreateTilesFromConfiguration(tile, configPrefab);
        }
    }

    private void Update()
    {
        map.RemovePriorHover();

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) //block clicks on gui elements
            {
               SelectTileOnMouseClick();
               SelectObjectOnMouseClick();
            }
        }
        
        else if (showHover && !mode.Equals(GameMode.Default))
        {
            FetchRayCastedTile();

            map.MarkHovering(selectedTile);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            RotateSelectedGameObject();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetDefaultMode();
        }
    }

    private void RotateSelectedGameObject()
    {
        if (unityObject != null)
        {
            RotationController.Rotate180Degrees(unityObject);
        }
    }

    private void SelectTileOnMouseClick()
    {
        FetchRayCastedTile();

        if (selectedTile)
        {
            switch (mode)
            {
                case GameMode.ObjectPlacement:
                    PlaceSelectedObjectOnTile(selectedTile);
                    break;
                case GameMode.ObjectRemoval:
                    RemoveObjectsFromSelectedZone(selectedTile);
                    break;
                case GameMode.Default:
                    // nothing so far 
                    break;
            }
        }
    }

    private void PlaceSelectedObjectOnTile(BaseTile baseTile)
    {
        if (ObjectIsAvailable() && HasEnoughMoney())
        {
            if (map.PlaceGameObjectOnSelectedTile(selectedTile, unityObject))
            {
                ReduceMapBudget(GetObjectPlacementCosts());
                Debug.Log("Map budget reduced by: " + GetObjectPlacementCosts() + ", current budget: " + map.budget);
            }
            else
            {
                Debug.Log("Object wasn't placed, current budget: " + map.budget);
            }
        }
    }

    private void RemoveObjectsFromSelectedZone(BaseTile baseTile)
    {
        List<BaseTile> tilesInZones = map.GetTilesWithObjectsOnZone(selectedTile);

        float budgetToReturn = 0.0f;

        foreach (BaseTile tile in tilesInZones) {
            unityObject = tile.unityObject;
            budgetToReturn += GetObjectRemovalCosts();
        }
        
         map.RemoveObjectFromZone(selectedTile);
         IncreaseMapBudget(budgetToReturn);
         Debug.Log("Map budget increased by: " + budgetToReturn + ", current budget: " + map.budget);
    }

    private bool ObjectIsAvailable()
    {
        return MapConfig.mapConfig.IsContained(unityObject.Type());
    }

    private bool HasEnoughMoney()
    {
        var objectPrice = GetObjectPlacementCosts();

        return objectPrice <= map.budget;
    }

    private void ReduceMapBudget(float byValue)
    {
        map.budget -= byValue;
        MapConfig.mapConfig.mapBudget = map.budget;
    }

    private void IncreaseMapBudget(float byValue)
    {
        map.budget += byValue;
        MapConfig.mapConfig.mapBudget = map.budget;
    }


    private float GetObjectPlacementCosts()
    {
        Debug.Log("Fetching placement costs for: " + unityObject.Type());
        return MapConfig.mapConfig.placeableObjectConfigs.FirstOrDefault(config => config.type.Equals(unityObject.Type())).placementCosts;
    }

    private float GetObjectRemovalCosts()
    {
        Debug.Log("Fetching removal costs for: " + unityObject.Type());
        return availableObjects.FirstOrDefault(config => config.type.Equals(unityObject.Type())).removalCosts;
    }

    private void FetchRayCastedTile()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetIntersectingRay(), out hit))
        {
            selectedTile = hit.collider.GetComponentInParent<BaseTile>();

            //Debug.Log("HoveredTile" + selectedTile.State);
            Debug.Log("Fetched Tile: " + selectedTile.Coordinate.x + ", " + selectedTile.Coordinate.y);
        }
    }

    private void FetchRayCastedObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetIntersectingRay(), out hit, 100f, 1 << 8))
        {
            Debug.Log("Fetched object " + hit.collider.GetComponent<UnityObject>());
            unityObject = hit.collider.GetComponent<UnityObject>();
        }
    }

    private void SelectObjectOnMouseClick()
    {
        FetchRayCastedObject();
    }

    private Ray GetIntersectingRay()
    {
        CheckCamera();
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    private void CheckCamera()
    {
        if (!Camera.main)
        {
            return;
        }
    }

    public void setZoneSizeWidth(string width)
    {
        if (!string.IsNullOrEmpty(width))
        {
            map.zoneSizeX = int.Parse(width);
            Debug.Log("InactiveZoneWidth: " + map.zoneSizeX);
        }
    }
    public void setZoneSizeHeight(string height)
    {
        if (!string.IsNullOrEmpty(height))
        {
            map.zoneSizeY = int.Parse(height);
            Debug.Log("InactiveZoneWidth: " + map.zoneSizeY);
        }
    }
    public void SetGameObjectPrefab(UnityObject selectedPrefab)
    {
        unityObject = selectedPrefab;
        map.zoneSizeX = (int)unityObject.SizeInTiles().x;
        map.zoneSizeY = (int)unityObject.SizeInTiles().z;
        SetObjectPlacementMode();
        map.zoneBrightness = Constants.INACTIVE_TILE;
    }
    public void RemoveSelectedObject()
    {
        Debug.Log("RemovalMode");
        SetObjectRemovalMode();
        map.zoneBrightness = Constants.ACTIVE_TILE;
    }
    public void SetObjectRemovalMode()
    {
        mode = GameMode.ObjectRemoval;
    }
    public void SetObjectPlacementMode()
    {
        mode = GameMode.ObjectPlacement;
    }
    public void SetDefaultMode()
    {
        mode = GameMode.Default;
    }

    public void SelectObjectType()
    {
        GameObjectType selectedType = Application.application.SelectedGameObjectType;
        var prefabToInstantiate = prefabs.FirstOrDefault(
            prefab => prefab.Type().Equals(selectedType));
        SetGameObjectPrefab(prefabToInstantiate);
    }

    public void SaveConfiguration()
    {
        MapConfig.mapConfig.tileConfigs = GetTilesConfiguration();

        UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password, () =>
        {
            MapsRepository.CreateUserMap(MapConfig.mapConfig, (id) =>
            {
                MapConfig.mapConfig.DatabaseId = id;
                Debug.Log("Created user map Id: " + id);
                UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password,
                    () => { MapsRepository.UpdateUserMap(MapConfig.mapConfig, id); });
            });
        });
    }


    private List<TileConfig> GetTilesConfiguration()
    {
        List<TileConfig> tileConfigs = new List<TileConfig>();

        TileType tileType;
        Vector2 coordinate = Vector2.zero;
        GameObjectType placedObjectType;

        foreach (var tile in map.tiles)
        {
            placedObjectType = tile.unityObject != null ? tile.unityObject.Type() : GameObjectType.Default;

            if (tile is WaterTile)
            {
                tileType = TileType.Water;
            }
            else if (tile is AsphaltTile)
            {
                tileType = TileType.Asphalt;
            }
            else
            {
                tileType = TileType.Grass;
            }

            tileConfigs.Add(new TileConfig(tileType, tile.State, tile.Coordinate, placedObjectType));
        }

        return tileConfigs;
    }
    public void ShowTemperature()
    {
        List<ISimulationTile> tilesWithObjects = map.GetTilesWithObjects();
        simulation.Calculation(tilesWithObjects);
        double result = simulation.GetAverageTemperature();
        tValue.text = result.ToString();
    }

    public void ChangeSunPosition()
    {
        if ( tSun.text == "S")
        {
            light1.enabled = false;
            light2.enabled = true;
            simulation.SunFromWest();
            tSun.text = "W";
            ShowTemperature();
        } else if (tSun.text == "W")
        {
            light1.enabled = true;
            light2.enabled = false;
            simulation.SunFromSouth();
            tSun.text = "S";
            ShowTemperature();
        }
    }


}
