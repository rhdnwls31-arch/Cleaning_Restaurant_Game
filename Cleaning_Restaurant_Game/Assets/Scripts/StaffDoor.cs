using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class StaffDoor : MonoBehaviour
{
    public CanvasGroup transitionCanvasGroup;
    public float fadeDuration = 0.8f;
    private bool isTransitioning = false;

    void OnMouseDown()
    {
        Debug.Log("문 클릭됨! hasKey = " + GameManager.Instance.hasKey);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // UI 클릭이랑 겹치는 거 방지
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // 열쇠가 없으면 문이 반응하지 않음 (잠겨있음)
        if (!GameManager.Instance.hasKey)
        {
            return;
        }

        if (isTransitioning)
        {
            return;
        }

        StartCoroutine(GoToStaffRoom());
    }

    IEnumerator GoToStaffRoom()
    {
        isTransitioning = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            transitionCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            yield return null;
        }
        transitionCanvasGroup.alpha = 1f;

        SceneManager.LoadScene("StaffRoom");
    }
}