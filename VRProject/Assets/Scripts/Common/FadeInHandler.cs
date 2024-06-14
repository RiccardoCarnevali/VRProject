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
    }

    private void OnDestroy()
    {
        Messenger<Action, float, float>.RemoveListener(MessageEvents.FADE_IN, CallFadeIn);
    }


    private void CallFadeIn(Action action, float waitTimeSeconds, float fadeDurationSeconds)
    {
        StartCoroutine(FadeIn(action, waitTimeSeconds, fadeDurationSeconds));
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
}
