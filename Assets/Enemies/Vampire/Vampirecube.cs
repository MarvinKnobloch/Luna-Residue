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
    private void OnEnable()
    {
        Invoke("dealdmg", explodetime);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(transform.position, new Vector3(15f, 0.8f, 15f));
    //}
    private void dealdmg()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(16f, 0.8f, 16f), Quaternion.identity, Aoetargets);
        foreach (Collider target in colliders)
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
        vampirecontroller.SetActive(false);
        gameObject.SetActive(false);
    }
}

