using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Scrollbarhandle : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private Image handleimage;
    private float scrolltimer;

    private DateTime startdate;
    private DateTime currentdate;
    private float time;

            
    void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        handleimage.raycastTarget = true;
        controlls.Enable();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Wheel.WasPerformedThisFrame())
        {
            startdate = DateTime.Now;
            handleimage.raycastTarget = false;
        }
        if(handleimage.raycastTarget == false)
        {
            currentdate = DateTime.Now;
            time = 0.0000001f * (currentdate.Ticks - startdate.Ticks);
            if (time > 0.1f) handleimage.raycastTarget = true;
        }
    }
}
