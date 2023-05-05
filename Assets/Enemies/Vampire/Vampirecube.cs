using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vampirecube : MonoBehaviour
{
    [SerializeField] private GameObject vampirecontroller;
    [SerializeField] private LayerMask target;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float explodetime;
    [NonSerialized] public Vector3 overlapboxpoint;
    private void OnEnable()
    {
        Invoke("dealdmg", explodetime);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(10f, 0.8f, 10f));
    }*/
    private void dealdmg()
    {
        if (Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapBox(overlapboxpoint, new Vector3(8f, 0.5f, 8f), Quaternion.identity, target, QueryTriggerInteraction.Ignore);
            foreach (Collider target in colliders)
            {
                if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
                {
                    target.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 3));
                    break;
                }
            }
        }
        vampirecontroller.SetActive(false);
        gameObject.SetActive(false);
    }
}

