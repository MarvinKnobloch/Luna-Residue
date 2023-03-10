using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Werewolfsphere : MonoBehaviour
{
    [SerializeField] private GameObject werewolfcontroller;
    [SerializeField] private GameObject dazeimage;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float explodetime;

    [SerializeField] private LayerMask Aoetargets;

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
        werewolfcontroller.SetActive(false);
        gameObject.SetActive(false);
        if (Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, Aoetargets);
            foreach (Collider target in colliders)
            {
                if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
                {
                    target.GetComponent<Playerhp>().TakeDamage(basedmg);
                    break;
                }
            }
        }
        if(LoadCharmanager.Overallmainchar.GetComponent<Movescript>().state == Movescript.State.Buttonmashstun)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
            Statics.dash = false;
            Statics.otheraction = false;
            dazeimage.SetActive(false);
        }
        werewolfcontroller.SetActive(false);
        gameObject.SetActive(false);
    }
}

