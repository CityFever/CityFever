namespace Calculation
{
    public class HeatMap
    {
        public const double BASE_TEMPERATURE = 20d;
        private int size { get; set; }
        private double[,] HeatValue { get; set; }

        public HeatMap(int size)
        {
            this.size = size;
            HeatValue = new double[size, size];
            Initialize();
        }
        public HeatMap(int size, bool basicTemp)
        {
            if (basicTemp)
            {
                this.size = size;
                HeatValue = new double[size, size];
                BasicTempearture();
            }            
        }
        private void Initialize()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    HeatValue[i, j] = 0d;
                }
        }
        private void BasicTempearture()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    HeatValue[i, j] = BASE_TEMPERATURE;
                }
        }
        public void SetHeatValue(int row, int col, double value)
        {
            HeatValue[row,col] = value;
        }
        public double GetHeatValue(int row, int col)
        {
            return HeatValue[row,col];
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
    }
}
