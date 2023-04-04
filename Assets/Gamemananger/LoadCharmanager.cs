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
    public GameObject[] teammates;
    public static GameObject Overallthirdchar;
    public static GameObject Overallforthchar;
    public static Vector3 savemainposi = new Vector3(15, 32, 687);      //new Vector3(-20,38, 420);          //new Vector3(116, 17, 707);        new Vector3(15,32,687);
    public static Quaternion savemainrota;
    public static float savecamvalueX;

    public static bool disableattackbuttons;
    public static bool gameispaused;
    public static bool interaction;
    public static bool cantsavehere;

    [SerializeField] private LayerMask meleehitbox;

    public static event Action setweapons;
    public static event Action swordcontrollerupdate;
    public static event Action fistcontrollerupdate;

    private void Awake()
    {
        expmanager = GetComponent<Expmanager>();
        uiactionscontroller = GetComponent<Uiactionscontroller>();
        Steuerung = Keybindinputmanager.inputActions;
        cantsavehere = false;
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
        resetvalues();                                                   //falls der kampf vorbei ist bei noch spezialattacks getriggert werden und man dann ins menu geht(menu open sollte nicht möglich sein aber zur sicherheit)
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
                disableattackbuttons = true;
                savemainposi = Overallmainchar.transform.position;
                savemainrota = Overallmainchar.transform.rotation;
                savecamvalueX = Cam1.m_XAxis.Value;
                foreach (GameObject mates in teammates)
                {
                    mates.gameObject.SetActive(false);
                }
                Statics.donttriggerenemies = true;
                gameispaused = true;
                Time.timeScale = 0f;
                Cam1.gameObject.SetActive(false);
                menu.SetActive(true);
                menuoverview.SetActive(true);
                Mouseactivate.enablemouse();
            }
            else
            {
                if (Menucontroller.inoverview == true && menuoverview.GetComponent<Menucontroller>().somethinginmenuisopen == false)
                {
                    maingamevalues();
                }
            }
        }
    }
    /*public void loadonfastravel()
    {
        Time.timeScale = Statics.normalgamespeed;                             // Time scale muss vorher wieder auf normalspeed gesetzt werden sonst wird onexit nicht getriggert
        Time.fixedDeltaTime = Statics.normaltimedelta;
        StartCoroutine("portchar");
    }
    IEnumerator portchar()
    {
        yield return null;
        Overallmainchar.transform.position = savemainposi;                   // ein frame dealy für timescale
        Overallmainchar.transform.rotation = savemainrota;
        Overallmainchar.gameObject.GetComponent<Movescript>().switchtoairstate();
        StartCoroutine("loadgamevalues");                                    //ein frame dealy sonst wird nicht getriggert 
    }
    IEnumerator loadgamevalues()                                            
    {
        yield return null;
        maingamevalues();
    }*/

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
        Cam1.m_YAxis.m_MaxSpeed = 0.003f * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        Cam1.m_XAxis.m_MaxSpeed = 0.6f * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        Cam1.m_XAxis.Value = savecamvalueX;
        aimcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0.2f * PlayerPrefs.GetFloat("rangeweaponaimsensitivity") / 50;
        aimcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0.2f * PlayerPrefs.GetFloat("rangeweaponaimsensitivity") / 50;
        aimcam.LookAt = Overallmainchar.transform;
        aimcam.Follow = Overallmainchar.transform;

        GetComponent<Charswitch>().setcharswitchimageafterload();
        Overallsecondchar.GetComponent<Weaponswitch>().resetmainweaponactiv();


        classandstatsupdate(Statics.currentfirstchar, allcharacters, 0);                   // bonus hp von guard wird im elemenu beim auswählen des stones gesetzt
        classandstatsupdate(Statics.currentsecondchar, allcharacters, 1);
        classandstatsupdate(Statics.currentthirdchar, teammates, 2);
        classandstatsupdate(Statics.currentforthchar, teammates, 3);
        Statics.playertookdmgfromamount = Statics.tookdmgfromamount[0];

        GetComponent<Healthuimanager>().sethealthbars();

        setweapons?.Invoke();

        swordcontrollerupdate?.Invoke();
        fistcontrollerupdate?.Invoke();

        uiactionscontroller.setimagecolor();

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
            Statics.donttriggerenemies = false;
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
        Statics.donttriggerenemies = false;
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
}
