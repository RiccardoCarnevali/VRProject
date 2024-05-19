using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SlateChooser : MonoBehaviour
{
    [SerializeField] private ShapesPuzzle puzzle;
    private Camera puzzleCamera;
    [SerializeField] private LayerMask slatesLayer;

    private void Start() {
        puzzleCamera = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            puzzle.ExitPuzzle();
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = puzzleCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));

            if (Physics.Raycast(ray, out RaycastHit hit, 2, slatesLayer)) {
                if (hit.transform.TryGetComponent(out Slate slate)) {
                    puzzle.TrySetSlate(slate);
                }
            }
        }
    }
}
