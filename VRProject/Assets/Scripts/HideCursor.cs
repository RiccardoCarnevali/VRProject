using UnityEngine;

public class HideCursor : MonoBehaviour
{
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        Cursor.lockState = Settings.paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = Settings.paused;
    }
}
