using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knightcontroller : MonoBehaviour
{
    private Vector3 waveposi;
    [SerializeField] private GameObject wavezero;
    [SerializeField] private GameObject[] waves;
    private int firstwave;
    private int secondwave;
    [SerializeField] private LayerMask waveraydonwlayer;

    [SerializeField] private float wavedmg;
    [SerializeField] private float wavespeed;


    private void Awake()
    {
        wavezero.GetComponent<Knightwavecontroller>().basedmg = wavedmg;
        wavezero.GetComponent<Knightwavecontroller>().speed = wavespeed / 2;
        foreach (GameObject wave in waves)
        {
            wave.GetComponent<Knightwavecontroller>().basedmg = wavedmg;
            wave.GetComponent<Knightwavecontroller>().speed = wavespeed;
        }
    }
    private void OnEnable()
    {
        foreach (GameObject wave in waves)
        {
            wave.SetActive(false);
        }
        waveposi = LoadCharmanager.Overallmainchar.transform.position;
        if (Physics.Raycast(LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 20, waveraydonwlayer, QueryTriggerInteraction.Ignore))
        {
            waveposi.y = hit.point.y - 0.5f;
        }
        float waveroation = Random.Range(0, 360);
        wavezero.transform.rotation = Quaternion.Euler(90, waveroation, 0);
        wavezero.transform.position = LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 10;
        wavezero.GetComponent<Knightwavecontroller>().endposi = LoadCharmanager.Overallmainchar.transform.position + Vector3.up * -10;
        waves[0].transform.position = waveposi + Vector3.right * 20;
        waves[0].GetComponent<Knightwavecontroller>().endposi = waveposi + Vector3.right * -20;
        waves[1].transform.position = waveposi + Vector3.forward * 20;
        waves[1].GetComponent<Knightwavecontroller>().endposi = waveposi + Vector3.forward * -20;
        waves[2].transform.position = waveposi + Vector3.right * -20;
        waves[2].GetComponent<Knightwavecontroller>().endposi = waveposi + Vector3.right * 20;
        waves[3].transform.position = waveposi + Vector3.forward * -20;
        waves[3].GetComponent<Knightwavecontroller>().endposi = waveposi + Vector3.forward * 20;
        Invoke("wave0", 0.1f);
    }
    private void wave0()
    {
        wavezero.SetActive(true);
        Invoke("wave1", 1.5f);
    }
    private void wave1()
    {
        firstwave = Random.Range(0, 4);
        waves[firstwave].SetActive(true);
        Invoke("wave2", 1.5f);
    }
    private void wave2()
    {
        getwavenumber();
        waves[secondwave].SetActive(true);
        Invoke("turnoff", 3f);
    }
    private void getwavenumber()
    {
        secondwave = Random.Range(0, 4);
        if(secondwave == firstwave)
        {
            getwavenumber();
        }
    }
    private void turnoff()
    {
        gameObject.SetActive(false);
    }
}

