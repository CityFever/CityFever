using Calculus;
public class WaterTile : BaseTile
{
    public override Type GetTileType()
    {
        return Type.Water;
    }
    private void Awake()
    {
        State = State.Unavailable;
    }
}
