using System.Collections;
using System.Collections.Generic;
using Database;
using UnityEngine;

public class BrowseMapListControl : MonoBehaviour
{
    [SerializeField] private GameObject mapButtonTemplate;

    private int numberOfMaps;

    private List<GameObject> mapButtons;

    private List<string> mapConfigIds = new List<string>();

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


        UsersRepository.Login("226435@edu.p.lodz.pl", "password", () =>
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
                    button.GetComponent<BrowseMapListMap>().SetId(i.ToString());
                    button.GetComponent<BrowseMapListMap>().DatabaseId = mapConfigIds[i];
                    mapButtons.Add(button);
                    button.GetComponent<BrowseMapListMap>().SetText();
                    button.transform.SetParent(mapButtonTemplate.transform.parent, false);
                }
            });
        });
    }
}
