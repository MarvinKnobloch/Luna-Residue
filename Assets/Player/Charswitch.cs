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

    [SerializeField] private Image charuiimage;
    [SerializeField] private Sprite[] charimages;

    public Image ability1;
    public Image ability2;

    public GameObject charmanager;
    private Manamanager manacontroller;
    private LoadCharmanager loadcharmanager;
    private Bonushealscript bonushealscript;

    [SerializeField] private Playersounds playersounds;
    void Awake()
    {
        manacontroller = charmanager.GetComponent<Manamanager>();
        loadcharmanager = GetComponent<LoadCharmanager>();
        bonushealscript = GetComponent<Bonushealscript>();
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
            if (Steuerung.Player.Charchange.WasPerformedThisFrame() && Statics.otheraction == false && Statics.charswitchbool == false)
            {
                Statics.otheraction = true;
                playersounds.playcharswitch();
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
        if(LoadCharmanager.Overallsecondchar != null && LoadCharmanager.Overallsecondchar.GetComponent<Playerhp>().playerisdead == false)
        {
            switchvalues();
            GetComponent<Healthuimanager>().switchtosecond();
            GlobalCD.currentcharswitchchar = Statics.currentsecondchar;
            GlobalCD.startcharswitch();                                            //hier wird auch der weaponcd zur�ckgesetzt
            ability1.color = Statics.spellcolors[2];
            ability2.color = Statics.spellcolors[3];
            Statics.playertookdmgfromamount = Statics.tookdmgfromamount[1];
            Statics.currentactiveplayer = 1;
            LoadCharmanager.Overallmainchar.gameObject.GetComponent<Weaponswitch>().imageupdateaftercharswitch();
            charuiimage.sprite = charimages[Statics.currentfirstchar];

            loadcharmanager.checkforattributebonus(Statics.currentsecondchar);
            if (Statics.infight == true)
            {
                enemytargetupdate(Statics.currentsecondchar);
                charswitchexplosion(Statics.currentsecondchar);
                if (Statics.bonushealovertimebool == true) bonushealscript.enabled = true;
                else bonushealscript.enabled = false;
            }            
        }                                                  
    }

    private void switchtomainchar()
    {
        if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == false)
        {
            switchvalues();
            GetComponent<Healthuimanager>().switchtomain();
            GlobalCD.currentcharswitchchar = Statics.currentfirstchar;
            GlobalCD.startcharswitch();
            ability1.color = Statics.spellcolors[0];
            ability2.color = Statics.spellcolors[1];
            Statics.playertookdmgfromamount = Statics.tookdmgfromamount[0];
            Statics.currentactiveplayer = 0;
            LoadCharmanager.Overallmainchar.gameObject.GetComponent<Weaponswitch>().imageupdateaftercharswitch();
            charuiimage.sprite = charimages[Statics.currentsecondchar];

            loadcharmanager.checkforattributebonus(Statics.currentfirstchar);
            if (Statics.infight == true)
            {
                enemytargetupdate(Statics.currentfirstchar);
                charswitchexplosion(Statics.currentfirstchar);
                if (Statics.bonushealovertimebool == true) bonushealscript.enabled = true;
                else bonushealscript.enabled = false;
            }
        }
    }
    private void switchvalues()
    {
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        LoadCharmanager.Overallsecondchar.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        LoadCharmanager.Overallsecondchar.transform.rotation = LoadCharmanager.Overallmainchar.transform.rotation;
        LoadCharmanager.Overallmainchar.SetActive(false);
        GameObject Savechar = LoadCharmanager.Overallmainchar;
        LoadCharmanager.Overallmainchar = LoadCharmanager.Overallsecondchar;
        LoadCharmanager.Overallmainchar.SetActive(true);
        LoadCharmanager.Overallsecondchar = Savechar;
        foreach (GameObject enemy in Infightcontroller.infightenemylists)
        {
            if(enemy.TryGetComponent(out Enemymovement enemymovement))
            {
                if(enemymovement.currenttarget == LoadCharmanager.Overallsecondchar)
                {
                    enemymovement.currenttarget = LoadCharmanager.Overallmainchar;
                }
            }
        }
        Cam1.LookAt = LoadCharmanager.Overallmainchar.transform;
        Cam1.Follow = LoadCharmanager.Overallmainchar.transform;
        aimcam.LookAt = LoadCharmanager.Overallmainchar.transform;
        aimcam.Follow = LoadCharmanager.Overallmainchar.transform;
        LoadCharmanager.Overallmainchar.gameObject.GetComponent<Playerhp>().playerhpuislot = 0;
        LoadCharmanager.Overallsecondchar.gameObject.GetComponent<Playerhp>().playerhpuislot = 1;
        manacontroller.Managemana(5);
    }
    public void setcharswitchimageafterload()
    {
        charuiimage.sprite = charimages[Statics.currentsecondchar];
    }
    private void enemytargetupdate(int currentchar)
    {
        foreach (GameObject obj in Infightcontroller.infightenemylists)
        {
            if (obj.TryGetComponent(out EnemyHP enemyhp))
            {
                if (enemyhp.currentplayerwithmosthits == 0) enemyhp.healthbar.targetupdate(currentchar);
            }
        }
    }
    private void charswitchexplosion(int charnumber)
    {
        if(Statics.bonuscharexplosion == true)
        {
            float explosiondmg = 30 + Statics.charweaponbuff[charnumber] + Statics.charswitchbuff[charnumber];
            float finalexplosiondmg = Mathf.Round(explosiondmg + ((Statics.charweaponbuff[charnumber] + Statics.charswitchbuff[charnumber]) * 0.01f * explosiondmg));
            float healamount = finalexplosiondmg * 0.5f;

            Globalplayercalculations.addplayerhealth(LoadCharmanager.Overallmainchar, healamount, true);
            Globalplayercalculations.addplayerhealth(LoadCharmanager.Overallsecondchar, healamount, false);
            Globalplayercalculations.addplayerhealth(LoadCharmanager.Overallthirdchar, healamount, false);
            Globalplayercalculations.addplayerhealth(LoadCharmanager.Overallforthchar, healamount, false);

            if(Infightcontroller.infightenemylists.Count > 0)
            {
                finalexplosiondmg = finalexplosiondmg / Infightcontroller.infightenemylists.Count;
                for (int i = 0; i < Infightcontroller.infightenemylists.Count; i++)
                {
                    if (Infightcontroller.infightenemylists[i].TryGetComponent(out EnemyHP enemyhp))
                    {
                        enemyhp.takeplayerdamage(finalexplosiondmg, 0, false);
                    }
                }
            }
        }
    }
}


/*
if (Statics.healmissingtime > 9f)
{
    Statics.healmissingtime = 9f;
    GlobalCD.onswitchhealingcd();
}*/


