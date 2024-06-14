using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInHandler : MonoBehaviour
{
    [SerializeField] private Image startBlack;

    void Awake()
    {
        Messenger<Action, float, float>.AddListener(MessageEvents.FADE_IN, CallFadeIn);
        Messenger<Action, float, float>.AddListener(MessageEvents.FADE_OUT, CallFadeOut);

    }

    private void OnDestroy()
    {
        Messenger<Action, float, float>.RemoveListener(MessageEvents.FADE_IN, CallFadeIn);
        Messenger<Action, float, float>.RemoveListener(MessageEvents.FADE_OUT, CallFadeOut);
    }


    private void CallFadeIn(Action action, float waitTimeSeconds, float fadeDurationSeconds)
    {
        StartCoroutine(FadeIn(action, waitTimeSeconds, fadeDurationSeconds));
    }

    private void CallFadeOut(Action action, float waitTimeSeconds, float fadeDurationSeconds)
    {
        StartCoroutine(FadeOut(action, waitTimeSeconds, fadeDurationSeconds));
    }

    private IEnumerator FadeIn(Action action, float waitTimeSeconds, float fadeDurationSeconds)
    {
        startBlack.color = Color.black;
        startBlack.enabled = true;
        yield return new WaitForSeconds(waitTimeSeconds);
        float progress = 0;
        Color transparent = new(0, 0, 0, 0);

        while (progress < fadeDurationSeconds)
        {
            progress += Time.deltaTime * Time.timeScale;
            startBlack.color = Color.Lerp(Color.black, transparent, progress / fadeDurationSeconds);
            yield return null;
        }

        startBlack.color = transparent;
        action.Invoke();
    }

    private IEnumerator FadeOut(Action action, float waitTimeSeconds, float fadeDurationSeconds)
    {
        startBlack.enabled = true;

        float progress = 0;
        Color transparent = new(0, 0, 0, 0);
        startBlack.color = transparent;

        while (progress < fadeDurationSeconds)
        {
            progress += Time.deltaTime * Time.timeScale;
            startBlack.color = Color.Lerp(transparent, Color.black, progress / fadeDurationSeconds);
            yield return null;
        }
        startBlack.color = Color.black;

        yield return new WaitForSeconds(waitTimeSeconds);

        action.Invoke();
    }
}
