using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Charswitch : MonoBehaviour
{
    private SpielerSteu Steuerung;
    [SerializeField] private CinemachineFreeLook Cam1;
    [SerializeField] private CinemachineVirtualCamera aimcam;
    public Image ability1;
    public Image ability2;

    public GameObject charmanager;
    private Manamanager manacontroller;

    void Awake()
    {
        manacontroller = charmanager.GetComponent<Manamanager>();
        Steuerung = Keybindinputmanager.inputActions;
    }

    private void OnEnable()
    {
        Steuerung.Enable();
    }

    void Update()
    {
        if (LoadCharmanager.disableattackbuttons == false)
        {
            if (Steuerung.Spielerboden.Charchange.WasPerformedThisFrame() && Statics.otheraction == false && Statics.charswitchbool == false)
            {
                if (Statics.currentactiveplayer == 0)
                {
                    switchtosecondchar();
                }
                else
                {
                    switchtomainchar();
                }
            }
        }
    }
    private void switchtosecondchar()
    {
        if (Statics.healmissingtime > 9f)
        {
            Statics.healmissingtime = 9f;
            GlobalCD.onswitchhealingcd();
        }
        Statics.otheraction = false;
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        LoadCharmanager.Overallsecondchar.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        LoadCharmanager.Overallsecondchar.transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
        LoadCharmanager.Overallmainchar.SetActive(false);
        LoadCharmanager.Savechar = LoadCharmanager.Overallmainchar;
        LoadCharmanager.Overallmainchar = LoadCharmanager.Overallsecondchar;
        LoadCharmanager.Overallmainchar.SetActive(true);
        LoadCharmanager.Overallsecondchar = LoadCharmanager.Savechar;
        GetComponent<HealthUImanager>().switchtosecond();
        Cam1.LookAt = LoadCharmanager.Overallmainchar.transform;
        Cam1.Follow = LoadCharmanager.Overallmainchar.transform;
        aimcam.LookAt = LoadCharmanager.Overallmainchar.transform;
        aimcam.Follow = LoadCharmanager.Overallmainchar.transform;
        GlobalCD.currentcharswitchchar = PlayerPrefs.GetInt("Secondcharindex");
        GlobalCD.startcharswitch();
        ability1.color = Statics.spellcolors[3];
        ability2.color = Statics.spellcolors[4];
        if(Statics.secondcharstoneclass == 1)
        {
            Statics.playertookdmgfromamount = 2;
        }
        else
        {
            Statics.playertookdmgfromamount = 1;
        }
        Statics.currentactiveplayer = 1;                                                     
        manacontroller.Managemana(5);
    }

    private void switchtomainchar()
    {
        if (Statics.healmissingtime > 9f)
        {
            Statics.healmissingtime = 9f;
            GlobalCD.onswitchhealingcd();
        }
        Statics.otheraction = false;
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        LoadCharmanager.Overallsecondchar.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        LoadCharmanager.Overallsecondchar.transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
        LoadCharmanager.Overallmainchar.SetActive(false);
        LoadCharmanager.Savechar = LoadCharmanager.Overallmainchar;
        LoadCharmanager.Overallmainchar = LoadCharmanager.Overallsecondchar;
        LoadCharmanager.Overallmainchar.SetActive(true);
        LoadCharmanager.Overallsecondchar = LoadCharmanager.Savechar;
        GetComponent<HealthUImanager>().switchtomain();
        Cam1.LookAt = LoadCharmanager.Overallmainchar.transform;
        Cam1.Follow = LoadCharmanager.Overallmainchar.transform;
        aimcam.LookAt = LoadCharmanager.Overallmainchar.transform;
        aimcam.Follow = LoadCharmanager.Overallmainchar.transform;
        GlobalCD.currentcharswitchchar = PlayerPrefs.GetInt("Maincharindex");
        GlobalCD.startcharswitch();
        ability1.color = Statics.spellcolors[0];
        ability2.color = Statics.spellcolors[1];
        if (Statics.maincharstoneclass == 1)
        {
            Statics.playertookdmgfromamount = 2;
        }
        else
        {
            Statics.playertookdmgfromamount = 1;
        }
        Statics.currentactiveplayer = 0;                      //PlayerPrefs.GetInt("Maincharindex");
        manacontroller.Managemana(5);
    }    
}


/*
if (Statics.healmissingtime > 9f)
{
    Statics.healmissingtime = 9f;
    GlobalCD.onswitchhealingcd();
}
Statics.otheraction = false;
Time.timeScale = normalgamespeed;
Time.fixedDeltaTime = normaltimedelta;
LockonUI.SetActive(false);
Cam1.m_RecenterToTargetHeading.m_RecenteringTime = 0.2f;
Movescript.lockoncheck = false;
Cam1.m_RecenterToTargetHeading.m_enabled = false;
Movescript.availabletargets.Clear();
LoadCharmanager.Overallsecondchar.transform.position = LoadCharmanager.Overallmainchar.transform.position;
LoadCharmanager.Overallsecondchar.transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
LoadCharmanager.Overallmainchar.SetActive(false);
LoadCharmanager.Overallsecondchar.SetActive(true);
GetComponent<HealthUImanager>().switchtosecond();
Cam1.LookAt = LoadCharmanager.Overallsecondchar.transform;
Cam1.Follow = LoadCharmanager.Overallsecondchar.transform;
Cam2.LookAt = LoadCharmanager.Overallsecondchar.transform;
Cam2.Follow = LoadCharmanager.Overallsecondchar.transform;
secondcharscript.enabled = true;
this.enabled = false;*/


