using UnityEngine;
using UnityEngine.EventSystems;

public class ClueObject : MonoBehaviour
{
    public Sprite clueSprite;
    public ClueDisplay clueDisplay;

    // 추가: 이 단서가 메인 홀 것인지 스태프방 것인지 Inspector에서 선택
    public RoomRegion region = RoomRegion.MainHall;

    private bool alreadyFound = false;

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
            GameManager.Instance.AddClue(region); // 이제 Inspector 연결 없이 바로 접근
        }
    }
}