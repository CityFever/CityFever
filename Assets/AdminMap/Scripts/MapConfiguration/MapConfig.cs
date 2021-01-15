using Assets.AdminMap.Scripts.MapConfiguration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapConfig : MonoBehaviour
{
    public static MapConfig mapConfig;

    public List<TileConfig> tileConfigs = new List<TileConfig>();

    public List<ObjectConfig> placeableObjectConfigs = new List<ObjectConfig>();
    
    public float mapBudget;
    public int mapSize = 100;

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
            Debug.Log("GameObjecType: " + type.ToString() + ", Placement costs changed: " + placementCosts + ", Removal costs changed: " + removalCosts);
        }
        else
        {
            placeableObjectConfigs.Add(new ObjectConfig(type, removalCosts, placementCosts));
            Debug.Log("GameObjecType: " + type.ToString() + ", Placement costs: " + placementCosts + ", Removal costs: " + removalCosts);
        }
    }

    public void RemoveConfig(GameObjectType type)
    {
        var objectOfThatType = placeableObjectConfigs.FirstOrDefault(config => config.type.Equals(type));

        if (objectOfThatType != null)
        {
            placeableObjectConfigs.Remove(objectOfThatType);
            Debug.Log("GameObjecType " + type.ToString() + " was removed from the config list.");
        }
        else
        {            
            Debug.Log("GameObjecType " + type.ToString() + " is not accessible in the config list");
        }
    }

    public bool IsContained(GameObjectType type)
    {
        return placeableObjectConfigs.FirstOrDefault(config => config.type.Equals(type)) != null;
    }

    public float GetPlacementCosts(GameObjectType type)
    {
        var costs = placeableObjectConfigs.FirstOrDefault(config => config.type.Equals(type)).placementCosts;
        return costs;
    }
    public float GetRemovalCosts(GameObjectType type)
    {
        var costs = placeableObjectConfigs.FirstOrDefault(config => config.type.Equals(type)).removalCosts;
        return costs;
    }
}
