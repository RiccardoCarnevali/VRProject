using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject warningPanel;

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
        if (warningPanel.activeSelf) {
            warningPanel.SetActive(false);
        }
        CursorManager.HideCursor();
        Settings.pauseMenuOn = false;
        pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
    }

    public void BackToMainMenu(){
        warningPanel.SetActive(true);
    }

    public void BackToMainMenuConfirmation(){
        SceneManager.LoadScene("MainMenu");
    }
}
