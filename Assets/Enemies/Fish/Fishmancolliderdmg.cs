using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fishmancolliderdmg : MonoBehaviour
{
    [SerializeField] private GameObject redline;
    private bool timerstart;

    [NonSerialized] public float basedmg;
    private void OnEnable()
    {
        timerstart = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(timerstart == false)
        {
            timerstart = true;
            StartCoroutine("turnoff");
        }
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            Debug.Log("dealdmg");
            LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().TakeDamage(basedmg);
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
