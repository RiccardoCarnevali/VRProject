using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Settings.pauseMenuOn) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Pause(){
        Settings.pauseMenuOn = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Resume(){
        Settings.pauseMenuOn = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit(){
        Application.Quit();
    }
}
