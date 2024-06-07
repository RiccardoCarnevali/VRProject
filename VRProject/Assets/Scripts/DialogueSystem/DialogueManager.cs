using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueArea;
    [SerializeField] private TextMeshProUGUI speakerLabel;
    [SerializeField] private TextMeshProUGUI dialogueTextArea;

    private float textDelay;

    private static DialogueManager instance = null;

    private Queue<string> sentences;

    private bool sentenceEnded = false;

    public static DialogueManager Instance() {
        if (instance == null) 
            instance = FindObjectOfType<DialogueManager>();
        return instance;
    }

    private void Start() {
        sentences = new Queue<string>();

        textDelay = 1f / PlayerPrefs.GetFloat("text_speed", Settings.defaultTextSpeed);
        Messenger<float>.AddListener(MessageEvents.TEXT_SPEED_CHANGED, OnTextSpeedChanged);
    }

    private void OnDestroy() {
        Messenger<float>.RemoveListener(MessageEvents.TEXT_SPEED_CHANGED, OnTextSpeedChanged);
    }

    private void Update() {
        if (Settings.dialogue) {

            //Skip to next line of dialogue
            if (Input.GetMouseButtonDown(0)) {
                if (!sentenceEnded)
                    sentenceEnded = true;
                else
                    NextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue) {

        Settings.dialogue = true;
        dialogueArea.SetActive(true);
        speakerLabel.text = dialogue.speaker;

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        NextSentence();
    }

    private void NextSentence() {

        StopAllCoroutines();
        if (sentences.Count == 0) {
            EndDialogue();
        }
        else {
            StartCoroutine(TypeSentence(sentences.Dequeue()));
        }
    }

    private void EndDialogue() {
        Settings.dialogue = false;
        dialogueArea.SetActive(false);
    }

    private IEnumerator TypeSentence(string sentence) {
        dialogueTextArea.text = "";

        sentenceEnded = false;

        foreach (char c in sentence) {
            dialogueTextArea.text += c;

            if (sentenceEnded) {
                dialogueTextArea.text = sentence;
                break;
            }

            yield return new WaitForSeconds(textDelay);
        }

        sentenceEnded = true;
    }

    private void OnTextSpeedChanged(float value) {
        textDelay = 1f / value;
    }
}
