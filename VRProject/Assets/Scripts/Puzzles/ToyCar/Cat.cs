using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoints;
    private Vector3 destination;
    private int checkPointIndex = 0;
    private float speed = 0.05f;

    private bool rotating = false;
    private float rotationTimeSeconds = 0.3f;

    private void Awake() {
        SetNextDestination();
    }

    private void Update() {
        
        if (!rotating) {
            transform.Translate(speed * Time.deltaTime * transform.forward, Space.World);

            if (Vector3.Distance(transform.position, destination) < 0.05f) {
                SetNextDestination();
                StartCoroutine(SmoothRotate(180));
            }
        }
    }

    private void SetNextDestination() {
        destination = checkPoints[checkPointIndex].position;
        ++checkPointIndex;

        if (checkPointIndex == checkPoints.Length) 
            checkPointIndex = 0;
    }

    private IEnumerator SmoothRotate(float degrees) {
        rotating = true;
        float progress = 0;

        Quaternion originalRotation = transform.localRotation;
        Quaternion finalRotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(0, degrees, 0));

        while (progress < rotationTimeSeconds) {
            progress += Time.deltaTime * Time.timeScale;
            transform.localRotation = Quaternion.Lerp(originalRotation, finalRotation, progress / rotationTimeSeconds);
            yield return null;
        }

        transform.localRotation = finalRotation;
        rotating = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out ToyCar toyCar)) {
            toyCar.Reset();
        }
    }
}
