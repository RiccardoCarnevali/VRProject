using UnityEngine;

public class KeypadSubmitInteractable : Interactable
{
    [SerializeField] private Keypad keypad;

    public override string GetLabel()
    {
        return InteractionLabels.SUBMIT;
    }

    public override void Interact()
    {
        keypad.Submit();
    }
}
