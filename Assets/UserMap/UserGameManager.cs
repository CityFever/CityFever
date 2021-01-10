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

    void Start()
    {
        CreateMap();
        map.adminAccess = true;

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
    private void Update()
    {
        map.removePriorHover();

        if (Input.GetMouseButtonDown(0))
        {
            //SelectGridCellOnMouseClick();
            SelectTileOnMouseClick();
        }
        else if (showHover)
        {
            FetchRaycastedTile();
            map.markHovering(selectedTile);
        }
    }
    private void SelectTileOnMouseClick()
    {
        FetchRaycastedTile();

        if (selectedTile)
        {
            switch (mode)
            {
                case GameMode.TileEdition:
                    //UpdateTileType();
                    break;
                case GameMode.ZoneEdition:
                    //SwitchZoneState();
                    break;
                case GameMode.ObjectPlacement:
                    map.PlaceGameObjectOnSelectedTile(selectedTile, _unityObjectPrefab);
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
    private void FetchRaycastedTile()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetIntersectingRay(), out hit))
        {
            selectedTile = hit.collider.GetComponentInParent<BaseTile>();
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
            map.zoneSizeX = int.Parse(height);
            Debug.Log("InactiveZoneWidth: " + map.zoneSizeX);
        }
    }
    public void SetGameObjectPrefab(UnityObject selectedPrefab)
    {
        Debug.Log("Set Object");
        _unityObjectPrefab = selectedPrefab;
        map.zoneSizeX = (int)_unityObjectPrefab.SizeInTiles().x;
        map.zoneSizeY = (int)_unityObjectPrefab.SizeInTiles().z;
        SetObjectPlacementMode();
        map.zoneBrightness = 0.5f;
    }
    public void ObjectRemoval()
    {
        SetObjectRemovalMode();
        map.zoneBrightness = 1 / 0.5f;
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
