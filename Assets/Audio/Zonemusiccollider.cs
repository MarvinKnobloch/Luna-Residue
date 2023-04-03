using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zonemusiccollider : MonoBehaviour
{
    [SerializeField] private int zonemusicint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject && Statics.currentzonemusicint != zonemusicint)
        {
            if(Musiccontroller.instance.oldsongint != zonemusicint)
            {
                Musiccontroller.instance.enternewzone(zonemusicint);              
            }
            else
            {
                Musiccontroller.instance.enteroldzone(zonemusicint);
            }
        }
    }
}
