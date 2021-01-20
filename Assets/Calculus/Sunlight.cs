namespace Calculus
{
    //Values calculated for Łódź 01.09.2020
    public class Sunlight
    {
        public double Temperature;
        private double ShadeLenght { get; set; }
        private int rowDirection { get; set; }
        private int columnDirection { get; set; }
        public void SetSunPosition(int time)
        {
            if (time == 13)
            {
                //Sun from South 13:00
                ShadeLenght = 1;
                rowDirection = 0;
                columnDirection = -1;
                Temperature = 20d;
            }
            else if (time == 18)
            {
                //Sun from West 18:00
                ShadeLenght = 5.7;
                rowDirection = 1;
                columnDirection = 0;
                Temperature = 18d;
            }
            else throw new System.Exception("Choose time 13 or 18");
        }
        public int GetRowDirection()
        {
            return rowDirection;
        }
        public int GetColumnDirection()
        {
            return columnDirection;
        }
        public double GetShadeLength()
        {
            return ShadeLenght;
        }
    }
}
