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
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1.25f);
    }
    private void dealdmg()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.25f, Aoetargets,QueryTriggerInteraction.Ignore);
        foreach (Collider target in colliders)
        {
            if (target.TryGetComponent(out Playerhp playerhp))
            {
                if (Statics.infight == true)
                {
                    if (target.GetComponent<Supportmovement>())
                    {
                        playerhp.TakeDamage(Mathf.Round(basedmg / 3));
                    }
                    else
                    {
                        playerhp.TakeDamage(basedmg);
                    }
                }
            }
        }
        gameObject.SetActive(false);
    }
}

