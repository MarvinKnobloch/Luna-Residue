using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiecontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] zombies;

    [SerializeField] private float zombiedmg;
    [SerializeField] private float howclosezombiehastowalk;
    [SerializeField] private float zombiespeed;


    private void Awake()
    {
        foreach (GameObject zombie in zombies)
        {
            zombie.GetComponent<Minizombiecontroller>().basedmg = zombiedmg;
            zombie.GetComponent<Minizombiecontroller>().range = howclosezombiehastowalk;
            zombie.GetComponent<Minizombiecontroller>().zombiemovementspeed = zombiespeed;
        }
    }
    private void OnEnable()
    {
        foreach (GameObject zombie in zombies)
        {
            zombie.SetActive(true);
        }
        Invoke("turnoff", 12f);
    }
    private void turnoff()
    {
        foreach (GameObject zombie in zombies)
        {
            zombie.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
