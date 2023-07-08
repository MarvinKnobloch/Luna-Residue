using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using System;
using UnityEngine.Video;

public class Tutorialcontroller : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] GameObject tutorialtextbox;
    public TextMeshProUGUI tutorialtext;
    [SerializeField] private Videocontroller videocontroller;
    [SerializeField] private GameObject currenttutorial;
    [SerializeField] CinemachineFreeLook Cam1;
    [SerializeField] CinemachineVirtualCamera Cam2;
    public Areacontroller areacontroller;

    private VideoClip videoclip;

    [NonSerialized] public bool opentutorialbox;
    [NonSerialized] public bool openvideobackground;

    [SerializeField] private GameObject healtutorial;
    [SerializeField] private GameObject attacktutorial;
    [SerializeField] private GameObject enemysizetutorial;

    [SerializeField] private Healthuimanager healthuimanager;
    [NonSerialized] public float savetutroialenemyspecialcd;
    [NonSerialized] public float newspecialcd = 7;


    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        controlls.Enable();
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
    public void opencurrenttutorial()
    {
        if (opentutorialbox == true) tutorialtextbox.SetActive(true);
        if (openvideobackground == true)
        {
            videocontroller.gameObject.SetActive(true);
            videocontroller.restartvideo();
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

    public void newvideo(VideoClip newvideoclip)
    {
        videoclip = newvideoclip;
        videocontroller.setnewvideo(videoclip);
    }
    public void healtutorialstart()
    {
        LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamageignoreiframes(20, false);
        LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().takedamageignoreiframes(20, false);
        healthuimanager.sethealthbars();
        Statics.charswitchbool = true;
        healtutorial.SetActive(true);
    }
    public void attacktutorialstart()
    {
        attacktutorial.SetActive(true);
    }
    public void enemysizetutorialstart()
    {
        savetutroialenemyspecialcd = Statics.enemyspecialcd;
        Statics.enemyspecialcd = newspecialcd;
        enemysizetutorial.SetActive(true);
    }
    public void currenttutorialdisable()
    {
        currenttutorial.SetActive(false);
    }
}
