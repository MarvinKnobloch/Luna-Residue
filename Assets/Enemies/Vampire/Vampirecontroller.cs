using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirecontroller : MonoBehaviour
{
    [SerializeField] private GameObject vampirecircleobj;
    [SerializeField] private GameObject spezialendspawn;
    [SerializeField] private Vampirecircle vampirecircle;
    [SerializeField] private Vampirecube vampirecube;
    [SerializeField] private LayerMask raycastlayer;

    [SerializeField] private float circledmg;
    [SerializeField] private float circledodgetime;
    [SerializeField] private float cubedmg;
    [SerializeField] private float cubedodgetime;
    private void Awake()
    {
        vampirecircle.basedmg = circledmg;
        vampirecircle.explodetime = circledodgetime;
        vampirecube.basedmg = cubedmg;
        vampirecube.explodetime = cubedodgetime;
    }
    private void OnEnable()
    {
        if(Physics.Raycast(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 30, raycastlayer, QueryTriggerInteraction.Ignore))
        {
            vampirecircleobj.transform.position = hit.point;
        }
        else vampirecircleobj.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        vampirecircleobj.SetActive(true);
        vampirecircle.overlapspherepoint = vampirecircleobj.transform.position;
        Invoke("spezialpart2", 1f);
    }
    private void spezialpart2()
    {
        if (Physics.Raycast(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 30, raycastlayer, QueryTriggerInteraction.Ignore))
        {
            vampirecircleobj.transform.position = hit.point;
        }
        else vampirecircleobj.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        vampirecircleobj.SetActive(true);
        vampirecircle.overlapspherepoint = vampirecircleobj.transform.position;
        Invoke("spezialpart3", 1f);
    }
    private void spezialpart3()
    {
        if (Physics.Raycast(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 30, raycastlayer, QueryTriggerInteraction.Ignore))
        {
            spezialendspawn.transform.position = hit.point + Vector3.up * 0.3f;
        }
        else spezialendspawn.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        spezialendspawn.SetActive(true);
        vampirecube.overlapboxpoint = spezialendspawn.transform.position;
    }
}
