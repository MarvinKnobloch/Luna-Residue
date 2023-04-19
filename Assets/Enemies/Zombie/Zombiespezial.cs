using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombiespezial : MonoBehaviour
{
    [SerializeField] private GameObject zombiecontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = zombiecontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void zombiespezial()
    {
        zombiecontroller.GetComponent<Zombiecontroller>().enemyposi = gameObject.transform.position;
        zombiecontroller.SetActive(true);
    }
    private void zombiespezialaudio()
    {
        enemyspezialsound.playzombiespezial();
    }
}
