using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Application = Assets.AdminMap.Scripts.Application;

public class ButtonListButtonPlay : MonoBehaviour
{
    [SerializeField]
    private Text myText;

    [SerializeField]
    private Button button;

    [SerializeField]
    private ButtonListControlPlay buttonControl;

    public UiManagerPlay uIManager;

    private string id;

    private int price;
    private int removalCost;

    public GameObjectType Type { get; set; } 
    private Sprite objectImage;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText()
    {
        myText.text = "Price: " + price.ToString() + "$" + Environment.NewLine + "Demolish: " + removalCost.ToString() + "$";
    }

    public void SetImage(Sprite img)
    {
        objectImage = img;
        button.GetComponent<Image>().sprite = objectImage;
    }

    public void SetId(string id)
    {
        this.id = id;
    }

    public void SetPrice(int price)
    {
        this.price = price;
    }

    public void SetRemovalCost(int removalCost)
    {
        this.removalCost = removalCost;
    }

    public void ShowPrice()
    {
        uIManager.setPriceLabel(price, id);
    }

    public string GetId()
    {
        return id;
    }

    public void SetObjectToBePlaced() 
    {
        Application.application.SelectedGameObjectType = Type;
        Debug.Log("Set global selected object to: " + Type);
    }
}
