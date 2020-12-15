using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemplateMapSelection : MonoBehaviour
{
    public void CreateMap()
    {
        SceneManager.LoadScene(5);
    }
}
