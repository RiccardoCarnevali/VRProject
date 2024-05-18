using System.Collections;
using UnityEngine;

public class InspectionScreen : MonoBehaviour
{
    [SerializeField] private float inspectedObjectDistanceFromCamera = 3f;
    [SerializeField] private float rotationSpeed = 40;
    [SerializeField] private GameObject inventoryCanvas;
    private Camera inspectionCamera;
    private GameObject inspectedObject;

    private void Start() {
        inspectionCamera = GetComponentInChildren<Camera>();
    }

    private void Update() {
        if (Settings.inspecting) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                StartCoroutine(StopInspecting());
            }
            else if (Input.GetMouseButton(0)) {
                inspectedObject.transform.RotateAround(inspectedObject.transform.position, new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0), rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void StartInspecting(GameObject objectToInspect) {
        CursorManager.ShowCursor();
        Settings.inspecting = true;
        inspectionCamera.enabled = true;
        inspectedObject = Instantiate(objectToInspect);
        inspectedObject.transform.SetParent(transform, false);
        inspectedObject.transform.localScale = Vector3.one;
        inspectedObject.transform.localPosition = new Vector3(0, 0, inspectedObjectDistanceFromCamera);
    }

    //This is a coroutine so that pressing escape and going to the inventory happen on subsequent frames, otherwise
    //the inventory would also immediately close
    private IEnumerator StopInspecting() {
        yield return null;
        Settings.inspecting = false;
        inspectionCamera.enabled = false;
        PlayerInventory.Instance().Open();
        Destroy(inspectedObject);
    }
}
