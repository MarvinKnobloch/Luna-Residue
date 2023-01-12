using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Paladincirclecontroller : MonoBehaviour
{
    private Vector3 dmgposi;
    public LayerMask playerlayer;

    [NonSerialized] public float basedmg;
    [NonSerialized] public float dodgetime;
    private void OnEnable()
    {
        Invoke("dealdmg", dodgetime);
    }
    private void dealdmg()
    {
        dmgposi = transform.position;
        dmgposi.y = LoadCharmanager.Overallmainchar.transform.position.y;
        Collider[] colliders = Physics.OverlapSphere(dmgposi, 7f, playerlayer);
        foreach (Collider players in colliders)
        {
            if (players.gameObject == LoadCharmanager.Overallmainchar)
            {
                LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().TakeDamage(basedmg);
            }
        }
        gameObject.SetActive(false);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 5f);
    }*/
}
