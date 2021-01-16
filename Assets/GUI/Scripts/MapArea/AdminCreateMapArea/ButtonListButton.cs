using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Application = Assets.AdminMap.Scripts.Application;

public class ButtonListButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text myText;

    [SerializeField] private Button button;

    [SerializeField] private ButtonListControl buttonControl;

    public PriceUI priceUi;

    private int placementCosts;
    private int removalCosts;

    private Sprite objectImage;

    public string Id { get; set; }
    public bool Available { get; set; }
    public GameObjectType ObjectType { get; set; }

    public void SetText(string textString)
    {
        myText.text = textString;
    }

    public void SetImage(Sprite img)
    {
        objectImage = img;
        button.GetComponent<Image>().sprite = objectImage;
    }

    public void SetValues(int placementCosts, int removalCosts)
    {
        this.placementCosts = placementCosts;
        this.removalCosts = removalCosts;
    }

    public void ShowValues() 
    {
        priceUi.SetLabels(placementCosts, removalCosts, ObjectType.ToString());
        Application.application.SelectedGameObjectType = ObjectType;

        Debug.Log("Show values for: " + Application.application.SelectedGameObjectType);
        if (MapConfig.mapConfig.IsContained(ObjectType))
        {
            Debug.Log(ObjectType + " Placement costs: " + MapConfig.mapConfig.GetPlacementCosts(ObjectType) + ", removal: " + MapConfig.mapConfig.GetRemovalCosts(ObjectType));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount.Equals(2))
        {
            Available = !Available;
            SetText(Available ? "Available" : "Unavailable");
        }
    }
}
