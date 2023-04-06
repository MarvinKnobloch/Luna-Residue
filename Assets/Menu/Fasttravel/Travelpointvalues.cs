using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Travelpoints", menuName = "New Travelpoints")]
public class Travelpointvalues : ScriptableObject
{
    public string travelpointname;
    public Vector3 travelcordinates;
    public Vector2 fasttravelmapcordinates;
}
