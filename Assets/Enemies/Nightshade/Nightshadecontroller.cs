using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightshadecontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;
    [SerializeField] private float towerdmg;
    [SerializeField] private float towertime;
    [SerializeField] private float dmgtoenemyoncompletion;


    private void Awake()
    {
        foreach (GameObject tower in towers)
        {
            tower.GetComponent<Towercontroller>().basedmg = towerdmg;
            tower.GetComponent<Towercontroller>().towercompletiontime = towertime;
            tower.GetComponent<Towercontroller>().completiondmg = dmgtoenemyoncompletion;
        }
    }
}
