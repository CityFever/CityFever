using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class GameBar : MonoBehaviour, IPointerClickHandler
{
    bool inactive = false;
    bool activate = false;
    bool demolish = false;
    bool grass = false;
    bool asphalt = false;
    bool water = false;
    bool item = false;


    private int budgetValue;
    private TMP_InputField budgetField;
    private Button saveBudgetButton;

    void Start()
    {
        inactive = false;
        activate = false;
        demolish = false;
        grass = true;
        asphalt = false;
        water = false;
        item = false;

        budgetField = transform.Find("SetBudgetBox").GetComponent<TMP_InputField>();
        saveBudgetButton = transform.Find("SaveBudgetButton").GetComponent<Button>();
        saveBudgetButton.interactable = false;
    }

    void Update()
    {
        saveBudgetButton.interactable = !String.IsNullOrWhiteSpace(budgetField.text);
    }

    void SetInactive()
    {
        inactive = true;
        activate = false;
        demolish = false;
        grass = false;
        asphalt = false;
        water = false;
        item = false;

    }

    public void SetBudget()
    {
        budgetValue = Convert.ToInt32(budgetField.text);
        MapConfig.mapConfig.mapBudget = budgetValue;
        Debug.Log("Set budget: " + MapConfig.mapConfig.mapBudget);
    }

    public void OnPointerClick(PointerEventData data)
    {
        // This will only execute if the objects collider was the first hit by the click's raycast
        Debug.Log(gameObject.name + ": I was clicked!");
    }
}
