using Calculation;
using Calculation.GameLogic;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example data for testing purpose
            Map map = new Map(10);
            for (int i = 0; i < map.GetMapSize(); i += 2)
                for (int j = 0; j < map.GetMapSize(); j++)
                {
                    map.GetTile(i, j).SetTileType(Calculation.Type.Grass);
                }
            GameObject o1 = new GameObject(map.GetTile(0, 0), 2);
            GameObject o2 = new GameObject(map.GetTile(2, 1), 4);
            GameObject o3 = new GameObject(map.GetTile(2, 2), 2);
            GameObject o4 = new GameObject(map.GetTile(3, 2), 3);
            GameObject o5 = new GameObject(map.GetTile(3, 3), 2);
            o1.objectType = Calculation.Type.Grass;
            map.baseObjects.Add(o1);
            map.baseObjects.Add(o2);
            map.baseObjects.Add(o3);
            map.baseObjects.Add(o4);
            map.baseObjects.Add(o5);
            UHISimulation simulation = new UHISimulation(map);
            simulation.Calculation();
            HeatMap heatMap = simulation.GetSurfaceHeatMap();
            HeatMap buildings = simulation.GetAtmosfericHeatMap();

            for (int i = 0; i < map.GetMapSize(); i++)
            {
                for (int j = 0; j < map.GetMapSize(); j++)
                {
                    Console.Write(buildings.GetHeatValue(i, j));
                    Console.Write("\t");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
            for (int i = 0; i < map.GetMapSize(); i++)
            {
                for (int j = 0; j < map.GetMapSize(); j++)
                {
                    Console.Write(heatMap.GetHeatValue(i, j));
                    Console.Write("\t");
                }
                Console.Write("\n");
            }
        }
    }
}
