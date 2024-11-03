using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void StartGame()
    {

        SceneManager.LoadScene("MainScene");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
   
}
