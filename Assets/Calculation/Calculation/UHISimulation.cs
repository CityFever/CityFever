using System;
using System.Collections.Generic;

namespace Calculation
{
    public class UHISimulation
    {
        int mapSize;
        HeatMap surfaceHeatMap;
        HeatMap atmosphericHeatMap;
        Sunlight sunlight;        
        Coord[,] coordsMap;
        List<Coord> coordsList;
        Dictionary<Type, double> evapoValues;
        Dictionary<Type, double> absorbtionValues;

        public UHISimulation(ISimulationMap map)
        {
            mapSize = map.GetMapSize();
            surfaceHeatMap = new HeatMap(mapSize, true);
            atmosphericHeatMap = new HeatMap(mapSize);
            sunlight = new Sunlight();
            sunlight.SetSunPosition(13);

            coordsMap = new Coord[mapSize, mapSize];
            coordsList = new List<Coord>();
            MapTranslate(map);
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

            List<ISimulationObject> objects = map.GetAllGameObjects();
            foreach(ISimulationObject _object in objects)
            {
                int row = _object.GetCentralTile().GetRow();
                int col = _object.GetCentralTile().GetColumn();
                int size = _object.GetSize();
                int halfSize = (size - 1) / 2;
                if (halfSize == 0) coordsMap[row, col].Height = _object.GetHeight();
                else
                {
                    for (int i = -halfSize; i < halfSize; i++)
                        for (int j = -halfSize; j < halfSize; j++)
                        {
                            coordsMap[row + i, col + j].Height = _object.GetHeight();
                            coordsMap[row, col].CoordType = _object.GetObjectType(); 
                        } 
                }
                //atmospheric map used for object representation
                atmosphericHeatMap.SetHeatValue(row, col, _object.GetHeight());
            }
        }
        private void EvapoDictionaryInit()
        {
            //Change of temp because of the evapotranspiration based on type
            evapoValues = new Dictionary<Type, double>
            {
                //Values will be changed
                { Type.Default, 0 },
                { Type.Grass, -0.2 },
                { Type.Water, -0.5 },
                { Type.Concrete, 0 }
            };
        }
        private void AbsorbtionDictionaryInit()
        {
            //Change of temp because of the heat absorbtion based on type
            absorbtionValues = new Dictionary<Type, double>
            {
                //Values will be changed
                { Type.Default, 0 },
                { Type.Grass, 0 },
                { Type.Water, -0.8 },
                { Type.Concrete, 2.5 }
            };
        }

        private void Shade()
        {
            HeatMap shadeHeatMap = new HeatMap(mapSize);
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
                           shadeHeatMap.SetHeatValue(row, col, -3);//Value to be changed
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
                            shadeHeatMap.SetHeatValue(row, col, -3);//Value to be changed
                        }
                        else shadeLeft = (int)Math.Ceiling(height * sunlight.GetShadeLength());
                    }
                }
            }
            surfaceHeatMap.AddHeatMap(shadeHeatMap);
        }
        //Should be sorted first, to increase efficiency
        private void Evapotranspiration()
        {
            EvapoDictionaryInit();
            HeatMap evapoHeatMap = new HeatMap(mapSize);
            foreach(Coord coord in coordsList)
            {
                evapoHeatMap.SetHeatValue(coord.Row, coord.Col, evapoValues[coord.CoordType]);               
            }
            surfaceHeatMap.AddHeatMap(evapoHeatMap);
        }
        private void Absorbtion()
        {
            AbsorbtionDictionaryInit();
            HeatMap absorbtionHeatMap = new HeatMap(mapSize);
            foreach (Coord coord in coordsList)
            {
                absorbtionHeatMap.SetHeatValue(coord.Row, coord.Col, absorbtionValues[coord.CoordType]);
            }
            surfaceHeatMap.AddHeatMap(absorbtionHeatMap);
        }

        public void Calculation()
        {
            Shade();
            Evapotranspiration();
            Absorbtion();
        }

        public HeatMap GetSurfaceHeatMap()
        {
            return surfaceHeatMap;
        }
        public HeatMap GetAtmosfericHeatMap()
        {
            return atmosphericHeatMap;
        }
    }
}
