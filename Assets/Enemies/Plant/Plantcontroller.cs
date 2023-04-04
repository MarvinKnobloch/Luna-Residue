using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plantcontroller : MonoBehaviour
{
    public GameObject[] plantspheres;
    private Vector3[] spawns = new Vector3[3];
    public Vector3 enemyposi;

    [SerializeField] private float basedmg;
    [SerializeField] private float sphereuptimer;

    private Color redcolor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#B72020", out redcolor);
        redcolor.a = 0.6f;
    }
    private void OnEnable()
    {
        spawns[0] = enemyposi + LoadCharmanager.Overallmainchar.transform.forward * 5 + Random.insideUnitSphere * 7;
        spawns[1] = enemyposi + Random.insideUnitSphere * 12;
        spawns[2] = enemyposi + LoadCharmanager.Overallmainchar.transform.forward * -5 + Random.insideUnitSphere * 7;

        setspawnpoints();

        plantspheres[0].GetComponent<Plantsphere>().isactivsphere = true;
        plantspheres[1].GetComponent<MeshRenderer>().material.color = redcolor;
        plantspheres[2].GetComponent<MeshRenderer>().material.color = redcolor;
        Invoke("dealdmg", sphereuptimer);
    }
    private void setspawnpoints()
    {
        for (int i = 0; i < plantspheres.Length; i++)
        {
            NavMeshHit hit;
            NavMesh.Raycast(enemyposi, spawns[i], out hit, NavMesh.AllAreas);
            spawns[i] = hit.position;
            plantspheres[i].transform.position = spawns[i] + Vector3.up * 0.5f;
            plantspheres[i].SetActive(true);
        }
    }
    private void dealdmg()
    {
        foreach (GameObject sphere in plantspheres)
        {
            if (sphere.activeSelf == true && Statics.infight == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().TakeDamage(basedmg + Globalplayercalculations.calculateenemyspezialdmg());
            }
            sphere.GetComponent<Plantsphere>().isactivsphere = false;
            sphere.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
