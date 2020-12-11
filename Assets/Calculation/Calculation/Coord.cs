namespace Calculation
{
    public class Coord
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Height { get; set; }
        public Type CoordType { get; set; }
        public Coord(int row, int col)
        {
            Row = row;
            Col = col;
            Height = 0;
            CoordType = Type.Default;
        }
    }
}
