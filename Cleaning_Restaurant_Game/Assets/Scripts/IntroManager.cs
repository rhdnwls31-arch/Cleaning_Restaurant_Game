using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Sprite[] slideImages;
    public Image introImage;

    // 슬라이드 디졸브용
    public CanvasGroup introCanvasGroup;
    public float slideFadeDuration = 0.3f;

    // 게임 씬 전환용 검은 화면
    public CanvasGroup transitionCanvasGroup;
    public float sceneFadeDuration = 1.5f;

    private int currentSlide = 0;
    private bool isTransitioning = false; // 페이드 도중 중복 클릭 방지

    void Start()
    {
        introImage.sprite = slideImages[currentSlide];
        introCanvasGroup.alpha = 1f;

        // 추가: 씬 시작할 때 까만 화면에서 서서히 밝아지기
        transitionCanvasGroup.alpha = 0.8f;
        StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 1f, 0f, sceneFadeDuration));
    }

    public void OnClickNext()
    {
        if (isTransitioning) return; // 페이드 중이면 클릭 무시

        currentSlide++;

        if (currentSlide >= slideImages.Length)
        {
            StartCoroutine(FadeToGameScene());
        }
        else
        {
            StartCoroutine(ChangeSlide(currentSlide));
        }
    }

    // 슬라이드 전환: 사라짐 → 그림 교체 → 나타남
    IEnumerator ChangeSlide(int index)
    {
        isTransitioning = true;

        // 검은 화면으로 덮기
        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 0f, 1f, slideFadeDuration));

        // 화면이 까맣게 가려진 상태에서 그림 교체
        introImage.sprite = slideImages[index];

        // 검은 화면 걷어내기
        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 1f, 0f, slideFadeDuration));

        isTransitioning = false;
    }

    // 게임 씬 전환: 화면이 서서히 까매진 뒤 씬 전환
    IEnumerator FadeToGameScene()
    {
        isTransitioning = true;

        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 0f, 1f, sceneFadeDuration));

        SceneManager.LoadScene("Game");
    }

    // 여러 곳에서 재사용하는 공용 페이드 함수
    IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        cg.alpha = to;
    }
}