using System.Collections.Generic;
using System.Linq;
using Assets.AdminMap.Scripts.MapConfiguration;
using UnityEngine;

public class MapConfig : MonoBehaviour
{
    public static MapConfig mapConfig;

    public List<TileConfig> tileConfigs;

    public List<ObjectConfig> placeableObjectConfigs;

    public float mapBudget { get; set; }
    public int mapSize { get; set; } = 100;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (mapConfig == null)
        {
            mapConfig = this;
        }
        else if (mapConfig != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddConfig(GameObjectType type, float removalCosts, float placementCosts)
    {
        var objectOfThatType = placeableObjectConfigs.FirstOrDefault(config => config.type.Equals(type));

        if (objectOfThatType != null)
        {
            objectOfThatType.removalCosts = removalCosts;
            objectOfThatType.placementCosts = placementCosts;
        }
        else
        {
            placeableObjectConfigs.Add(new ObjectConfig(type, removalCosts, placementCosts));
        }
    }
}
