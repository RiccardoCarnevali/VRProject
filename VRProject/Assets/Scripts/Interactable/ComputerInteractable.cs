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
        return "Interact";
    }

    public override void Interact()
    {
        Settings.inPuzzle = true;
        computerScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(passwordInput);
    }
}
