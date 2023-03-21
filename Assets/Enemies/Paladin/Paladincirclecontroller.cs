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
        if(Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(dmgposi, 7f, playerlayer, QueryTriggerInteraction.Ignore);
            foreach (Collider players in colliders)
            {
                if (players.gameObject == LoadCharmanager.Overallmainchar)
                {
                    LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(basedmg + Globalplayercalculations.calculateenemyspezialdmg());
                    break;
                }
            }
        }
        gameObject.SetActive(false);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 7f);
    }*/
}
