using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCharwechselalt : MonoBehaviour
{
    /*private SpielerSteu Steuerung;
    [SerializeField] internal Movescript Movementscript;
    public GameObject Mainchar;
    public GameObject Secondchar;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;
    public GameObject LockonUI;
    void Awake()
    {
        Mainchar = LoadCharmanager.Overallmainchar;
        Steuerung = new SpielerSteu();
        Secondchar.SetActive(false);
    }
    private void OnEnable()
    {
        Mainchar = LoadCharmanager.Overallmainchar;
        Secondchar = LoadCharmanager.Overallsecondchar;
        Steuerung.Enable();
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
            Movescript.availabletargets.Clear();            //Lockontraget.availabletargets.Clear();
            //animator.Play("Idle", 0, 0f);
            Secondchar.transform.position = Mainchar.transform.position;
            Secondchar.transform.rotation = Mainchar.transform.rotation;
            LoadCharmanager.Overallmainchar.SetActive(false);
            Mainchar.SetActive(false);
            Secondchar.SetActive(true);
            Cam1.LookAt = Secondchar.transform;
            Cam1.Follow = Secondchar.transform;
            Cam2.LookAt = Secondchar.transform;
            Cam2.Follow = Secondchar.transform;
        }
    }*/
}
