using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PicturePrintSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip shortPrintSFX;
    [SerializeField] private AudioClip longPrintSFX;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void ShortPrint() {
        audioSource.PlayOneShot(shortPrintSFX);
    }

    private void LongPrint() {
        audioSource.PlayOneShot(longPrintSFX);
    }
}
