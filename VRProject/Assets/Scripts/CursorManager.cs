using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Start() {
        HideCursor();
    }

    public static void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void ShowCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
