using Assets.AdminMap.Scripts.Controllers;
using Assets.AdminMap.Scripts.MapConfiguration;
using Library;
using System.Collections.Generic;
using System.Linq;
using Assets.AdminMap.Scripts;
using Database;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = Assets.AdminMap.Scripts.Application;
using UnityEngine.EventSystems;

public class AdminGameManager : MonoBehaviour
{
    private Map map;
    private const int mapSize = 100;

    private BaseTile baseTilePrefab;
    private BaseTile selectedTile;
    private UnityObject unityObject;

    private bool showHover = true;

    private GameMode mode = GameMode.Default;

    private float currentObjectPlacmentCosts = 0.0f;
    private float currentObjectRemovalCosts = 0.0f;

    private GameObjectType currectObjectType = GameObjectType.Default;
    //private bool isActiveZoneMode = true;

    [SerializeField] private Map mapPrefab;
    [SerializeField] private List<UnityObject> prefabs;

    private void Start()
    {
        CreateMap();
        if (StaticScript.mapButton == "grass")
        {
            GenerateGrassMap();
        } else
        {
            GenerateStandardMap();
        }
        Debug.Log("Start()" + Application.application.SelectedGameObjectType);
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
            FetchRaycastedTile();
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
                    map.PlaceGameObjectOnSelectedTile(selectedTile, unityObject);
                   //SetDefaultMode();
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

    private void SelectObjectOnMouseClick()
    {
        FetchRayCastedObject();
    }

    private void FetchRayCastedObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetIntersectingRay(), out hit, Constants.MAX_DISTANCE_FROM_POINT, 1 << 8))
        {
            Debug.Log("Fetched object: " + hit.collider.GetComponent<UnityObject>());
            unityObject = hit.collider.GetComponent<UnityObject>();
        }
    }

    private void FetchRaycastedTile()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetIntersectingRay(), out hit))
        {
            selectedTile = hit.collider.GetComponentInParent<BaseTile>();
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
        if (map.zoneBrightness.Equals(Constants.INACTIVE_TILE))
        {
            BuildInactiveZone(State.Available, State.Off);
        }
        else
        {
            BuildInactiveZone(State.Off, State.Available);
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
        map.zoneSizeX = (int) unityObject.SizeInTiles().x;
        map.zoneSizeY = (int) unityObject.SizeInTiles().z;
        map.zoneBrightness = Constants.INACTIVE_TILE;
        currectObjectType = selectedPrefab.Type();
        SetObjectPlacementMode();
    }

    public void MarkInactiveZones()
    {
        SetZoneEditionMode();
        map.zoneBrightness = Constants.INACTIVE_TILE;
    }

    public void MarkActiveZones()
    {
        SetZoneEditionMode();
        map.zoneBrightness = Constants.ACTIVE_TILE;
    }

    public void RemoveSelectedObject()
    {
        SetObjectRemovalMode();
        map.zoneBrightness = Constants.ACTIVE_TILE;
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
        Application.application.SelectedGameObjectType = GameObjectType.Default;
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

    public void SaveMapConfiguration()
    {
        Debug.Log("SaveMapConfiguration");
        MapConfig.mapConfig.tileConfigs = GetTilesConfiguration();

        UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password, () =>
        {
            MapsRepository.CreateAdminMap(MapConfig.mapConfig, (id) =>
            {
                MapConfig.mapConfig.DatabaseId = id;
                Debug.Log("Created map Id: " + id);
                UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password,
                    () => { MapsRepository.UpdateAdminMap(MapConfig.mapConfig, id); });
            });
        });

        //SceneManager.LoadScene("PlayerGame");
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

    public void SetPlacementCosts(string costs)
    {
        if (!string.IsNullOrEmpty(costs))
        {
            float price = float.Parse(costs, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            currentObjectPlacmentCosts = price;
            Debug.Log("Placement costs: " + currentObjectPlacmentCosts);
        }
    }

    public void SetRemovalCosts(string costs)
    {
        if (!string.IsNullOrEmpty(costs))
        {
            float price = float.Parse(costs, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            currentObjectRemovalCosts = price;
            Debug.Log("Removal costs: " + currentObjectRemovalCosts);
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
        AddObjectConfig(Application.application.SelectedGameObjectType, currentObjectRemovalCosts, currentObjectPlacmentCosts);
        Application.application.SetDefaultType();
        //currectObjectType = GameObjectType.Default;
        currentObjectRemovalCosts = 0;
        currentObjectPlacmentCosts = 0;
    }

    public void RemoveObjectConfig()
    {
        GameObjectType currenType = Application.application.SelectedGameObjectType;
        if (currenType != GameObjectType.Default)
        {
            MapConfig.mapConfig.RemoveConfig(currenType);
            currectObjectType = GameObjectType.Default;
        }
        else
        {
            Debug.Log("Game object type is not specified.");
        }
    }

    public void OnZoneHeightChanged(float newValue)
    {
        map.zoneSizeX = (int) newValue;
    }

    public void OnZoneWidthChanged(float newValue)
    {
        map.zoneSizeY = (int) newValue;
    }

    public void SelectObjectType()
    {
        Debug.Log("SelectedObjectType() " + Application.application.SelectedGameObjectType);
        GameObjectType selectedType = Application.application.SelectedGameObjectType;
        var prefabToInstantiate = prefabs.FirstOrDefault(
            prefab => prefab.Type().Equals(selectedType));
        Debug.Log("Instantiated: " + prefabToInstantiate.Type());
        SetGameObjectPrefab(prefabToInstantiate);
    }
}