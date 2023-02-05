using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPageMouseoverPointer : MonoBehaviour
{
    public Texture2D cursorArrow;
    public Texture2D cursorHand;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
}
