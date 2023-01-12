using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ninjaspezial : MonoBehaviour
{
    [SerializeField] private GameObject ninjacontroller;
    [SerializeField] private GameObject savezone;

    public NavMeshAgent Meshagent;

    private void ninjaspezial()
    {
        Vector3 mainchar = LoadCharmanager.Overallmainchar.transform.position;
        mainchar.y = transform.position.y;
        Vector3 spawn = mainchar + Random.insideUnitSphere * 17;
        NavMeshHit hit1;
        NavMesh.Raycast(mainchar, spawn, out hit1, NavMesh.AllAreas);
        spawn = hit1.position;
        savezone.transform.position = spawn;
        ninjacontroller.SetActive(true);
        savezone.SetActive(true);
    }
}
