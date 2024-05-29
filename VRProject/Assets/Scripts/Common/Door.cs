using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private float doorOpenTimeSeconds = 1f;
    [SerializeField] private float yEnd;

    public void Open() {
        StartCoroutine(OpenCoroutine());
    }

    private IEnumerator OpenCoroutine() {
        Vector3 start = transform.localPosition;
        Vector3 end = transform.localPosition;
        end.y = yEnd;

        float doorOpenProgress = 0;

        while (doorOpenProgress < doorOpenTimeSeconds) {
            doorOpenProgress += Time.deltaTime * Time.timeScale;
            transform.localPosition = Vector3.Lerp(start, end, doorOpenProgress / doorOpenTimeSeconds);
            yield return null;
        }
    }
}
