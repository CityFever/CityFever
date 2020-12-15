using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrowseMapSelection : MonoBehaviour
{
    public void OpenMap()
    {
        SceneManager.LoadScene("AdminBrowse");
    }
}
