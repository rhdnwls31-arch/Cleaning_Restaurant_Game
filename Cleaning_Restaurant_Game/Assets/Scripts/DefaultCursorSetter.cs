using UnityEngine;

public class DefaultCursorSetter : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Vector2 hotspot = Vector2.zero;

    void Start()
    {
        CursorHover.defaultCursor = defaultCursor;
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }
}