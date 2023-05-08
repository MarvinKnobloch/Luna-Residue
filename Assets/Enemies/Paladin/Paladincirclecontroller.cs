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

    [SerializeField] private Enemyspezialsound enemyspezialsound;
    private void OnEnable()
    {
        Invoke("dealdmg", dodgetime);
    }
    private void dealdmg()
    {
        enemyspezialsound.playpaladinspezial();
        dmgposi = transform.position;
        dmgposi.y = LoadCharmanager.Overallmainchar.transform.position.y;
        if(Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(dmgposi, 7.5f, playerlayer, QueryTriggerInteraction.Ignore);
            foreach (Collider players in colliders)
            {
                if (players.gameObject == LoadCharmanager.Overallmainchar)
                {
                    LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamagecheckiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 10), true);
                    break;
                }
            }
        }
        gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 7.5f);
    }
}
