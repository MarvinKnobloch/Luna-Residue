using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mariaweapons : MonoBehaviour
{
    public GameObject[] allweapons;
    public List<MonoBehaviour> scripts;
    public RuntimeAnimatorController[] weaponanimation;
    private SpielerSteu Steuerung;

    private int charnumber = 0;

    public int mainweaponindex;
    public int secondweaponindex;
    private GameObject weapon1;
    private GameObject weapon2;
    private Animator animator;

    private bool switchweaponbool;
    void Awake()
    {
        animator = GetComponent<Animator>();
        Steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        Steuerung.Enable();
        LoadCharmanager.setweapons += weaponupdate;
    }
    private void OnDisable()
    {
        LoadCharmanager.setweapons -= weaponupdate;
    }
    void Update()
    {
        if (LoadCharmanager.disableattackbuttons == false)
        {
            if (Steuerung.Player.Weaponchange.WasPerformedThisFrame() && Statics.otheraction == false && Statics.weapsonswitchbool == false)
            {
                Statics.otheraction = true;
                if (switchweaponbool == true)
                {
                    spawnmainweapon();
                }
                else
                {
                    spawnsecondweapon();
                }
            }
        }
    }
    private void weaponupdate()
    {
        mainweaponindex = PlayerPrefs.GetInt("Mariamainweaponindex");
        secondweaponindex = PlayerPrefs.GetInt("Mariasecondweaponindex");
        foreach (MonoBehaviour attackscripts in scripts)
        {
            attackscripts.enabled = false;
        }
        foreach (GameObject weapon in allweapons)
        {
            weapon.SetActive(false);
        }
        weapon1 = allweapons[mainweaponindex];
        weapon2 = allweapons[secondweaponindex];
        GlobalCD.startweaponswitchcd();
        if (switchweaponbool == false)
        {
            weapon1.SetActive(true);
            animator.runtimeAnimatorController = weaponanimation[mainweaponindex];
            scripts[mainweaponindex].enabled = true;
        }
        else
        {
            weapon2.SetActive(true);
            animator.runtimeAnimatorController = weaponanimation[secondweaponindex];
            scripts[secondweaponindex].enabled = true;
        }
    }

    private void spawnmainweapon()
    {
        if (Statics.healmissingtime > 9f)
        {
            Statics.healmissingtime = 9f;
            GlobalCD.onswitchhealingcd();
        }
        GlobalCD.currentweaponswitchchar = charnumber;
        GlobalCD.startweaponswitchcd();
        GlobalCD.startweaponswitchbuff();
        weaponswitchbuffapply();
        GetComponent<Movescript>().gravitation = Statics.playergravity;
        weapon2.SetActive(false);
        weapon1.SetActive(true);
        animator.runtimeAnimatorController = weaponanimation[mainweaponindex];
        scripts[secondweaponindex].enabled = false;
        scripts[mainweaponindex].enabled = true;
        switchweaponbool = false;
        Statics.otheraction = false;
    }
    private void spawnsecondweapon()
    {
        if (Statics.healmissingtime > 9f)
        {
            Statics.healmissingtime = 9f;
            GlobalCD.onswitchhealingcd();
        }
        GlobalCD.currentweaponswitchchar = charnumber;
        GlobalCD.startweaponswitchcd();
        GlobalCD.startweaponswitchbuff();
        weaponswitchbuffapply();
        GetComponent<Movescript>().gravitation = Statics.playergravity;
        weapon1.SetActive(false);
        weapon2.SetActive(true);
        animator.runtimeAnimatorController = weaponanimation[secondweaponindex];
        scripts[mainweaponindex].enabled = false;
        scripts[secondweaponindex].enabled = true;
        switchweaponbool = true;
        Statics.otheraction = false;
    }
    public void weaponswitchbuffapply()
    {
        Statics.weaponswitchbuff = Statics.charweaponbuff[charnumber];                      // weaponswitchbuff, muss nicht erst in den attributecontroller, weil jeder sein eigenes weaponscript hat
    }
}
