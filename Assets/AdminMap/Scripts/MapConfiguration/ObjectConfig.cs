using System;

namespace Assets.AdminMap.Scripts.MapConfiguration
{
    [Serializable]
    public class ObjectConfig
    {
        public GameObjectType type;
        public float removalCosts;
        public float placementCosts;

        public ObjectConfig(GameObjectType type, float removalCosts, float placementCosts)
        {
            this.type = type;
            this.removalCosts = removalCosts;
            this.placementCosts = placementCosts;
        }
    }
}
