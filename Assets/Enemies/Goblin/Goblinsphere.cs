using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goblinsphere : MonoBehaviour
{
    [NonSerialized] public float basedmg;
    [NonSerialized] public float timetoexplode;

    [SerializeField] private LayerMask Aoetargets;
    private void OnEnable()
    {
        Invoke("dealdmg", timetoexplode);
    }
    private void dealdmg()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, Aoetargets);
        foreach (Collider target in colliders)
        {
            if (target.GetComponent<SpielerHP>())
            {
                if (target.GetComponent<Supportmovement>())
                {
                    target.GetComponent<SpielerHP>().TakeDamage(basedmg / 3);
                }
                else
                {
                    target.GetComponent<SpielerHP>().TakeDamage(basedmg);
                }
            }
        }
        gameObject.SetActive(false);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 3f);
    }*/
}

