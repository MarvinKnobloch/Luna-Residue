using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enterpuzzlezone : MonoBehaviour
{
    public float newdashcd;
    public float newdashcost;
    private float normaldashcd;
    private float normaldashcost;
    private void Awake()
    {
        normaldashcd = Statics.dashcd;
        normaldashcost = Statics.dashcost;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar)
        {
            LoadCharmanager.cantsavehere = true;
            Statics.dashcd = newdashcd;
            Statics.dashcost = newdashcost;
            Statics.dashcdmissingtime = newdashcost + Statics.dashcost;             // + dashcost weil beim globalstart einmal dashcosts abgezogen wird
            GlobalCD.startdashcd();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            LoadCharmanager.cantsavehere = false;
            Statics.dashcd = normaldashcd;
            Statics.dashcost = normaldashcost;
            Statics.dashcdmissingtime = normaldashcost + Statics.dashcost;
            GlobalCD.startdashcd();
        }
    }
}
