using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vampirecube : MonoBehaviour
{
    [SerializeField] private GameObject vampirecontroller;
    [SerializeField] private LayerMask Aoetargets;
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
        Collider[] colliders = Physics.OverlapBox(overlapboxpoint, new Vector3(8f, 0.5f, 8f), Quaternion.identity, Aoetargets, QueryTriggerInteraction.Ignore);
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
        vampirecontroller.SetActive(false);
        gameObject.SetActive(false);
    }
}

