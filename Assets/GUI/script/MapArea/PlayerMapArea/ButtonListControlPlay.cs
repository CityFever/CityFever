using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControlPlay : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    private int numberOfButtons;

    private List<GameObject> buttons;

    public Sprite[] spriteImages; //add images in the inspector

    // Start is called before the first frame update
    void Start()
    {
        SetNumberOfButtons();

        buttons = new List<GameObject>();

        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        for (int i = 1; i <= numberOfButtons; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<ButtonListButtonPlay>().SetId(i.ToString()); //lets set the id as the order
            button.GetComponent<ButtonListButtonPlay>().SetImage(spriteImages[i - 1]); //lets set the id as the order


            buttons.Add(button);
            //lets just set some random prices for now, normally load from DB
            int rInt = Random.Range(0, 100);
            button.GetComponent<ButtonListButtonPlay>().SetPrice(rInt);
            int sInt = Random.Range(0, 100);
            button.GetComponent<ButtonListButtonPlay>().SetRemovalCost(sInt);
            button.GetComponent<ButtonListButtonPlay>().SetText();

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumberOfButtons() //edit accordingly to the actual amount
    {
        numberOfButtons = 6;
    }

}
