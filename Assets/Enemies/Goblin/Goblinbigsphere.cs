using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goblinbigsphere : MonoBehaviour
{
    public float basedmg;
    [NonSerialized] public float timetoexplode;

    [SerializeField] private LayerMask targets;
    [SerializeField] private GameObject explosioneffect;

    private Enemyspezialsound enemyspezialsound;

    private void Awake()
    {
        enemyspezialsound = GetComponentInParent<Enemyspezialsound>();
    }
    private void OnEnable()
    {
        Invoke("dealdmg", timetoexplode);
    }
    private void dealdmg()
    {
        if (Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 6, targets, QueryTriggerInteraction.Ignore);
            foreach (Collider target in colliders)
            {
                if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
                {
                    float dmg = Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 6);
                    target.GetComponent<Playerhp>().takedamageignoreiframes(dmg * 2, true);
                    break;
                }
            }
        }
        enemyspezialsound.playgoblinexplosionspezial();
        explosioneffect.transform.position = transform.position;
        explosioneffect.SetActive(true);
        gameObject.SetActive(false);
    }
}
