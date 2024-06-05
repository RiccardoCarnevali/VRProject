using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    void Start(){
        CursorManager.ShowCursor();
    }

    public void NewGame(){
        SaveSystem.DeleteSave();
        CursorManager.HideCursor();
        Settings.load = false;
        SceneManager.LoadScene("MainScene");
    }

    public void Continue(){
        if (SaveSystem.Load()) {
            CursorManager.HideCursor();
            Settings.load = true;
            SceneManager.LoadScene("MainScene");
        }
        else {
            //No save file found
        }
    }

    public void SettingsMenu(){
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);

    }

    public void Back(){
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Exit(){
        Application.Quit();
    }
}
