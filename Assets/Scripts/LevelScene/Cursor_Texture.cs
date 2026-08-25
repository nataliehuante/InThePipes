using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Texture : MonoBehaviour
{
    public Texture2D cursorTexture;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }
}
