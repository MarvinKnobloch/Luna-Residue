using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Secondcharwechsel : MonoBehaviour
{
    /*[SerializeField] internal Charswitch maincharscript;
    private SpielerSteu Steuerung;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;
    public Image ability1;
    public Image ability2;
    private float normalgamespeed;
    private float normaltimedelta;

    public GameObject charmanager;
    private Manamanager manacontroller;
    void Awake()
    {
        manacontroller = charmanager.GetComponent<Manamanager>();
        normalgamespeed = Time.timeScale;
        //normaltimedelta = Time.fixedDeltaTime;
        Steuerung = Keybindinputmanager.inputActions;
        this.enabled = false;
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
                newwechsel();
            }
        }
    }
    private void newwechsel()
    {
        if (LoadCharmanager.disableattackbuttons == false)
        {
            if (Statics.healmissingtime > 9f)
            {
                Statics.healmissingtime = 9f;
                GlobalCD.onswitchhealingcd();
            }
            Statics.otheraction = false;
            Time.timeScale = normalgamespeed;
            Time.fixedDeltaTime = normaltimedelta;
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
            Cam2.LookAt = LoadCharmanager.Overallmainchar.transform;
            Cam2.Follow = LoadCharmanager.Overallmainchar.transform;
            GlobalCD.currentcharswitchchar = PlayerPrefs.GetInt("Maincharindex");
            GlobalCD.startcharswitch();
            ability1.color = Statics.spellcolors[0];
            ability2.color = Statics.spellcolors[1];
            maincharscript.enabled = true;
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
            this.enabled = false;
        }
    }*/
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
LoadCharmanager.Overallmainchar.transform.position = LoadCharmanager.Overallsecondchar.transform.position;
LoadCharmanager.Overallmainchar.transform.rotation = LoadCharmanager.Overallsecondchar.transform.rotation;
LoadCharmanager.Overallsecondchar.SetActive(false);
LoadCharmanager.Overallmainchar.SetActive(true);
GetComponent<HealthUImanager>().switchtomain();
Cam1.LookAt = LoadCharmanager.Overallmainchar.transform;
Cam1.Follow = LoadCharmanager.Overallmainchar.transform;
Cam2.LookAt = LoadCharmanager.Overallmainchar.transform;
Cam2.Follow = LoadCharmanager.Overallmainchar.transform;
maincharscript.enabled = true;
this.enabled = false;*/
