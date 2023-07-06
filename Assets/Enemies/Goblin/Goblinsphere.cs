using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goblinsphere : MonoBehaviour
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
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1.5f);
    }*/
    private void dealdmg()
    {
        if (Statics.infight == true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f, targets, QueryTriggerInteraction.Ignore);
            foreach (Collider target in colliders)
            {
                if (target.gameObject == LoadCharmanager.Overallmainchar.gameObject)
                {
                    target.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 6), true);
                    break;
                }
            }
        }
        enemyspezialsound.playgoblinexplosionspezial();
        explosioneffect.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0,360),0,0);
        explosioneffect.transform.position = transform.position;
        explosioneffect.SetActive(true);
        gameObject.SetActive(false);
    }
}

