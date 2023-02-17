using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouseactivate
{
    public static void enablemouse()
    {
#if !UNITY_EDITOR
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
#endif
    }
    public static void disablemouse()
    {
#if !UNITY_EDITOR
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
#endif
    }
}
