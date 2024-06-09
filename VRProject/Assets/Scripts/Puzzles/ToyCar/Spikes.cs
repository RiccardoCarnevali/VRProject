using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float angle = 45f;
    private float delaySeconds = 5f;
    private float rotationTimeSeconds = 0.2f;

    private void Start() {
        InvokeRepeating("GoDown", 0, delaySeconds*2);
        InvokeRepeating("GoUp", delaySeconds, delaySeconds*2);
    }

    private void GoUp() {
        StartCoroutine(SmoothRotate(angle));
    }

    private void GoDown() {
        StartCoroutine(SmoothRotate(-angle));
    }

    private IEnumerator SmoothRotate(float degrees) {
        float progress = 0;

        Quaternion originalRotation = transform.localRotation;
        Quaternion finalRotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(degrees, 0, 0));

        while (progress < rotationTimeSeconds) {
            progress += Time.deltaTime * Time.timeScale;
            transform.localRotation = Quaternion.Lerp(originalRotation, finalRotation, progress / rotationTimeSeconds);
            yield return null;
        }

        transform.localRotation = finalRotation;
    }
}
