using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseMapListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject mapButtonTemplate;

    private int numberOfMaps;

    private List<GameObject> mapButtons;

    // Start is called before the first frame update
    void Start()
    {
        SetNumberOfMaps();

        mapButtons = new List<GameObject>();

        if (mapButtons.Count > 0)
        {
            foreach (GameObject button in mapButtons)
            {
                Destroy(button.gameObject);
            }
            mapButtons.Clear();
        }

        for (int i = 1; i <= numberOfMaps; i++)
        {
            GameObject button = Instantiate(mapButtonTemplate) as GameObject;
            button.SetActive(true);

            //now we just set id as the order. There it needs to read ids from the DB
            button.GetComponent<BrowseMapListMap>().SetId(i.ToString());

            mapButtons.Add(button);

            button.GetComponent<BrowseMapListMap>().SetText();

            button.transform.SetParent(mapButtonTemplate.transform.parent, false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumberOfMaps() //need to get no. of maps from the DB
    {
        numberOfMaps = 6;
    }
}
