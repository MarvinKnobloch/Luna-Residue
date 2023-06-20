using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;

    void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(0.5f, 0.5f), CursorMode.ForceSoftware);
    }
}
