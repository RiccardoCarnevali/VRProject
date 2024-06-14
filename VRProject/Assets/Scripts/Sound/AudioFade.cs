using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFade : MonoBehaviour
{
    private AudioSource audioSource;

    private float fadeInTimeSeconds = 10f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator FadeIn()
    {
        float progress = 0;

        float volume = audioSource.volume;
        while (audioSource.volume < 1)
        {
            progress += Time.deltaTime * Time.timeScale;
            audioSource.volume = Mathf.Lerp(0, volume, progress / fadeInTimeSeconds);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float progress = 0;

        float volume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            progress += Time.deltaTime * Time.timeScale;
            audioSource.volume = Mathf.Lerp(volume, 0, progress / fadeInTimeSeconds);
            yield return null;
        }
    }
}
