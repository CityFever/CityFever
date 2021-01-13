using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButtonPlay : MonoBehaviour
{
    [SerializeField]
    private Text myText;

    [SerializeField]
    private Button button;

    [SerializeField]
    private ButtonListControlPlay buttonControl;

    public UiManagerPlay uIManager;

    private string id;

    private int price;

    private Sprite objectImage;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText()
    {
        myText.text = price.ToString()+"$";
    }

    public void SetImage(Sprite img)
    {
        objectImage = img;
        button.GetComponent<Image>().sprite = objectImage;
    }

    public void SetId(string id)
    {
        this.id = id;
    }

    public void SetPrice(int price)
    {
        this.price = price;
    }

    public void ShowPrice()
    {
        uIManager.setPriceLabel(price, id);
    }

    public string GetId()
    {
        return id;
    }

    public void SetObjectToBePlaced() //method invoked when object on the list is clicked
    {
        // currentButtonId stores the id of the currently selected object
    }
}
