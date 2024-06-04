using UnityEngine;

public class UIToggle : MonoBehaviour
{
    [SerializeField] private GameObject ui;

    void Start()
    {
        Messenger.AddListener(MessageEvents.TOGGLE_UI, ToggleUI);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(MessageEvents.TOGGLE_UI, ToggleUI);
    }

    private void ToggleUI() {
        ui.SetActive(!ui.activeSelf);
    }
}
