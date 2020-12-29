using UnityEngine;
using System;
using Grid = Assets.Scripts.Grid.Grid;

namespace Library
{
    [Serializable]

    public class Map : MonoBehaviour
    {
        private int mapSize = 100;

        private Grid grid;

        private BaseTile[,] tiles;
        public float budget { get; set; }
        public int zoneSizeX { get; set; } = 3;
        public int zoneSizeY { get; set; } = 2;
        public float zoneBrightness { get; set; } = 0.5f;

        [SerializeField] private Grid gridPrefab;
        [SerializeField] private GrassTile grassTilePrefab;
        [SerializeField] private WaterTile waterTilePrefab;
        [SerializeField] private AsphaltTile asphaltTilePrefab;

        public Map Initialize(int size)
        {
            mapSize = size;

            init();

            return this;
        }

        private void init()
        {
            tiles = new BaseTile[mapSize, mapSize];

            CreateGrid();
        }

        private void CreateGrid()
        {
            grid = Instantiate(gridPrefab, transform).Initialize(mapSize);
        }

        public void GenerateGrassMap()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    tiles[i, j] = Instantiate(grassTilePrefab, grid.GetTransform(i, j));
                    tiles[i, j].transform.position = new Vector3(i, 0, j);
                    tiles[i, j].Coordinate = new Vector2(i, j);
                    grid.SetAvailable(i, j, false);
                }
            }
        }

        public void GenerateStandardMap()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (((i > 5 && i < 15) && (j > 5 && j < 15)) || ((i > 11 && i < 21) && (j > 11 && j < 21)))
                    {
                        tiles[i, j] = Instantiate(waterTilePrefab, grid.GetTransform(i, j));
                    }
                    else if ((i >= 75 && i <= 81) || (j >= 75 && j <= 81))
                    {
                        tiles[i, j] = Instantiate(asphaltTilePrefab, grid.GetTransform(i, j));
                    }
                    else
                    {
                        tiles[i, j] = Instantiate(grassTilePrefab, grid.GetTransform(i, j));
                    }

                    tiles[i, j].transform.position = new Vector3(i, 0, j);
                    tiles[i, j].Coordinate = new Vector2(i, j);
                    grid.SetAvailable(i, j, false);
                }
            }
        }

        public void SetTile(BaseTile tile)
        {
            tiles[(int)tile.Coordinate.x, (int)tile.Coordinate.y] = tile;
        }

        public BaseTile GetTile(Vector2 coordinate)
        {
            return tiles[(int)coordinate.x, (int)coordinate.y];
        }

        public BaseTile GetTileByCoordinates(int x, int y)
        {
            return tiles[x, y];
        }

        public void CreateBaseTile(BaseTile prefab, GridCell cell)
        {
            Instantiate(prefab, cell.transform).Initialize(cell.GridCoordinate);
            cell.IsAvailable = false;
        }

        public void UpdateTileType(BaseTile selectedTile, BaseTile newTile)
        {
            Transform parent = selectedTile.transform.parent;
            Vector2 position = selectedTile.Coordinate;

            foreach (var child in parent.transform.GetComponentsInChildren<BaseTile>())
            {
                Destroy(child.gameObject);
            }

            BaseTile tile = Instantiate(newTile, parent).Initialize(position);

            SetTile(tile);

            Debug.Log("UpdateTileType: "
                      + tile.Coordinate.x + ", "
                      + tile.Coordinate.y);
        }

        public void SetInactiveTile(Vector2 coordinate, State state1, State state2)
        {
            Vector2Int coordinateInt = new Vector2Int((int)coordinate.x, (int)coordinate.y);
            int halfZoneSizeX = (int) (zoneSizeX/2.0); //round down
            int halfZoneSizeY = (int) (zoneSizeY/2.0); //round down
            //makes an uneven Zone symmetric 
            int symmetricOffsetX = -1;
            if (zoneSizeX % 2 == 0)
                symmetricOffsetX = 0; 
            int symmetricOffsetY = -1;
            if (zoneSizeY % 2 == 0)
                symmetricOffsetY = 0;

            for (int x = coordinateInt.x - halfZoneSizeX; x < coordinateInt.x + halfZoneSizeX -symmetricOffsetX; x++)
            {
                for (int y = coordinateInt.y - halfZoneSizeY; y < coordinateInt.y + halfZoneSizeY - symmetricOffsetY; y++)
                {
                    if (!OutsideGrid(x, y))
                    {
                        UpdateZoneState(x, y, state1, state2);
                    }
                }
            }            
        }      

        private bool OutsideGrid(int coordinateX, int coordinateY)
        {
            return coordinateX < 0 || coordinateX > (mapSize - 1) || coordinateY < 0 || coordinateY > (mapSize - 1);
        }

        private void UpdateZoneState(int coordinateX, int coordinateY, State state1, State state2)
        {
            BaseTile tile = GetTileByCoordinates(coordinateX, coordinateY);
            if (tile.State == state1)
            {
                tile.GetComponentInChildren<Renderer>().material.color *= zoneBrightness;
                GetTileByCoordinates(coordinateX, coordinateY).State = state2;
            }
        }

        
        public void PlaceGameObjectOnSelectedTile(BaseTile selectedTile,UnityObject _unityObject)
        {
            //place Object on desired Tile
            UnityObject unityObject = Instantiate(_unityObject, selectedTile.transform);

            //deactivate surrounding Tiles regarding Objects size
            Vector2 sizeInTiles = _unityObject.SizeInTiles();
            zoneSizeX = (int) sizeInTiles.x;
            zoneSizeY = (int) sizeInTiles.y;

            this.SetInactiveTile(selectedTile.Coordinate, State.Available, State.Off);

            selectedTile.State = State.Unavailable;
        }
    }
}
