using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    private float waitTimeSeconds = 1f;
    [SerializeField] private Image startBlack;
    private float fadeDurationSeconds = 2f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {
        startBlack.enabled = true;
        yield return new WaitForSeconds(waitTimeSeconds);
        float progress = 0;
        Color transparent = new Color(0, 0, 0, 0);

        while (progress < fadeDurationSeconds) {
            progress += Time.deltaTime * Time.timeScale;
            startBlack.color = Color.Lerp(Color.black, transparent, progress / fadeDurationSeconds);
            yield return null;
        }

        startBlack.color = transparent;
        Settings.gameStarting = false;
    }
}
