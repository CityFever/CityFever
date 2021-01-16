using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorType : MonoBehaviour, IPointerClickHandler
{
    bool inactive = false;
    bool activate = false;
    bool demolish = false;
    bool grass = false;
    bool asphalt = false;
    bool water = false;
    bool item = false;
    int sliderH;
    int sliderW;

    void Start()
    {
        inactive = false;
        activate = false;
        demolish = false;
        grass = true;
        asphalt = false;
        water = false;
        item = false;
        sliderH = 1;
        sliderW = 1;
    }

    void Update()
    {  
       // Debug.Log("inactive: " + inactive + " demolish: " + demolish + " item: " + item + " water: " + water + " asphalt: " + asphalt + " grass: " + grass + " sliderH =" + sliderH);
    }

    public void Reset()
    {
        inactive = false;
        activate = false;
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

    public void SetActivate() //making tiles active for the player
    {
        Reset();
        activate = true;
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

    public void OnPointerClick(PointerEventData data)
    {
        // This will only execute if the objects collider was the first hit by the click's raycast
        Debug.Log(gameObject.name + ": I was clicked!");
    }
}
