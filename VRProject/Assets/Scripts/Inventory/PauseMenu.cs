using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Settings.pauseMenuOn) {
                Resume();
            } else if (!Settings.paused){
                Pause();
            }
        }
    }

    public void Pause(){
        CursorManager.ShowCursor();
        Settings.pauseMenuOn = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Resume(){
        CursorManager.HideCursor();
        Settings.pauseMenuOn = false;
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
    }

    public void Exit(){
        Application.Quit();
    }
}
