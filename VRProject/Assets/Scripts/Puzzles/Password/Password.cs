using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Password : MonoBehaviour
{
    private TMP_InputField passwordInput;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private string password;
    [SerializeField] private GameObject computerScreen;
    [SerializeField] private string loseText;
    [SerializeField] private string[] winText;

    private Coroutine lastCoroutine = null;

    private bool won = false;

    [SerializeField] private PasswordTarget target;

    [SerializeField] private Interactable computer;

    private void Start() {
        passwordInput = GetComponent<TMP_InputField>();
    }

    void Update()
    {

        if (won)
            return;

        //Keeps the input field always focused
        //https://forum.unity.com/threads/unity-4-6-ui-how-to-prevent-deselect-losing-focus-in-inputfield.272387/
        if(!passwordInput.isFocused)
        {
            EventSystem.current.SetSelectedGameObject(passwordInput.gameObject, null);
            passwordInput.OnPointerClick(new PointerEventData(EventSystem.current));
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Settings.inPuzzle = false;
            computerScreen.SetActive(false);
            return; 
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (lastCoroutine != null)
                StopCoroutine(lastCoroutine);

            if (passwordInput.text.ToLower() == password.ToLower())
                StartCoroutine(Win());
            else
                lastCoroutine = StartCoroutine(Lose());
        }
    }

    private IEnumerator Lose() {
        resultText.color = Color.red;
        resultText.text = "";
        yield return ShowResult(loseText);
    }

    private IEnumerator Win() {
        won = true;
        passwordInput.enabled = false;
        resultText.color = Color.green;
        resultText.text = "";
        foreach (string s in winText) {
            yield return ShowResult(s + "\n");
            yield return new WaitForSeconds(1f);
        }
        target.Unlock();

        computer.DisableInteraction();
        Settings.inPuzzle = false;
        computerScreen.SetActive(false);
    }

    private IEnumerator ShowResult(string result) {
        foreach (char c in result) {
            resultText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
