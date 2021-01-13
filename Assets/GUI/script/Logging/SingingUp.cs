using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SingingUp : MonoBehaviour
{
    private TMP_InputField loginField;
    private TMP_InputField passwordField;
    private TMP_InputField passwordRepeatField;
    private Text warning;
    private Button signUpButton;

    // Start is called before the first frame update
    void Start()
    {
        warning = transform.Find("Warning").GetComponent<Text>();
        loginField = transform.Find("LoginInput").GetComponent<TMP_InputField>();
        passwordField = transform.Find("PasswordInput").GetComponent<TMP_InputField>();
        passwordRepeatField = transform.Find("PasswordRepeatInput").GetComponent<TMP_InputField>();
        signUpButton = transform.Find("SignUpButton").GetComponent<Button>();
        signUpButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Login = " + loginField.text + " Pass = " + passwordField.text + " Repeated pass = " + passwordRepeatField.text);
        SetWarningMessage();
    }

    void SetWarningMessage()
    {
        if (string.IsNullOrEmpty(loginField.text) && string.IsNullOrEmpty(passwordField.text))
        {
            warning.text = "PROVIDE LOGIN AND PASSWORD. ";
            signUpButton.interactable = false;

        }
        else if (string.IsNullOrEmpty(loginField.text) && !string.IsNullOrEmpty(passwordField.text))
        {
            warning.text = "PROVIDE LOGIN. ";
            signUpButton.interactable = false;

        }
        else if (!string.IsNullOrEmpty(loginField.text) && string.IsNullOrEmpty(passwordField.text))
        {
            warning.text = "PROVIDE PASSWORD. ";
            signUpButton.interactable = false;

        }
        else if (!passwordRepeatField.text.Equals(passwordField.text))
        {
            warning.text = " PASSWORDS DO NOT MATCH";
            signUpButton.interactable = false;
        }
        else
        {
            warning.text = "";
            signUpButton.interactable = true;

        }
        
    }
    public void GoToAdminMenu()
    {
        SceneManager.LoadScene("AdminMenu");
    }

    public void GoToPlayerMenu()
    {
        SceneManager.LoadScene("PlayerMenu");
    }

    public void GoBackToLoginPage()
    {
        SceneManager.LoadScene("LogSign");
    }
}

