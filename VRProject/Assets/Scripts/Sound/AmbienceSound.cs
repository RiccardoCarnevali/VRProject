using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioFade))]
public class AmbienceSound : MonoBehaviour
{
    private void Start()
    {
        AudioFade audioFade = GetComponent<AudioFade>();
        StartCoroutine(audioFade.FadeIn());
    }
}
