using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "New Quest")]
public class Quests : ScriptableObject
{
    public string questname;
    public int questid;
    public bool questactiv;
    public bool questcomplete;
    public Vector3 mapvalues;
}
