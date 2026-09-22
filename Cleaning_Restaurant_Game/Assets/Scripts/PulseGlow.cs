using UnityEngine;
using UnityEngine.UI;

public class PulseGlow : MonoBehaviour
{
    private Image image;
    public float speed = 2f;
    public float minAlpha = 0.3f;
    public float maxAlpha = 0.8f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);

        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
}