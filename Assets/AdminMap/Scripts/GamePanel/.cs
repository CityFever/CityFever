using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceResolver 
{
    public Dictionary<GameObjectType, float> GameObjectPrice { get; set; }

    public PriceResolver()
    {
        GameObjectPrice = new Dictionary<GameObjectType, float>();
    }

    public float GetObjectPrice(GameObjectType type)
    {
        return GameObjectPrice[type];
    }

    public void SetPriceForType(GameObjectType type, float price)
    {
        if (GameObjectPrice.ContainsKey(type))
        {
            GameObjectPrice[type] = price;
        }
        else
        {
            GameObjectPrice.Add(type, price);
        }
    }
}
