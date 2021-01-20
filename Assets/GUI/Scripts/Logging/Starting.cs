using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void GoToLogIn()
    {
        SceneManager.LoadScene("LogIn");
    }

    public void GoToSignUp()
    {
        SceneManager.LoadScene("SignUp");
    }

}
