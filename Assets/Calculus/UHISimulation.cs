using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculus
{
    public class UHISimulation
    {
        private int mapSize;
        private HeatMap surfaceHeatMap;
        private Sunlight sunlight;
        private Coord[,] coordsMap;
        private List<Coord> coordsList;
        private Dictionary<Type, double> evapoValues;
        private Dictionary<Type, double> absorbtionValues;
        private Dictionary<Type, double> shadeValues;
        private ISimulationMap map;
        public UHISimulation(ISimulationMap map)
        {
            mapSize = map.GetMapSize();
            surfaceHeatMap = new HeatMap(mapSize);
            sunlight = new Sunlight();
            sunlight.SetSunPosition(13);

            coordsMap = new Coord[mapSize, mapSize];
            coordsList = new List<Coord>();
            this.map = map;

        }
        public void SunFromSouth()
        {
            sunlight.SetSunPosition(13);
        }
        public void SunFromWest()
        {
            sunlight.SetSunPosition(18);
        }
        private void MapTranslate(ISimulationMap map)
        {
            for (int row = 0; row < mapSize; row++)
                for (int col = 0; col < mapSize; col++)
                {
                    Coord coord = new Coord(row, col);
                    coordsMap[row, col] = coord;
                    coordsList.Add(coord);
                    coord.CoordType = map.GetTile(row, col).GetTileType();
                }

            List<ISimulationTile> tilesWithObjects = new List<ISimulationTile>();
            tilesWithObjects = map.GetTilesWithObjects();
            if (tilesWithObjects != null && tilesWithObjects.Count() != 0)
            {
                foreach (ISimulationTile tile in tilesWithObjects)
                {
                    int row = tile.GetRow();
                    int col = tile.GetColumn();
                    int rowSize = tile.GetGameObject().GetRowSize();
                    int colSize = tile.GetGameObject().GetColSize();
                    int halfRowSize = (rowSize - 1) / 2;
                    int halfColSize = (colSize - 1) / 2;
                    if (halfRowSize > 0 && halfColSize > 0)
                    {
                        for (int i = -halfRowSize; i < halfRowSize; i++)
                            for (int j = -halfColSize; j < halfColSize; j++)
                            {
                                coordsMap[row + i, col + j].Height = tile.GetGameObject().GetHeight();
                                coordsMap[row + i, col + j].CoordType = TypeMapping(tile.GetGameObject().GetObjectType());
                            }
                    }
                    else if (halfRowSize > 0 && halfColSize == 0)
                    {
                        for (int i = -halfRowSize; i < halfRowSize; i++)
                        {
                            coordsMap[row + i, col].Height = tile.GetGameObject().GetHeight();
                            coordsMap[row + i, col].CoordType = TypeMapping(tile.GetGameObject().GetObjectType());
                        }
                    }
                    else if (halfRowSize == 0 && halfColSize > 0)
                    {
                        for (int j = -halfColSize; j < halfColSize; j++)
                        {
                            coordsMap[row, col + j].Height = tile.GetGameObject().GetHeight();
                            coordsMap[row, col + j].CoordType = TypeMapping(tile.GetGameObject().GetObjectType());
                        }
                    }
                    else if (halfRowSize == 0 && halfColSize == 0)
                    {
                        coordsMap[row, col].Height = tile.GetGameObject().GetHeight();
                        coordsMap[row, col].CoordType = TypeMapping(tile.GetGameObject().GetObjectType());
                    }
                }
            }
            ShadeMapping();
        }
        private void EvapoDictionaryInit()
        {
            //Change of temp because of the evapotranspiration based on type
            evapoValues = new Dictionary<Type, double>
            {
                { Type.Default, 0 },
                { Type.Grass, -0.53 },
                { Type.Shrubs, -0.61 },
                { Type.Trees, -0.82 },
                { Type.Water, -4.2 }
            };
        }
        private void AbsorbtionDictionaryInit()
        {
            //Change of temp because of the heat absorbtion based on type
            absorbtionValues = new Dictionary<Type, double>
            {
                { Type.Default, sunlight.Temperature},
                { Type.Grass, 1.441 * sunlight.Temperature},
                { Type.Water, sunlight.Temperature},
                { Type.Concrete,  2.213 * sunlight.Temperature},
                { Type.Roof, 2.731 * sunlight.Temperature }
            };
        }
        private void ShadeDictionaryInit()
        {
            //Change of temp because of the shade based on type
            shadeValues = new Dictionary<Type, double>
            {
                { Type.Default, 0},
                { Type.Grass, -0.221 * sunlight.Temperature},
                { Type.Water, -0.113 * sunlight.Temperature},
                { Type.Concrete,  -0.481 * sunlight.Temperature},
                { Type.Roof, -0.851 * sunlight.Temperature }
            };
        }
        private void ShadeMapping()
        {
            int shadeLeft;
            //Shade from South
            if (sunlight.GetRowDirection() == 0)
            {
                for (int col = 0; col < mapSize; col++)
                {
                    shadeLeft = 0;
                    for (int row = mapSize - 1; row >= 0; row--)
                    {
                        shadeLeft--;
                        int height = coordsMap[row, col].Height;
                        if (shadeLeft >= height)
                        {
                            coordsMap[row, col].IsShaded = true;
                        }
                        else shadeLeft = (int)Math.Ceiling(height * sunlight.GetShadeLength());
                    }
                }
            }
            //Shade from West
            if (sunlight.GetColumnDirection() == 0)
            {
                for (int row = 0; row < mapSize; row++)
                {
                    shadeLeft = 0;
                    for (int col = 0; col < mapSize; col++)
                    {
                        shadeLeft--;
                        int height = coordsMap[row, col].Height;
                        if (shadeLeft >= height)
                        {
                            coordsMap[row, col].IsShaded = true;
                        }
                        else shadeLeft = (int)Math.Ceiling(height * sunlight.GetShadeLength());
                    }
                }
            }
        }

        private Type TypeMapping(GameObjectType type)
        {
            switch (type)
            {
                case GameObjectType.Tree:
                case GameObjectType.FirTree:
                    return Type.Trees;
                case GameObjectType.Bush:
                case GameObjectType.FancyBush:
                case GameObjectType.Flower:
                    return Type.Shrubs;
                case GameObjectType.Fountain:
                    return Type.Water;
                case GameObjectType.Sandpit:
                    return Type.Concrete;
                case GameObjectType.House:
                case GameObjectType.Church:
                case GameObjectType.Shop:
                case GameObjectType.ModernHouse:
                case GameObjectType.SolarHouse:
                    return Type.Roof;
                default:
                    return Type.Default;
            }
        }
        //Should be sorted first, to increase efficiency
        private void Evapotranspiration()
        {
            EvapoDictionaryInit();
            HeatMap evapoHeatMap = new HeatMap(mapSize);
            foreach (Coord coord in coordsList)
            {
                if (evapoValues.ContainsKey(coord.CoordType))
                    evapoHeatMap.SetHeatValue(coord.Row, coord.Col, evapoValues[coord.CoordType]);
                else
                    evapoHeatMap.SetHeatValue(coord.Row, coord.Col, evapoValues[Type.Default]);
            }
            surfaceHeatMap.AddHeatMap(evapoHeatMap);
        }
        private void Absorbtion()
        {
            AbsorbtionDictionaryInit();
            HeatMap absorbtionHeatMap = new HeatMap(mapSize);
            foreach (Coord coord in coordsList)
            {
                if (absorbtionValues.ContainsKey(coord.CoordType))
                    absorbtionHeatMap.SetHeatValue(coord.Row, coord.Col, absorbtionValues[coord.CoordType]);
                else
                    absorbtionHeatMap.SetHeatValue(coord.Row, coord.Col, absorbtionValues[Type.Default]);
            }
            surfaceHeatMap.AddHeatMap(absorbtionHeatMap);
        }
        private void Shade()
        {
            ShadeDictionaryInit();
            HeatMap shadeHeatMap = new HeatMap(mapSize);
            foreach (Coord coord in coordsList)
            {
                if (shadeValues.ContainsKey(coord.CoordType) && coord.IsShaded)
                    shadeHeatMap.SetHeatValue(coord.Row, coord.Col, shadeValues[coord.CoordType]);
                else
                    shadeHeatMap.SetHeatValue(coord.Row, coord.Col, shadeValues[Type.Default]);
            }
            surfaceHeatMap.AddHeatMap(shadeHeatMap);
        }

        public void Calculation()
        {
            MapTranslate(map);
            Evapotranspiration();
            Absorbtion();
            Shade();
        }

        public HeatMap GetSurfaceHeatMap()
        {
            return surfaceHeatMap;
        }

        public double GetAverageTemperature()
        {
            return surfaceHeatMap.GetAverageTeperature();
        }
    }
}
