using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gasmancontroller : MonoBehaviour
{
    [SerializeField] private GameObject Redpillar;
    [SerializeField] private GameObject Bluepillar;
    [SerializeField] private GameObject Redtarget;
    [SerializeField] private GameObject Bluetarget;

    private Vector3 mainchar;
    private Vector3 maincharleft;
    private Vector3 maincharright;

    private Vector3 pillarspawn1;
    private Vector3 pillarspawn2;
    private Vector3 targetspawn1;
    private Vector3 targetspawn2;

    [SerializeField] private LayerMask checkforposilayer;
    private bool lasercanthit;

    [SerializeField] private GameObject redline;
    [SerializeField] private GameObject blueline;

    [SerializeField] private GameObject[] linetargets;
    public int targetscomplete;

    public float toslowdmg;
    public float linefaildmg;

    public float timetocomplete;
    private void Awake()
    {
        foreach (GameObject targets in linetargets)
        {
            targets.GetComponent<Gasmantarget>().faillinedmg = linefaildmg;
        }
    }

    private void OnEnable()
    {
        targetscomplete = 0;
        placepillars();   
    }

    private void placepillars()
    {
        lasercanthit = false;
        mainchar = LoadCharmanager.Overallmainchar.transform.position;
        mainchar = findyposi(mainchar);

        maincharleft = mainchar + LoadCharmanager.Overallmainchar.transform.right * 7;
        maincharleft = findyposi(maincharleft);

        maincharright = mainchar + LoadCharmanager.Overallmainchar.transform.right * -7;
        maincharright = findyposi(maincharright);

        pillarspawn1 = maincharleft + Random.insideUnitSphere * 5;
        pillarspawn1 = findyposi(pillarspawn1);
        NavMeshHit hit1;
        NavMesh.Raycast(maincharleft, pillarspawn1, out hit1, NavMesh.AllAreas);
        Redpillar.transform.position = hit1.position + Vector3.up * 0.5f;

        pillarspawn2 = maincharright + Random.insideUnitSphere * 5;
        pillarspawn2 = findyposi(pillarspawn2);
        NavMeshHit hit2;
        NavMesh.Raycast(maincharright, pillarspawn2, out hit2, NavMesh.AllAreas);
        Bluepillar.transform.position = hit2.position + Vector3.up * 0.5f;

        targetspawn1 = mainchar + Random.insideUnitSphere * 10;
        targetspawn1 = findyposi(targetspawn1);
        NavMeshHit hit3;
        NavMesh.Raycast(mainchar, targetspawn1, out hit3, NavMesh.AllAreas);
        Redtarget.transform.position = hit3.position + Vector3.up * 0.5f;

        targetspawn2 = mainchar + Random.insideUnitSphere * 10;
        targetspawn2 = findyposi(targetspawn2);
        NavMeshHit hit4;
        NavMesh.Raycast(mainchar, targetspawn2, out hit4, NavMesh.AllAreas);
        Bluetarget.transform.position = hit4.position + Vector3.up * 0.5f;

        StartCoroutine("checkforrightposi");
    }
    private Vector3 findyposi(Vector3 posi)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(posi, out hit, 10, NavMesh.AllAreas);
        return hit.position;
    }

    IEnumerator checkforrightposi()
    {
        yield return null;
        if (Physics.Linecast(Redpillar.transform.position + Vector3.up, Redtarget.transform.position + Vector3.up, checkforposilayer)) lasercanthit = true;
        if (Physics.Linecast(Bluepillar.transform.position + Vector3.up, Bluetarget.transform.position + Vector3.up, checkforposilayer)) lasercanthit = true;
        if (lasercanthit == true)// || Vector3.Distance(Redtarget.transform.position, Bluetarget.transform.position) < 1)
        {
            placepillars();
            Debug.Log("canthit");
        }
        else
        {
            Invoke("turnonline", 1f); 
        }
    }
    private void turnonline()
    {
        blueline.SetActive(true);
        redline.SetActive(true);
        Invoke("turnoffeverything", timetocomplete);
    }
    private void turnoffeverything()
    {
        LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(toslowdmg);
        blueline.SetActive(false);
        redline.SetActive(false);
        gameObject.SetActive(false);
    }
    public void checktargets()
    {
        targetscomplete += 1;
        if (targetscomplete == 2)
        {
            CancelInvoke();
            Invoke("success", 0.3f);
        }
    }
    private void success()
    {
        CancelInvoke();
        blueline.SetActive(false);
        redline.SetActive(false);
        gameObject.SetActive(false);
    }
}
