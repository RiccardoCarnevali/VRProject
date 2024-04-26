using UnityEngine;

public class InspectionScreen : MonoBehaviour
{
    [SerializeField] private float inspectedObjectDistanceFromCamera = 3f;
    [SerializeField] private float rotationSpeed = 40;
    private Camera inspectionCamera;
    private GameObject inspectedObject;

    private void Start() {
        inspectionCamera = GetComponentInChildren<Camera>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (!inspectionCamera.enabled)  {
                StartInspecting(GameObject.CreatePrimitive(PrimitiveType.Cube));
            }
            else {
                StopInspecting();
            }
        }

        if (inspectionCamera.enabled && Input.GetMouseButton(0)) {
            inspectedObject.transform.RotateAround(inspectedObject.transform.position, new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0), rotationSpeed * Time.deltaTime);
        }
    }

    public void StartInspecting(GameObject objectToInspect) {
        inspectionCamera.enabled = true;
        inspectedObject = objectToInspect;
        inspectedObject.transform.parent = transform;
        inspectedObject.transform.localPosition = new Vector3(0, 0, inspectedObjectDistanceFromCamera);
    }

    public void StopInspecting() {
        inspectionCamera.enabled = false;
        Destroy(inspectedObject);
    }
}
