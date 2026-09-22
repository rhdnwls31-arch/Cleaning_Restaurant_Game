using UnityEngine;

public class DraggableTrash : MonoBehaviour
{
    public Transform trashBin;
    public float distanceToDelete = 1.0f;

    // 추가: 이 쓰레기가 메인 홀 것인지 스태프방 것인지
    public RoomRegion region = RoomRegion.MainHall;

    public GameObject hiddenKey;

    private bool isDragging = false;

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