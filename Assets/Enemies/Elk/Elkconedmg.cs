using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Elkconedmg : MonoBehaviour
{
    [SerializeField] private Elkcontroller elkcontroller;
    [NonSerialized] public float basedmg;
    private bool dmgonce;

    [SerializeField] private GameObject hiteffect;
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
                other.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 2), true);
                hiteffect.SetActive(true);
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
