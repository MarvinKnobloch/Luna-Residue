using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closecurrentopenmenu : MonoBehaviour
{
    public GameObject currentopenmenu;
    public GameObject Menucontroller;
    public GameObject Characteroverview;
    private SpielerSteu Steuerung;

    private void Awake()
    {
        Steuerung = new SpielerSteu();
    }
    private void OnEnable()
    {
        Steuerung.Enable();
    }

    void Update()
    {
        if (Steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            closecurrentopenmenu();
        }
    }

    private void closecurrentopenmenu()
    {
        currentopenmenu.SetActive(false);
        Menucontroller.SetActive(true);
        Characteroverview.SetActive(true);
    }
}
