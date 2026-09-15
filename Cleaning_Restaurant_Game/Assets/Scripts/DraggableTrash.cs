using UnityEngine;

public class DraggableTrash : MonoBehaviour
{
    public Transform trashBin;
    public float distanceToDelete = 1.0f;

    // 추가: 쓰레기 치웠을 때 GameManager한테 알려주기 위해 연결
    public GameManager gameManager;

    private bool isDragging = false;

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        float distance = Vector3.Distance(transform.position, trashBin.position);

        if (distance < distanceToDelete)
        {
            // 추가: 사라지기 전에 GameManager한테 "쓰레기 하나 치웠어요" 알려주기
            if (gameManager != null)
            {
                gameManager.AddTrashCleaned();
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