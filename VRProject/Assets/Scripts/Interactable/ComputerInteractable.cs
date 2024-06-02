using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerInteractable : Interactable
{
    [SerializeField] private GameObject computerScreen;
    [SerializeField] private GameObject passwordInput;

    private void Start() {
        computerScreen.SetActive(false);
    }

    public override string GetLabel()
    {
        return InteractionLabels.INTERACT;
    }

    public override void Interact()
    {
        Settings.inPuzzle = true;
        computerScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(passwordInput);
    }
}
