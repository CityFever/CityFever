using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.GUI.Scripts.MapArea;
using UnityEngine;
using UnityEngine.EventSystems;
using Application = Assets.AdminMap.Scripts.Application;
using Random = UnityEngine.Random;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private ImageResolverList imageResolverList;

    private List<GameObject> buttons;

    void Start()
    {
        Debug.Log("BUTTON LIST CONTROL");
        foreach (var VARIABLE in imageResolverList.imageResolverItems)
        {
            Debug.Log(VARIABLE.Type);
        }

        buttons = new List<GameObject>();

        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        for (int i = 0; i < GetObjectsCount() - 1; i++)
        {
            GameObject button = Instantiate(buttonTemplate);
            GameObjectType type = GetEnumValue(i);
            button.SetActive(true);
            button.GetComponent<ButtonListButton>().SetText("Available");
            button.GetComponent<ButtonListButton>().ObjectType = type;
            button.GetComponent<ButtonListButton>().SetImage(GetSpriteFromResolver(type));
            button.GetComponent<ButtonListButton>().Id = i.ToString();
            buttons.Add(button);

            int placementCosts = 0;
            int removalCosts = 0;
            bool Available = false;

            if (!Application.application.SelectedGameObjectType.Equals(GameObjectType.Default))
            {
                placementCosts = (int)MapConfig.mapConfig.GetPlacementCosts(Application.application.SelectedGameObjectType);
                removalCosts = (int)MapConfig.mapConfig.GetRemovalCosts(Application.application.SelectedGameObjectType);
                Available = MapConfig.mapConfig.IsContained(Application.application.SelectedGameObjectType);
            }

            button.GetComponent<ButtonListButton>().Available = Available;
            button.GetComponent<ButtonListButton>().SetValues(placementCosts, removalCosts);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    public int GetObjectsCount() 
    { 
        return GetAllObjectTypes().Length;
    }

    private GameObjectType GetEnumValue(int index)
    {
        return (GameObjectType)GetAllObjectTypes().GetValue(index);
    }

    private Array GetAllObjectTypes()
    {
        return Enum.GetValues(typeof(GameObjectType));
    }

    public void SaveNewValues(int newPrice, int remCost, string buttonId)
    {
        foreach (GameObject button in buttons)
        {
            if (button.GetComponent<ButtonListButton>().Id == buttonId)
            {
                button.GetComponent<ButtonListButton>().SetValues(newPrice, remCost);
            }
        }
    }

    private Sprite GetSpriteFromResolver(GameObjectType configType)
    {
        var imageResolver = imageResolverList.imageResolverItems.FirstOrDefault(resolver => resolver.Type.Equals(configType));
        return imageResolver != null ? imageResolver.Sprite : null;
    }
}
