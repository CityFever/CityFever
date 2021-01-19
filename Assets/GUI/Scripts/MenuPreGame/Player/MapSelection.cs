using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

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
        // some code here, maybe connected with the DB, then:
        PlayGame();
    }

    public void SetMapId(string id)
    {
        currentMapId = id;
    }

}
