using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fishmancolliderdmg : MonoBehaviour
{
    [SerializeField] private GameObject colliderpreview;

    private bool dealdmgonce;
    [NonSerialized] public float basedmg;

    private void OnEnable()
    {
        StartCoroutine("turnoff");
        dealdmgonce = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Statics.infight == true)
        {
            if (other.gameObject == LoadCharmanager.Overallmainchar && dealdmgonce == false)
            {
                dealdmgonce = true;
                other.GetComponent<Playerhp>().TakeDamage(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 2));
            }
        }
    }
    IEnumerator turnoff()
    {
        yield return null;
        StopAllCoroutines();
        gameObject.SetActive(false);
        colliderpreview.SetActive(false);
    }
}
