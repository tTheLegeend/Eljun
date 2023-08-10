using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;     //Pause Menu GameObject, used to link to it to be capable to disable/enable it
    [SerializeField] private GameObject _GameOverMenu;     //Game Over GameObject, used to link to it to be capable to disable/enable it

    bool pause = false;

    public void Menu()
    {
        SceneManager.LoadScene(0);      //Load Main Menu if Pressed
        Time.timeScale = 1.0f;          //Unpause game
        _pauseMenu.SetActive(false);    //Disable Pause Menu 
        pause = false;
        print("Resume");
    }
    public void PauseButton()
    {
        Time.timeScale = 0f;            //Freeze game/Pause game
        _pauseMenu.SetActive(true);     //Enable Pause Menu 
        pause = true;
        print("Pause");
    }

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;          //Unpause Game
        _pauseMenu.SetActive(false);    //Enable Pause Menu 
        pause = false;
        print("Resume");
    }

    //Pasue Menu
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if(_GameOverMenu.activeSelf == false)
            {
                if (pause == true)
                {
                    ResumeButton();
                }
                else
                {
                    PauseButton();
                }
            }
        }
    }
}
