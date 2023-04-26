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

    private Vector3 mainchar;
    private Vector3 maincharleft;
    private Vector3 maincharright;

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

    public float basedmg;
    public float timetocomplete;

    private void OnEnable()
    {
        target1activate = false;
        target2activate = false;
        target1complete = false;
        target2complete = false;
        target1.GetComponent<Renderer>().material.color = Color.white;
        target2.GetComponent<Renderer>().material.color = Color.white;
        placepillars();   
    }
    private void placepillars()
    {
        mainchar = LoadCharmanager.Overallmainchar.transform.position;
        mainchar = findyposi(mainchar);

        maincharleft = mainchar + LoadCharmanager.Overallmainchar.transform.forward * -4 + LoadCharmanager.Overallmainchar.transform.right * 7;
        maincharleft = findyposi(maincharleft);

        maincharright = mainchar + LoadCharmanager.Overallmainchar.transform.forward * -4 + LoadCharmanager.Overallmainchar.transform.right * -7;
        maincharright = findyposi(maincharright);

        pillarspawn1 = maincharleft + UnityEngine.Random.insideUnitSphere * 5;
        pillarspawn1 = findyposi(pillarspawn1);
        NavMeshHit hit1;
        NavMesh.Raycast(maincharleft, pillarspawn1, out hit1, NavMesh.AllAreas);
        pillar1.transform.position = hit1.position + Vector3.up * 0.4f;

        pillarspawn2 = maincharright + UnityEngine.Random.insideUnitSphere * 5;
        pillarspawn2 = findyposi(pillarspawn2);
        NavMeshHit hit2;
        NavMesh.Raycast(maincharright, pillarspawn2, out hit2, NavMesh.AllAreas);
        pillar2.transform.position = hit2.position + Vector3.up * 0.4f;

        targetspawn1 = mainchar + LoadCharmanager.Overallmainchar.transform.forward * 10 + UnityEngine.Random.insideUnitSphere * 9;
        targetspawn1 = findyposi(targetspawn1);
        NavMeshHit hit3;
        NavMesh.Raycast(mainchar, targetspawn1, out hit3, NavMesh.AllAreas);
        target1.transform.position = hit3.position + Vector3.up * 1f;

        int randomposi = UnityEngine.Random.Range(-3, 3);
        targetspawn2 = mainchar + LoadCharmanager.Overallmainchar.transform.right * randomposi + LoadCharmanager.Overallmainchar.transform.forward * 8 + UnityEngine.Random.insideUnitSphere * 7;
        targetspawn2 = findyposi(targetspawn2);
        NavMeshHit hit4;
        NavMesh.Raycast(mainchar, targetspawn2, out hit4, NavMesh.AllAreas);
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
        LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 1));
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
}
