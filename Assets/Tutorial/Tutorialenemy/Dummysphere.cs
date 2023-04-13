using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dummysphere : MonoBehaviour
{
    [SerializeField] private Dummyspezialcontroller dummycontroller;
    [SerializeField] private GameObject dazeimage;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float explodetime;

    [SerializeField] private LayerMask targets;

    private void OnEnable()
    {
        Invoke("dealdmg", explodetime);
    }
    private void dealdmg()
    {
        if (LoadCharmanager.Overallmainchar.GetComponent<Movescript>().state == Movescript.State.Buttonmashstun)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtogroundstate();
            Statics.dash = false;
            Statics.otheraction = false;
        }
        if (Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, targets, QueryTriggerInteraction.Ignore);
            foreach (Collider target in colliders)
            {
                if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
                {
                    target.GetComponent<Playerhp>().TakeDamage(basedmg + Globalplayercalculations.calculateenemyspezialdmg());
                    break;
                }
            }
            if(colliders.Length == 0)
            {
                dummycontroller.tutorialsuccess();
            }
            else dummycontroller.gameObject.SetActive(false);
        }
        else
        {
            dummycontroller.gameObject.SetActive(false);
        }
        dazeimage.SetActive(false);
        gameObject.SetActive(false);
    }
}
