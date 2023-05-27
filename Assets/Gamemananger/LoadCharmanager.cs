using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

//elemenu spells beim charwechsel reseten?

public class LoadCharmanager : MonoBehaviour
{
    private SpielerSteu Steuerung;
    public CinemachineFreeLook Cam1;
    [SerializeField] CinemachineVirtualCamera aimcam;
    private Uiactionscontroller uiactionscontroller;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject menuoverview;
    public static Expmanager expmanager;

    public GameObject[] allcharacters;
    public static GameObject Overallmainchar;
    public static GameObject Overallsecondchar;
    [SerializeField] private GameObject triggercolliderobj;
    public static GameObject triggercollider;
    public GameObject[] teammates;
    public static GameObject Overallthirdchar;
    public static GameObject Overallforthchar;
    public static Vector3 savemainposi = new Vector3(31, 31, 671);   //new Vector3(31,31,671);                    //new Vector3(19, 2, 867); tutorial
    public static Quaternion savemainrota;
    public static float savecamvalueX;

    public static bool disableattackbuttons;
    public static bool gameispaused;
    public static bool interaction;
    public static bool cantsavehere;

    [SerializeField] private GameObject map;
    [SerializeField] private LayerMask meleehitbox;

    public static event Action setweapons;
    public static event Action swordcontrollerupdate;
    public static event Action fistcontrollerupdate;

    private void Awake()
    {
        expmanager = GetComponent<Expmanager>();
        uiactionscontroller = GetComponent<Uiactionscontroller>();
        Steuerung = Keybindinputmanager.inputActions;
        triggercollider = triggercolliderobj;
        cantsavehere = false;
        setdifficulty();
    }

