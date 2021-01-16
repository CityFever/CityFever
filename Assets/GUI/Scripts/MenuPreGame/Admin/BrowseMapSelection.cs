using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
        // some code here, maybe connected with the DB, then:
        OpenMap();
    }

    public void SetMapId(string id)
    {
        currentMapId = id;
    }
    
}
