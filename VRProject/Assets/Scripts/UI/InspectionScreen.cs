using System.Collections;
using UnityEngine;

public class InspectionScreen : MonoBehaviour
{
    [SerializeField] private float inspectedObjectDistanceFromCamera = 3f;
    [SerializeField] private float rotationSpeed = 40;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject inspectionCamera;
    [SerializeField] private GameObject inspectionCanvas;
    private GameObject inspectedObject;

    private void Start() {
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
        CursorManager.ShowCursor();
        Settings.inspecting = true;
        inspectionCamera.SetActive(true);
        inspectionCanvas.SetActive(true);

        //Clone the object and adjust its transform to appear in front of the camera
        inspectedObject = Instantiate(objectToInspect);
        inspectedObject.transform.SetParent(transform, false);
        inspectedObject.transform.localScale = Vector3.one;
        inspectedObject.transform.localPosition = new Vector3(0, 0, inspectedObjectDistanceFromCamera);
    }

    public void StopInspecting() {
        StartCoroutine(StopInspectingCoroutine());
    }

    //This is a coroutine so that pressing escape and going to the inventory happen on subsequent frames, otherwise
    //the inventory would also immediately close
    private IEnumerator StopInspectingCoroutine() {
        yield return null;
        Settings.inspecting = false;
        inspectionCamera.SetActive(false);
        inspectionCanvas.SetActive(false);
        PlayerInventory.Instance().Open();
        Destroy(inspectedObject);
    }
}
