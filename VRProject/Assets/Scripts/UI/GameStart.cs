using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Dialogue startDialogue;
    private void Start()
    {
        Settings.gameStarting = true;
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        Messenger<Action, float, float>.Broadcast(MessageEvents.FADE_IN, StartingDialogue, 1f, 2f);
    }

    private void StartingDialogue() {
        Settings.gameStarting = false;

        if (!Settings.load)
            DialogueManager.Instance().StartDialogue(startDialogue);
    }
}
