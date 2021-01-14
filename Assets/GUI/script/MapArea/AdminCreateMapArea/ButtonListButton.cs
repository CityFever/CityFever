using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Text myText;

    [SerializeField]
    private Button button;

    [SerializeField]
    private ButtonListControl buttonControl;

    public PriceUI priceUi;

    private string id;

    private int price;
    private int removalCost;

    private Sprite objectImage;

    private bool available;


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string textString)
    {
        myText.text = textString;
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

    public void SetValues(int price, int removalCost)
    {
        this.price = price;
        this.removalCost = removalCost;
    }

    public void ShowValues() //called from button in the inspector
    {
        priceUi.SetLabels(price, removalCost, id);
    }

    public string GetId()
    {
        return id;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Data" + eventData.clickCount);

        if (eventData.clickCount == 2)
        {
            Debug.Log("Click Data Double click");

            if (available)
            {
                available = false;
                SetText("Unavailable");
            }
            else
            {
                available = true; 
                SetText("Available");
            }
        }
    }
}
