using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Pause(){
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    
    public void Resume(){
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Exit(){
        Application.Quit();
    }
}
