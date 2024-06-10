using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogPileInteractable : Interactable
{
    [SerializeField] private Dialogue _dialogue;

    public override string GetLabel()
    {
        return InteractionLabels.INSPECT;
    }

    public override void Interact()
    {
        DialogueManager.Instance().StartDialogue(_dialogue);
    }
}
