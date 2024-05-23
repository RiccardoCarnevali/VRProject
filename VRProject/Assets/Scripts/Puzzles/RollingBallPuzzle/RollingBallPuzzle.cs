using UnityEngine;

public class RollingBallPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject puzzleCamera;
 
    public void StartPuzzle() {
        Settings.inPuzzle = true;
        puzzleCamera.SetActive(true);
    }
}
