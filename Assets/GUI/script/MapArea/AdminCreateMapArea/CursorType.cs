using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorType : MonoBehaviour
{
    bool inactive = false;
    bool demolish = false;
    bool grass = false;
    bool asphalt = false;
    bool water = false;
    bool item = false;
    int sliderH;
    int sliderW;


    // Start is called before the first frame update
    void Start()
    {
        inactive = false;
        demolish = false;
        grass = true;
        asphalt = false;
        water = false;
        item = false;
        sliderH = 1;
        sliderW = 1;
    }

    // Update is called once per frame
    void Update()
    {  
        Debug.Log("inactive: " + inactive + " demolish: " + demolish + " item: " + item + " water: " + water + " asphalt: " + asphalt + " grass: " + grass + " sliderH =" + sliderH);
    }

    public void OnValueHChanged(float newValue)
    {
        sliderH = (int)newValue;
       Debug.Log(sliderW);
    }

    public void OnValueWChanged(float newValue)
    {
        sliderW = (int)newValue;
        Debug.Log(sliderW);
    }

    public void Reset()
    {
        inactive = false;
        demolish = false;
        grass = false;
        asphalt = false;
        water = false;
        item = false;

    }

    public void SetInactive()
    {
        Reset();
        inactive = true;
    }

    public void SetDemolish()
    {
        Reset();
        demolish = true;


    }

    public void SetGrass()
    {
        Reset();
        grass = true;
        

    }

    public void SetAsphalt()
    {
        Reset();
        asphalt = true;
      
    }


    public void SetWater()
    {
        Reset();
        water = true ; 
      
    }


    public void SetItem()
    {
        Reset();
        item = true;

    }


}
