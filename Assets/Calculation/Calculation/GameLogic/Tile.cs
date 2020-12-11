namespace Calculation.GameLogic
{
    public class Tile : ISimulationTile
    {
        public int row;
        public int column;
        public Type TileType = Type.Default;
        public Tile (int row, int column)
        {
            this.row = row;
            this.column = column;
        }
        public int GetRow()
        {
            return row;
        }

        public int GetColumn()
        {
            return column;
        }

        public Type GetTileType()
        {
            return TileType;
        }

        public void SetTileType(Type type)
        {
            TileType = type;
        }
    }
}
