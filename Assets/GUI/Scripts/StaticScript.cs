using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//i needed this to enable passing the value of the btn pressed

public class StaticScript : MonoBehaviour
{

    static public string mapButton = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string btn = EventSystem.current.currentSelectedGameObject.name;
        if (btn == "GrassButton")
        {
            mapButton = "grass";
        }
        else if (btn == "GeneratedButton")
        {
            mapButton = "generated";
        }
    }
}
