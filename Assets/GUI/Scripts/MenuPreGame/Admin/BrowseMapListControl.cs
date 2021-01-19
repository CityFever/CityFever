using System.Collections;
using System.Collections.Generic;
using Database;
using UnityEngine;
using Application = Assets.AdminMap.Scripts.Application;

public class BrowseMapListControl : MonoBehaviour
{
    [SerializeField] private GameObject mapButtonTemplate;

    private List<GameObject> mapButtons;
    private List<string> userMapIds = new List<string>();
    private List<string> adminMapIds = new List<string>();
    private List<MapConfig> maps = new List<MapConfig>();

    void Start()
    {
        Debug.Log("ADMIN MAP BrowseMapListControl");

        mapButtons = new List<GameObject>();

        if (mapButtons.Count > 0)
        {
            foreach (GameObject button in mapButtons)
            {
                Destroy(button.gameObject);
            }
            mapButtons.Clear();
        }
    }

    public void FetchData()
    {
        ClearData();
        FetchAdminMapIds();
    }

    private void FetchAdminMapIds()
    {
        UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password, () =>
            {
                Debug.Log("FetchAdminMapIds is started");

                MapsRepository.GetAllAdminMapIds((list) =>
                {
                    foreach (var mapId in list)
                    {
                        adminMapIds.Add(mapId);
                        Debug.Log(mapId);
                    }
                    InstantiateButtons(adminMapIds);
                });
                
            },
            () =>
            {
                Debug.Log("Too heavy load");
            });
    }

    public void InstantiateButtons(List<string> ids)
    {
        for (int i = 0; i < ids.Count; i++)
        {
            GameObject button = Instantiate(mapButtonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<BrowseMapListMap>().SetId(i.ToString());
            button.GetComponent<BrowseMapListMap>().DatabaseId = ids[i];
            mapButtons.Add(button);
            button.GetComponent<BrowseMapListMap>().SetText();
            button.transform.SetParent(mapButtonTemplate.transform.parent, false);
        }
    }

    public void OnEnable()
    {
        FetchData();
    }

    public void OnDisable()
    {
        ClearData();
    }

    public void ClearData()
    {
        maps.Clear();
    }
}
