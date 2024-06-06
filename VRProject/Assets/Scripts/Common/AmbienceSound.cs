using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbienceSound : MonoBehaviour
{
    private AudioSource audioSource;

    private float fadeInTimeSeconds = 10f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {
        float progress = 0;

        audioSource.volume = 0;
        while (audioSource.volume < 1) {
            progress += Time.deltaTime * Time.timeScale;
            audioSource.volume = Mathf.Lerp(0, 1, progress / fadeInTimeSeconds);
            yield return null;
        }
    }
}
