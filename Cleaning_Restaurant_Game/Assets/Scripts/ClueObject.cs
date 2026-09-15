using UnityEngine;
using UnityEngine.EventSystems;

public class ClueObject : MonoBehaviour
{
    public Sprite clueSprite;
    public ClueDisplay clueDisplay;

    // GameManager를 여기에 연결해서, 클릭할 때 "단서 찾았어요"라고 알려줄 거예요
    public GameManager gameManager;

    // 이 단서를 이미 한 번 확인했는지 (중복으로 카운트되지 않게)
    private bool alreadyFound = false;

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        clueDisplay.ShowClue(clueSprite);

        // 처음 확인하는 거라면 카운트 올리기
        if (!alreadyFound)
        {
            alreadyFound = true;
            gameManager.AddClue();
        }
    }
}