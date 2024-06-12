using System.Collections;
using UnityEngine;

public class ShapesPuzzle : MonoBehaviour
{
    [SerializeField] private Camera puzzleCamera;
    [SerializeField] private GameObject slatesPrefab;
    [SerializeField] private GameObject slates;
    [SerializeField] private LayerMask slatesLayer;

    [SerializeField] private GameObject[] slateSpots;
    [SerializeField] private Slate.SlateShape[] solution;
    private Slate[] chosenSlates = new Slate[] {null, null, null};

    private bool won = false;
    private Color winColor = new Color(0, 1, 0, 0.1f);
    private Color loseColor = new Color(1, 0, 0, 0.1f);
    private float resetDelaySeconds = 1f;

    [SerializeField] private GameObject cup;
    [SerializeField] private GameObject cupFallen;
    [SerializeField] private GameObject ball;
    private ShapesPuzzleTrigger shapesPuzzleTrigger;

    private void Start() {
        shapesPuzzleTrigger = GetComponent<ShapesPuzzleTrigger>();
        if (Settings.load && SaveSystem.CheckFlag("shapes_puzzle_won")) {
            cup.SetActive(false);
            cupFallen.SetActive(true);
            ball.SetActive(true);
            shapesPuzzleTrigger.DisableInteraction();
        }
    }

    private void Update() {
        if (won || !puzzleCamera.gameObject.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            ExitPuzzle();
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = puzzleCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));

            if (Physics.Raycast(ray, out RaycastHit hit, 2, slatesLayer)) {
                if (hit.transform.TryGetComponent(out Slate slate)) {
                    TrySetSlate(slate);
                }
            }
        }
    }

    public void StartPuzzle() {
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        Settings.inPuzzle = true;
        puzzleCamera.gameObject.SetActive(true);
        CursorManager.ShowCursor();
    }

    private void TrySetSlate(Slate slate) {
        if (chosenSlates[(int) slate.Size] == null) {
            chosenSlates[(int) slate.Size] = slate;
            PlaceSlate(slate);

            if (AllSlotsFull()) {
                if (CheckSolution()) {
                    StartCoroutine(Win());
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

    private IEnumerator Win() {
        won = true;
        SaveSystem.SetFlag("shapes_puzzle_won");
        foreach (Slate slate in chosenSlates) {
            slate.gameObject.GetComponent<Renderer>().material.color = winColor;
        }

        cup.SetActive(false);
        cupFallen.SetActive(true);
        cupFallen.GetComponent<AudioSource>().Play();
        ball.SetActive(true);
        shapesPuzzleTrigger.DisableInteraction();

        yield return Reset();

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

    private void ExitPuzzle() {
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        puzzleCamera.gameObject.SetActive(false);
        Settings.inPuzzle = false;
        CursorManager.HideCursor();
    }
}
