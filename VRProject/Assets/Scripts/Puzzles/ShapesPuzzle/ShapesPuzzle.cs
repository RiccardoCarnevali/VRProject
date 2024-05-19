using System.Collections;
using UnityEngine;

public class ShapesPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject puzzleCamera;
    [SerializeField] private GameObject slatesPrefab;
    [SerializeField] private GameObject slates;

    [SerializeField] private GameObject[] slateSpots;
    [SerializeField] private Slate.SlateShape[] solution;
    private Slate[] chosenSlates = new Slate[] {null, null, null};

    private Color winColor = new Color(0, 1, 0, 0.1f);
    private Color loseColor = new Color(1, 0, 0, 0.1f);
    private float resetDelaySeconds = 1f;

    public void StartPuzzle() {
        Settings.inPuzzle = true;
        puzzleCamera.SetActive(true);
        CursorManager.ShowCursor();
    }

    public void TrySetSlate(Slate slate) {
        if (chosenSlates[(int) slate.Size] == null) {
            chosenSlates[(int) slate.Size] = slate;
            PlaceSlate(slate);

            if (AllSlotsFull()) {
                if (CheckSolution()) {
                    Win();
                }
                else {
                    Lose();
                }
            }
        }
    }

    private void PlaceSlate(Slate slate) {
        slate.gameObject.transform.SetParent(slateSpots[(int) slate.Size].transform, false);
        slate.gameObject.transform.localPosition = Vector3.zero;
    }

    private bool AllSlotsFull() {
        foreach (Slate slate in chosenSlates) {
            if (slate == null)
                return false;
        }
        return true;
    }

    private bool CheckSolution() {
        foreach (Slate slate in chosenSlates) {
            if (slate.Shape != solution[(int) slate.Size])
                return false;
        }
        return true;
    }

    private void Lose() {
        foreach (Slate slate in chosenSlates) {
            slate.gameObject.GetComponent<Renderer>().material.color = loseColor;
        }

        StartCoroutine(Reset());
    }

    private void Win() {
        foreach (Slate slate in chosenSlates) {
            slate.gameObject.GetComponent<Renderer>().material.color = winColor;
        }

        ExitPuzzle();
    }

    private IEnumerator Reset() {
        yield return new WaitForSeconds(resetDelaySeconds);

        Destroy(slates);
        foreach (Slate slate in chosenSlates) {
            Destroy(slate.gameObject);
        }

        slates = Instantiate(slatesPrefab, transform);
        slates.transform.localPosition = Vector3.zero;
    }

    public void ExitPuzzle() {
        puzzleCamera.SetActive(false);
        Settings.inPuzzle = false;
        CursorManager.HideCursor();
    }
}
