using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;
using UnityEngine.InputSystem;

//beim equipslot wechsel werden die itemwerte/stats nicht upgedated
//wenn bow ausgewechselt wird kommen animator fehlermeldungen
//wenn nicht infight und man vom mob wegläuft, spawnen beim reset die teammates
//elemenu spells beim charwechsel reseten?

public class LoadCharmanager : MonoBehaviour
{
    private SpielerSteu Steuerung;
    public CinemachineFreeLook Cam1;
    [SerializeField] CinemachineVirtualCamera aimcam;
    private Uiactionscontroller uiactionscontroller;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject menuoverview;

    public GameObject[] allcharacters;
    public static GameObject Overallmainchar;
    public static GameObject Overallsecondchar;
    public GameObject[] teammates;
    public static GameObject Overallthirdchar;
    public static GameObject Overallforthchar;
    public static GameObject Savechar;                                       //wird für charwechsel benutzt
    public static Vector3 savemainposi = new Vector3(0,5,0);
    public static Quaternion savemainrota;                                      //memorypuzzle(-40,25,685) //boxpuzzle(305,25,565) switchpuzzle(280,2,180)
    public static float savecamvalueX;                                          //statuepuzzle(-200,2,340) //woods(255,20,435)  //watercave(-330,12,-40)  //lantern(-210,32,500)

    private int maincharload;
    private int secondcharload;

    public static bool disableattackbuttons;
    public static bool gameispaused;
    public static bool interaction;

    private float guardbonushp;

    public static event Action setweapons;
    public static event Action swordcontrollerupdate;
    public static event Action fistcontrollerupdate;

    private void Awake()
    {
        Statics.otheraction = false;
        uiactionscontroller = GetComponent<Uiactionscontroller>();
        Steuerung = Keybindinputmanager.inputActions;
        maingamevalues();
    }

    private void Start()
    {
        guardbonushp = Statics.guardbonushpeachlvl;
    }
    private void OnEnable()
    {
        Steuerung.Enable();
    }
    void Update()
    {
        if (Statics.infight == false && interaction == false && Steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame() && Statics.otheraction == false)
        {
            if (gameispaused == false)
            {
                disableattackbuttons = true;
                Physics.IgnoreLayerCollision(0, 6);               //collision wird deaktiviert und nach einem frame wird das menü aufgerufen damit die area collider getriggert werden
                savemainposi = Overallmainchar.transform.position;
                savemainrota = Overallmainchar.transform.rotation;
                savecamvalueX = Cam1.m_XAxis.Value;
                foreach (GameObject mates in teammates)
                {
                    mates.gameObject.SetActive(false);
                }
                gameispaused = true;
                StartCoroutine(activatemenu());

#if !UNITY_EDITOR
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
#endif
            }
            else
            {
                if (Menucontroller.inoverview == true)
                {
                    maingamevalues();
                }
            }
        }    
    }
    IEnumerator activatemenu()
    {
        yield return null;
        Time.timeScale = 0f;
        foreach (GameObject chars in allcharacters)
        {
            chars.SetActive(false);
        }
        Cam1.gameObject.SetActive(false);
        menu.SetActive(true);
        menuoverview.SetActive(true);
    }

