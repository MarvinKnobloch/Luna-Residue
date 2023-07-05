using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Paladincontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] showspezial;

    private Vector3 spawnposi;
    private Vector3 playerposi;
    private int currentspezial;

    [SerializeField] private float spezialdmg;
    [SerializeField] private float timetododge;
    [SerializeField] private float spawntimer;
    [SerializeField] private float spawnradius;

    private void Awake()
    {
        foreach (GameObject spezial in showspezial)
        {
            spezial.GetComponent<Paladincirclecontroller>().basedmg = spezialdmg;
            spezial.GetComponent<Paladincirclecontroller>().dodgetime = timetododge;
        }
    }
    private Vector3 spawnlocation(Vector3 spawn)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(spawn, out hit, 20, NavMesh.AllAreas);
        spawn = hit.position;
        return spawn;
    }
    private void OnEnable()
    {
        StopCoroutine("controllerdisable");
        currentspezial = 0;
        spawnposi = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi = spawnlocation(spawnposi);
        showspezial[currentspezial].transform.position = spawnposi;
        showspezial[currentspezial].SetActive(true);

        currentspezial++;
        playerposi = spawnlocation(LoadCharmanager.Overallmainchar.transform.position);
        showspezial[currentspezial].transform.position = playerposi;                 //2. spawnt auf dem spieler

        Invoke("secondspawn", spawntimer);
    }
    private void secondspawn()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi = spawnlocation(spawnposi);
        showspezial[currentspezial].transform.position = spawnposi;

        Invoke("thirdspawn", spawntimer);
    }
    private void thirdspawn()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi = LoadCharmanager.Overallmainchar.transform.position + LoadCharmanager.Overallmainchar.transform.forward * 15;    //4. spawnt vor dem Spieler
        spawnposi = spawnlocation(spawnposi);
        showspezial[currentspezial].transform.position = spawnposi;

        InvokeRepeating("spawns", spawntimer, spawntimer);
    }
    private void spawns()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi = spawnlocation(spawnposi);
        showspezial[currentspezial].transform.position = spawnposi;

        if (currentspezial >= showspezial.Length - 1)
        {
            CancelInvoke();
            Invoke("finalspawn", spawntimer);
        }
    }
    private void finalspawn()
    {
        showspezial[currentspezial].SetActive(true);
        StartCoroutine("controllerdisable");
    }

    IEnumerator controllerdisable()
    {
        yield return new WaitForSeconds(1 + timetododge);
        gameObject.SetActive(false);
    }
}
