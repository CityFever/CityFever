using UnityEngine;
using System;
using Grid = Assets.Scripts.Grid.Grid;
using System.Collections.Generic;

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

        public static Color HOVERINGCOLOR = new Color(100, 100, 100,0); 

        private List<BaseTile> hoveredTiles;

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

        //returns List with all updated Tiles
        public List<BaseTile> UpdateZoneOfTiles(Vector2 coordinate, State state1, State state2)
        {
            List<BaseTile> updatedTiles = new List<BaseTile>();
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
                        BaseTile tile = GetTileByCoordinates(x, y);
                        bool updated  = UpdateTileState(tile, state1, state2);

                        if (updated)
                            updatedTiles.Add(tile);
                    }
                }
            }
            return updatedTiles;
        }      

        private bool OutsideGrid(int coordinateX, int coordinateY)
        {
            return coordinateX < 0 || coordinateX > (mapSize - 1) || coordinateY < 0 || coordinateY > (mapSize - 1);
        }
        //returns a bool if the Tile has been updated or not
        private bool UpdateTileState(BaseTile tile, State state1, State state2)
        {
            
            if (tile.State == state1)
            {
                //Changing State Activ or inactive
                if(state2 == State.Off || state2 == State.Available && state1 == State.Off)
                {
                    tile.GetComponentInChildren<Renderer>().material.color *= zoneBrightness;

                }
                //changing to hovered
                else if(state2 == State.Hovered)
                {
                    tile.GetComponentInChildren<Renderer>().material.color += HOVERINGCOLOR;
                }
                //changing back from hovered
                else if(state2 == State.Available && state1 == State.Hovered)
                {
                    tile.GetComponentInChildren<Renderer>().material.color -= HOVERINGCOLOR;
                }
                //appliying changes
                tile.State = state2;
                
                return true;
            }
            return false;
        }

        
        public void PlaceGameObjectOnSelectedTile(BaseTile selectedTile,UnityObject _unityObject)
        {
            if (CheckRestrictions(selectedTile, _unityObject))
            {
                //place Object on desired Tile
                UnityObject clone = Instantiate(_unityObject, selectedTile.transform);
                selectedTile.unityObject = clone;
                //deactivate surrounding Tiles regarding Objects size
                Vector2 sizeInTiles = _unityObject.SizeInTiles();
                zoneSizeX = (int)sizeInTiles.x;
                zoneSizeY = (int)sizeInTiles.y;
                this.UpdateZoneOfTiles(selectedTile.Coordinate, State.Available, State.Off);

                selectedTile.State = State.Unavailable;
            }
        }
        public void RemoveObjectFromZone(BaseTile selectedTile)
        {
            List<BaseTile> tilesWithObjectsInZone = new List<BaseTile>();

            //search Zone for tiles with Objects
            Vector2Int coordinateInt = new Vector2Int((int)selectedTile.Coordinate.x, (int)selectedTile.Coordinate.y);
            int halfZoneSizeX = (int)(this.zoneSizeX / 2.0); //round down
            int halfZoneSizeY = (int)(this.zoneSizeY / 2.0); //round down
            //makes an uneven Zone symmetric 
            int symmetricOffsetX = -1;
            if (this.zoneSizeX % 2 == 0)
                symmetricOffsetX = 0;
            int symmetricOffsetY = -1;
            if (this.zoneSizeY % 2 == 0)
                symmetricOffsetY = 0;
            for (int x = coordinateInt.x - halfZoneSizeX; x < coordinateInt.x + halfZoneSizeX - symmetricOffsetX; x++)
            {
                for (int y = coordinateInt.y - halfZoneSizeY; y < coordinateInt.y + halfZoneSizeY - symmetricOffsetY; y++)
                {
                    if (!OutsideGrid(x, y))
                    {
                        BaseTile tile = GetTileByCoordinates(x, y);
                        if(tile.unityObject != null)
                        {
                            tilesWithObjectsInZone.Add(tile);
                        }
                    }
                }
            }
            //delete objects and set sourrounding active again
            ////save Zonesize
            int zoneSizeX = this.zoneSizeX;
            int zoneSizeY = this.zoneSizeY;
            foreach (BaseTile tile in tilesWithObjectsInZone)
            {
                tile.unityObject.DestroyUnityObject();
                tile.State = State.Off;
                this.zoneSizeX = (int)tile.unityObject.SizeInTiles().x;
                this.zoneSizeY = (int)tile.unityObject.SizeInTiles().y;
                UpdateZoneOfTiles(tile.Coordinate, State.Off, State.Available);
            }
            this.zoneSizeX = zoneSizeX;
            this.zoneSizeY = zoneSizeY;

        }
        public void markHovering(BaseTile hoveredTile)
        {
            if (hoveredTile != null)
            {
                this.hoveredTiles =
                        this.UpdateZoneOfTiles(hoveredTile.Coordinate, State.Available, State.Hovered);
            }
        }
        public void removePriorHover()
        {
            if (hoveredTiles == null)
                return;
            foreach(BaseTile tile in hoveredTiles)
            {
                this.UpdateTileState(tile, State.Hovered, State.Available);
            }
            hoveredTiles = null;
        }

        public bool CheckRestrictions(BaseTile selectedTile, UnityObject _unityObject)
        {
            BaseTile tileType = selectedTile.GetComponentInChildren<BaseTile>();

            if(selectedTile.State != State.Unavailable && selectedTile.State != State.Off)
            {
                if(tileType is GrassTile)
                {
                    if (_unityObject.grass == true)
                        return true;
                } 
                else if(tileType is AsphaltTile)
                {
                    if (_unityObject.asphalt == true)
                        return true;
                }
                else if (tileType is WaterTile)
                {
                    if (_unityObject.water == true)
                        return true;
                }
            }
            
            return false;
        }
    }
}
