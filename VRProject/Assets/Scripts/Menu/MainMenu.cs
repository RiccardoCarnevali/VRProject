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
        CursorManager.HideCursor();
        SceneManager.LoadScene("MainScene");
    }

    public void Continue(){
        //TODO
    }

    public void Settings(){
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
