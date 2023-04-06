using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Fasttravelpointunlock))]
public class Addfasttravelpoint : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Fasttravelpointunlock myScript = (Fasttravelpointunlock)target;
        if (GUILayout.Button("add fasttravelpoint"))
        {
            myScript.addtravelpoint();
        }
    }
}
