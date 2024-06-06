using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject controlsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Settings.pauseMenuOn) {
                Back();
            } else if (!Settings.paused){
                Pause();
            }
        }
    }

    public void Pause(){
        CursorManager.ShowCursor();
        Settings.pauseMenuOn = true;
        pauseMenu.SetActive(true);
    }
    
    public void SettingsMenu(){
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ControlsMenu() {
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
    
    public void Resume(){
        CursorManager.HideCursor();
        Settings.pauseMenuOn = false;
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Back(){
        if (settingsMenu.activeSelf) {
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
        }
        else if (controlsMenu.activeSelf) {
            controlsMenu.SetActive(false);
            settingsMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
        }
        else {
            Resume();
        }
    }

    public void BackToMainMenu(){
        SaveSystem.Save();
        Settings.pauseMenuOn = false;
        SceneManager.LoadScene("MainMenu");
    }
}
