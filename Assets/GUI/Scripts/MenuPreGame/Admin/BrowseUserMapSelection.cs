using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Database;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Application = Assets.AdminMap.Scripts.Application;


public class BrowseUserMapSelection : MonoBehaviour
{
    private string currentMapId;
    private Button viewMapButton;


    // Start is called before the first frame update
    void Start()
    {
        currentMapId = "";
        viewMapButton = transform.Find("LoadButton").GetComponent<Button>();
        viewMapButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Selected map id is: " + currentMapId);
        if (string.IsNullOrEmpty(currentMapId))
        {
            viewMapButton.gameObject.SetActive(false);
        }
        else
        {
            viewMapButton.gameObject.SetActive(true);
        }
    }

    public void OpenMap()
    {
        string mapId = Application.application.SelectedUserMapId;
        Debug.Log("LoadSelectedMap BrowseUserMapSelection" +  Application.application.SelectedUserMapId);

        MapConfig.mapConfig.mapBudget = Application.application.MapConfig.mapBudget;
        MapConfig.mapConfig.tileConfigs = Application.application.MapConfig.tileConfigs;
        MapConfig.mapConfig.placeableObjectConfigs = Application.application.MapConfig.placeableObjectConfigs;
        SceneManager.LoadScene("AdminBrowse");
    }

    public void LoadSelectedMap()
    {
        OpenMap();
    }

    public void SetMapId(string id)
    {
        currentMapId = id;
    }
    
}
