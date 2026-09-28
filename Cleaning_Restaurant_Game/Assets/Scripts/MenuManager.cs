using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public CanvasGroup howToPlayCanvasGroup;
    public float fadeDuration = 0.5f;

    public CanvasGroup transitionCanvasGroup;
    public float sceneFadeDuration = 1f;

    private bool justToggled = false;
    private bool isTransitioning = false;

    // 시작하기 버튼 - 검은 화면 페이드 후 Intro 씬으로 전환
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

    // 게임 방법 버튼 - 이미지 페이드인
    public void OnClickHowToPlay()
    {
        if (justToggled) return;

        StopAllCoroutines();
        StartCoroutine(FadeIn());

        justToggled = true;
        CancelInvoke(nameof(ClearJustToggled));
        Invoke(nameof(ClearJustToggled), 0.2f);
    }

    // 게임 방법 화면 닫기 - 이미지 페이드아웃
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

    // 종료 버튼 - 빌드된 실행 파일에서만 작동 (에디터에서는 반응 없음)
    public void OnClickQuit()
    {
        Application.Quit();
    }
}