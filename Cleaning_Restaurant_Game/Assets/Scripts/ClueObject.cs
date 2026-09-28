using UnityEngine;
using UnityEngine.EventSystems;

public class ClueObject : MonoBehaviour
{
    public Sprite clueSprite;
    public ClueDisplay clueDisplay;
    public RoomRegion region = RoomRegion.MainHall;

    // 추가: 이 단서만의 고유 이름표
    public string uniqueId;

    private bool alreadyFound = false;

    void Start()
    {
        // 추가: 이미 확인했던 단서라면, 씬이 다시 로드돼도 카운트가 또 올라가지 않게
        if (!string.IsNullOrEmpty(uniqueId) && GameManager.Instance.IsCollected(uniqueId))
        {
            alreadyFound = true;
        }
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        clueDisplay.ShowClue(clueSprite);

        if (!alreadyFound)
        {
            alreadyFound = true;
            GameManager.Instance.AddClue(region);

            if (!string.IsNullOrEmpty(uniqueId))
            {
                GameManager.Instance.MarkCollected(uniqueId);
            }
        }
    }
}