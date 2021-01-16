using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Logging : MonoBehaviour
{
    private TMP_InputField loginField;
    private TMP_InputField passwordField;
    private Text warning;
    private Button logInButton;

    // Start is called before the first frame update
    void Start()
    {
        warning = transform.Find("Warning").GetComponent<Text>();
        loginField = transform.Find("LoginInput").GetComponent<TMP_InputField>();
        passwordField = transform.Find("PasswordInput").GetComponent<TMP_InputField>();
        logInButton = transform.Find("LogInButton").GetComponent<Button>();
        logInButton.interactable = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Login = " + loginField.text + " Pass = " + passwordField.text);
        setWarningMessage();
    }

    void setWarningMessage()
    {
        warning.text = "";
        if (string.IsNullOrEmpty(loginField.text) && string.IsNullOrEmpty(passwordField.text))
        {
            warning.text = "PROVIDE LOGIN AND PASSWORD. ";
            logInButton.interactable = false;

        }
        else if (string.IsNullOrEmpty(loginField.text) && !string.IsNullOrEmpty(passwordField.text))
        {
            warning.text = "PROVIDE LOGIN. ";
            logInButton.interactable = false;

        }
        else if (!string.IsNullOrEmpty(loginField.text) && string.IsNullOrEmpty(passwordField.text))
        {
            warning.text = "PROVIDE PASSWORD. ";
            logInButton.interactable = false;

        }
        else
        {
            warning.text = "";
            logInButton.interactable = true;

        }
    }

    //now we invoke go to admin on Login button and go to player on Sign Up button. Need to check credentials - whether the player or admin and then call one of them
    public void goToAdminMenu()
    {
        SceneManager.LoadScene("AdminMenu");
    }

    public void goToPlayerMenu()
    {
        SceneManager.LoadScene("PlayerMenu");
    }
}
