using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.AdminMap.Scripts.MapConfiguration
{
    public class ObjectConfig
    {
        public readonly float DEFAULT_PLACEMENT_COSTS = 10;
        public readonly float DEFAULT_REMOVAL_COSTS = 10;

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
