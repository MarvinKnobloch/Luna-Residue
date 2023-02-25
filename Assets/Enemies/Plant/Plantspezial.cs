using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plantspezial : MonoBehaviour
{
    public NavMeshAgent Meshagent;

    [SerializeField] private Plantcontroller plantcontroller;

    private Vector3 spawn1;
    private Vector3 spawn2;
    private Vector3 spawn3;

    private Vector3 mainchar;

    private void Start()
    {
        Meshagent = GetComponent<NavMeshAgent>();
    }
    private void plantspezial()
    {
        plantcontroller.gameObject.SetActive(true);
        mainchar = LoadCharmanager.Overallmainchar.transform.position;
        mainchar.y = transform.position.y;
        spawn1 = mainchar + LoadCharmanager.Overallmainchar.transform.forward * 7 + Random.insideUnitSphere * 8;
        spawn2 = mainchar + LoadCharmanager.Overallmainchar.transform.forward * -7 + Random.insideUnitSphere * 8;
        spawn3 = mainchar + Random.insideUnitSphere * 13;

        NavMeshHit hit1;
        NavMesh.Raycast(mainchar, spawn1, out hit1, NavMesh.AllAreas);
        spawn1 = hit1.position;
        plantcontroller.plantspheres[0].transform.position = spawn1 + Vector3.up;

        NavMeshHit hit2;
        NavMesh.Raycast(mainchar, spawn2, out hit2, NavMesh.AllAreas);
        spawn2 = hit2.position;
        plantcontroller.plantspheres[1].transform.position = spawn2 + Vector3.up;

        NavMeshHit hit3;
        NavMesh.Raycast(mainchar, spawn3, out hit3, NavMesh.AllAreas);
        spawn3 = hit3.position;
        plantcontroller.plantspheres[2].transform.position = spawn3 + Vector3.up;
    }
}
