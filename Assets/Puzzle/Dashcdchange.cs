using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashcdchange : MonoBehaviour
{
    public float newdashcd;
    public float newdashcost;
    private float normaldashcd = 4f;
    private float normaldashcost = 2f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar)
        {
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
            Statics.dashcd = normaldashcd;
            Statics.dashcost = normaldashcost;
            Statics.dashcdmissingtime = normaldashcost + Statics.dashcost;
            GlobalCD.startdashcd();
        }
    }
}
