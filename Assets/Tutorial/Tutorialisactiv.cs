using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Tutorialisactiv : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] GameObject tutorialtextbox;
    [SerializeField] private Videocontroller videocontroller;
    [SerializeField] private CinemachineFreeLook Cam1;
    [SerializeField] private CinemachineVirtualCamera Cam2;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        Mouseactivate.enablemouse();
        Cam1.m_YAxis.m_MaxSpeed = 0;
        Cam1.m_XAxis.m_MaxSpeed = 0;
        Cam2.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
        Cam2.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
        if (Cam2.gameObject.activeSelf == true)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().disableaimcam();
        }
        Time.timeScale = 0;
        LoadCharmanager.disableattackbuttons = true;
        LoadCharmanager.interaction = true;
    }
    private void Update()
    {
        if (controlls.Menusteuerung.F1.WasPressedThisFrame())
        {
            endtutorial();
        }
    }
    public void endtutorial()
    {
        Mouseactivate.disablemouse();
        Cam1.m_YAxis.m_MaxSpeed = Statics.presetcamymaxspeed * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        Cam1.m_XAxis.m_MaxSpeed = Statics.presetcamxmaxspeed * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        Cam2.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0.2f * PlayerPrefs.GetFloat("rangeweaponaimsensitivity") / 50;
        Cam2.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0.2f * PlayerPrefs.GetFloat("rangeweaponaimsensitivity") / 50;
        Time.timeScale = Statics.normalgamespeed;
        LoadCharmanager.disableattackbuttons = false;
        LoadCharmanager.interaction = false;
        tutorialtextbox.SetActive(false);
        videocontroller.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
