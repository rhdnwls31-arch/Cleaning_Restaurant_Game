using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private int clueCount = 0;
    public int totalClues = 3;

    // 단서 개수(0,1,2,3)에 맞는 이미지들을 순서대로 담을 배열
    public Sprite[] clueCountSprites; // Inspector에서 0/3, 1/3, 2/3, 3/3 순서로 등록
    public Image clueCountDisplay;    // 화면에 보여줄 UI Image

    private int trashCleanedCount = 0;
    public int totalTrash = 4;
    public Image cleanBarFill;

    public Image endingImageUI;
    public CanvasGroup endingCanvasGroup;
    public float fadeDuration = 1.0f;

    private bool endingShown = false;

    void Start()
    {
        UpdateClueCountDisplay();
        UpdateCleanBar();
    }

    public void AddClue()
    {
        clueCount++;
        UpdateClueCountDisplay();
    }

    // 지금 단서 개수에 맞는 이미지로 통째로 교체
    void UpdateClueCountDisplay()
    {
        clueCountDisplay.sprite = clueCountSprites[clueCount];
    }

    public void AddTrashCleaned()
    {
        trashCleanedCount++;
        UpdateCleanBar();
        CheckEnding();
    }

    public void CheckEnding()
    {
        if (endingShown) return;
        if (clueCount >= totalClues && trashCleanedCount >= totalTrash)
        {
            endingShown = true;
            ShowEnding();
        }
    }

    void UpdateCleanBar()
    {
        cleanBarFill.fillAmount = (float)trashCleanedCount / totalTrash;
    }

    void ShowEnding()
    {
        endingImageUI.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            endingCanvasGroup.alpha = elapsed / fadeDuration;
            yield return null;
        }
        endingCanvasGroup.alpha = 1f;
    }
}