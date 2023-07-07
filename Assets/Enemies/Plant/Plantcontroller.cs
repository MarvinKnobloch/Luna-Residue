using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plantcontroller : MonoBehaviour
{
    public GameObject[] plantmushrooms;
    [SerializeField] private GameObject[] plantexplosions;
    private Vector3[] spawns = new Vector3[3];
    public Vector3 enemyposi;

    [SerializeField] private float basedmg;
    [SerializeField] private float mushroomuptimer;
    [SerializeField] private GameObject planttimer;
    public float remainingtime;

    private Color posioncolor;
    private Enemyspezialsound enemyspezialsound;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#982AE3", out posioncolor);
        plantmushrooms[0].GetComponent<MeshRenderer>().material.color = posioncolor;
        enemyspezialsound = GetComponentInParent<Enemyspezialsound>();
    }
    private void OnEnable()
    {
        StopCoroutine("controllerdisable");
        spawns[0] = enemyposi + Random.insideUnitSphere * 13;
        spawns[1] = enemyposi + Random.insideUnitSphere * 12;
        spawns[2] = enemyposi + Random.insideUnitSphere * 11;

        setspawnpoints();
        planttimer.transform.position = plantmushrooms[0].transform.position + Vector3.up * 1.5f;
        planttimer.gameObject.SetActive(true);

        plantmushrooms[1].GetComponent<MeshRenderer>().material.color = Color.white;
        plantmushrooms[2].GetComponent<MeshRenderer>().material.color = Color.white;
        remainingtime = mushroomuptimer;
        Invoke("dealdmg", mushroomuptimer);
    }
    private void Update()
    {
        remainingtime -= Time.deltaTime;
    }
    private void setspawnpoints()
    {
        for (int i = 0; i < plantmushrooms.Length; i++)
        {
            NavMeshHit hit;
            NavMesh.Raycast(enemyposi, spawns[i], out hit, NavMesh.AllAreas);
            spawns[i] = hit.position;
            plantmushrooms[i].transform.position = spawns[i];
            plantmushrooms[i].SetActive(true);
        }
    }
    private void dealdmg()
    {
        if(Statics.infight == true)
        {
            for (int i = 0; i < plantmushrooms.Length; i++)
            {
                if (plantmushrooms[i].activeSelf == true)
                {
                    LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamageignoreiframes(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, spawns.Length), true);
                    plantexplosions[i].transform.position = plantmushrooms[i].transform.position + Vector3.up;
                    plantexplosions[i].SetActive(true);
                    enemyspezialsound.playplantmushroomexplosionspezial();
                }
            }
        }
        for (int i = 0; i < plantmushrooms.Length; i++) plantmushrooms[i].SetActive(false);
        planttimer.SetActive(false);
        StartCoroutine("controllerdisable");
    }
    IEnumerator controllerdisable()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < plantexplosions.Length; i++)
        {
            plantexplosions[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
