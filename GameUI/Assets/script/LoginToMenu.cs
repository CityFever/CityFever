using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LoginToMenu : MonoBehaviour
{
    public void goToAdminMenu()
    {
        SceneManager.LoadScene("AdminMenu");
    }

    public void goToPlayerMenu()
    {
        SceneManager.LoadScene("PlayerMenu");
    }


}
