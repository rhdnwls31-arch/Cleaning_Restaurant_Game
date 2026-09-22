using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnDoor : MonoBehaviour
{
    public CanvasGroup transitionCanvasGroup;
    public float fadeDuration = 0.8f;
    private bool isTransitioning = false;

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (isTransitioning)
        {
            return;
        }

        StartCoroutine(GoBackToMainHall());
    }

    IEnumerator GoBackToMainHall()
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

        SceneManager.LoadScene("Game");
    }
}