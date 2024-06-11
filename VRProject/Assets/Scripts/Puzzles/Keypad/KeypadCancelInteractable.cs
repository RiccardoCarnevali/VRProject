using UnityEngine;

public class KeypadCancelInteractable : Interactable
{
    [SerializeField] private Keypad keypad;

    public override string GetLabel()
    {
        return InteractionLabels.CANCEL;
    }

    public override void Interact()
    {
        keypad.Cancel();
    }
}
