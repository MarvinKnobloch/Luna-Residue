using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblincontroller : MonoBehaviour
{
    [SerializeField] private GameObject cast1;
    [SerializeField] private GameObject cast2;
    [SerializeField] private GameObject cast3;

    [SerializeField] private float spheredmg;
    [SerializeField] private float timebetweenspawn;
    [SerializeField] private float explodetimer;

    private void Awake()
    {
        cast1.GetComponent<Goblinsphere>().basedmg = spheredmg;
        cast1.GetComponent<Goblinsphere>().timetoexplode = explodetimer;
        cast2.GetComponent<Goblinsphere>().basedmg = spheredmg;
        cast2.GetComponent<Goblinsphere>().timetoexplode = explodetimer;
        cast3.GetComponent<Goblinsphere>().basedmg = spheredmg;
        cast3.GetComponent<Goblinsphere>().timetoexplode = explodetimer;
    }
    private void OnEnable()
    {
        cast1.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        cast1.SetActive(true);
        Invoke("spezial2", timebetweenspawn);
    }
    private void spezial2()
    {
        cast2.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        cast2.SetActive(true);
        Invoke("spezial3", timebetweenspawn);
    }
    private void spezial3()
    {
        cast3.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        cast3.SetActive(true);
        Invoke("spezial4", timebetweenspawn);
    }
    private void spezial4()
    {
        cast1.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        cast1.SetActive(true);
        Invoke("spezial5", timebetweenspawn);
    }
    private void spezial5()
    {
        cast2.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        cast2.SetActive(true);
        Invoke("spezial6", timebetweenspawn);
    }
    private void spezial6()
    {
        cast3.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        cast3.SetActive(true);
        Invoke("spezial7", timebetweenspawn);
    }
    private void spezial7()
    {
        cast1.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        cast1.SetActive(true);
        Invoke("spezial8", timebetweenspawn);
    }
    private void spezial8()
    {
        cast2.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        cast2.SetActive(true);
        Invoke("spezial9", timebetweenspawn);
    }
    private void spezial9()
    {
        cast3.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        cast3.SetActive(true);
        Invoke("turnoffcontroller", 1);
    }
    private void turnoffcontroller()
    {
        gameObject.SetActive(false);
    }
}
