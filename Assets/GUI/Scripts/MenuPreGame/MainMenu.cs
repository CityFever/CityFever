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
        SceneManager.LoadScene("LogSign"); 
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
        //MapConfig map = new MapConfig();
        //UsersRepository.Login("224089@edu.p.lodz.pl", "admin13", () => {
        //    Debug.Log("start");
        //    MapsRepository.GetAllUsersMaps((list) => { Debug.Log(list.Count); });
        //});
    }
}
