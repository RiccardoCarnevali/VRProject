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
            return InteractionLabels.INTERACT;
        else if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.JOYSTICK))
            return InteractionLabels.USE_ITEM;
        else 
            return InteractionLabels.INSPECT;
    }

    public override void Interact()
    {
        if (joystickInserted) {
            rollingBallPuzzle.StartPuzzle();
        }
        else if (PlayerInventory.Instance().CheckSelectedItem(Item.ItemType.JOYSTICK)) {
            PlayerInventory.Instance().ConsumeSelectedItem();
            joystickShaft.SetActive(true);
            joystickInserted = true;
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectDialogue);
        }
    }
}
