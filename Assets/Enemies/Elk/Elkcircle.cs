using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Elkcircle : MonoBehaviour
{
    [SerializeField] private LayerMask Aoetargets;
    [NonSerialized] public float basedmg;

    public void dealdmg()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f, Aoetargets);
        foreach (Collider target in colliders)
        {
            if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
            {
                target.GetComponent<SpielerHP>().TakeDamage(basedmg);
            }
        }
        gameObject.SetActive(false);
    }
}
