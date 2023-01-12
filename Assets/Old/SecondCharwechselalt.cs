using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SecondCharwechselalt : MonoBehaviour
{
    /*private SpielerSteu Steuerung;
    [SerializeField] internal Movescript Movementscript;
    public GameObject Secondchar;
    public GameObject Mainchar;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;
    public GameObject LockonUI;
    void Awake()
    {
        Steuerung = new SpielerSteu();
    }
    private void OnEnable()
    {
        Steuerung.Enable();
        Mainchar = LoadCharmanager.Overallmainchar;
        Secondchar = LoadCharmanager.Overallsecondchar;
    }

    void Update()
    {
        if (Steuerung.Spielerboden.Wechsel.WasPerformedThisFrame())
        {
            Time.timeScale = Movementscript.normalgamespeed;
            Time.fixedDeltaTime = Movementscript.normaltimedelta;
            LockonUI.SetActive(false);
            Cam1.m_RecenterToTargetHeading.m_RecenteringTime = 0.2f;
            //Movementscript.lockoncheck = false;
            Cam1.m_RecenterToTargetHeading.m_enabled = false;
            Movescript.availabletargets.Clear();                             //Lockontraget.availabletargets.Clear();
            //animator.Play("Idle", 0, 0f);
            Mainchar.transform.position = Secondchar.transform.position;
            Mainchar.transform.rotation = Secondchar.transform.rotation;
            Secondchar.SetActive(false);
            Mainchar.SetActive(true);
            Cam1.LookAt = Mainchar.transform;
            Cam1.Follow = Mainchar.transform;
            Cam2.LookAt = Mainchar.transform;
            Cam2.Follow = Mainchar.transform;
        }
    }*/
}