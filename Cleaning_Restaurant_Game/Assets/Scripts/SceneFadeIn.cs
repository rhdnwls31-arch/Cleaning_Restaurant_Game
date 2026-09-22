using UnityEngine;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public CanvasGroup transitionCanvasGroup;
    public float fadeDuration = 0.8f;

    void Start()
    {
        transitionCanvasGroup.alpha = 1f;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            transitionCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }
        transitionCanvasGroup.alpha = 0f;
    }
}