    public void maingamevalues()
    {
        Physics.IgnoreLayerCollision(0, 6, false);               //wegenfasttravel?
        maincharload = Statics.currentfirstchar;
        secondcharload = Statics.currentsecondchar;
        Overallmainchar = allcharacters[maincharload];
        Overallsecondchar = allcharacters[secondcharload];
        Overallmainchar.transform.position = savemainposi;
        Overallmainchar.transform.rotation = savemainrota;
        Overallmainchar.SetActive(true);
        Overallsecondchar.SetActive(true);
        Statics.currentactiveplayer = Statics.currentfirstchar;                            //PlayerPrefs.GetInt("Maincharindex");
        if (Statics.currentthirdchar != -1)
        {
            Overallthirdchar = teammates[Statics.currentthirdchar];     //3 ist aktiv
            Overallthirdchar.SetActive(true);                                       //wird momentan im Infightcontroller wieder ausgeblendet
            if (Statics.currentforthchar != -1)
            {
                Overallforthchar = teammates[Statics.currentforthchar];   //wenn 3 aktiv ist wieder gecheckt ob 4 aktiv ist
                Overallforthchar.SetActive(true);
            }
            else
            {
                Overallforthchar = null;
            }
        }
        else
        {
            if (Statics.currentforthchar != -1)                      //wenn 3 nicht aktiv ist wird gecheckt ob 4 aktiv ist
            {
                Overallthirdchar = teammates[Statics.currentforthchar];
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
        Cam1.m_YAxis.m_MaxSpeed = 0.008f * Statics.mousesensitivity;
        Cam1.m_XAxis.m_MaxSpeed = 0.6f * Statics.mousesensitivity;
        Cam1.m_XAxis.Value = savecamvalueX;
        aimcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0.2f * Statics.rangeweaponaimsensitivity;
        aimcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0.2f * Statics.rangeweaponaimsensitivity;
        aimcam.LookAt = Overallmainchar.transform;
        aimcam.Follow = Overallmainchar.transform;


        classrollupdate();
        foreach (GameObject chars in allcharacters)
        {
            chars.GetComponent<Attributecontroller>().updateattributes();
        }
        foreach (GameObject mates in teammates)
        {
            mates.GetComponent<Attributecontroller>().updateattributes();
        }
        setweapons?.Invoke();
        swordcontrollerupdate?.Invoke();
        fistcontrollerupdate?.Invoke();

        GetComponent<HealthUImanager>().sethealthbars();
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
        gameispaused = false;
        disableattackbuttons = false;
        menu.SetActive(false);
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        Cam1.gameObject.SetActive(true);
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void classrollupdate()
    {
        float dmgbonus = Statics.groupstonedmgbonus;
        float defensebonus = Statics.groupstonedefensebonus;
        float healbonus = Statics.groupstonehealbonus;

        if (Statics.maincharstoneclass == 0)
        {
            if (allcharacters[maincharload].TryGetComponent(out Attributecontroller atbcontroller))
            {
                if(Statics.thirdcharstoneclass == 0 || Statics.forthcharstoneclass == 0)
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus * 3;
                }
                else
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus * 2;
                }
                atbcontroller.overallstonedmgreduction = defensebonus;
                atbcontroller.overallstonehealbonus = healbonus;
                atbcontroller.ishealer = false;
                if (atbcontroller.guardhpbuff == true)
                {
                    atbcontroller.guardhpbuff = false;
                    Statics.charmaxhealth[maincharload] -= Statics.charcurrentlvl * guardbonushp;
                }
                if (Statics.currentactiveplayer == 0)                                 //es gibt kein secondplayertookdmgfrom, desewegen sollte hier alles richtig sein
                {
                    Statics.playertookdmgfromamount = 1;
                }
            }
        }
        else if (Statics.maincharstoneclass == 1)
        {
            if (allcharacters[maincharload].TryGetComponent(out Attributecontroller atbcontroller))
            {
                atbcontroller.overallstonebonusdmg = dmgbonus;
                atbcontroller.overallstonedmgreduction = defensebonus * 2;
                atbcontroller.overallstonehealbonus = healbonus;
                atbcontroller.ishealer = false;
                if (atbcontroller.guardhpbuff == false)
                {
                    atbcontroller.guardhpbuff = true;
                    Statics.charmaxhealth[maincharload] += Statics.charcurrentlvl * guardbonushp;
                }
                if (Statics.currentactiveplayer == 0)
                {
                    Statics.playertookdmgfromamount = 2;
                }
            }
        }
        else if (Statics.maincharstoneclass == 2)
        {
            if (allcharacters[maincharload].TryGetComponent(out Attributecontroller atbcontroller))
            {
                atbcontroller.overallstonebonusdmg = dmgbonus;
                atbcontroller.overallstonedmgreduction = defensebonus;
                atbcontroller.overallstonehealbonus = healbonus * 2;
                atbcontroller.ishealer = true;
                if (atbcontroller.guardhpbuff == true)
                {
                    atbcontroller.guardhpbuff = false;
                    Statics.charmaxhealth[maincharload] -= Statics.charcurrentlvl * guardbonushp;
                }
                if (Statics.currentactiveplayer == 0)
                {
                    Statics.playertookdmgfromamount = 1;
                }
            }
        }
        else                   //falls ein stone aktviert worden ist aber keiner ausgewählt wurde
        {
            if (allcharacters[maincharload].TryGetComponent(out Attributecontroller atbcontroller))
            {
                if (Statics.thirdcharstoneclass == 0 || Statics.forthcharstoneclass == 0)
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus * 2;
                }
                else
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus;
                }
                atbcontroller.overallstonedmgreduction = defensebonus;
                atbcontroller.overallstonehealbonus = healbonus;
            }
        }

