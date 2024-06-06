using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject newGameConfirmationPopup;
    [SerializeField] private GameObject continueFailedPopup;
    [SerializeField] private Button continueButton;

    void Start(){
        CursorManager.ShowCursor();

        if (!SaveSystem.SaveDataExists()) {
            continueButton.interactable = false;
            continueButton.image.color = new Color32(100, 100, 100, 255);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Back();
        }
    }

    public void NewGame() {

        if (SaveSystem.SaveDataExists()) {
            newGameConfirmationPopup.SetActive(true);
        }
        else {
            CursorManager.HideCursor();
            Settings.load = false;
            SceneManager.LoadScene("MainScene");
        }
    }

    public void ConfirmNewGame() {
        SaveSystem.DeleteSave();
        CursorManager.HideCursor();
        Settings.load = false;
        SceneManager.LoadScene("MainScene");
    }

    public void CancelNewGame() {
        newGameConfirmationPopup.SetActive(false);
    }

    public void Continue(){
        if (SaveSystem.Load()) {
            CursorManager.HideCursor();
            Settings.load = true;
            SceneManager.LoadScene("MainScene");
        }
        else {
            //Save data missing or corrupted
            continueFailedPopup.SetActive(true);
        }
    }

    public void CloseContinueFailedPopup() {
        continueFailedPopup.SetActive(false);
    }

    public void SettingsMenu(){
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ControlsMenu() {
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Back(){
        if (settingsMenu.activeSelf) {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (controlsMenu.activeSelf) {
            controlsMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
    }

    public void BackToSettings() {
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Exit(){
        Application.Quit();
    }
}
