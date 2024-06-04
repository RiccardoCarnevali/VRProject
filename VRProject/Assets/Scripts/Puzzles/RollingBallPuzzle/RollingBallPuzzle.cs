using UnityEngine;

public class RollingBallPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject puzzleCamera;
 
    public void StartPuzzle() {
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        Settings.inPuzzle = true;
        puzzleCamera.SetActive(true);
    }
}
