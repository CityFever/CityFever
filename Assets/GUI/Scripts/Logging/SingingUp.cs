using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Database;

public class SingingUp : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField confPassword;
    private Text warning;
    private Button signUpButton;
    private const string emailPattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$";
    bool credentialsVerified = false;

    // Start is called before the first frame update
    void Start()
    {
        email = transform.Find("EmailSignUp").GetComponent<TMP_InputField>();
        password = transform.Find("PasswordSignUp").GetComponent<TMP_InputField>();
        confPassword = transform.Find("PasswordRepeat").GetComponent<TMP_InputField>();
        warning = transform.Find("Warning").GetComponent<Text>();
        signUpButton = transform.Find("SignUpButton").GetComponent<Button>();
        signUpButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchField();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SignUpBtn();
        }

        //Debug.Log("E-mail = " + email.text + " Pass = " + password.text + " Repeated pass = " + confPassword.text);
        SetWarningMessage();
    }
    
    private void SwitchField()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (email.isFocused)
            {
                password.Select();
            }
            if (password.isFocused)
            {
                confPassword.Select();
            }
            if (confPassword.isFocused)
            {
                email.Select();
            }
        }
    }

    private void SetWarningMessage()
    {
        if (string.IsNullOrEmpty(email.text) && string.IsNullOrEmpty(password.text))
        {
            warning.text = "PROVIDE E-MAIL AND PASSWORD. ";
            signUpButton.interactable = false;
            credentialsVerified = false;
        }
        else if (string.IsNullOrEmpty(email.text) && !string.IsNullOrEmpty(password.text))
        {
            warning.text = "PROVIDE E-MAIL. ";
            signUpButton.interactable = false;
            credentialsVerified = false;
        }
        else if (!string.IsNullOrEmpty(email.text) && string.IsNullOrEmpty(password.text))
        {
            warning.text = "PROVIDE PASSWORD. ";
            signUpButton.interactable = false;
            credentialsVerified = false;
        }
        else if (!confPassword.text.Equals(password.text))
        {
            warning.text = "PASSWORDS DO NOT MATCH. ";
            signUpButton.interactable = false;
            credentialsVerified = false;
        }
        else if (!EmailValidation()) 
        {
            warning.text = "INCORRECT E-MAIL FORMAT. ";
            signUpButton.interactable = false;
            credentialsVerified = false;
        } 
        else if (password.text.Length < 6)
        {
            warning.text = "PASSWORD MUST BE AT LEAST 6 CHARACTERS";
            signUpButton.interactable = false;
            credentialsVerified = false;
        }
        else
        {
            warning.text = "";
            signUpButton.interactable = true;
            credentialsVerified = true;
        }
    }

    public void SignUpBtn()
    {
        if (credentialsVerified)
        {
            //register in db
            Debug.Log("registering");
            UsersRepository.Register(email.text, password.text);
            //SceneManager.LoadScene("LogSign");
        }
    }

    private bool EmailValidation()
    {
        Regex r = new Regex(emailPattern);
        if (r.IsMatch(email.text))
        {
            return true;
        }
        return false;
    }

    public void GoBackToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    /*
    public void GoToAdminMenu()
    {
        SceneManager.LoadScene("AdminMenu");
    }

    public void GoToPlayerMenu()
    {
        SceneManager.LoadScene("PlayerMenu");
    }*/

}

