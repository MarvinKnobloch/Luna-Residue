using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirecontroller : MonoBehaviour
{
    [SerializeField] private GameObject spezialendspawn;
    [SerializeField] private Vampiresphere vampiresphere;
    [SerializeField] private Vampirecube vampirecube;
    [SerializeField] private LayerMask raycastlayer;

    [SerializeField] private float circledmg;
    [SerializeField] private float circledodgetime;
    [SerializeField] private float cubedmg;
    [SerializeField] private float cubedodgetime;

    [SerializeField] private GameObject sphereeffect;
    [SerializeField] private GameObject cubeeffect;
    private void Awake()
    {
        vampiresphere.basedmg = circledmg;
        vampiresphere.explodetime = circledodgetime;
        vampirecube.basedmg = cubedmg;
        vampirecube.explodetime = cubedodgetime;
    }
    private void OnEnable()
    {
        StopCoroutine("controllerdisable");
        if (Physics.Raycast(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 30, raycastlayer, QueryTriggerInteraction.Ignore))
        {
            vampiresphere.gameObject.transform.position = hit.point;
        }
        else vampiresphere.gameObject.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        vampiresphere.gameObject.SetActive(true);
        vampiresphere.overlapspherepoint = vampiresphere.gameObject.transform.position;
        Invoke("spezialpart2", 1f);
    }
    private void spezialpart2()
    {
        if (Physics.Raycast(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 30, raycastlayer, QueryTriggerInteraction.Ignore))
        {
            vampiresphere.gameObject.transform.position = hit.point;
        }
        else vampiresphere.gameObject.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        vampiresphere.gameObject.SetActive(true);
        vampiresphere.overlapspherepoint = vampiresphere.gameObject.transform.position;
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

    public void controllerdisablestart()
    {
        StartCoroutine("controllerdisable");
    }
    IEnumerator controllerdisable()
    {
        yield return new WaitForSeconds(0.6f);
        sphereeffect.SetActive(false);
        cubeeffect.SetActive(false);
        gameObject.SetActive(false);
    }
}
