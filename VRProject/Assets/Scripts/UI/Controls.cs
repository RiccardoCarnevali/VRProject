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

    [SerializeField] private CanvasGroup inventoryControls;
    private float inventoryControlsShowTimeSeconds = 5f;

    [SerializeField] private CanvasGroup newLensControls;
    private float newLensControlsShowTimeSeconds = 5f;

    private float disappearTimeSeconds = 1f;

    private void Awake() {
        Messenger.AddListener(MessageEvents.POLAROID_PICKED_UP, OnPolaroidPickedUp);
        Messenger.AddListener(MessageEvents.FIRST_ITEM_PICKED_UP, OnFirstItemPickedUp);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(MessageEvents.POLAROID_PICKED_UP, OnPolaroidPickedUp);
        Messenger.RemoveListener(MessageEvents.FIRST_ITEM_PICKED_UP, OnFirstItemPickedUp);
    }

    void Start()
    {
        StartCoroutine(OnGameStart());
    }

    private IEnumerator OnGameStart() {
        while (Settings.paused)
            yield return null;
        
        Messenger.Broadcast(MessageEvents.TOGGLE_UI);

        if (!Settings.load) {
            StartCoroutine(Show(moveControls, moveControlsShowTimeSeconds));
            StartCoroutine(Show(lookControls, lookControlsShowTimeSeconds));
        }
    }

    private IEnumerator Show(CanvasGroup group, float waitSeconds) {
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
        StartCoroutine(Show(takePictureControls, takePictureControlsShowTimeSeconds));
    }

    private  void OnFirstItemPickedUp() {
        StartCoroutine(Show(inventoryControls, inventoryControlsShowTimeSeconds));
    }

    private void OnPolaroidLensPickedUp() {
        StartCoroutine(Show(newLensControls, newLensControlsShowTimeSeconds));
    }
}
