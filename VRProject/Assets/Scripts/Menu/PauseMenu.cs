using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

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
    
    public void SettingsMenu(){
        settingsMenu.SetActive(true);
    }
    
    public void Resume(){
        CursorManager.HideCursor();
        Settings.pauseMenuOn = false;
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
    }

    public void Back(){
        settingsMenu.SetActive(false);
    }

    public void BackToMainMenu(){
        Settings.pauseMenuOn = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
