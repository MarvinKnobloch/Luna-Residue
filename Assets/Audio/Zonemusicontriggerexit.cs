using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zonemusicontriggerexit : MonoBehaviour
{
    [SerializeField] private int zonemusicint;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject && Statics.currentzonemusicint != zonemusicint)
        {
            if (Musiccontroller.instance.oldsongint != zonemusicint)
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
