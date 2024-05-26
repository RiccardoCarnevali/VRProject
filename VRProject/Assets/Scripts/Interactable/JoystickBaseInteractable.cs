using UnityEngine;

[RequireComponent(typeof(RollingBallPuzzle))]
public class JoystickBaseInteractable : Interactable
{
    [SerializeField] Dialogue inspectDialogue;
    private bool joystickInserted = false;
    private RollingBallPuzzle rollingBallPuzzle;

    private void Start() {
        rollingBallPuzzle = GetComponent<RollingBallPuzzle>();
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
            Debug.Log("Used item");
            joystickInserted = true;
        }
        else {
            DialogueManager.Instance().StartDialogue(inspectDialogue);
        }
    }

    private bool IsJoystickSelected() {
        return true;
    }
}
