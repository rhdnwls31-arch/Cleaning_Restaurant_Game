using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private int clueCount = 0;
    public int totalClues = 3;
    public TextMeshProUGUI clueCountText;

    // 쓰레기 치운 개수도 똑같이 관리
    private int trashCleanedCount = 0;
    public int totalTrash = 2; // 지금 만든 쓰레기 개수(Trash_1, Trash_2)

    public Image endingImageUI;
    public CanvasGroup endingCanvasGroup;
    public float fadeDuration = 1.0f;

    // 엔딩이 이미 떴는지 (중복 실행 방지)
    private bool endingShown = false;

    void Start()
    {
        UpdateClueCountText();
    }

    // 단서 찾았을 때 - 개수만 올리고, 엔딩 조건은 여기서 확인 안 함
    public void AddClue()
    {
        clueCount++;
        UpdateClueCountText();
    }

    // 쓰레기 치웠을 때 - 개수 올리고, 여기서는 바로 조건 확인
    // (쓰레기는 화면 가릴 UI가 없으니 즉시 확인해도 괜찮음)
    public void AddTrashCleaned()
    {
        trashCleanedCount++;
        CheckEnding();
    }

    // 단서 이미지를 닫았을 때 ClueDisplay가 불러줄 함수
    public void CheckEnding()
    {
        if (endingShown)
        {
            return; // 이미 떴으면 다시 안 뜨게
        }

        if (clueCount >= totalClues && trashCleanedCount >= totalTrash)
        {
            endingShown = true;
            ShowEnding();
        }
    }

    void UpdateClueCountText()
    {
        clueCountText.text = "단서 " + clueCount + " / " + totalClues;
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