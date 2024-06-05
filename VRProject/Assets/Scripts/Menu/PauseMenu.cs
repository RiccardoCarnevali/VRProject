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
                if (settingsMenu.activeSelf)
                    Back();
                else
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
    }
    
    public void SettingsMenu(){
        settingsMenu.SetActive(true);
    }
    
    public void Resume(){
        CursorManager.HideCursor();
        Settings.pauseMenuOn = false;
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Back(){
        settingsMenu.SetActive(false);
    }

    public void BackToMainMenu(){
        SaveSystem.Save();
        Settings.pauseMenuOn = false;
        SceneManager.LoadScene("MainMenu");
    }
}
