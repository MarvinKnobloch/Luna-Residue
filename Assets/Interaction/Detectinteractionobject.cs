using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Detectinteractionobject : MonoBehaviour
{
    public static event Action enableinteractionfield;
    public static event Action disableinteractionfield;

    private void OnDisable()
    {
        Statics.interactionobjects.Remove(this.gameObject);

        if (Statics.interactionobjects.Count == 0)
        {
            disableinteractionfield?.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && enabled == true)                     //enabled == true, hatte schon das problem das die textbox aufgetaucht ist, obwohl das script deaktviert war
        {
            enableinteractionfield?.Invoke();
            if (Statics.interactionobjects.Contains(this.gameObject) == false)
            {
                Statics.interactionobjects.Add(this.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            Statics.interactionobjects.Remove(this.gameObject);

            if(Statics.interactionobjects.Count == 0)
            {
                disableinteractionfield?.Invoke();
            }
        }
    }
}
