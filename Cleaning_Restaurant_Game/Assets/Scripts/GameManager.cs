using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public enum RoomRegion { MainHall, StaffRoom }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int mainHallClueCount = 0;
    public int mainHallTotalClues = 3;
    private int mainHallTrashCount = 0;
    public int mainHallTotalTrash = 8;

    private int staffClueCount = 0;
    public int staffTotalClues = 2;
    private int staffTrashCount = 0;
    public int staffTotalTrash = 4;

    public bool hasKey = false;
    private bool endingShown = false;
    public float fadeDuration = 1.0f;
    public bool enableEndingCheck = false;

    // 추가: 이미 처리된 오브젝트의 고유 ID를 기억하는 목록
    private HashSet<string> collectedIds = new HashSet<string>();

    public void OnClickBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private Image mainHallClueDisplay;
    private Sprite[] mainHallClueSprites;
    private Image mainHallCleanBarFill;
    private Image endingImageUI;
    private CanvasGroup endingCanvasGroup;

    private Image staffClueDisplay;
    private Sprite[] staffClueSprites;
    private Image staffCleanBarFill;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 추가: 이 ID가 이미 처리됐는지 확인
    public bool IsCollected(string id)
    {
        return collectedIds.Contains(id);
    }

    // 추가: 이 ID를 "처리 완료"로 기록
    public void MarkCollected(string id)
    {
        collectedIds.Add(id);
    }

    public void RegisterMainHallUI(Image clueDisplay, Sprite[] clueSprites, Image cleanBar, Image endingImg, CanvasGroup endingCg)
    {
        mainHallClueDisplay = clueDisplay;
        mainHallClueSprites = clueSprites;
        mainHallCleanBarFill = cleanBar;
        endingImageUI = endingImg;
        endingCanvasGroup = endingCg;

        UpdateMainHallClueDisplay();
        UpdateMainHallCleanBar();

        // 추가: 메인 홀에 들어올 때마다 엔딩 조건 자동 확인 (문 클릭 안 해도 됨)
        CheckEnding();
    }

    public void RegisterStaffRoomUI(Image clueDisplay, Sprite[] clueSprites, Image cleanBar)
    {
        staffClueDisplay = clueDisplay;
        staffClueSprites = clueSprites;
        staffCleanBarFill = cleanBar;

        UpdateStaffClueDisplay();
        UpdateStaffCleanBar();
    }

    public void AddClue(RoomRegion region)
    {
        if (region == RoomRegion.MainHall)
        {
            mainHallClueCount++;
            UpdateMainHallClueDisplay();
        }
        else
        {
            staffClueCount++;
            UpdateStaffClueDisplay();
        }
    }

    public void AddTrashCleaned(RoomRegion region)
    {
        if (region == RoomRegion.MainHall)
        {
            mainHallTrashCount++;
            UpdateMainHallCleanBar();
        }
        else
        {
            staffTrashCount++;
            UpdateStaffCleanBar();
        }

        CheckEnding();
    }

    public void CollectKey()
    {
        hasKey = true;
    }

    public bool IsStaffRoomComplete()
    {
        return staffClueCount >= staffTotalClues && staffTrashCount >= staffTotalTrash;
    }

    public void CheckEnding()
    {
        if (!enableEndingCheck) return;
        if (endingShown) return;
        if (SceneManager.GetActiveScene().name != "Game") return;

        bool mainHallDone = mainHallClueCount >= mainHallTotalClues && mainHallTrashCount >= mainHallTotalTrash;

        if (mainHallDone && IsStaffRoomComplete())
        {
            endingShown = true;
            ShowEnding();
        }
    }

    void UpdateMainHallClueDisplay()
    {
        if (mainHallClueDisplay != null && mainHallClueSprites != null && mainHallClueCount < mainHallClueSprites.Length)
        {
            mainHallClueDisplay.sprite = mainHallClueSprites[mainHallClueCount];
        }
    }

    void UpdateMainHallCleanBar()
    {
        if (mainHallCleanBarFill != null)
        {
            mainHallCleanBarFill.fillAmount = (float)mainHallTrashCount / mainHallTotalTrash;
        }
    }

    void UpdateStaffClueDisplay()
    {
        if (staffClueDisplay != null && staffClueSprites != null && staffClueCount < staffClueSprites.Length)
        {
            staffClueDisplay.sprite = staffClueSprites[staffClueCount];
        }
    }

    void UpdateStaffCleanBar()
    {
        if (staffCleanBarFill != null)
        {
            staffCleanBarFill.fillAmount = (float)staffTrashCount / staffTotalTrash;
        }
    }

    void ShowEnding()
    {
        if (endingImageUI == null) return;
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