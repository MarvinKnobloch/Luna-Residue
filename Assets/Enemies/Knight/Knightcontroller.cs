using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knightcontroller : MonoBehaviour
{
    private Vector3 maincharposi;
    [SerializeField] private GameObject[] waves;
    private int nextwave;

    [SerializeField] private float wavedmg;
    [SerializeField] private float wavespeed;


    private void Awake()
    {
        foreach (GameObject wave in waves)
        {
            wave.GetComponent<Wavescript>().basedmg = wavedmg;
            wave.GetComponent<Wavescript>().speed = wavespeed;
        }
    }
    private void OnEnable()
    {
        foreach (GameObject wave in waves)
        {
            wave.SetActive(false);
        }
        maincharposi = LoadCharmanager.Overallmainchar.transform.position;
        RaycastHit hit;
        Ray nachunten = new Ray(LoadCharmanager.Overallmainchar.transform.position, Vector3.down * 10);
        if (Physics.Raycast(nachunten, out hit))
        {
            maincharposi.y = hit.point.y;
        }
        waves[0].transform.position = maincharposi + LoadCharmanager.Overallmainchar.transform.forward * 20;
        waves[0].transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
        waves[0].GetComponent<Wavescript>().endposi = maincharposi + LoadCharmanager.Overallmainchar.transform.forward * -20;
        waves[1].transform.position = maincharposi + LoadCharmanager.Overallmainchar.transform.forward * -20;
        waves[1].transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
        waves[1].GetComponent<Wavescript>().endposi = maincharposi + LoadCharmanager.Overallmainchar.transform.forward * 20;
        waves[2].transform.position = maincharposi + LoadCharmanager.Overallmainchar.transform.right * 20;
        waves[2].transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
        waves[2].GetComponent<Wavescript>().endposi = maincharposi + LoadCharmanager.Overallmainchar.transform.right * -20;
        waves[3].transform.position = maincharposi + LoadCharmanager.Overallmainchar.transform.right * -20;
        waves[3].transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
        waves[3].GetComponent<Wavescript>().endposi = maincharposi + LoadCharmanager.Overallmainchar.transform.right * 20;
        Invoke("wave1", 0.5f);
    }
    private void wave1()
    {
        nextwave = Random.Range(0, 3);
        waves[nextwave].SetActive(true);
        if (nextwave >= 3)
        {
            nextwave = 0;
        }
        else
        {
            nextwave++;
        }
        Invoke("wave2", 1f);
    }
    private void wave2()
    {
        waves[nextwave].SetActive(true);
        if (nextwave >= 3)
        {
            nextwave = 0;
        }
        else
        {
            nextwave++;
        }
        Invoke("wave3", 1f);
    }
    private void wave3()
    {
        waves[nextwave].SetActive(true);
        if (nextwave >= 3)
        {
            nextwave = 0;
        }
        else
        {
            nextwave++;
        }
        Invoke("wave4", 1f);
    }
    private void wave4()
    {
        waves[nextwave].SetActive(true);
        Invoke("turnoff", 2f);
    }
    private void turnoff()
    {
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().movementspeed = LoadCharmanager.Overallmainchar.GetComponent<Movescript>().normalmovementspeed;
        Statics.dazestunstart = false;
        Statics.otheraction = false;
        Statics.dash = false;
        gameObject.SetActive(false);
    }
}

