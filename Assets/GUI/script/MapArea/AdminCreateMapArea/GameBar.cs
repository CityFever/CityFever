using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameBar : MonoBehaviour
{
    bool inactive = false;
    bool demolish = false;
    bool grass = false;
    bool asphalt = false;
    bool water = false;
    bool item = false;

    private int budgetValue;

    private TMP_InputField budgetField;
    private Button saveBudgetButton;

    // Start is called before the first frame update
    void Start()
    {
        inactive = false;
        demolish = false;
        grass = true;
        asphalt = false;
        water = false;
        item = false;

        budgetField = transform.Find("SetBudgetBox").GetComponent<TMP_InputField>();
        saveBudgetButton = transform.Find("SaveBudgetButton").GetComponent<Button>();

        saveBudgetButton.interactable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (budgetField.text != "")
        {
            saveBudgetButton.interactable = true;
        }
        else { saveBudgetButton.interactable = false; }
    }

    void SetInactive()
    {
        inactive = true;
        demolish = false;
        grass = false;
        asphalt = false;
        water = false;
        item = false;

    }

    public void SetBudget()
    {
        budgetValue = Convert.ToInt32(budgetField.text);
        //now we have budget stored. Next step is passing it to the database or sth
    }
}
