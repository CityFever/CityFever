using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Application = Assets.AdminMap.Scripts.Application;


public class BrowseMapSelection : MonoBehaviour
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
        SceneManager.LoadScene("AdminBrowse");
    }

    public void LoadSelectedMap()
    {
        String mapId = Application.application.SelectedAdminMapId;
        Debug.Log("LoadSelectedMap BrowseMapSelection" + Application.application.SelectedAdminMapId);

        UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password, () => {
            Debug.Log("Started fetching a map");
            MapsRepository.GetAdminMap(mapId, (fetchedMap) =>
            {
                foreach (var tile in fetchedMap.tileConfigs)
                {
                    Debug.Log("Tile type: " + tile.type);
                }

                MapConfig.mapConfig.mapBudget = fetchedMap.mapBudget;
                MapConfig.mapConfig.tileConfigs = fetchedMap.tileConfigs;
                MapConfig.mapConfig.placeableObjectConfigs = fetchedMap.placeableObjectConfigs;
                OpenMap();
            });
        });
    }

    public void SetMapId(string id)
    {
        currentMapId = id;
    }
    
}