        if (Statics.secondcharstoneclass == 0)
        {
            if (allcharacters[secondcharload].TryGetComponent(out Attributecontroller atbcontroller))
            {
                if (Statics.thirdcharstoneclass == 0 || Statics.forthcharstoneclass == 0)
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus * 3;
                }
                else
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus * 2;
                }
                atbcontroller.overallstonedmgreduction = defensebonus;
                atbcontroller.overallstonehealbonus = healbonus;
                atbcontroller.ishealer = false;
                if (atbcontroller.guardhpbuff == true)
                {
                    atbcontroller.guardhpbuff = false;
                    Statics.charmaxhealth[secondcharload] -= Statics.charcurrentlvl * guardbonushp;
                }
                if (Statics.currentactiveplayer == 1)
                {
                    Statics.playertookdmgfromamount = 1;
                }
            }
        }
        else if (Statics.secondcharstoneclass == 1)
        {
            if (allcharacters[secondcharload].TryGetComponent(out Attributecontroller atbcontroller))
            {
                atbcontroller.overallstonebonusdmg = dmgbonus;
                atbcontroller.overallstonedmgreduction = defensebonus * 2;
                atbcontroller.overallstonehealbonus = healbonus;
                atbcontroller.ishealer = false;
                if (atbcontroller.guardhpbuff == false)
                {
                    atbcontroller.guardhpbuff = true;
                    Statics.charmaxhealth[secondcharload] += Statics.charcurrentlvl * guardbonushp;
                }
                if (Statics.currentactiveplayer == 1)
                {
                    Statics.playertookdmgfromamount = 2;
                }
            }
        }
        else if (Statics.secondcharstoneclass == 2)
        {
            if (allcharacters[secondcharload].TryGetComponent(out Attributecontroller atbcontroller))
            {
                atbcontroller.overallstonebonusdmg = dmgbonus;
                atbcontroller.overallstonedmgreduction = defensebonus;
                atbcontroller.overallstonehealbonus = healbonus * 2;
                atbcontroller.ishealer = true;
                if (atbcontroller.guardhpbuff == true)
                {
                    atbcontroller.guardhpbuff = false;
                    Statics.charmaxhealth[secondcharload] -= Statics.charcurrentlvl * guardbonushp;
                }
                if (Statics.currentactiveplayer == 1)
                {
                    Statics.playertookdmgfromamount = 1;
                }
            }
        }
        else
        {
            if (allcharacters[secondcharload].TryGetComponent(out Attributecontroller atbcontroller))
            {
                if (Statics.thirdcharstoneclass == 0 || Statics.forthcharstoneclass == 0)
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus * 2;
                }
                else
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus;
                }
                atbcontroller.overallstonedmgreduction = defensebonus;
                atbcontroller.overallstonehealbonus = healbonus;
            }
        }

        if(Overallthirdchar != null)
        {
            if (Statics.thirdcharstoneclass == 0)
            {
                if (Overallthirdchar.TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus * 2;
                    atbcontroller.overallstonedmgreduction = defensebonus;
                    atbcontroller.overallstonehealbonus = healbonus;
                    Statics.thirdcharishealer = false;
                    if (atbcontroller.guardhpbuff == true)
                    {
                        atbcontroller.guardhpbuff = false;
                        Statics.charmaxhealth[Statics.currentthirdchar] -= Statics.charcurrentlvl * guardbonushp;
                    }
                    Statics.thirdchartookdmgformamount = 1;
                }
            }
            else if (Statics.thirdcharstoneclass == 1)
            {
                if (Overallthirdchar.TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus;
                    atbcontroller.overallstonedmgreduction = defensebonus * 2;
                    atbcontroller.overallstonehealbonus = healbonus;
                    Statics.thirdcharishealer = false;
                    if (atbcontroller.guardhpbuff == false)
                    {
                        atbcontroller.guardhpbuff = true;
                        Statics.charmaxhealth[Statics.currentthirdchar] += Statics.charcurrentlvl * guardbonushp;
                    }
                    Statics.thirdchartookdmgformamount = 2;
                }
            }
            else if (Statics.thirdcharstoneclass == 2)
            {
                if (Overallthirdchar.TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus;
                    atbcontroller.overallstonedmgreduction = defensebonus;
                    atbcontroller.overallstonehealbonus = healbonus * 2;
                    Statics.thirdcharishealer = true;
                    if (atbcontroller.guardhpbuff == true)
                    {
                        atbcontroller.guardhpbuff = false;
                        Statics.charmaxhealth[Statics.currentthirdchar] -= Statics.charcurrentlvl * guardbonushp;
                    }
                    Statics.thirdchartookdmgformamount = 1;
                }
            }
            else
            {
                if (Overallthirdchar.TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus;
                    atbcontroller.overallstonedmgreduction = defensebonus;
                    atbcontroller.overallstonehealbonus = healbonus;
                }
            }
        }
        if (Overallforthchar != null)
        {
            if (Statics.forthcharstoneclass == 0)
            {
                if (Overallforthchar.TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus * 2;
                    atbcontroller.overallstonedmgreduction = defensebonus;
                    atbcontroller.overallstonehealbonus = healbonus;
                    Statics.forthcharishealer = false;
                    if (atbcontroller.guardhpbuff == true)
                    {
                        atbcontroller.guardhpbuff = false;
                        Statics.charmaxhealth[Statics.currentforthchar] -= Statics.charcurrentlvl * guardbonushp;
                    }
                    Statics.forthchartookdmgformamount = 1;
                }
            }
            else if (Statics.forthcharstoneclass == 1)
            {
                if (Overallforthchar.TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus;
                    atbcontroller.overallstonedmgreduction = defensebonus * 2;
                    atbcontroller.overallstonehealbonus = healbonus;
                    Statics.forthcharishealer = false;
                    if (atbcontroller.guardhpbuff == false)
                    {
                        atbcontroller.guardhpbuff = true;
                        Statics.charmaxhealth[Statics.currentforthchar] += Statics.charcurrentlvl * guardbonushp;
                    }
                    Statics.forthchartookdmgformamount = 2;
                }
            }
            else if (Statics.forthcharstoneclass == 2)
            {
                if (Overallforthchar.TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus;
                    atbcontroller.overallstonedmgreduction = defensebonus;
                    atbcontroller.overallstonehealbonus = healbonus * 2;
                    Statics.forthcharishealer = true;
                    if (atbcontroller.guardhpbuff == true)
                    {
                        atbcontroller.guardhpbuff = false;
                        Statics.charmaxhealth[Statics.currentforthchar] -= Statics.charcurrentlvl * guardbonushp;
                    }
                    Statics.forthchartookdmgformamount = 1;
                }
            }
            else
            {
                if (Overallforthchar.TryGetComponent(out Attributecontroller atbcontroller))
                {
                    atbcontroller.overallstonebonusdmg = dmgbonus;
                    atbcontroller.overallstonedmgreduction = defensebonus;
                    atbcontroller.overallstonehealbonus = healbonus;
                }
            }
        }
    }
}