    private void OnEnable()
    {
        Steuerung.Enable();
        for (int i = 0; i < Statics.charcurrenthealth.Length; i++)
        {
            Statics.charcurrenthealth[i] = Statics.charmaxhealth[i];
        }
        maingamevalues();
    }
    private void Start()
    {
        resetvalues();                                                   //falls der kampf vorbei ist bei noch spezialattacks getriggert werden und man dann ins menu geht(menu open sollte nicht m�glich sein aber zur sicherheit)
        Statics.gameoverposi = savemainposi;
        Statics.gameoverrota = savemainrota;
        Statics.gameovercam = savecamvalueX;
        Invoke("setgameovermusic", 0.1f);                            //damit auch der collider mit der neuen musik bei start/fasttravel getriggert worden ist
    }
    private void setgameovermusic()
    {
        Statics.aftergameovermusic = Statics.currentzonemusicint;
    }
    private void resetvalues()
    {
        Statics.dash = false;
        Statics.enemyspezialtimescale = false;
        Overallmainchar.GetComponent<Movescript>().movementspeed = Statics.playermovementspeed;
        Overallsecondchar.GetComponent<Movescript>().movementspeed = Statics.playermovementspeed;
    }
    void Update()
    {
        if (Steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame() && Statics.infight == false && interaction == false && Statics.otheraction == false)
        {
            if (gameispaused == false)
            {
                if(map.activeSelf == false)
                {
                    //GlobalCD.instance.stopwatermovementtimer();
                    disableattackbuttons = true;
                    savemainposi = Overallmainchar.transform.position;
                    savemainrota = Overallmainchar.transform.rotation;
                    savecamvalueX = Cam1.m_XAxis.Value;
                    foreach (GameObject mates in teammates)
                    {
                        mates.gameObject.SetActive(false);
                    }
                    gameispaused = true;
                    Time.timeScale = 0f;
                    Cam1.gameObject.SetActive(false);
                    map.SetActive(false);
                    menu.SetActive(true);
                    menuoverview.SetActive(true);
                    Mouseactivate.enablemouse();
                }
            }
            else
            {
                if (menuoverview.GetComponent<Menucontroller>().somethinginmenuisopen == false)
                {
                    maingamevalues();
                }
            }
        }
    }
    public void maingamevalues()
    {
        foreach (GameObject chars in allcharacters)
        {
            chars.SetActive(false);
        }
        Overallmainchar = allcharacters[Statics.currentfirstchar];
        Overallsecondchar = allcharacters[Statics.currentsecondchar];
        Statics.currentactiveplayer = 0;
        Overallmainchar.transform.position = savemainposi;
        Overallmainchar.transform.rotation = savemainrota;
        Overallmainchar.SetActive(true);
        Overallsecondchar.SetActive(true);
        Overallmainchar.GetComponent<Playerhp>().playerhpuislot = 0;
        Overallsecondchar.GetComponent<Playerhp>().playerhpuislot = 1;
        if (Statics.currentthirdchar != -1)
        {
            Overallthirdchar = teammates[Statics.currentthirdchar];     //3 ist aktiv
            Overallthirdchar.GetComponent<Playerhp>().playerhpuislot = 2;
            Overallthirdchar.SetActive(true);                                       //wird momentan im Infightcontroller wieder ausgeblendet

            if (Statics.currentforthchar != -1)
            {
                Overallforthchar = teammates[Statics.currentforthchar];   //wenn 3 aktiv ist wieder gecheckt ob 4 aktiv ist
                Overallforthchar.GetComponent<Playerhp>().playerhpuislot = 3;
                Overallforthchar.SetActive(true);
            }
            else
            {
                Overallforthchar = null;
            }
        }
        else
        {
            if (Statics.currentforthchar != -1)                      //wenn 3 nicht aktiv ist wird gecheckt ob 4 aktiv ist, und 4 ist dann 3
            {
                Statics.currentthirdchar = Statics.currentforthchar;
                Statics.currentforthchar = -1;
                Overallthirdchar = teammates[Statics.currentthirdchar];
                Overallthirdchar.GetComponent<Playerhp>().playerhpuislot = 2;
                Overallthirdchar.SetActive(true);
                Overallforthchar = null;
            }
            else
            {
                Overallthirdchar = null;
                Overallforthchar = null;
            }
        }

        Cam1.LookAt = Overallmainchar.transform;
        Cam1.Follow = Overallmainchar.transform;
        Cam1.m_YAxis.m_MaxSpeed = Statics.presetcamymaxspeed * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        Cam1.m_XAxis.m_MaxSpeed = Statics.presetcamxmaxspeed * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        Cam1.m_XAxis.Value = savecamvalueX;
        aimcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0.2f * PlayerPrefs.GetFloat("rangeweaponaimsensitivity") / 50;
        aimcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0.2f * PlayerPrefs.GetFloat("rangeweaponaimsensitivity") / 50;
        aimcam.LookAt = Overallmainchar.transform;
        aimcam.Follow = Overallmainchar.transform;

        GetComponent<Charswitch>().setcharswitchimageafterload();
        Overallsecondchar.GetComponent<Weaponswitch>().resetmainweaponactiv();


        classandstatsupdate(Statics.currentfirstchar, allcharacters, 0);                   // bonus hp von guard wird im elemenu beim ausw�hlen des stones gesetzt
        classandstatsupdate(Statics.currentsecondchar, allcharacters, 1);
        classandstatsupdate(Statics.currentthirdchar, teammates, 2);
        classandstatsupdate(Statics.currentforthchar, teammates, 3);
        Statics.playertookdmgfromamount = Statics.tookdmgfromamount[0];

        GetComponent<Healthuimanager>().sethealthbars();

        setweapons?.Invoke();

        swordcontrollerupdate?.Invoke();
        fistcontrollerupdate?.Invoke();

        uiactionscontroller.hotkeysupdate();

        foreach (GameObject mates in teammates)
        {
            mates.gameObject.SetActive(false);
        }

        Overallsecondchar.SetActive(false);
        if (Overallthirdchar != null)                           //damit die Stats richtig geladen werden
        {
            Overallthirdchar.SetActive(false);
        }
        if (Overallforthchar != null)
        {
            Overallforthchar.SetActive(false);
        }
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        if(menu.activeSelf == true) StartCoroutine("waitbecausecollision");
        else
        {
            disableattackbuttons = false;
        }
        gameispaused = false;
        menu.SetActive(false);
        Cam1.gameObject.SetActive(true);
        Mouseactivate.disablemouse();
    }
    IEnumerator waitbecausecollision()
    {
        yield return null;
        disableattackbuttons = false;
    }
    private void classandstatsupdate(int charnumber, GameObject[] playerorsupport, int threatslot)
    {
        if(charnumber != -1)
        {
            if (Statics.characterclassroll[charnumber] == 0)
            {
                if (playerorsupport[charnumber].TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.isdmgclassroll = true;
                    atbcontroller.isguardclassroll = false;
                    atbcontroller.ishealerclassroll = false;
                    Statics.tookdmgfromamount[threatslot] = 1;
                    atbcontroller.classrollupdate();
                }
            }
            else if (Statics.characterclassroll[charnumber] == 1)
            {
                if (playerorsupport[charnumber].TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.isdmgclassroll = false;
                    atbcontroller.isguardclassroll = true;
                    atbcontroller.ishealerclassroll = false;
                    Statics.tookdmgfromamount[threatslot] = 2;
                    atbcontroller.classrollupdate();
                }
            }
            else if (Statics.characterclassroll[charnumber] == 2)
            {
                if (playerorsupport[charnumber].TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.isdmgclassroll = false;
                    atbcontroller.isguardclassroll = false;
                    atbcontroller.ishealerclassroll = true;
                    Statics.tookdmgfromamount[threatslot] = 1;
                    atbcontroller.classrollupdate();
                }
            }
            else
            {
                if (playerorsupport[charnumber].TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.isdmgclassroll = false;
                    atbcontroller.isguardclassroll = false;
                    atbcontroller.ishealerclassroll = false;
                    Statics.tookdmgfromamount[threatslot] = 1;
                    atbcontroller.classrollupdate();
                }
            }
        }
    }
    private void setdifficulty()
    {
        if (Statics.difficulty == 0) easydifficulty();
        else if (Statics.difficulty == 1) normaldifficulty();
        else harddifficulty();
    }
    public void easydifficulty()
    {
        Statics.enemydmgmultiplier = 1.1f;
        Statics.enemyspezialdmgbonus = 3;
        Statics.enemyhealthpercantageadded = 0.05f;
        Statics.enemyspecialcd = 18;
        Statics.enemydifficultyminusdmg = 4;
    }
    public void normaldifficulty()
    {
        Statics.enemydmgmultiplier = 1.3f;
        Statics.enemyspezialdmgbonus = 4;
        Statics.enemyhealthpercantageadded = 0.075f;
        Statics.enemyspecialcd = 16;
        Statics.enemydifficultyminusdmg = 2;
    }
    public void harddifficulty()
    {
        Statics.enemydmgmultiplier = 1.5f;
        Statics.enemyspezialdmgbonus = 5;
        Statics.enemyhealthpercantageadded = 0.1f;
        Statics.enemyspecialcd = 14;
        Statics.enemydifficultyminusdmg = 0;
    }
}
