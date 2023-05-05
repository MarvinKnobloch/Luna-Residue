using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vampirecircle : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float explodetime;
    [NonSerialized] public Vector3 overlapspherepoint;

    private void OnEnable()
    {
        Invoke("dealdmg", explodetime);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 3f);
    }*/
    private void dealdmg()
    {
        if (Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(overlapspherepoint, 3f, player, QueryTriggerInteraction.Ignore);
            foreach (Collider target in colliders)
            {
                if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
                {
                    target.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 3));
                    break;
                }
            }
        }
        gameObject.SetActive(false);
    }
}

