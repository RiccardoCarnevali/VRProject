using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    void Start(){
        CursorManager.ShowCursor();
        //mainMenu.SetActive(true);
    }

    public void NewGame(){
        CursorManager.HideCursor();
        mainMenu.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    public void Continue(){
        //TODO
    }

    public void Settings(){
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);

    }

    public void Exit(){
        Application.Quit();
    }
}
