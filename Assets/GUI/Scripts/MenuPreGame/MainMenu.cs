using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Database;

public class MainMenu : MonoBehaviour
{

    public void LogOut()
    { //code to log out
        Debug.Log("LogOut");
        UsersRepository.Logout();
        SceneManager.LoadScene("LogIn"); 
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
