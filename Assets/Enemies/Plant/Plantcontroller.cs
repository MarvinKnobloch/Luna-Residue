using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plantcontroller : MonoBehaviour
{
    public GameObject[] plantmushrooms;
    private Vector3[] spawns = new Vector3[3];
    public Vector3 enemyposi;

    [SerializeField] private float basedmg;
    [SerializeField] private float mushroomuptimer;

    private Color greencolor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#2BFF00", out greencolor);
        plantmushrooms[0].GetComponent<MeshRenderer>().material.color = greencolor;
    }
    private void OnEnable()
    {
        spawns[0] = enemyposi + LoadCharmanager.Overallmainchar.transform.forward * 5 + Random.insideUnitSphere * 7;
        spawns[1] = enemyposi + Random.insideUnitSphere * 12;
        spawns[2] = enemyposi + LoadCharmanager.Overallmainchar.transform.forward * -5 + Random.insideUnitSphere * 7;

        setspawnpoints();

        plantmushrooms[0].GetComponent<Plantmushroom>().isactivmushroom = true;
        plantmushrooms[1].GetComponent<MeshRenderer>().material.color = Color.white;
        plantmushrooms[2].GetComponent<MeshRenderer>().material.color = Color.white;
        Invoke("dealdmg", mushroomuptimer);
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
        foreach (GameObject mushroom in plantmushrooms)
        {
            if (mushroom.activeSelf == true && Statics.infight == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(Globalplayercalculations.calculateenemyspezialdmg(basedmg, Statics.currentenemyspeziallvl, spawns.Length));
            }
            mushroom.GetComponent<Plantmushroom>().isactivmushroom = false;
            mushroom.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
