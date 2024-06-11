using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    [SerializeField] private int[] solution;
    private int[] insertedDigits = new int[] {-1, -1, -1};
    private int currentDigitIndex = 0;

    [SerializeField] private TextMeshProUGUI[] placeholders;
    [SerializeField] private TextMeshProUGUI[] displayedDigits;

    private AudioSource audioSource;
    [SerializeField] private AudioClip digitInsertAudioClip;
    [SerializeField] private AudioClip correctCodeAudioClip;
    [SerializeField] private AudioClip wrongCodeAudioClip;

    [SerializeField] private Door door;

    [SerializeField] private Interactable[] keypadInteractables;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        if (Settings.load && SaveSystem.CheckFlag("keypad_puzzle_won")) {
            DisableInteraction();
        }
    }

    public void InsertDigit(int num) {

        if (currentDigitIndex == insertedDigits.Length)
            return;
        
        audioSource.PlayOneShot(digitInsertAudioClip);

        insertedDigits[currentDigitIndex] = num;
        displayedDigits[currentDigitIndex].text = num.ToString();
        displayedDigits[currentDigitIndex].enabled = true;
        placeholders[currentDigitIndex].enabled = false;
        ++currentDigitIndex;
    }

    public void Cancel() {
        for (int i = 0; i < insertedDigits.Length; ++i) {
            insertedDigits[i] = -1;
            displayedDigits[i].text = "";
            displayedDigits[i].enabled = false;
            placeholders[i].enabled = true;
        }
        currentDigitIndex = 0;
    }

    public void Submit() {
        for (int i = 0; i < solution.Length; ++i) {
            if (solution[i] != insertedDigits[i]) {
                audioSource.PlayOneShot(wrongCodeAudioClip, 0.5f);
                Cancel();
                return;
            }
        }

        StartCoroutine(Win());
    }

    private IEnumerator Win() {
        audioSource.PlayOneShot(correctCodeAudioClip);
        yield return new WaitForSeconds(1f);

        SaveSystem.SetFlag("keypad_puzzle_won");
        door.Unlock();

        DisableInteraction();
    }

    private void DisableInteraction() {
        GetComponent<HighlightEffect>().enabled = false;
        foreach (Interactable keypadInteractable in keypadInteractables) 
            keypadInteractable.DisableInteraction();
    }
}
