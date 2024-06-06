using System.Collections;
using Unity.VisualScripting;
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


    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip cinematicBoomSfx;

    [SerializeField] private Image fadeImage;
    private float fadeTimeSeconds = 2f;

    void Start(){
        StartCoroutine(FadeIn());
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

    private IEnumerator FadeIn() {
        fadeImage.color = Color.black;
        fadeImage.raycastTarget = true;
        yield return new WaitForSeconds(2f);
        yield return Fade(Color.black, new Color(0, 0, 0, 0));
        fadeImage.raycastTarget = false;
    }

    private IEnumerator Play() {
        CursorManager.HideCursor();
        backgroundMusicAudioSource.Stop();
        sfxAudioSource.PlayOneShot(cinematicBoomSfx);
        fadeImage.raycastTarget = true;
        yield return new WaitForSeconds(2f);
        yield return Fade(new Color(0, 0, 0 ,0), Color.black);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator Fade(Color startColor, Color endColor) {
        float progress = 0;

        while (progress < fadeTimeSeconds) {
            progress += Time.deltaTime * Time.timeScale;
            // fadeImage.color = Color.Lerp(startColor, endColor, progress / fadeTimeSeconds);
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(startColor.a, endColor.a, progress / fadeTimeSeconds);
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.color = endColor;
    }

    public void NewGame() {

        if (SaveSystem.SaveDataExists()) {
            newGameConfirmationPopup.SetActive(true);
        }
        else {
            Settings.load = false;
            StartCoroutine(Play());
        }
    }

    public void ConfirmNewGame() {
        SaveSystem.DeleteSave();
        Settings.load = false;
        StartCoroutine(Play());
    }

    public void CancelNewGame() {
        newGameConfirmationPopup.SetActive(false);
    }

    public void Continue(){
        if (SaveSystem.Load()) {
            Settings.load = true;
            StartCoroutine(Play());
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
