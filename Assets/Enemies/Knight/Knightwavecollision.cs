using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knightwavecollision : MonoBehaviour
{
    private bool dmgonce;
    private float basedmg;
    private Knightwavecontroller knightwavecontroller;

    private void OnEnable()
    {
        knightwavecontroller = GetComponentInParent<Knightwavecontroller>();
        basedmg = knightwavecontroller.basedmg;
        dmgonce = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(Statics.infight == true && dmgonce == false)
        {
            if (other.gameObject == LoadCharmanager.Overallmainchar)
            {
                dmgonce = true;
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 3));
            }
        }
    }
}
