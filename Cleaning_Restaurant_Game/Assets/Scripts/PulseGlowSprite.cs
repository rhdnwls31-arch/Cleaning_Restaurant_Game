using UnityEngine;

public class PulseGlowSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float speed = 2f;
    public float minAlpha = 0.15f;
    public float maxAlpha = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);

        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}