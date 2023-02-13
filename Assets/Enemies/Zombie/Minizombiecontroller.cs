using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Minizombiecontroller : MonoBehaviour
{
    public NavMeshAgent Meshagent;
    [SerializeField] private GameObject Zombie;

    [NonSerialized] public float basedmg;
    [NonSerialized] public float range;
    [NonSerialized] public float zombiemovementspeed;
    private void Awake()
    {
        Meshagent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        float zombiespeed = UnityEngine.Random.Range(3, 6);
        zombiespeed = zombiespeed * zombiemovementspeed;                                //damit ich Kommazahlen bekomme
        Meshagent.GetComponent<NavMeshAgent>().speed = zombiespeed;
    }
    private void Update()
    {
        Meshagent.SetDestination(Zombie.transform.position);
        facezombie();
        if(Vector3.Distance(transform.position, Zombie.transform.position) < range)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(basedmg);
            gameObject.SetActive(false);
        }
        if (Zombie.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
    }
    private void facezombie()
    {
        Vector3 direction = (Zombie.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
