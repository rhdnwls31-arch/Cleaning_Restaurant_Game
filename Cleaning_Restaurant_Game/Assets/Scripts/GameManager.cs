using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// 오브젝트가 어느 방 소속인지 구분하는 이름표
public enum RoomRegion { MainHall, StaffRoom }

public class GameManager : MonoBehaviour
{
    // 어디서든 GameManager.Instance로 이 오브젝트에 접근할 수 있게 함
    public static GameManager Instance;

    // ===== 메인 홀 진행 상황 =====
    private int mainHallClueCount = 0;
    public int mainHallTotalClues = 3;
    private int mainHallTrashCount = 0;
    public int mainHallTotalTrash = 4;

    // ===== 스태프방 진행 상황 =====
    private int staffClueCount = 0;
    public int staffTotalClues = 1;
    private int staffTrashCount = 0;
    public int staffTotalTrash = 2;

    // ===== 공통 =====
    public bool hasKey = false;
    private bool endingShown = false;
    public float fadeDuration = 1.0f;

    // 지금 활성화된 씬의 UI를 담아두는 칸 (씬 바뀔 때마다 다시 채워짐)
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
        // 이미 다른 GameManager가 존재하면(씬 재진입 등), 이 새 오브젝트는 없앰
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 이 오브젝트는 파괴 안 됨
    }

    // ===== 메인 홀 씬이 자기 UI를 등록할 때 호출 =====
    public void RegisterMainHallUI(Image clueDisplay, Sprite[] clueSprites, Image cleanBar, Image endingImg, CanvasGroup endingCg)
    {
        mainHallClueDisplay = clueDisplay;
        mainHallClueSprites = clueSprites;
        mainHallCleanBarFill = cleanBar;
        endingImageUI = endingImg;
        endingCanvasGroup = endingCg;

        UpdateMainHallClueDisplay();
        UpdateMainHallCleanBar();
    }

    // ===== 스태프방 씬이 자기 UI를 등록할 때 호출 =====
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

    // 메인 홀에 있을 때만 최종 엔딩 조건을 확인함
    public void CheckEnding()
    {
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
        if (mainHallClueDisplay != null && mainHallClueSprites != null)
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
        if (staffClueDisplay != null && staffClueSprites != null)
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