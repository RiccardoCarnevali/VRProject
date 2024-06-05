using System.Collections;
using UnityEngine;

public class InspectionScreen : MonoBehaviour
{
    [SerializeField] private float inspectedObjectDistanceFromCamera = 3f;
    [SerializeField] private float rotationSpeed = 40;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject inspectionCamera;
    private GameObject inspectedObject;

    private void Start() {
        inspectionCamera.SetActive(false);
    }

    private void Update() {
        if (Settings.inspecting) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                StopInspecting();
            }
            else if (Input.GetMouseButton(0)) {
                inspectedObject.transform.RotateAround(inspectedObject.transform.position, new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0), rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void StartInspecting(GameObject objectToInspect) {
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        CursorManager.ShowCursor();
        Settings.inspecting = true;
        inspectionCamera.SetActive(true);
        inspectedObject = Instantiate(objectToInspect);
        inspectedObject.transform.SetParent(transform, false);
        inspectedObject.transform.localScale = Vector3.one;
        inspectedObject.transform.localPosition = new Vector3(0, 0, inspectedObjectDistanceFromCamera);
    }

    private void StopInspecting() {
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);
        Settings.inspecting = false;
        inspectionCamera.SetActive(false);
        PlayerInventory.Instance().Open();
        Destroy(inspectedObject);
    }
}
