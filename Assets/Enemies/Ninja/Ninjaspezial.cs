using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ninjaspezial : MonoBehaviour
{
    [SerializeField] private Ninjacontroller spezialcontroller;

    public NavMeshAgent Meshagent;
    private void ninjaspezial()
    {
        spezialcontroller.gameObject.SetActive(true);

        /*Vector3 mainchar = LoadCharmanager.Overallmainchar.transform.position;
        mainchar.y = transform.position.y;
        Vector3 spawn = mainchar + Random.insideUnitSphere * 17;
        NavMeshHit hit1;
        NavMesh.Raycast(mainchar, spawn, out hit1, NavMesh.AllAreas);
        spawn = hit1.position;
        savezone.transform.position = spawn;
        savezone.SetActive(true);*/
    }
}
