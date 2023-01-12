using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninjacontroller : MonoBehaviour
{
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
