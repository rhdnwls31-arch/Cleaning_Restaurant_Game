using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public CanvasGroup howToPlayCanvasGroup;
    public float fadeDuration = 0.5f;

    // 추가: 씬 전환용 검은 화면
    public CanvasGroup transitionCanvasGroup;
    public float sceneFadeDuration = 0.8f;

    private bool justToggled = false;
    private bool isTransitioning = false;

    public void OnClickStart()
    {
        if (isTransitioning) return;
        StartCoroutine(FadeToIntroScene());
    }

    IEnumerator FadeToIntroScene()
    {
        isTransitioning = true;

        float elapsed = 0f;
        while (elapsed < sceneFadeDuration)
        {
            elapsed += Time.deltaTime;
            transitionCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / sceneFadeDuration);
            yield return null;
        }
        transitionCanvasGroup.alpha = 1f;

        SceneManager.LoadScene("Intro");
    }

    public void OnClickHowToPlay()
    {
        if (justToggled) return;

        StopAllCoroutines();
        StartCoroutine(FadeIn());

        justToggled = true;
        CancelInvoke(nameof(ClearJustToggled));
        Invoke(nameof(ClearJustToggled), 0.2f);
    }

    public void OnClickCloseHowToPlay()
    {
        if (justToggled) return;

        StopAllCoroutines();
        StartCoroutine(FadeOutThenDisable());

        justToggled = true;
        CancelInvoke(nameof(ClearJustToggled));
        Invoke(nameof(ClearJustToggled), 0.2f);
    }

    void ClearJustToggled()
    {
        justToggled = false;
    }

    IEnumerator FadeIn()
    {
        howToPlayPanel.SetActive(true);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            howToPlayCanvasGroup.alpha = elapsed / fadeDuration;
            yield return null;
        }
        howToPlayCanvasGroup.alpha = 1f;
    }

    IEnumerator FadeOutThenDisable()
    {
        float elapsed = 0f;
        float startAlpha = howToPlayCanvasGroup.alpha;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            howToPlayCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }
        howToPlayCanvasGroup.alpha = 0f;

        howToPlayPanel.SetActive(false);
    }
}