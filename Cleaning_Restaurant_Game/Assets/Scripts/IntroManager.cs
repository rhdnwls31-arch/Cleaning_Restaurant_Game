using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
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
        introImage.sprite = slideImages[currentSlide];

        // 씬 시작할 때 까만 화면에서 서서히 밝아지기
        transitionCanvasGroup.alpha = 1f;
        StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 1f, 0f, sceneFadeDuration));
    }

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

    public void OnClickDoor()
    {
        if (isTransitioning) return;
        StartCoroutine(FadeToGameScene());
    }

    IEnumerator ChangeSlide(int index)
    {
        isTransitioning = true;

        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 0f, 1f, slideFadeDuration));
        introImage.sprite = slideImages[index];
        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 1f, 0f, slideFadeDuration));

        isTransitioning = false;
    }

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

    IEnumerator FadeToGameScene()
    {
        isTransitioning = true;

        doorButton.interactable = false;
        doorGlow.SetActive(false); // 추가: 문 클릭하는 순간 빛 효과를 바로 꺼버림

        yield return StartCoroutine(FadeCanvasGroup(transitionCanvasGroup, 0f, 1f, sceneFadeDuration));

        SceneManager.LoadScene("Game");
    }

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