using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gasmancontroller : MonoBehaviour
{
    [SerializeField] private GameObject redline;
    [SerializeField] private GameObject blueline;

    [SerializeField] private GameObject[] linetargets;
    [NonSerialized] public int targetscomplete;

    public float toslowdmg;
    public float linefaildmg;

    public float timetocomplete;
    private void Awake()
    {
        foreach (GameObject targets in linetargets)
        {
            targets.GetComponent<Gasmantarget>().faillinedmg = linefaildmg;
        }
    }

    private void OnEnable()
    {
        targetscomplete = 0;
        Invoke("turnonline", 1f);    
    }
    private void turnonline()
    {
        blueline.SetActive(true);
        redline.SetActive(true);
        Invoke("turnoffeverything", timetocomplete);
    }
    private void turnoffeverything()
    {
        LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().TakeDamage(toslowdmg);
        blueline.SetActive(false);
        redline.SetActive(false);
        gameObject.SetActive(false);
    }
    public void checktargets()
    {
        targetscomplete += 1;
        if (targetscomplete == 2)
        {
            CancelInvoke();
            Invoke("success", 0.3f);
        }
    }
    private void success()
    {
        CancelInvoke();
        blueline.SetActive(false);
        redline.SetActive(false);
        gameObject.SetActive(false);
    }
}
