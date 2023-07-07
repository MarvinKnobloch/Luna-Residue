using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Werewolfsphere : MonoBehaviour
{
    [SerializeField] private GameObject dazeimage;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float dmglevel;
    [NonSerialized] public float explodetime;
    [SerializeField] private GameObject explosioneffect;

    [SerializeField] private LayerMask targets;

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
        Gizmos.DrawSphere(transform.position, 5f);
    }*/
    private void dealdmg()
    {
        if (Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, targets, QueryTriggerInteraction.Ignore);
            foreach (Collider target in colliders)
            {
                if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
                {
                    target.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 1), true);
                    break;
                }
            }
            enemyspezialsound.playwerewolfexplosionspezial();
        }
        if(LoadCharmanager.Overallmainchar.GetComponent<Movescript>().state == Movescript.State.Buttonmashstun)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
            Statics.dash = false;
            Statics.otheraction = false;
        }
        dazeimage.SetActive(false);
        explosioneffect.transform.position = transform.position;
        explosioneffect.SetActive(true);
        gameObject.SetActive(false);
    }
}

