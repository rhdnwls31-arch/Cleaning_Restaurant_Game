using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    // 메인 홀에서 정문으로 나갔을 때, 슬라이드 건너뛰고 바로 외관 화면부터 시작하기 위한 스위치
    public static bool skipToStorefront = false;

    public Sprite[] slideImages;
    public Sprite storefrontImage;

    public Image introImage;
    public CanvasGroup transitionCanvasGroup;
    public float slideFadeDuration = 0.4f;
    public float sceneFadeDuration = 0.8f;

    public Button screenClickButton;
    public GameObject doorGlow;
    public Button doorButton;

    private int currentSlide = 0;
    private bool isTransitioning = false;
    private bool atStorefront = false;

    void Start()
    {
        // 메인 홀에서 돌아온 경우, 슬라이드 없이 바로 외관 화면으로
        if (skipToStorefront)
        {
            skipToStorefront = false;
            StartCoroutine(ShowStorefrontDirect());
            return;
        }

        introImage.sprite = slideImages[currentSlide];
        transitionCanvasGroup.alpha = 1f;
        StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 1f, 0f, sceneFadeDuration));
    }

    // 화면 클릭(슬라이드 넘기기)
    public void OnClickNext()
    {
        if (isTransitioning || atStorefront) return;

        currentSlide++;

        if (currentSlide >= slideImages.Length)
        {
            StartCoroutine(ShowStorefront());
        }
        else
        {
            StartCoroutine(ChangeSlide(currentSlide));
        }
    }

    // 문 클릭 - 게임 씬으로 전환
    public void OnClickDoor()
    {
        if (isTransitioning) return;
        StartCoroutine(FadeToGameScene());
    }

    // 슬라이드 사이 전환: 검은 화면으로 덮었다가 그림 교체 후 걷기
    IEnumerator ChangeSlide(int index)
    {
        isTransitioning = true;

        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 0f, 1f, slideFadeDuration));
        introImage.sprite = slideImages[index];
        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 1f, 0f, slideFadeDuration));

        isTransitioning = false;
    }

    // 슬라이드가 다 끝나면 가게 외관으로 전환하고 문 활성화
    IEnumerator ShowStorefront()
    {
        isTransitioning = true;

        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 0f, 1f, slideFadeDuration));
        introImage.sprite = storefrontImage;
        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 1f, 0f, slideFadeDuration));

        atStorefront = true;
        screenClickButton.interactable = false;
        doorGlow.SetActive(true);
        doorButton.interactable = true;

        isTransitioning = false;
    }

    // 메인 홀에서 돌아왔을 때: 페이드인하면서 곧바로 외관 화면 + 문 활성화까지 처리
    IEnumerator ShowStorefrontDirect()
    {
        introImage.sprite = storefrontImage;
        atStorefront = true;
        screenClickButton.interactable = false;
        doorGlow.SetActive(true);
        doorButton.interactable = true;

        transitionCanvasGroup.alpha = 1f;
        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 1f, 0f, sceneFadeDuration));
    }

    // 문 클릭 후 Game 씬으로 전환
    IEnumerator FadeToGameScene()
    {
        isTransitioning = true;

        doorButton.interactable = false;
        doorGlow.SetActive(false);
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