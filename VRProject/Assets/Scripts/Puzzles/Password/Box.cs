using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : PasswordTarget
{
    [SerializeField] private Transform coverPivot;
    private Quaternion openingAngle;
    private float openingTimeSeconds = 0.5f;
    private AudioSource audioSource;

    [SerializeField] private GameObject ball;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        openingAngle = Quaternion.Euler(135, 90, 0);

        if (Settings.load && SaveSystem.CheckFlag("box_open")) {
            ball.SetActive(true);
            coverPivot.localRotation = openingAngle;
        }
    }

    public override void Unlock()
    {
        SaveSystem.SetFlag("box_open");
        StartCoroutine(Open());
    }

    private IEnumerator Open() {
        ball.SetActive(true);
        audioSource.Play();
        float progress = 0;
        Quaternion startAngle = coverPivot.localRotation;

        while (progress < openingTimeSeconds) {
            progress += Time.deltaTime * Time.timeScale;
            coverPivot.localRotation = Quaternion.Lerp(startAngle, openingAngle, progress / openingTimeSeconds);
            yield return null;
        }

        coverPivot.localRotation = openingAngle;
    }
}
