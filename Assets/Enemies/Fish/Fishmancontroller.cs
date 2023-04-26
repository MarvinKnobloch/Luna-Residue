using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishmancontroller : MonoBehaviour
{
    [SerializeField] private GameObject showspezial;
    [SerializeField] private GameObject fishmancircle;
    [SerializeField] private Fishmancolliderdmg fishmancolliderdmg;

    [SerializeField] private float playermovementspeedslow;
    [SerializeField] private float dodgetime;
    [SerializeField] private float spezialdmg;

    private Enemyspezialsound enemyspezialsound;

    private void Awake()
    {
        fishmancolliderdmg.basedmg = spezialdmg;
        enemyspezialsound = GetComponentInParent<Enemyspezialsound>();
    }
    private void OnEnable()
    {
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().movementspeed = playermovementspeedslow;
        LoadCharmanager.Overallsecondchar.GetComponent<Movescript>().movementspeed = playermovementspeedslow;
        fishmancircle.SetActive(true);
        Invoke("spezialpart1", 1.5f);
    }
    private void Update()
    {
        fishmancircle.transform.position = LoadCharmanager.Overallmainchar.transform.position;
    }
    private void spezialpart1()
    {
        showspezial.transform.rotation = Quaternion.Euler(90, LoadCharmanager.Overallmainchar.transform.eulerAngles.y, 0);
        showspezial.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        showspezial.SetActive(true);
        Invoke("dealfirstdmg", dodgetime);
    }
    private void dealfirstdmg()
    {
        enemyspezialsound.playfishmanspezialend();
        fishmancolliderdmg.gameObject.SetActive(true);
        Invoke("spezialpart2", 1.3f);
    }
    private void spezialpart2()
    {
        showspezial.transform.rotation = Quaternion.Euler(90, LoadCharmanager.Overallmainchar.transform.eulerAngles.y, 0);
        showspezial.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        showspezial.SetActive(true);
        Invoke("dealseconddmg", dodgetime);
    }
    private void dealseconddmg()
    {
        enemyspezialsound.playfishmanspezialend();
        fishmancolliderdmg.gameObject.SetActive(true);
        Invoke("fishmanspezialend", 0.1f);
    }
    private void fishmanspezialend()
    {
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().movementspeed = Statics.playermovementspeed;
        LoadCharmanager.Overallsecondchar.GetComponent<Movescript>().movementspeed = Statics.playermovementspeed;
        fishmancircle.SetActive(false);
        gameObject.SetActive(false);
    }
}
