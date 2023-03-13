using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fishmancolliderdmg : MonoBehaviour
{
    [SerializeField] private GameObject redline;

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
                other.GetComponent<Playerhp>().TakeDamage(basedmg + Globalplayercalculations.calculateenemyspezialdmg());
            }
        }
    }
    IEnumerator turnoff()
    {
        yield return null;
        StopAllCoroutines();
        gameObject.SetActive(false);
        redline.SetActive(false);
    }
}
