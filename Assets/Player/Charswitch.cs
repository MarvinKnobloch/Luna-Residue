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
            if (Steuerung.Player.Charchange.WasPerformedThisFrame() && Statics.otheraction == false && Statics.charswitchbool == false)
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
        if(LoadCharmanager.Overallsecondchar != null && LoadCharmanager.Overallsecondchar.GetComponent<Playerhp>().playerisdead == false)
        {
            switchvalues();
            GetComponent<Healthuimanager>().switchtosecond();
            GlobalCD.currentcharswitchchar = Statics.currentsecondchar;
            GlobalCD.startcharswitch();                                            //hier wird auch der weaponcd zurückgesetzt
            ability1.color = Statics.spellcolors[3];
            ability2.color = Statics.spellcolors[4];
            Statics.playertookdmgfromamount = Statics.tookdmgfromamount[1];
            Statics.currentactiveplayer = 1;
            charuiimage.sprite = charimages[Statics.currentfirstchar];
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
            charuiimage.sprite = charimages[Statics.currentsecondchar];
        }
    }
    private void switchvalues()
    {
        Statics.donttriggerenemies = true;
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
        LoadCharmanager.Overallmainchar.gameObject.GetComponent<Weaponswitch>().imageupdateaftercharswitch();
        manacontroller.Managemana(5);
        StartCoroutine("triggerenemiesdisable");
    }
    public void setcharswitchimageafterload()
    {
        charuiimage.sprite = charimages[Statics.currentsecondchar];
    }

    IEnumerator triggerenemiesdisable()
    {
        yield return null;
        Statics.donttriggerenemies = false;
    }
}


/*
if (Statics.healmissingtime > 9f)
{
    Statics.healmissingtime = 9f;
    GlobalCD.onswitchhealingcd();
}*/


