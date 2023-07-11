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

    [SerializeField] private GameObject cubeeffect;

    private Enemyspezialsound enemyspezialsound;

    private void Awake()
    {
        enemyspezialsound = GetComponentInParent<Enemyspezialsound>();
    }
    private void OnEnable()
    {
        Invoke("dealdmg", explodetime);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(8f, 0.8f, 8f));
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
                    target.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 3), true);
                    break;
                }
            }
        }
        enemyspezialsound.playvampireendspezial();
        cubeeffect.transform.position = transform.position;
        cubeeffect.SetActive(true);
        vampirecontroller.GetComponent<Vampirecontroller>().controllerdisablestart();
        gameObject.SetActive(false);
    }
}

