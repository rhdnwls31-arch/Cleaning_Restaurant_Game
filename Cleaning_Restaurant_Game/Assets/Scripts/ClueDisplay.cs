using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClueDisplay : MonoBehaviour
{
    public Image clueImageUI;
    public CanvasGroup clueCanvasGroup;
    public GameObject backgroundOverlay;
    public float fadeDuration = 0.5f;

    // 추가: 닫힐 때 엔딩 조건을 확인하기 위해 연결
    public GameManager gameManager;

    private bool justOpened = false;

    public void ShowClue(Sprite sprite)
    {
        clueImageUI.sprite = sprite;
        backgroundOverlay.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(FadeTo(1f));

        justOpened = true;
        CancelInvoke(nameof(ClearJustOpened));
        Invoke(nameof(ClearJustOpened), 0.2f);
    }

    void ClearJustOpened()
    {
        justOpened = false;
    }

    public void HideClue()
    {
        if (justOpened)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(FadeOutThenDisable());
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        clueImageUI.gameObject.SetActive(true);

        float startAlpha = clueCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            clueCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        clueCanvasGroup.alpha = targetAlpha;
    }

    IEnumerator FadeOutThenDisable()
    {
        yield return StartCoroutine(FadeTo(0f));

        clueImageUI.gameObject.SetActive(false);
        backgroundOverlay.SetActive(false);

        // 추가: 단서 이미지가 완전히 닫힌 후에 엔딩 조건 확인
        if (gameManager != null)
        {
            gameManager.CheckEnding();
        }
    }
}