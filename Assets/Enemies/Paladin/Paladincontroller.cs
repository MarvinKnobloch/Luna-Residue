using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladincontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] showspezial;

    private Vector3 spawnposi1;
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

    private void OnEnable()
    {
        currentspezial = 0;
        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;
        showspezial[currentspezial].SetActive(true);

        currentspezial++;
        showspezial[currentspezial].transform.position = LoadCharmanager.Overallmainchar.transform.position;                 //2. spawnt auf dem spieler
        Invoke("secondspawn", spawntimer);
    }
    private void secondspawn()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("thirdspawn", spawntimer);
    }
    private void thirdspawn()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + LoadCharmanager.Overallmainchar.transform.forward * 15;    //4. spawnt vor dem Spieler
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("forthspawn", spawntimer);
    }
    private void forthspawn()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("fifthspawn", spawntimer);
    }
    private void fifthspawn()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("spawnsix", spawntimer);
    }
    private void spawnsix()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("spawnseven", spawntimer);
    }
    private void spawnseven()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("spawneight", spawntimer);
    }
    private void spawneight()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("spawnnine", spawntimer);
    }
    private void spawnnine()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("spawnten", spawntimer);
    }
    private void spawnten()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("spawneleven", spawntimer);
    }
    private void spawneleven()
    {
        showspezial[currentspezial].SetActive(true);
        currentspezial++;

        spawnposi1 = LoadCharmanager.Overallmainchar.transform.position + Random.insideUnitSphere * spawnradius;
        spawnposi1.y = LoadCharmanager.Overallmainchar.transform.position.y;
        showspezial[currentspezial].transform.position = spawnposi1;

        Invoke("spawntwelve", spawntimer);
    }
    private void spawntwelve()
    {
        showspezial[currentspezial].SetActive(true);
        Invoke("turnoff", 1.1f);
    }

    private void turnoff()
    {
        gameObject.SetActive(false);
    }
}
