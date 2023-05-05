using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Elkcircle : MonoBehaviour
{
    [SerializeField] private LayerMask targets;
    [NonSerialized] public float basedmg;

    public void dealdmg()
    {
        if (Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 6f, targets, QueryTriggerInteraction.Ignore);
            foreach (Collider target in colliders)
            {
                if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
                {
                    target.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 2));
                    break;
                }
            }
        }
        gameObject.SetActive(false);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 6f);
    }*/
}
