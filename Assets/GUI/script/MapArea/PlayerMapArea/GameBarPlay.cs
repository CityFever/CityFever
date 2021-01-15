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
    private int spentVal;

    private TMP_Text budgetText;
    private TMP_Text tValue;
    private TMP_Text moneySpent;

    // Start is called before the first frame update
    void Start()
    {
        budgetText = transform.Find("Budget").GetComponent<TMP_Text>();
        moneySpent = transform.Find("Spent").GetComponent<TMP_Text>();
        tValue = GameObject.Find("TemperatureButton").GetComponentInChildren<TMP_Text>();

        LoadAll();
        ShowAll();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //those methods need to be changed and linked with logic, for example to change budget and spent money
    //as the player will be adding new objects

    public void CalculateTemperature()
    {
        tempVal = "25.4\u00B0C"; //it needs to be calculated
        ShowTemperature();
    }

    private void LoadBudget()
    {
        budgetVal = 1200; //there will be getting budget from the DB or sth 
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
        tValue.text = tempVal;
    }

    private void ShowMoneySpent()
    {
        moneySpent.text = spentVal.ToString();
    }

    private void ShowAll()
    {
        ShowBudget();
        ShowMoneySpent();
    }

    private void LoadAll()
    {
        LoadBudget();
        LoadMoneySpent();
    }

}
