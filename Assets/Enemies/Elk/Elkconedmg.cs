using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Elkconedmg : MonoBehaviour
{
    [SerializeField] private Elkcontroller elkcontroller;
    [NonSerialized] public float basedmg;
    private bool dmgonce;
    private void OnEnable()
    {
        StartCoroutine(oneframe());
        dmgonce = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(Statics.infight == true && dmgonce == false)
        {
            if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
            {
                dmgonce = true;
                other.GetComponent<Playerhp>().TakeDamage(basedmg + Globalplayercalculations.calculateenemyspezialdmg());
            }
        }
    }
    IEnumerator oneframe()
    {
        yield return null;
        StopAllCoroutines();
        elkcontroller.conedisable();
    }
}
