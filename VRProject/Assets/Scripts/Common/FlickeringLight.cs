using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour
{
    private Light flickeringLight;

    //The probability of the light to start flickering at each second is 1 out of expectedSecondsWaitTime
    private int expectedSecondsWaitTime = 6;
    private float waitProgress = 0;
    private float minFlickerDurationSeconds = 0.2f;
    private float maxFlickerDurationSeconds = 0.6f;
    private float flickerIntervalSeconds = 0.1f;

    private bool flickering = false;

    private void Start() {
        flickeringLight = GetComponent<Light>();
    }

    private void Update() {
        if (Settings.paused || flickering)
            return;

        waitProgress += Time.deltaTime * Time.timeScale;

        //Every one second try flickering
        if (waitProgress >= 1) {
            waitProgress = 0;
            if (Random.Range(1, expectedSecondsWaitTime) == 1) {
                flickering = true;
                StartCoroutine(Flicker(Random.Range(minFlickerDurationSeconds, maxFlickerDurationSeconds)));
            }
        }
    }

    private IEnumerator Flicker(float durationSeconds) {
        float start = Time.time;

        while (Time.time - start < durationSeconds) {
            flickeringLight.enabled = !flickeringLight.enabled;
            yield return new WaitForSeconds(flickerIntervalSeconds);
        }

        flickeringLight.enabled = true;
        flickering = false;
    }
}
