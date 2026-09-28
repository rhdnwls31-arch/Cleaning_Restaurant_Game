using UnityEngine;

public class DraggableTrash : MonoBehaviour
{
    public Transform trashBin;
    public float distanceToDelete = 1.0f;
    public RoomRegion region = RoomRegion.MainHall;
    public GameObject hiddenKey;

    // 추가: 이 쓰레기만의 고유 이름표 (Inspector에서 직접 입력)
    public string uniqueId;

    private bool isDragging = false;

    void Start()
    {
        // 추가: 예전에 이미 치운 적 있는 쓰레기라면, 씬이 다시 로드돼도 곧바로 사라지게
        if (!string.IsNullOrEmpty(uniqueId) && GameManager.Instance.IsCollected(uniqueId))
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;

        if (hiddenKey != null)
        {
            hiddenKey.SetActive(true);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        float distance = Vector3.Distance(transform.position, trashBin.position);

        if (distance < distanceToDelete)
        {
            GameManager.Instance.AddTrashCleaned(region);

            // 추가: 이 쓰레기를 치웠다고 기록
            if (!string.IsNullOrEmpty(uniqueId))
            {
                GameManager.Instance.MarkCollected(uniqueId);
            }

            Destroy(gameObject);
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