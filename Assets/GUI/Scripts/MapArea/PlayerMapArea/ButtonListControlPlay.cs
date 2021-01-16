using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Assets.GUI.Scripts.MapArea;
using UnityEngine;
using Application = Assets.AdminMap.Scripts.Application;

public class ButtonListControlPlay : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    private int numberOfButtons;

    private List<GameObject> buttons;

    public Sprite[] spriteImages;

    [SerializeField] private ImageResolverList imageResolverList;

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

        Debug.Log("IMAGE RESOLVER LIST " + imageResolverList.imageResolverItems.Count);
        foreach (var resolver in imageResolverList.imageResolverItems)
        {
            Debug.Log(resolver.Type);
        }

        foreach (var config in MapConfig.mapConfig.placeableObjectConfigs)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.SetActive(true);
            button.GetComponent<ButtonListButtonPlay>().Type = config.type;
            button.GetComponent<ButtonListButtonPlay>().SetPrice((int)config.placementCosts);
            button.GetComponent<ButtonListButtonPlay>().SetRemovalCost((int)config.removalCosts);
            button.GetComponent<ButtonListButtonPlay>().SetText();
            button.GetComponent<ButtonListButtonPlay>().SetImage(GetSpriteFromResolver(config.type));
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    private Sprite GetSpriteFromResolver(GameObjectType configType)
    {
        var imageResolver = imageResolverList.imageResolverItems.FirstOrDefault(resolver => resolver.Type.Equals(configType));
        return imageResolver != null ? imageResolver.Sprite : null;
    }

    public void SetNumberOfButtons() 
    {
        numberOfButtons = MapConfig.mapConfig.placeableObjectConfigs.Count;
    }
}
