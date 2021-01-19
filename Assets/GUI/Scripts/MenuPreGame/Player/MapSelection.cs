using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using Database;
using Application = Assets.AdminMap.Scripts.Application;

public class MapSelection : MonoBehaviour
{

    private string currentMapId;
    private Button playButton;


    // Start is called before the first frame update
    void Start()
    {
        currentMapId = "";
        playButton = transform.Find("GoButton").GetComponent<Button>();
        playButton.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Selected map id is: " + currentMapId);
        if (string.IsNullOrEmpty(currentMapId))
        {
            playButton.gameObject.SetActive(false);
        } else
        {
            playButton.gameObject.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayerGame");
    }

    public void LoadSelectedMap()
    {


        UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password, () =>
        {
            Debug.Log("from user list selected map id " + Assets.AdminMap.Scripts.Application.application.SelectedAdminMapId);
            MapsRepository.GetAdminMap(Application.application.SelectedAdminMapId, (mapConfig) =>
            {
                Debug.Log("fetched " + mapConfig.DatabaseId + " with " + mapConfig.tileConfigs.Count + " tiles and " + mapConfig.placeableObjectConfigs.Count + " objects");

                MapConfig.mapConfig.mapBudget = mapConfig.mapBudget;
                MapConfig.mapConfig.placeableObjectConfigs = mapConfig.placeableObjectConfigs;
                MapConfig.mapConfig.tileConfigs = mapConfig.tileConfigs;

                foreach (var objectConfig in mapConfig.placeableObjectConfigs)
                {
                    Debug.Log("available" + objectConfig.type + ", " + objectConfig.placementCosts);
                }

                foreach (var config in MapConfig.mapConfig.placeableObjectConfigs)
                {
                    Debug.Log("available after assignment" + config.type + ", " + config.placementCosts);
                }

                PlayGame();

                //map = Instantiate(mapPrefab, transform).Initialize(100);

                /* foreach (var tile in mapConfig.tileConfigs)
                 {
                     UnityObject configPrefab = null;

                     if (!tile.ObjectType.Equals(GameObjectType.Default))
                     {
                         configPrefab = prefabs.FirstOrDefault(prefab => prefab.Type().Equals(tile.ObjectType));
                     }
                     map.CreateTilesFromConfiguration(tile, configPrefab);
                 }*/
            });
        });


        // some code here, maybe connected with the DB, then:
    }

    public void SetMapId(string id)
    {
        currentMapId = id;
    }

}
