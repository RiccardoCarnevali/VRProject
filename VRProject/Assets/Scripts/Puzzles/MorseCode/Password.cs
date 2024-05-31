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

    private Coroutine lastCoroutine = null;

    private bool won = false;

    [SerializeField] private Door door;

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
        yield return ShowResult("Access denied");
    }

    private IEnumerator Win() {
        won = true;
        passwordInput.enabled = false;
        resultText.color = Color.green;
        yield return ShowResult("Access granted");
        yield return new WaitForSeconds(1);
        door.Open();

        computer.DisableInteraction();
        Settings.inPuzzle = false;
        computerScreen.SetActive(false);
    }

    private IEnumerator ShowResult(string result) {
        resultText.text = "";
        foreach (char c in result) {
            resultText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
