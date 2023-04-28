using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Playerhp))]
public class Dealplayerdmg : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Playerhp myScript = (Playerhp)target;
        if (GUILayout.Button("Dealdmg"))
        {
            myScript.TakeDamage(myScript.maxhealth);
        }
    }
}
