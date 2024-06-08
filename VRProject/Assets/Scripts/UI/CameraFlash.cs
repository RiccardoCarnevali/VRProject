using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFlash : MonoBehaviour
{

    [SerializeField] private Image cameraFlash;
    private float flashDurationSeconds = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        Messenger<Color>.AddListener(MessageEvents.CAMERA_FLASH, OnCameraFlash);
    }

    private void OnDestroy() {
        Messenger<Color>.RemoveListener(MessageEvents.CAMERA_FLASH, OnCameraFlash);
    }

    private void OnCameraFlash(Color flashColor) {
        StartCoroutine(FlashCoroutine(flashColor));
    }

    private IEnumerator FlashCoroutine(Color flashColor) {
        float progress = 0;

        Color endColor = flashColor;
        endColor.a = 0;

        while (progress < flashDurationSeconds) {
            progress += Time.deltaTime * Time.timeScale;
            cameraFlash.color = Color.Lerp(flashColor, endColor, progress / flashDurationSeconds);
            yield return null;
        }
        cameraFlash.color = endColor;
    }
}
