using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombiecontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] jumppad;
    [SerializeField] private GameObject bomb;
    [SerializeField] private LayerMask balllayer;

    [SerializeField] private float basedmg;
    [SerializeField] private float collecttime;

    public Vector3 enemyposi;

    private Vector3 spawn1;
    private Vector3 spawn2;
    private Vector3 ballspawn;

    [SerializeField] private ParticleSystem particlesystem;
    [SerializeField] private GameObject explosioneffect;
    private Enemyspezialsound enemyspezialsound;

    private void Awake()
    {
        enemyspezialsound = GetComponentInParent<Enemyspezialsound>();
    }
    private void OnEnable()
    {
        StopCoroutine("controllerdisable");
        spawn1 = enemyposi + LoadCharmanager.Overallmainchar.transform.forward * 5 + Random.insideUnitSphere * 7;
        spawn2 = enemyposi + LoadCharmanager.Overallmainchar.transform.forward * -5 + Random.insideUnitSphere * 7;

        NavMeshHit hit1;
        NavMesh.Raycast(enemyposi, spawn1, out hit1, NavMesh.AllAreas);
        spawn1 = hit1.position;
        jumppad[0].transform.position = spawn1;

        NavMeshHit hit2;
        NavMesh.Raycast(enemyposi, spawn2, out hit2, NavMesh.AllAreas);
        spawn2 = hit2.position;
        jumppad[1].transform.position = spawn2 + Vector3.up * 0.5f;

        foreach (GameObject pad in jumppad)
        {
            pad.SetActive(true);
        }

        ballspawn = enemyposi + Vector3.up * 11;
        Vector3 spawn = ballspawn;
        spawn.x += Random.Range(-8, 8);
        spawn.z += Random.Range(-8, 8);
        spawn.y += Random.Range(-3, 3);
        if (Physics.Linecast(ballspawn, spawn, out RaycastHit hit, balllayer, QueryTriggerInteraction.Ignore))
        {
            bomb.transform.position = hit.point;
        }
        else
        {
            bomb.transform.position = spawn;
        }
        bomb.SetActive(true);
        Invoke("turnoff", collecttime);
    }
    private void turnoff()
    {
        if (bomb.activeSelf == true && Statics.infight == true)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 1), true);
            bomb.SetActive(false);
         
            explosioneffect.transform.position = bomb.transform.position;
            explosioneffect.SetActive(true);
            particlesystem.Play();

            enemyspezialsound.playzombiebombspezial();
        }
        StartCoroutine("controllerdisable");
    }
    IEnumerator controllerdisable()
    {
        yield return new WaitForSeconds(1);
        explosioneffect.SetActive(false);
        gameObject.SetActive(false);
    }
}
