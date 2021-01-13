using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Sliders : MonoBehaviour
{
    TextMeshProUGUI Val;
    Slider slider;


    void Start()
    {
        Val = GetComponent<TextMeshProUGUI>();

        slider = GetComponentInParent<Slider>();
        if (slider != null && Val != null)
            slider.onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(float value)
    {
        Val.text = value.ToString();
    }


}
