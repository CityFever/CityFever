using Assets.AdminMap.Scripts.Controllers;
using Assets.AdminMap.Scripts.MapConfiguration;
using Library;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminGameManager : MonoBehaviour
{
    private Map map;
    private const int mapSize = 100;

    private BaseTile baseTilePrefab;
    private BaseTile selectedTile;
    private UnityObject unityObject;

    private bool showHover = true;

    private GameMode mode = GameMode.Default;

    [SerializeField] private Map mapPrefab;

    private float currentObjectPlacmentCosts = 0.0f;
    private float currentObjectRemovalCosts = 0.0f;
    private GameObjectType currectObjectType = GameObjectType.Default;

    private void Start()
    {
        CreateMap();
        GenerateStandardMap();
    }

    private void Update()
    {
        map.RemovePriorHover();

        if (Input.GetMouseButtonDown(0))
        {
            SelectTileOnMouseClick();
            SelectObjectOnMouseClick();
        }
        else if (showHover)
        {
            FetchRaycastedTile();
            map.MarkHovering(selectedTile);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
           RotateSelectedGameObject();
        }
    }

    private void RotateSelectedGameObject()
    {
        if (unityObject != null)
        {
            RotationController.Rotate180Degrees(unityObject);
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

    private void SelectObjectOnMouseClick()
    {
        FetchRayCastedObject();
    }

    private void FetchRayCastedObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetIntersectingRay(), out hit, 100, 1 << 8))
        {
            Debug.Log("Fetched object: " + hit.collider.GetComponent<UnityObject>());
            unityObject = hit.collider.GetComponent<UnityObject>();
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
                    map.PlaceGameObjectOnSelectedTile(selectedTile, unityObject);
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
        if (!string.IsNullOrEmpty(size))
        {
            map.zoneSizeX = int.Parse(size);
            Debug.Log("InactiveZoneWidth: " + map.zoneSizeX);
        }
    }

    public void SetInactiveZoneHeight(string size)
    {
        if (!string.IsNullOrEmpty(size))
        {
            map.zoneSizeY = int.Parse(size);
            Debug.Log("InactiveZoneSizeHeight: " + map.zoneSizeY);
        }
    }

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
        unityObject = selectedPrefab;
        map.zoneSizeX = (int)unityObject.SizeInTiles().x;
        map.zoneSizeY = (int)unityObject.SizeInTiles().z;
        SetObjectPlacementMode();
        map.zoneBrightness = 0.5f;
        currectObjectType = selectedPrefab.Type();
    }

    public void MarkInactiveZones()
    {
        SetZoneEditionMode();
        map.zoneBrightness = 0.5f;
    }

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
        }
    }

    public void LoadUserScene()
    {
        MapConfig.mapConfig.tileConfigs = GetTilesConfiguration();
        Debug.Log("Map budget before the scene switch: " + MapConfig.mapConfig.mapBudget);
        foreach (var config in MapConfig.mapConfig.placeableObjectConfigs)
        {
            Debug.Log("Object type: " + config.type + ", placement costs: " + config.placementCosts + ", removal costs: " + config.removalCosts);
        }
        SceneManager.LoadScene("UserScene");
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
            };

            tileConfigs.Add(new TileConfig(tileType, tile.State, tile.Coordinate, placedObjectType));
        }

        return tileConfigs;
    }

    public void SetPlacementCosts(string costs)
    {
        if (!string.IsNullOrEmpty(costs))
        {
            float price = float.Parse(costs, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            currentObjectPlacmentCosts = price;
            //Debug.Log("Placement costs: " + currentObjectPlacmentCosts);
        }
    }

    public void SetRemovalCosts(string costs)
    {
        if (!string.IsNullOrEmpty(costs))
        {
            float price = float.Parse(costs, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            currentObjectRemovalCosts = price;
            //Debug.Log("Removal costs: " + currentObjectRemovalCosts);
        }
    }

    public void AddObjectConfig(GameObjectType type, float removalPrice, float placementPrice)
    {
        if (removalPrice > 0 && placementPrice > 0 && type != GameObjectType.Default)
        {
            MapConfig.mapConfig.AddConfig(type, removalPrice, placementPrice);
        }
        else
        {
            Debug.Log("Some properties are not specified.");
        }
    }

    public void SaveObjectConfig()
    {
        AddObjectConfig(currectObjectType, currentObjectRemovalCosts, currentObjectPlacmentCosts);
        currectObjectType = GameObjectType.Default;
        currentObjectRemovalCosts = 0;
        currentObjectPlacmentCosts = 0;
    }

    public void RemoveObjectConfig()
    {
        if (currectObjectType != GameObjectType.Default)
        {
            MapConfig.mapConfig.RemoveConfig(currectObjectType);
            currectObjectType = GameObjectType.Default;
        }
        else
        {
            Debug.Log("Game object type is not specified.");
        }
    }
}