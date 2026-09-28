using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.1f;   // 마우스 올렸을 때 커지는 배율
    public float animDuration = 0.15f; // 커지고 줄어드는 데 걸리는 시간

    private Vector3 originalScale;
    private Coroutine currentAnim;

    void Start()
    {
        originalScale = transform.localScale;
    }

    // 마우스가 버튼 위에 올라왔을 때 자동으로 호출됨
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentAnim != null) StopCoroutine(currentAnim);
        currentAnim = StartCoroutine(ScaleTo(originalScale * hoverScale));
    }

    // 마우스가 버튼에서 벗어났을 때 자동으로 호출됨
    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentAnim != null) StopCoroutine(currentAnim);
        currentAnim = StartCoroutine(ScaleTo(originalScale));
    }

    IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / animDuration);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}