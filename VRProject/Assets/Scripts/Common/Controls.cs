using System.Collections;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private CanvasGroup moveControls;
    private float moveControlsShowTimeSeconds = 10f;
    [SerializeField] private CanvasGroup lookControls;
    private float lookControlsShowTimeSeconds = 10f;

    [SerializeField] private CanvasGroup takePictureControls;
    private float takePictureControlsShowTimeSeconds = 5f;

    private float disappearTimeSeconds = 1f;

    private void Awake() {
        Messenger.AddListener(MessageEvents.POLAROID_PICKED_UP, OnPolaroidPickedUp);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(MessageEvents.POLAROID_PICKED_UP, OnPolaroidPickedUp);
    }

    void Start()
    {
        StartCoroutine(Show(moveControlsShowTimeSeconds, moveControls));
        StartCoroutine(Show(lookControlsShowTimeSeconds, lookControls));
    }

    private IEnumerator Show(float waitSeconds, CanvasGroup group) {
        group.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitSeconds);

        float disappearProgress = 0;

        while (disappearProgress < disappearTimeSeconds) {
            disappearProgress += Time.deltaTime * Time.timeScale;
            group.alpha = Mathf.Lerp(1, 0, disappearProgress / disappearTimeSeconds);
            yield return null;
        }

        group.gameObject.SetActive(false);
    }

    private void OnPolaroidPickedUp() {
        StartCoroutine(Show(takePictureControlsShowTimeSeconds, takePictureControls));
    }
}
