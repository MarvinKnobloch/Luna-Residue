#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Playerhp))]
public class Playerdmginui : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Playerhp myScript = (Playerhp)target;
        if (GUILayout.Button("Dealdmg"))
        {
            myScript.takedamagecheckiframes(myScript.maxhealth, false);
        }
    }
}
#endif
