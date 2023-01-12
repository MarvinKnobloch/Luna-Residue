using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombiespezial : MonoBehaviour
{
    public NavMeshAgent Meshagent;

    [SerializeField] private GameObject zombiecontroller;

    [SerializeField] private GameObject Zombie1;
    [SerializeField] private GameObject Zombie2;
    [SerializeField] private GameObject Zombie3;
    [SerializeField] private GameObject Zombie4;

    private Vector3 spawn1;
    private Vector3 spawn2;
    private Vector3 spawn3;
    private Vector3 spawn4;

    private void Start()
    {
        Meshagent = GetComponent<NavMeshAgent>();
    }
    private void zombiespezial()
    {
        spawn1 = transform.position + transform.forward * 15;
        NavMeshHit hit1;
        NavMesh.Raycast(transform.position, spawn1, out hit1, NavMesh.AllAreas);
        spawn1 = hit1.position;
        Zombie1.transform.position = spawn1;

        spawn2 = transform.position + transform.forward * -15;
        NavMeshHit hit2;
        NavMesh.Raycast(transform.position, spawn2, out hit2, NavMesh.AllAreas);
        spawn2 = hit2.position;
        Zombie2.transform.position = spawn2;

        spawn3 = transform.position + transform.right * 15;
        NavMeshHit hit3;
        NavMesh.Raycast(transform.position, spawn3, out hit3, NavMesh.AllAreas);
        spawn3 = hit3.position;
        Zombie3.transform.position = spawn3;

        spawn4 = transform.position + transform.right * -15;
        NavMeshHit hit4;
        NavMesh.Raycast(transform.position, spawn4, out hit4, NavMesh.AllAreas);
        spawn4 = hit4.position;
        Zombie4.transform.position = spawn4;

        zombiecontroller.SetActive(true);
    }
}
