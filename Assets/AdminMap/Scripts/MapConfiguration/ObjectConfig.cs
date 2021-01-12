namespace Assets.AdminMap.Scripts.MapConfiguration
{
    public class ObjectConfig
    {
        public GameObjectType type { get; set; }
        public float removalCosts { get; set; }
        public float placementCosts { get; set; }

        public ObjectConfig(GameObjectType type, float removalCosts, float placementCosts)
        {
            this.type = type;
            this.removalCosts = removalCosts;
            this.placementCosts = placementCosts;
        }
    }
}
