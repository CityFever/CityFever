using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameBarPlay : MonoBehaviour
{//player

    private int budgetVal;
    private string tempVal;

    private TMP_Text budgetText;
    private TMP_Text tValue;

    bool demolishModeActive;

    // Start is called before the first frame update
    void Start()
    {
        demolishModeActive = false;

        budgetText = transform.Find("Budget").GetComponent<TMP_Text>();
        tValue = GameObject.Find("TemperatureButton").GetComponentInChildren<TMP_Text>();

        LoadAll();
        ShowAll();
    }

    // Update is called once per frame
    void Update()
    {
        LoadBudget();
        ShowBudget();
    }

    public void CalculateTemperature()
    {
        tempVal = "25.4\u00B0C"; //it needs to be calculated
        ShowTemperature();
    }

    private void LoadBudget()
    {
        budgetVal = (int) MapConfig.mapConfig.mapBudget;
    }

    private void ShowBudget()
    {
        budgetText.text = budgetVal.ToString();
    }

    private void ShowTemperature()
    {
        tValue.text = tempVal;
    }

    private void ShowAll()
    {
        ShowBudget();
    }

    private void LoadAll()
    {
        LoadBudget();
    }

    public void DemolishOn()
    {
        demolishModeActive = true;
    }

    public void DemolishOff()
    {
        demolishModeActive = false;
    }

}
