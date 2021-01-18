using System;

namespace Calculus
{
    public class HeatMap
    {
        private int size { get; set; }
        private double[,] HeatValue { get; set; }

        public HeatMap(int size)
        {
            this.size = size;
            HeatValue = new double[size, size];
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    HeatValue[i, j] = 0d;
                }
        }

        public void SetHeatValue(int row, int col, double value)
        {
            HeatValue[row, col] = value;
        }
        public double GetHeatValue(int row, int col)
        {
            return Math.Round(HeatValue[row, col], 1, MidpointRounding.ToEven);
        }
        public void AddHeatMap(HeatMap heatMap)
        {
            if (size == heatMap.size)
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        HeatValue[i, j] += heatMap.GetHeatValue(i, j);
                    }
            }
            else throw new System.Exception("Maps have different size");
        }
        public double GetAverageTeperature()
        {
            double result = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    result += HeatValue[i, j] / (size * size);
                }
            return Math.Round(result, 2, MidpointRounding.ToEven);
        }
    }
}
