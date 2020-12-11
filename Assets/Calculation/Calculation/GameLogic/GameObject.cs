namespace Calculation.GameLogic
{
    public class GameObject : ISimulationObject
    {
        public int height;
        public int sizeInTiles;
        public ISimulationTile centralTile;
        public Type objectType = Type.Default;

        public GameObject(ISimulationTile centralTile, int height)
        {
            this.centralTile = centralTile;
            this.height = height;
        }

        public ISimulationTile GetCentralTile()
        {
            return centralTile;
        }

        public int GetHeight()
        {
            return height;
        }

        public Type GetObjectType()
        {
            return objectType;
        }

        public int GetSize()
        {
            return 1;
        }

    }
}
