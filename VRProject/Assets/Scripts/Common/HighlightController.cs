using HighlightPlus;
using UnityEngine;

[RequireComponent(typeof(HighlightEffect))]
public class HighlightController : MonoBehaviour
{
    private HighlightEffect highlightEffect;

    private float loopTimeSeconds = 1f;
    private float progress;
    private bool highlightRising = false;

    private void Awake() {
        highlightEffect = GetComponent<HighlightEffect>();
    }

    private void Update() {
        highlightEffect.innerGlow = Mathf.Lerp(0, 1, progress / loopTimeSeconds);

        if (highlightRising) {
            progress += Time.deltaTime * Time.timeScale;

            if (progress >= loopTimeSeconds)
                highlightRising = false;
        }
        else {
            progress -= Time.deltaTime * Time.timeScale;

            if (progress <= 0)
                highlightRising = true;
        }
    }

    public void DisableHighlight() {
        Destroy(highlightEffect);
        Destroy(this);
    }
}
