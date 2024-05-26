using UnityEngine;

[RequireComponent(typeof(RollingBallPuzzle))]
public class JoystickBaseInteractable : Interactable
{
    [SerializeField] Dialogue inspectDialogue;
    private bool joystickInserted = false;
    private RollingBallPuzzle rollingBallPuzzle;
    [SerializeField] private GameObject joystickShaft;

    private void Start() {
        rollingBallPuzzle = GetComponent<RollingBallPuzzle>();
        joystickShaft.SetActive(false);
    }

    public override string GetLabel()
    {
        if (joystickInserted) 
            return "Interact";
        else if (IsJoystickSelected())
            return "Use item";
        else 
            return "Inspect";
    }

    public override void Interact()
    {
        if (joystickInserted) {
            rollingBallPuzzle.StartPuzzle();
        }
        else if (IsJoystickSelected()) {
            PlayerInventory.Instance().ConsumeSelectedItem();
            joystickShaft.SetActive(true);
            joystickInserted = true;
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectDialogue);
        }
    }

    private bool IsJoystickSelected() {
        Item item = PlayerInventory.Instance().GetSelectedItem();
        return item != null && item.type == Item.ItemType.JOYSTICK;
    }
}
