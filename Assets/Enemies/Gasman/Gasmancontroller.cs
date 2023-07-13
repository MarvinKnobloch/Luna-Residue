using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Gasmancontroller : MonoBehaviour
{
    [SerializeField] private GameObject pillar1;
    [SerializeField] private GameObject pillar2;
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;

    private Vector3 enemyposi;
    private Vector3 leftspawn;
    private Vector3 rightspawn;

    private Vector3 pillarspawn1;
    private Vector3 pillarspawn2;
    private Vector3 targetspawn1;
    private Vector3 targetspawn2;

    public bool target1activate;
    public bool target2activate;
    public bool target1complete;
    public bool target2complete;

    [SerializeField] private LayerMask checkforposilayer;

    [SerializeField] private GameObject redline;
    [SerializeField] private GameObject blueline;

    [SerializeField] private GameObject[] linetargets;

    private Color whitecolor = Color.white;

    public float basedmg;
    public float timetocomplete;

    private void Awake()
    {
        whitecolor.a = 0.6f;
    }
    private void OnEnable()
    {
        target1activate = false;
        target2activate = false;
        target1complete = false;
        target2complete = false;
        target1.GetComponent<Renderer>().material.color = whitecolor;
        target2.GetComponent<Renderer>().material.color = whitecolor;
        placepillars();   
    }
    private void placepillars()
    {
        leftspawn = enemyposi + LoadCharmanager.Overallmainchar.transform.forward * -4 + LoadCharmanager.Overallmainchar.transform.right * 7;
        leftspawn = findyposi(leftspawn);

        rightspawn = enemyposi + LoadCharmanager.Overallmainchar.transform.forward * -4 + LoadCharmanager.Overallmainchar.transform.right * -7;
        rightspawn = findyposi(rightspawn);

        pillarspawn1 = leftspawn + UnityEngine.Random.insideUnitSphere * 5;
        pillarspawn1 = findyposi(pillarspawn1);
        NavMeshHit hit1;
        NavMesh.Raycast(enemyposi, pillarspawn1, out hit1, NavMesh.AllAreas);
        pillar1.transform.position = hit1.position + Vector3.up * 0.4f;

        pillarspawn2 = rightspawn + UnityEngine.Random.insideUnitSphere * 5;
        pillarspawn2 = findyposi(pillarspawn2);
        NavMeshHit hit2;
        NavMesh.Raycast(enemyposi, pillarspawn2, out hit2, NavMesh.AllAreas);
        pillar2.transform.position = hit2.position + Vector3.up * 0.4f;
        
        int randomposi1 = UnityEngine.Random.Range(-7, 1);
        targetspawn1 = enemyposi + LoadCharmanager.Overallmainchar.transform.right * randomposi1 + LoadCharmanager.Overallmainchar.transform.forward * 12 + UnityEngine.Random.insideUnitSphere * 6;
        targetspawn1 = findyposi(targetspawn1);
        NavMeshHit hit3;
        NavMesh.Raycast(enemyposi, targetspawn1, out hit3, NavMesh.AllAreas);
        target1.transform.position = hit3.position + Vector3.up * 1f;

        int randomposi2 = UnityEngine.Random.Range(-1, 7);
        targetspawn2 = enemyposi + LoadCharmanager.Overallmainchar.transform.right * randomposi2 + LoadCharmanager.Overallmainchar.transform.forward * 8 + UnityEngine.Random.insideUnitSphere * 6;
        targetspawn2 = findyposi(targetspawn2);
        NavMeshHit hit4;
        NavMesh.Raycast(enemyposi, targetspawn2, out hit4, NavMesh.AllAreas);
        target2.transform.position = hit4.position + Vector3.up * 1f;

        Invoke("turnonline", 1f);
    }
    private Vector3 findyposi(Vector3 posi)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(posi, out hit, 10, NavMesh.AllAreas);
        return hit.position;
    }

    private void turnonline()
    {
        blueline.SetActive(true);
        redline.SetActive(true);
        Invoke("turnoffeverything", timetocomplete);
    }
    private void turnoffeverything()
    {
        if(Statics.infight == true)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 1), true);
        }
        blueline.SetActive(false);
        redline.SetActive(false);
        gameObject.SetActive(false);
    }
    public void checkforcomplete()
    {
        if(target1complete == true && target2complete == true)
        {
            CancelInvoke();
            blueline.SetActive(false);
            redline.SetActive(false);
            Invoke("success", 0.5f);
        }
    }
    private void success()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
    public void setenemy(Vector3 enemy)
    {
        enemyposi = enemy;
    } 
}
