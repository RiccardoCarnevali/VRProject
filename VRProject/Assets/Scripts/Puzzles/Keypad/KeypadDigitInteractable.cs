using UnityEngine;

public class KeypadDigitInteractable : Interactable
{
    [SerializeField] private Keypad keypad;
    [SerializeField] private int digit;

    public override string GetLabel()
    {
        return InteractionLabels.INSERT + " " + digit.ToString();
    }

    public override void Interact()
    {
        keypad.InsertDigit(digit);
    }
}
