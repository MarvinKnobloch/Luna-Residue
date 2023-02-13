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
    public static Vector3 savemainposi = new Vector3(0,5,0);
    public static Quaternion savemainrota;                                      //memorypuzzle(-40,25,685) //boxpuzzle(305,25,565) switchpuzzle(280,2,180)
    public static float savecamvalueX;                                          //statuepuzzle(-200,2,340) //woods(255,20,435)  //watercave(-330,12,-40)  //lantern(-210,32,500)

    public static bool disableattackbuttons;
    public static bool gameispaused;
    public static bool interaction;

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
        Physics.IgnoreLayerCollision(0, 6, false);               //collision wird deaktiviert und nach einem frame wird das menü aufgerufen damit die area collider getriggert werden
        Overallmainchar = allcharacters[Statics.currentfirstchar];
        Overallsecondchar = allcharacters[Statics.currentsecondchar];
        Overallmainchar.transform.position = savemainposi;
        Overallmainchar.transform.rotation = savemainrota;
        Statics.currentactiveplayer = 0;
        Overallmainchar.SetActive(true);
        Overallsecondchar.SetActive(true);
        Overallmainchar.GetComponent<Playerhp>().playerhpuislot = 0;
        Overallsecondchar.GetComponent<Playerhp>().playerhpuislot = 1;
        Statics.currentactiveplayer = Statics.currentfirstchar;                            //PlayerPrefs.GetInt("Maincharindex");
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


        classandstatsupdate(Statics.currentfirstchar);                   // bonus hp von guard wird im elemenu beim auswählen des stones gesetzt
        classandstatsupdate(Statics.currentsecondchar);
        classandstatsupdate(Statics.currentthirdchar);
        classandstatsupdate(Statics.currentforthchar);
        GetComponent<HealthUImanager>().sethealthbars();

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
        gameispaused = false;
        disableattackbuttons = false;
        menu.SetActive(false);
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        Cam1.gameObject.SetActive(true);

 #if !UNITY_EDITOR
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
#endif
    }
    private void classandstatsupdate(int charnumber)
    {
        if (Statics.characterclassroll[charnumber] == 0)
        {
            if (allcharacters[charnumber].TryGetComponent(out Attributecontroller atbcontroller))
            {
                atbcontroller.isdmgclassroll = true;
                atbcontroller.isguardclassroll = false;
                atbcontroller.ishealerclassroll = false;
                atbcontroller.classrollupdate();
            }
        }
        else if (Statics.characterclassroll[charnumber] == 1)
        {
            if (allcharacters[charnumber].TryGetComponent(out Attributecontroller atbcontroller))
            {
                atbcontroller.isdmgclassroll = false;
                atbcontroller.isguardclassroll = true;
                atbcontroller.ishealerclassroll = false;
                atbcontroller.classrollupdate();
            }
        }
        else if (Statics.characterclassroll[charnumber] == 2)
        {
            if (allcharacters[charnumber].TryGetComponent(out Attributecontroller atbcontroller))
            {
                atbcontroller.isdmgclassroll = false;
                atbcontroller.isguardclassroll = false;
                atbcontroller.ishealerclassroll = true;
                atbcontroller.classrollupdate();
            }
        }
        else
        {
            if (allcharacters[charnumber].TryGetComponent(out Attributecontroller atbcontroller))
            {
                atbcontroller.isdmgclassroll = false;
                atbcontroller.isguardclassroll = false;
                atbcontroller.ishealerclassroll = false;
                atbcontroller.classrollupdate();
            }
        }
    }
}
