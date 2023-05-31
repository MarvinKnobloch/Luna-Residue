#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Expmanager))]
public class Gainexp : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Expmanager myScript = (Expmanager)target;
        if (GUILayout.Button("Expgain"))
        {
            myScript.gainexp(100);
        }
    }
}
#endif
