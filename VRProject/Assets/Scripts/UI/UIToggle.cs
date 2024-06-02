using UnityEngine;

public class UIToggle : MonoBehaviour
{
    [SerializeField] private Canvas ui;

    void Start()
    {
        Messenger.AddListener(MessageEvents.TOGGLE_UI, ToggleUI);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(MessageEvents.TOGGLE_UI, ToggleUI);
    }

    private void ToggleUI() {
        ui.enabled = !ui.enabled;
    }
}
