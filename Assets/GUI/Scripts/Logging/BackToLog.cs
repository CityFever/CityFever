using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToLog : MonoBehaviour
{
    public void goToLog()
    {
        SceneManager.LoadScene("LogSign");
    }
}
