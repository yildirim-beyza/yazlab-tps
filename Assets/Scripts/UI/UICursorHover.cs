using UnityEngine;
using UnityEngine.EventSystems;

public class UICursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Cursor")]
    public Texture2D handCursor;
    public Vector2 hotspot = new Vector2(8, 0); // elin týklanan noktasý (baþparmak ucu gibi)

    public void OnPointerEnter(PointerEventData _)
    {
        if (handCursor) Cursor.SetCursor(handCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData _)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // varsayýlan
    }

    void OnDisable() => Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
}