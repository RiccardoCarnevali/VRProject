using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour
{
    private Light flickeringLight;

    //The probability of the light to start flickering at each frame is 1 out of expectedFrameWaitTime
    private int expectedFrameWaitTime = 1000;
    private float minFlickerDurationSeconds = 0.2f;
    private float maxFlickerDurationSeconds = 0.6f;

    private bool flickering = false;

    private void Start() {
        flickeringLight = GetComponent<Light>();
    }

    private void Update() {
        if (Settings.paused || flickering)
            return;

        if (Random.Range(1, expectedFrameWaitTime) == 1) {
            flickering = true;
            StartCoroutine(Flicker(Random.Range(minFlickerDurationSeconds, maxFlickerDurationSeconds)));
        }
    }

    private IEnumerator Flicker(float durationSeconds) {
        float start = Time.time;

        while (Time.time - start < durationSeconds) {
            flickeringLight.enabled = !flickeringLight.enabled;
            yield return null;
        }

        flickeringLight.enabled = true;
        flickering = false;
    }
}
