using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirecontroller : MonoBehaviour
{
    [SerializeField] private GameObject spherespawn1;
    [SerializeField] private GameObject spherespawn2;
    [SerializeField] private GameObject spezialendspawn;

    [SerializeField] private float spheredmg;
    [SerializeField] private float spheredodgetime;
    [SerializeField] private float cubedmg;
    [SerializeField] private float cubedodgetime;
    private void Awake()
    {
        spherespawn1.GetComponent<Vampiresphere>().basedmg = spheredmg;
        spherespawn1.GetComponent<Vampiresphere>().explodetime = spheredodgetime;
        spherespawn2.GetComponent<Vampiresphere>().basedmg = spheredmg;
        spherespawn2.GetComponent<Vampiresphere>().explodetime = spheredodgetime;
        spezialendspawn.GetComponent<Vampirecube>().basedmg = cubedmg;
        spezialendspawn.GetComponent<Vampirecube>().explodetime = cubedodgetime;
    }
    private void OnEnable()
    {
        spherespawn1.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        spherespawn1.SetActive(true);
        Invoke("spezialpart2", 1f);

    }
    private void spezialpart2()
    {
        spherespawn2.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        spherespawn2.SetActive(true);
        Invoke("spezialpart3", 1f);
    }
    private void spezialpart3()
    {
        spezialendspawn.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        spezialendspawn.transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
        spezialendspawn.SetActive(true);
    }
}
