using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public void LoadNextScene() //Function to load respective level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //Load Next Scene
        Time.timeScale = 1.0f;
    }

    public void LoadMainMenu()    //Function to load main menu
    {
        SceneManager.LoadScene("MainMenu"); //Load main menu
    }
}