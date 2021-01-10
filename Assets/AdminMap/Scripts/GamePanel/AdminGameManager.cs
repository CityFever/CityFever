﻿using System.Collections.Generic;
using Assets.AdminMap.Scripts.MapConfiguration;
using Library;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;

public class AdminGameManager : MonoBehaviour
{
    private Map map;
    private const int mapSize = 100;

    private BaseTile baseTilePrefab;
    private BaseTile selectedTile;
    private UnityObject _unityObjectPrefab;

    private bool showHover = true;

    private GameMode mode = GameMode.Default;

    [SerializeField] private Map mapPrefab;

    private void Start()
    {
        CreateMap();
        GenerateStandardMap();
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

    private void CreateMap()
    {
        map = Instantiate(mapPrefab, transform).Initialize(mapSize);
    }

    public void GenerateGrassMap()
    {
        map.GenerateGrassMap();
    }

    public void GenerateStandardMap()
    {
        map.GenerateStandardMap();
    }

    private void SelectGridCellOnMouseClick()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetIntersectingRay(), out hit))
        {
            GridCell selectedCell = hit.collider.GetComponentInParent<GridCell>();

            if (selectedCell)
            {
                PlaceBaseTileOnGrid(selectedCell);
            }
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
                    UpdateTileType();
                    break;
                case GameMode.ZoneEdition:
                    SwitchZoneState();
                    break;
                case GameMode.ObjectPlacement:
                    map.PlaceGameObjectOnSelectedTile(selectedTile,_unityObjectPrefab);
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

    private void UpdateTileType()
    {       
        map.UpdateTileType(selectedTile, baseTilePrefab);
    }

    private void BuildInactiveZone(State state1, State state2)
    {
        Vector2 coordinate = selectedTile.Coordinate;
        Debug.Log("BuildInactiveZone: " + selectedTile.Coordinate.x + ", " + selectedTile.Coordinate.y);

        map.UpdateZoneOfTiles(coordinate, state1, state2);
    }

    private void SwitchZoneState()
    {
        if (map.zoneBrightness == 0.5f)
        {
            BuildInactiveZone(State.Available, State.Off);
        }
        else
        {
            BuildInactiveZone(State.Off, State.Available);
        }
    }

    public void SetInavtiveZoneWidth(string size)
    {
        if (!string.IsNullOrEmpty(size)){
            map.zoneSizeX = int.Parse(size);
            Debug.Log("InactiveZoneWidth: " + map.zoneSizeX);
        }
    }

    public void SetInactiveZoneHeight(string size)
    {
        if (!string.IsNullOrEmpty(size)){
            map.zoneSizeY = int.Parse(size);
            Debug.Log("InactiveZoneSizeHeight: " + map.zoneSizeY);
        }
    }

    // used when user places tiles on an empty grid
    private void PlaceBaseTileOnGrid(GridCell gridCell)
    {
        map.CreateBaseTile(baseTilePrefab, gridCell);
    }

    public void SetBaseTilePrefab(BaseTile selected)
    {
        SetTileEditionMode();
        map.zoneSizeX = 1;
        map.zoneSizeY = 1;
        baseTilePrefab = selected;
    }

    public void SetGameObjectPrefab(UnityObject selectedPrefab)
    {
        _unityObjectPrefab = selectedPrefab;
        map.zoneSizeX = (int)_unityObjectPrefab.SizeInTiles().x;
        map.zoneSizeY = (int)_unityObjectPrefab.SizeInTiles().z;
        SetObjectPlacementMode();
        map.zoneBrightness = 0.5f;
    }

    //if we mark inactive zones we decrease the brightness of the tile
    public void MarkInactiveZones()
    {
        SetZoneEditionMode();
        map.zoneBrightness = 0.5f;
    }

    //if we mark active zones we increase the brightness of the tile
    public void MarkActiveZones()
    {
        SetZoneEditionMode();
        map.zoneBrightness = 1 / 0.5f;
    }

    public void ObjectRemoval()
    {
        SetObjectRemovalMode();
        map.zoneBrightness = 1 / 0.5f;
    }

    public void SetZoneEditionMode()
    {
        mode = GameMode.ZoneEdition;
    }

    void SetTileEditionMode()
    {
        mode = GameMode.TileEdition;
    }

    public void SetDefaultMode()
    {
        mode = GameMode.Default;
    }

    public void SetObjectPlacementMode()
    {
        mode = GameMode.ObjectPlacement;
    }

    public void SetObjectRemovalMode()
    {
        mode = GameMode.ObjectRemoval;
    }

    public void SetMapBudget(string budget)
    {
        if (!string.IsNullOrEmpty(budget))
        {
            float amount = float.Parse(budget, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            MapConfig.mapConfig.mapBudget = amount;
            MapConfig.mapConfig.mapBudget = mapSize;
        }
    }

    public void LoadUserScene()
    {
        MapConfig.mapConfig.tileCongigurations = GetMapConfiguration();
        SceneManager.LoadScene("UserScene");
    }

    private List<TileConfig> GetMapConfiguration()
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
            };

            tileConfigs.Add(new TileConfig(tileType, tile.State, tile.Coordinate, placedObjectType));
        }

        return tileConfigs;
    }
}