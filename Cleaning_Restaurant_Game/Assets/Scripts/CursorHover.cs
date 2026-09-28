using UnityEngine;

public class CursorHover : MonoBehaviour
{
    public Texture2D hoverCursor;

    // 모든 오브젝트가 공통으로 참조할 기본 커서
    public static Texture2D defaultCursor;

    public Vector2 hotspot = Vector2.zero;

    void OnMouseEnter()
    {
        Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }
}