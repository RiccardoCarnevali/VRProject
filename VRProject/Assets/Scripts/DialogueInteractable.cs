using UnityEngine;

public class DialogueInteractable : Interactable
{
    [SerializeField] private Dialogue dialogue;

    public override void Interact()
    {
        DialogueManager.Instance().StartDialogue(dialogue);
    }
}
