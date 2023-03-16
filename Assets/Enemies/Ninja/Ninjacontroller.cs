using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ninjacontroller : MonoBehaviour
{
    [SerializeField] private GameObject savezone;

    [SerializeField] private GameObject[] ninjastars;
    [SerializeField] private float ninjastardmg;
    [SerializeField] private float ninjastarspawnspeed;
    [SerializeField] private float ninjastarspeed;
    [SerializeField] private float ninjastarspawnradius;

    private int currentstar;

    private void Awake()
    {
        foreach (GameObject ninjastar in ninjastars)
        {
            ninjastar.GetComponent<Ninjastar>().basedmg = ninjastardmg;
            ninjastar.GetComponent<Ninjastar>().spawnspeed = ninjastarspawnspeed;
            ninjastar.GetComponent<Ninjastar>().speedmultiplicator = ninjastarspeed;
        }
    }
    private void OnEnable()
    {      
        Ray ray = new Ray(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.3f, Vector3.down);
        Physics.Raycast(ray, out RaycastHit hit, 30);
        Vector3 startpoint = hit.point;
        Vector3 spawn = startpoint + Random.insideUnitSphere * 17;
        NavMeshHit hit1;
        NavMesh.Raycast(startpoint, spawn, out hit1, NavMesh.AllAreas);
        spawn = hit1.position;
        savezone.transform.position = spawn + Vector3.up;
        savezone.SetActive(true);

        currentstar = 0;
        ninjastars[currentstar].transform.position = LoadCharmanager.Overallmainchar.transform.position + LoadCharmanager.Overallmainchar.transform.forward * ninjastarspawnradius + LoadCharmanager.Overallmainchar.transform.up * 2;
        ninjastars[currentstar].SetActive(true);
        currentstar++;

        ninjastars[currentstar].transform.position = LoadCharmanager.Overallmainchar.transform.position + LoadCharmanager.Overallmainchar.transform.forward * -ninjastarspawnradius + LoadCharmanager.Overallmainchar.transform.up * 2;
        ninjastars[currentstar].SetActive(true);
        currentstar++;

        ninjastars[currentstar].transform.position = LoadCharmanager.Overallmainchar.transform.position + LoadCharmanager.Overallmainchar.transform.right * ninjastarspawnradius + LoadCharmanager.Overallmainchar.transform.up * 2;
        ninjastars[currentstar].SetActive(true);
        currentstar++;

        ninjastars[currentstar].transform.position = LoadCharmanager.Overallmainchar.transform.position + LoadCharmanager.Overallmainchar.transform.right * -ninjastarspawnradius + LoadCharmanager.Overallmainchar.transform.up * 2;
        ninjastars[currentstar].SetActive(true);
    }
}
