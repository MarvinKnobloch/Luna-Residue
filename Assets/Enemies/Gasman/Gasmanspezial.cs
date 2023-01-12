using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gasmanspezial : MonoBehaviour
{
    [SerializeField] private GameObject gasmancontroller;
    [SerializeField] private GameObject Redpillar;
    [SerializeField] private GameObject Bluepillar;
    [SerializeField] private GameObject Redtarget;
    [SerializeField] private GameObject Bluetarget;

    [SerializeField] private NavMeshAgent Meshagent;
    [SerializeField] private LayerMask layer;

    private Vector3 mainchar;
    private Vector3 maincharleft;
    private Vector3 maincharright;

    private Vector3 pillarspawn1;
    private Vector3 pillarspawn2;
    private Vector3 targetspawn1;
    private Vector3 targetspawn2;

    private bool lasercanthit;

    private void Start()
    {
        Meshagent = GetComponent<NavMeshAgent>();
    }
    private void gasmanspezial()
    {
        lasercanthit = false;
        mainchar = LoadCharmanager.Overallmainchar.transform.position;
        mainchar.y = transform.position.y;     //falls der char gerade in der luft ist

        maincharleft = mainchar + LoadCharmanager.Overallmainchar.transform.right * 7;
        maincharright = mainchar + LoadCharmanager.Overallmainchar.transform.right * -7;


        pillarspawn1 = maincharleft + Random.insideUnitSphere * 5;
        pillarspawn1.y = transform.position.y;
        NavMeshHit hit3;
        NavMesh.Raycast(maincharleft, pillarspawn1, out hit3, NavMesh.AllAreas);
        pillarspawn1 = hit3.position;
        Redpillar.transform.position = pillarspawn1;

        pillarspawn2 = maincharright + Random.insideUnitSphere * 5;
        pillarspawn2.y = transform.position.y;
        NavMeshHit hit4;
        NavMesh.Raycast(maincharright, pillarspawn2, out hit4, NavMesh.AllAreas);
        pillarspawn2 = hit4.position;
        Bluepillar.transform.position = pillarspawn2;

        targetspawn1 = mainchar + Random.insideUnitSphere * 10;
        targetspawn1.y = transform.position.y;
        NavMeshHit hit1;
        NavMesh.Raycast(mainchar, targetspawn1, out hit1, NavMesh.AllAreas);
        targetspawn1 = hit1.position;
        Redtarget.transform.position = targetspawn1;

        targetspawn2 = mainchar + Random.insideUnitSphere * 10;
        targetspawn2.y = transform.position.y;
        NavMeshHit hit2;
        NavMesh.Raycast(mainchar, targetspawn2, out hit2, NavMesh.AllAreas);
        targetspawn2 = hit2.position;
        Bluetarget.transform.position = targetspawn2;

        if(gasmancontroller.activeSelf == false)
        {
            gasmancontroller.SetActive(true);
        }
        StartCoroutine("checkforrightposi");
    }
    IEnumerator checkforrightposi()
    {
        yield return null;
        RaycastHit hit1;
        if (Physics.Linecast(Redpillar.transform.position + Vector3.up, Redtarget.transform.position + Vector3.up, out hit1, layer))
        {
            if (hit1.collider.gameObject != Redtarget.gameObject)
            {
                lasercanthit = true;
            }
        }
        RaycastHit hit2;
        if (Physics.Linecast(Bluepillar.transform.position + Vector3.up, Bluetarget.transform.position + Vector3.up, out hit2, layer))
        {
            if (hit2.collider.gameObject != Bluetarget.gameObject)
            {
                lasercanthit = true;
            }
        }
        if (lasercanthit == true || Vector3.Distance(Redpillar.transform.position, Bluepillar.transform.position) < 1 || Vector3.Distance(Redtarget.transform.position, Bluetarget.transform.position) < 1)
        {
            Debug.Log("canthit");
            gasmanspezial();
        }
    }
}
