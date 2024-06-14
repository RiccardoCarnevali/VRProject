using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheldonScooper : MonoBehaviour
{
    [SerializeField] private RawImage _bloodSplatters;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Scoop()
    {
        GetComponent<Animator>().SetTrigger("Scoop");
    }

    public void PlayScoopingSound() {
        _audioSource.Play();
    }

    public void BloodSplatter() {
        _bloodSplatters.enabled = true;
    }
}
