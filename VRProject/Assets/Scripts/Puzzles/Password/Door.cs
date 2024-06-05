using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Door : PasswordTarget
{
    private float doorOpenTimeSeconds = 1.4f;

    [SerializeField] private int id;
    [SerializeField] private float yEnd;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();

        if (Settings.load && SaveSystem.CheckFlag("door_" + id + "_open")) {
            Vector3 end = transform.localPosition;
            end.y = yEnd;
            transform.localPosition = end;
        }
    }

    public override void Unlock() {
        SaveSystem.SetFlag("door_" + id + "_open");
        StartCoroutine(Open());
    }

    private IEnumerator Open() {
        Vector3 start = transform.localPosition;
        Vector3 end = transform.localPosition;
        end.y = yEnd;

        float doorOpenProgress = 0;

        audioSource.Play();
        while (doorOpenProgress < doorOpenTimeSeconds) {
            doorOpenProgress += Time.deltaTime * Time.timeScale;
            transform.localPosition = Vector3.Lerp(start, end, doorOpenProgress / doorOpenTimeSeconds);
            yield return null;
        }

        transform.localPosition = end;
    }
}
