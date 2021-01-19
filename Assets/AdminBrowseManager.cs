using System.Collections;
using System.Collections.Generic;
using Database;
using UnityEngine;
using Application = Assets.AdminMap.Scripts.Application;

public class AdminBrowseManager : MonoBehaviour
{
    public string mapId { get; set; }
    void Start()
    {
        mapId = Application.application.SelectedAdminMapId;
        Debug.Log(Application.application.SelectedAdminMapId);

        FetcMapFromDatabase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FetcMapFromDatabase()
    {
        UsersRepository.Login("226435@edu.p.lodz.pl", "password", () => {
            MapsRepository.GetAdminMap(mapId, (map) =>
            {
                Debug.Log(map.mapBudget);
            });
        });
    }
}
