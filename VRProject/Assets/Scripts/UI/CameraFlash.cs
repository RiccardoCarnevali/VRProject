using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFlash : MonoBehaviour
{

    [SerializeField] private Image cameraFlash;
    private float cameraFlashTime = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener(MessageEvents.CAMERA_FLASH, OnCameraFlash);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(MessageEvents.CAMERA_FLASH, OnCameraFlash);
    }

    private void OnCameraFlash() {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine() {
        float startTime = Time.time;

        Color white = Color.white;
        Color originalColor = cameraFlash.color;

        while (Time.time - startTime < cameraFlashTime) {
            cameraFlash.color = Color.Lerp(white, originalColor, (Time.time - startTime) / cameraFlashTime);
            yield return null;
        }
        cameraFlash.color = originalColor;
    }
}
