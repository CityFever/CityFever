using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Application = Assets.AdminMap.Scripts.Application;

public class PlayerMapListMap : MonoBehaviour
{
    [SerializeField]
    private Text myText;

    [SerializeField]
    private Button mapButton;

    [SerializeField]
    private PlayerMapListControl mapButtonControl;

    public MapSelection uIManager;

    private string id;

    public string DatabaseId { get; set; }
    public MapConfig SelectedMapConfig { get; set; }

    // Start is called before the first frame update
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

    public void PassId() //called from button in the inspector
    {
        uIManager.SetMapId(id);
        Application.application.SelectedAdminMapId = DatabaseId;
        Debug.Log("Database Id: " + Application.application.SelectedAdminMapId);
    }
}
