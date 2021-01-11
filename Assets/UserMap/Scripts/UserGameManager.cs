using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.AdminMap.Scripts.MapConfiguration;
using Library;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserGameManager : MonoBehaviour
{
    private Map map;

    [SerializeField] private Map mapPrefab;
    [SerializeField] private List<UnityObject> prefabs;

    private UnityObject _unityObjectPrefab;

    private BaseTile selectedTile;

    private bool showHover = true;

    private GameMode mode = GameMode.Default;

    private List<ObjectConfig> availableObjects;

    void Start()
    {
        CreateMap();
        map.adminAccess = false;
        foreach (var config in availableObjects)
        {
            Debug.Log("USER: Object type: " + config.type + ", placement costs: " + config.placementCosts + ", removal costs: " + config.removalCosts);
        }
    }

    private void CreateMap()
    {
        float savedBudget = MapConfig.mapConfig.mapBudget;
        int savedMapSize = MapConfig.mapConfig.mapSize;

        map = Instantiate(mapPrefab, transform).Initialize(savedMapSize);
        map.budget = savedBudget;
        ConfigureTiles();
        SetAvailableObjects();
    }

    private void SetAvailableObjects()
    {
        availableObjects = MapConfig.mapConfig.placeableObjectConfigs;
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
        }
    }
    private void Update()
    {
        map.RemovePriorHover();

        if (Input.GetMouseButtonDown(0))
        {
            SelectTileOnMouseClick();
        }
        else if (showHover)
        {
            FetchRayCastedTile();
            map.MarkHovering(selectedTile);
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
                    SetDefaultMode();
                    break;
                case GameMode.ObjectRemoval:
                    map.RemoveObjectFromZone(selectedTile);
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
            if (map.PlaceGameObjectOnSelectedTile(selectedTile, _unityObjectPrefab))
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

    private bool ObjectIsAvailable()
    {
        return MapConfig.mapConfig.IsContained(_unityObjectPrefab.Type());
    }

    private bool HasEnoughMoney()
    {
        var objectPrice = GetObjectPlacementCosts();

        return objectPrice <= map.budget;
    }

    private void ReduceMapBudget(float byValue)
    {
        map.budget -= byValue;
    }

    private float GetObjectPlacementCosts()
    {
        Debug.Log("Fetching placement costs for: " + _unityObjectPrefab.Type());
        return MapConfig.mapConfig.placeableObjectConfigs.FirstOrDefault(config => config.type.Equals(_unityObjectPrefab.Type())).placementCosts;
    }

    private float GetObjectRemovalCosts()
    {
        Debug.Log("Fetching removal costs for: " + _unityObjectPrefab.Type());
        return availableObjects.FirstOrDefault(config => config.type.Equals(_unityObjectPrefab.Type())).removalCosts;
    }

    private void FetchRayCastedTile()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetIntersectingRay(), out hit))
        {
            selectedTile = hit.collider.GetComponentInParent<BaseTile>();
            //Debug.Log("HoveredTile" + selectedTile.State);
            //Debug.Log("Fetched Tile: " + selectedTile.Coordinate.x + ", " + selectedTile.Coordinate.y);
        }
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
        _unityObjectPrefab = selectedPrefab;
        map.zoneSizeX = (int)_unityObjectPrefab.SizeInTiles().x;
        map.zoneSizeY = (int)_unityObjectPrefab.SizeInTiles().z;
        SetObjectPlacementMode();
        map.zoneBrightness = 0.8f;
    }
    public void ObjectRemoval()
    {
        SetObjectRemovalMode();
        map.zoneBrightness = 1 / 0.8f;
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
}
