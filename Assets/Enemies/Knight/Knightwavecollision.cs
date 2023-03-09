using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knightwavecollision : MonoBehaviour

{
    private float basedmg;

    private void OnEnable()
    {
        basedmg = GetComponentInParent<Knightwavecontroller>().basedmg;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(basedmg);
        }
    }
}
