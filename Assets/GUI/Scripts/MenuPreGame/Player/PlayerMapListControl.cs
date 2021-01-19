using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;

public class PlayerMapListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject mapButtonTemplate;

    private int numberOfMaps;

    private List<GameObject> mapButtons;

    private List<string> mapConfigIds = new List<string>();
    private List<MapConfig> maps = new List<MapConfig>();

    // Start is called before the first frame update
    void Start()
    {
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
        UsersRepository.Login(UserSingleton.Instance.Email, UserSingleton.Instance.Password, () =>
        {
            Debug.Log("start");

            MapsRepository.GetAllAdminMapIds((list) =>
            {
                foreach (var mapId in list)
                {
                    mapConfigIds.Add(mapId);
                    Debug.Log(mapId);
                }

                for (int i = 0; i < mapConfigIds.Count; i++)
                {
                    GameObject button = Instantiate(mapButtonTemplate) as GameObject;
                    button.SetActive(true);
                    button.GetComponent<PlayerMapListMap>().SetId(i.ToString());
                    button.GetComponent<PlayerMapListMap>().DatabaseId = mapConfigIds[i];
                    //button.GetComponent<PlayerMapListMap>().SelectedMapConfig = maps[i];
                    mapButtons.Add(button);
                    button.GetComponent<PlayerMapListMap>().SetText();
                    button.transform.SetParent(mapButtonTemplate.transform.parent, false);
                }
            });
        },
            () =>
            {
                Debug.Log("Too heavy load");
            });
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

    
