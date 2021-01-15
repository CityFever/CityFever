using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        mapButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

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
    }

}
