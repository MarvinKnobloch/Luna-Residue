using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Nightshadecontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;

    private Vector3 towerspawn;

    [SerializeField] private float towerdmg;
    [SerializeField] private float towertime;
    [NonSerialized] public GameObject enemy;

    private void Awake()
    {
        foreach (GameObject tower in towers)
        {
            tower.GetComponent<Towercontroller>().basedmg = towerdmg;
            tower.GetComponent<Towercontroller>().towercompletiontime = towertime;
            tower.GetComponent<Towercontroller>().completiondmg = Statics.charcurrentlvl * 5;
        }
    }
    private void OnEnable()
    {
        towerspawn = enemy.transform.position + UnityEngine.Random.insideUnitSphere * 10;
        NavMeshHit hit;
        NavMesh.SamplePosition(towerspawn, out hit, 20, NavMesh.AllAreas);
        NavMeshHit hit1;
        NavMesh.Raycast(enemy.transform.position, hit.position, out hit1, NavMesh.AllAreas);
        towerspawn = hit1.position;

        int choosetower = UnityEngine.Random.Range(0, 5);
        towers[choosetower].transform.position = towerspawn + new Vector3(0, 4f, 0);
        towers[choosetower].SetActive(true);
        towers[choosetower].GetComponent<Towercontroller>().setenemy(enemy);
    }
}
