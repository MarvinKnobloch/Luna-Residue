using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nightshadespezial : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;
    public NavMeshAgent Meshagent;
    private Vector3 mainchar;
    private Vector3 spawn;
    private Vector3 towerspawn;

    private void Start()
    {
        Meshagent = GetComponent<NavMeshAgent>();
    }
    private void nightshadespezial()
    {
        mainchar = LoadCharmanager.Overallmainchar.transform.position;
        int chooseposi = Random.Range(0, 2);
        if (chooseposi == 0)
        {
            spawn = mainchar + LoadCharmanager.Overallmainchar.transform.forward * 10 + LoadCharmanager.Overallmainchar.transform.right * 10;
        }
        else
        {
            spawn = mainchar + LoadCharmanager.Overallmainchar.transform.forward * 10 + LoadCharmanager.Overallmainchar.transform.right * -10;
        }
        mainchar.y = transform.position.y;
        spawn.y = transform.position.y;
        NavMeshHit hit;
        NavMesh.Raycast(mainchar, spawn, out hit, NavMesh.AllAreas);
        if (chooseposi == 0)
        {
            towerspawn = hit.position + LoadCharmanager.Overallmainchar.transform.forward * -4 + LoadCharmanager.Overallmainchar.transform.right * -4;
        }
        else
        {
            towerspawn = hit.position + LoadCharmanager.Overallmainchar.transform.forward * -4 + LoadCharmanager.Overallmainchar.transform.right * 4;
        }

        int choosetower = Random.Range(0, 4);
        towers[choosetower].transform.position = towerspawn + new Vector3(0, 4f, 0);
        towers[choosetower].SetActive(true);
        towers[choosetower].GetComponent<Towercontroller>().setenemy(this.gameObject);
    }
}
