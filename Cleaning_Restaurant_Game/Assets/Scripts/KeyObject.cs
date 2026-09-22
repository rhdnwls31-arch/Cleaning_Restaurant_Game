using UnityEngine;
using UnityEngine.EventSystems;

public class KeyObject : MonoBehaviour
{
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        GameManager.Instance.CollectKey();
        Destroy(gameObject);
    }
}