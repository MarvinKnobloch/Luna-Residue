using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vampiresphere : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    [NonSerialized] public float basedmg;
    [NonSerialized] public float explodetime;

    [SerializeField] private GameObject sphereeffect;

    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = GetComponentInParent<Enemyspezialsound>();
    }

    private void OnEnable()
    {
        Invoke("dealdmg", explodetime);
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 3f);
    }*/
    private void dealdmg()
    {
        if (Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, player, QueryTriggerInteraction.Ignore);
            foreach (Collider target in colliders)
            {
                if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
                {
                    target.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 3), true);
                    break;
                }
            }
        }
        enemyspezialsound.playvampirespherespezial();
        sphereeffect.transform.position = transform.position;
        sphereeffect.SetActive(true);      
        gameObject.SetActive(false);
    }
}
