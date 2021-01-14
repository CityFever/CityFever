using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PriceUI : MonoBehaviour
{

    private TMP_Text priceLabel;
    private TMP_InputField newPrice;

    private TMP_Text removalCostLabel;
    private TMP_InputField removalCost;

    private TMP_Text selectedObjectLabel;

    public ButtonListControl buttonControl;

    private string currentButtonId;
    private int newPriceInt;

    private int removalCostInt;
    private Button placeButton;
    private Button changePriceBtn;

    void Start()
    {
        priceLabel = transform.Find("PriceLabel").GetComponent<TMP_Text>();
        newPrice = transform.Find("PriceInputField").GetComponent<TMP_InputField>();

        removalCostLabel = transform.Find("RemovalCostLabel").GetComponent<TMP_Text>();
        removalCost = transform.Find("RemovalCostInputField").GetComponent<TMP_InputField>();

        selectedObjectLabel = transform.Find("SelectedObjectLabel").GetComponent<TMP_Text>();

        currentButtonId = "";
        placeButton = transform.Find("PlaceObjectButton").GetComponent<Button>();
        placeButton.gameObject.SetActive(false);
        changePriceBtn = transform.Find("ChangePriceButton").GetComponent<Button>();
        changePriceBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 
        Debug.Log("Selected map id is: " + currentButtonId);
        if (string.IsNullOrEmpty(currentButtonId))
        {
            placeButton.gameObject.SetActive(false);
            changePriceBtn.gameObject.SetActive(false);
        }
        else
        {
            placeButton.gameObject.SetActive(true);
            changePriceBtn.gameObject.SetActive(true);
        }

        if (string.IsNullOrEmpty(newPrice.text) || string.IsNullOrEmpty(removalCost.text) || string.IsNullOrEmpty(currentButtonId))
        {
            changePriceBtn.gameObject.SetActive(false);
        }
        else
        {
            changePriceBtn.gameObject.SetActive(true);
        }
    }

    public void SetLabels(int price, int remCost, string id)
    {
        currentButtonId = id;
        priceLabel.text = "Placement costs " + price.ToString();
        removalCostLabel.text = "Removal cost: " + remCost.ToString();
        selectedObjectLabel.text = id;
    }

    public void SetNewValues()
    {
        newPriceInt = Convert.ToInt32(newPrice.text);
        removalCostInt = Convert.ToInt32(removalCost.text);
        buttonControl.SaveNewValues(newPriceInt, removalCostInt, currentButtonId);
        SetLabels(newPriceInt, removalCostInt, currentButtonId);
        newPrice.text = "";
        removalCost.text = "";
    }

    public void SetObjectToBePlaced() //method invoked when clicking place object button
    {
        // currentButtonId stores the id of the currently selected object
    }
}
