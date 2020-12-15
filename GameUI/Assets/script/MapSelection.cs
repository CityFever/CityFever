using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelection : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayerGame");
    }
}
