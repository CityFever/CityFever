using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Application = Assets.AdminMap.Scripts.Application;

public class BrowseGameBar : MonoBehaviour
{
    private int budgetVal;
    private string tempVal;
    private int spentVal;

    private TMP_Text budgetText;
    private TMP_Text temperature;
    private TMP_Text moneySpent;

    // Start is called before the first frame update
    void Start()
    {
        budgetText = transform.Find("Budget").GetComponent<TMP_Text>();
        temperature = transform.Find("Temperature").GetComponent<TMP_Text>();
        moneySpent = transform.Find("Spent").GetComponent<TMP_Text>();

        LoadAll();
        ShowAll();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadBudget()
    { 
        //budgetVal = (int) Application.application.SelectedMapConfig.mapBudget; 
    }
    private void LoadTemperature()
    {
        tempVal = "25.4\u00B0C"; //there will be getting budget from the DB or sth 
    }
    private void LoadMoneySpent()
    {
        spentVal = 434; //there will be getting budget from the DB or sth 
    }

    private void ShowBudget()
    {
        budgetText.text = budgetVal.ToString();
    }

    private void ShowTemperature()
    {
        temperature.text = tempVal;
    }

    private void ShowMoneySpent()
    {
        moneySpent.text = spentVal.ToString();
    }

    private void ShowAll()
    {
        ShowBudget();
        ShowMoneySpent();
        ShowTemperature();
    }

    private void LoadAll()
    {
        LoadBudget();
        LoadMoneySpent();
        LoadTemperature();
    }

}
