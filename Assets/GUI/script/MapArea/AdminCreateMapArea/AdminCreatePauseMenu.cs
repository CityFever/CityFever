using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminCreatePauseMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("AdminMenu");

    }

    public void SaveMap()
    {
        Debug.Log("There will be some code to save the map using DB");
    }
}
