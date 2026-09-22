using UnityEngine;

public class MovableObject : MonoBehaviour
{
    // 이 오브젝트 밑에 숨겨진 게 있다면 연결 (없으면 비워둬도 됨)
    public GameObject hiddenItem;

    // 원래 자리에서 이만큼 이상 멀어지면 밑에 숨긴 걸 드러냄
    public float revealDistance = 1.0f;

    private Vector3 startPosition; // 처음 놓여있던 자리를 기억
    private bool isDragging = false;
    private bool alreadyRevealed = false;

    void Start()
    {
        // 게임 시작할 때, 지금 위치를 "원래 자리"로 저장해둠
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // 원래 자리에서 얼마나 멀어졌는지 계산
        float movedDistance = Vector3.Distance(transform.position, startPosition);

        if (movedDistance > revealDistance && hiddenItem != null && !alreadyRevealed)
        {
            hiddenItem.SetActive(true);
            alreadyRevealed = true;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            transform.position = mouseWorldPosition;
        }
    }
}