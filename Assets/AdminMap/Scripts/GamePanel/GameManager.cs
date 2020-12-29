using Library;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Map map;
    private const int mapSize = 100;

    private BaseTile baseTilePrefab;
    private BaseTile selectedTile;
    private UnityObject _unityObjectPrefab;

    private GameMode mode = GameMode.Default;

    [SerializeField] private Map mapPrefab;

    private void Start()
    {
        CreateMap();
        GenerateStandardMap();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SelectGridCellOnMouseClick();
            SelectTileOnMouseClick();
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
            Debug.Log("Fetched Tile: " + selectedTile.Coordinate.x + ", " + selectedTile.Coordinate.y);
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

        map.SetInactiveTile(coordinate, state1, state2);
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

    //if we mark inactive zones we decrease the brightness of the tile
    public void MarkInactiveZones()
    {
        map.zoneBrightness = 0.5f;
    }

    //if we mark active zones we increase the brightness of the tile
    public void MarkActiveZones()
    {
        map.zoneBrightness = 1 / 0.5f;
    }

    public void SetInactiveZoneSize(string size)
    {
        if (!string.IsNullOrEmpty(size)){
            map.zoneSizeX = int.Parse(size);
            //for the secound input
            map.zoneSizeY = map.zoneSizeX;
            Debug.Log("InactiveZoneSize: " + map.zoneSizeX);
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
        baseTilePrefab = selected;
    }

   

    public void SetGameObjectPrefab(UnityObject selectedPrefab)
    {
        SetObjectPlacementMode();
        _unityObjectPrefab = selectedPrefab;
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

    public void SetMapBudget(float budget)
    {
        map.budget = budget;
    }
}
