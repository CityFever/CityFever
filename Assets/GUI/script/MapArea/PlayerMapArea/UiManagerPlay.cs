﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UiManagerPlay : MonoBehaviour
{

    public ButtonListControlPlay buttonControl;

    private string currentButtonId;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPriceLabel(int price, string id)
    {
        currentButtonId = id;
    }

}