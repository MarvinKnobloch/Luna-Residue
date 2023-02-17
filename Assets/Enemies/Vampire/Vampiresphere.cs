using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vampiresphere : MonoBehaviour
{
    [SerializeField] private LayerMask Aoetargets;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float explodetime;

    private void OnEnable()
    {
        Invoke("dealdmg", explodetime);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 2.5f);
    }*/
    private void dealdmg()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f, Aoetargets);
        foreach (Collider target in colliders)
        {
            if (target.GetComponent<Playerhp>())
            {
                if (target.GetComponent<Supportmovement>())
                {
                    target.GetComponent<Playerhp>().TakeDamage(Mathf.Round(basedmg / 3));
                }
                else
                {
                    target.GetComponent<Playerhp>().TakeDamage(basedmg);
                }
            }
        }
        gameObject.SetActive(false);
    }
}