/*void Update()
{
    if (Statics.infight == false)
    {
        if (gameispaused == false)
        {
            if (Steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame())
            {
                Overallsecondchar.SetActive(true);
                savecamvalueX = Cam1.m_XAxis.Value;
                savemainrota = Overallmainchar.transform.rotation;
                savemainposi = Overallmainchar.transform.position;
                openmenu();
            }
        }
        if (Steuerung.Player.Openelemenu.WasPerformedThisFrame())                                 // Pausemenu aktivieren ist momentan im Loadcharmanager
        {
            if (gameispaused == false)
            {
                gameispaused = true;
                disableattackbuttons = true;
                Elementalmenu.gameObject.SetActive(true);
                Cam1.gameObject.SetActive(false);
                if (Overallthirdchar != null)
                {
                    Overallthirdchar.SetActive(false);
                }
                if (Overallforthchar != null)
                {
                    Overallforthchar.SetActive(false);
                }
                Time.timeScale = 0f;
            }
            else
            {
                gameispaused = false;
                disableattackbuttons = false;
                uiactionscontroller.setimagecolor();
                Elementalmenu.gameObject.SetActive(false);
                classrollupdate();
                foreach (GameObject chars in allcharacters)
                {
                    chars.GetComponent<Attributecontroller>().updateattributes();
                }
                foreach (GameObject mates in teammates)
                {
                    mates.GetComponent<Attributecontroller>().updateattributes();
                }
                GetComponent<HealthUImanager>().secondcharhpupdate();
                Overallmainchar.GetComponent<SpielerHP>().UpdatehealthUI();
                if (Overallthirdchar != null)
                {
                    Overallthirdchar.GetComponent<SpielerHP>().UpdatehealthUI();
                }
                if (Overallforthchar != null)
                {
                    Overallforthchar.GetComponent<SpielerHP>().UpdatehealthUI();
                }
                swordcontrollerupdate?.Invoke();
                fistcontrollerupdate?.Invoke();
                Cam1.gameObject.SetActive(true);
                Time.timeScale = 1f;
            }
        }
        if (Steuerung.Player.Openequipentmenu.WasPerformedThisFrame())
        {
            if (disableattackbuttons == false)
            {
                Statics.currentequiptmentchar = 0;
                disableattackbuttons = true;
                Equipmentmenu.gameObject.SetActive(true);
                Cam1.gameObject.SetActive(false);
                if (Overallthirdchar != null)
                {
                    Overallthirdchar.SetActive(false);
                }
                if (Overallforthchar != null)
                {
                    Overallforthchar.SetActive(false);
                }
                Time.timeScale = 0f;
            }
            else
            {
                foreach (GameObject chars in allcharacters)
                {
                    chars.GetComponent<Attributecontroller>().updateattributes();
                }
                foreach (GameObject mates in teammates)
                {
                    mates.GetComponent<Attributecontroller>().updateattributes();
                }
                GetComponent<HealthUImanager>().secondcharhpupdate();
                Overallmainchar.GetComponent<SpielerHP>().UpdatehealthUI();
                if (Overallthirdchar != null)
                {
                    Overallthirdchar.GetComponent<SpielerHP>().UpdatehealthUI();
                }
                if (Overallforthchar != null)
                {
                    Overallforthchar.GetComponent<SpielerHP>().UpdatehealthUI();
                }
                swordcontrollerupdate?.Invoke();
                fistcontrollerupdate?.Invoke();
                disableattackbuttons = false;
                Equipmentmenu.gameObject.SetActive(false);
                Cam1.gameObject.SetActive(true);
                Time.timeScale = 1f;
            }
        }
    }
}
private void openmenu()
{
    Overallmainchar.SetActive(false);
    Overallsecondchar.SetActive(false);
    Movescript.availabletargets.Clear();
    SceneManager.LoadScene(1);
}*/
