using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towergoal : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            tower.GetComponent<Towercontroller>().dealdmg = false;
            tower.GetComponent<Towercontroller>().dealdmgtoenemyroot();
            other.GetComponent<Healingscript>().groupheal();
            gameObject.SetActive(false);
        }
    }
}
