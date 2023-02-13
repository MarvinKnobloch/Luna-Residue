using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Elkconedmg : MonoBehaviour
{
    [SerializeField] private GameObject elkcontroller;
    [NonSerialized] public float basedmg;
    private bool timerstart;
    private void OnEnable()
    {
        timerstart = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (timerstart == false)
        {
            timerstart = true;
            StartCoroutine(oneframe());
        }
        if (other.gameObject.GetComponent<Playerhp>())
        {
            if (other.GetComponent<Supportmovement>())
            {
                other.GetComponent<Playerhp>().TakeDamage(Mathf.Round(basedmg / 3));
            }
            else
            {
                other.GetComponent<Playerhp>().TakeDamage(basedmg);
            }
        }
    }
    IEnumerator oneframe()
    {
        yield return null;
        StopAllCoroutines();
        elkcontroller.GetComponent<Elkcontroller>().conedisable();
        gameObject.SetActive(false);
    }
}
