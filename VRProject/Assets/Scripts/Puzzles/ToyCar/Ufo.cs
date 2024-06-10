using System.Collections;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] private Transform ufoStart;
    [SerializeField] private Transform ufoEnd;
    [SerializeField] private Collider rayCollider;
    [SerializeField] private Light rayLight;

    private float rotationSpeed = 90f;
    private float tripTimeForwardSeconds = 5f;     
    private float tripTimeBackwardSeconds = 2f;    

    private void Start() {
        StartCoroutine(Cycle());
    }

    private void Update() {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    private IEnumerator Cycle() {
        while (true) {
            yield return GoToEnd();
            yield return GoToStart();
        }
    }

    private IEnumerator GoToEnd() {
        rayLight.enabled = true;
        rayCollider.enabled = true;

        float progress = 0;

        while (progress < tripTimeForwardSeconds) {
            progress += Time.deltaTime * Time.timeScale;
            transform.position = Vector3.Lerp(ufoStart.position, ufoEnd.position, progress / tripTimeForwardSeconds);
            yield return null;
        }

        transform.position = ufoEnd.position;
    }

    private IEnumerator GoToStart() {
        rayLight.enabled = false;
        rayCollider.enabled = false;

        float progress = 0;

        while (progress < tripTimeBackwardSeconds) {
            progress += Time.deltaTime * Time.timeScale;
            transform.position = Vector3.Lerp(ufoEnd.position, ufoStart.position, progress / tripTimeBackwardSeconds);
            yield return null;
        }

        transform.position = ufoStart.position;
    }
}
