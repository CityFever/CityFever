using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Application = Assets.AdminMap.Scripts.Application;

public class BrowseMapListMap : MonoBehaviour
{
    [SerializeField]
    private Text myText;

    [SerializeField]
    private Button mapButton;

    [SerializeField]
    private BrowseMapListControl mapButtonControl;

    public BrowseMapSelection uIManager;

    private string id;

    public string DatabaseId { get; set; }

    void Start()
    {
        mapButton = GetComponent<Button>();
    }


    public void SetText()
    {
        myText.text = "Map #" + id.ToString();
    }

    public void SetId(string id)
    {
        this.id = id;
    }

    public string GetId()
    {
        return id;
    }

    public void PassId() 
    {
        uIManager.SetMapId(id);
        Application.application.SelectedAdminMapId = DatabaseId;
        Debug.Log("Database Id: " + Application.application.SelectedAdminMapId);
    }
}
