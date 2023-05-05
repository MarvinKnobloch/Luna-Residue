using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombiecontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] jumppad;
    [SerializeField] private GameObject[] balls;
    [SerializeField] private LayerMask balllayer;

    [SerializeField] private float basedmg;
    [SerializeField] private float collecttime;

    public Vector3 enemyposi;

    private Vector3 spawn1;
    private Vector3 spawn2;
    private Vector3 ballspawn;

    private void OnEnable()
    {
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
        foreach (GameObject ball in balls)
        {
            Vector3 spawn = ballspawn;
            spawn.x += Random.Range(-8, 8);
            spawn.z += Random.Range(-8, 8);
            spawn.y += Random.Range(-3, 3);
            if(Physics.Linecast(ballspawn, spawn, out RaycastHit hit, balllayer, QueryTriggerInteraction.Ignore))
            {
                ball.transform.position = hit.point;
            }
            else
            {
                ball.transform.position = spawn;
            }
            ball.SetActive(true);
        }

        Invoke("turnoff", collecttime);
    }
    private void turnoff()
    {
        foreach (GameObject ball in balls)
        {
            if(ball.activeSelf == true && Statics.infight == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, 1));
                ball.SetActive(false);
            }
        }
        gameObject.SetActive(false);
    }
}
