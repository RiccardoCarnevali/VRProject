using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{

    private Rigidbody rb;
    private AudioSource rollingBallAudioSource;
    private bool rolling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rollingBallAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (rb.velocity.magnitude >= 0.2f && !rolling) {
            rollingBallAudioSource.Play();
            rolling = true;
        }
        else if (rb.velocity.magnitude < 0.2f) {
            rollingBallAudioSource.Stop();
            rolling = false;
        }
    }
}
