using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using Database;


public class Logging : MonoBehaviour
{
    private TMP_InputField emailField;
    private TMP_InputField passwordField;
    private Text warning;
    private Button logInButton;
    private const string emailPattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$";
    private const string emailAdminPattern = @"^((\w+)@[\w]{0,3}\.([\w]{1})\.[\w]{4}\.[\w]{2})$";
    bool credentialsVerified = false;
    bool error, lastError = false;
    string err = "";

    Starting startScreen;

    // Start is called before the first frame update
    void Start()
    {
        warning = transform.Find("Warning").GetComponent<Text>();
        emailField = transform.Find("EmailLogin").GetComponent<TMP_InputField>();
        passwordField = transform.Find("PasswordInput").GetComponent<TMP_InputField>();
        logInButton = transform.Find("LogInButton").GetComponent<Button>();
        logInButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchField();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LogInBtn();
        }

        //Debug.Log("Login = " + emailField.text + " Pass = " + passwordField.text);
        SetWarningMessage();
    }

    private void SwitchField()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (emailField.isFocused)
            {
                passwordField.Select();
            }
            if (passwordField.isFocused)
            {
                emailField.Select();
            }
        }
    }

    void SetWarningMessage()
    {
        warning.text = "";

        if (string.IsNullOrEmpty(emailField.text) && string.IsNullOrEmpty(passwordField.text))
        {
            warning.text = "PROVIDE E-MAIL AND PASSWORD. ";
            logInButton.interactable = false;
            credentialsVerified = false;
        }
        else if (string.IsNullOrEmpty(emailField.text) && !string.IsNullOrEmpty(passwordField.text))
        {
            warning.text = "PROVIDE E-MAIL. ";
            logInButton.interactable = false;
            credentialsVerified = false;
        }
        else if (!string.IsNullOrEmpty(emailField.text) && string.IsNullOrEmpty(passwordField.text))
        {
            warning.text = "PROVIDE PASSWORD. ";
            logInButton.interactable = false;
            credentialsVerified = false;
        }
        else if (!EmailValidation())
        {
            warning.text = "INCORRECT E-MAIL FORMAT. ";
            logInButton.interactable = false;
            credentialsVerified = false;
        }
        else if (passwordField.text.Length < 6)
        {
            warning.text = "PASSWORD MUST BE AT LEAST 6 CHARACTERS";
            logInButton.interactable = false;
            credentialsVerified = false;
        }  
        else
        {
            warning.text = "";
            logInButton.interactable = true;
            credentialsVerified = true;
        }

        if (error)
        {
            warning.text = "INVALID E-MAIL OR PASSWORD";

            if (emailField.isFocused || passwordField.isFocused)
            {
                error = false;
                warning.text = "";
            }
        }
    }

    public void LogInBtn()
    {
        if (credentialsVerified)
        {
            UsersRepository.Login(emailField.text, passwordField.text,
                () => {
                    SetLoginError(false);
                    UserSingleton.Instance.Email = emailField.text;
                    UserSingleton.Instance.Password = passwordField.text;
                    Redirect();
                },
                () => {
                    SetLoginError(true);
            });
        }
    }

    private void SetLoginError(bool err)
    {
        error = err;
    }

    private void Redirect()
    {
        //SceneManager.LoadScene("AdminMenu");

        Regex r = new Regex(emailAdminPattern);
        if (r.IsMatch(emailField.text))
        {
            SceneManager.LoadScene("AdminMenu");
        }
        else
        {
            SceneManager.LoadScene("PlayerMenu");
        }
        
    }

    private bool EmailValidation()
    {
        Regex r = new Regex(emailPattern);
        if (r.IsMatch(emailField.text))
        {
            return true;
        }
        return false;
    }

    public void GoBackToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
