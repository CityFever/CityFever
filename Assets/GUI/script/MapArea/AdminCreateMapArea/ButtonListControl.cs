using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Application = Assets.AdminMap.Scripts.Application;
using Random = UnityEngine.Random;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    private int numberOfButtons;

    private List<GameObject> buttons;

    public Sprite[] spriteImages; //add images in the inspector

    // Start is called before the first frame update
    void Start()
    {
        SetNumberOfButtons();

        buttons = new List<GameObject>();

        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.SetActive(true);

            button.GetComponent<ButtonListButton>().SetText("Available");
            button.GetComponent<ButtonListButton>().SetImage(spriteImages[i]);
            button.GetComponent<ButtonListButton>().Id = i.ToString();
            button.GetComponent<ButtonListButton>().ObjectType = GetEnumValue(i);
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

    public void SetNumberOfButtons() 
    {
        numberOfButtons = GetAllObjectTypes().Length;
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
}
