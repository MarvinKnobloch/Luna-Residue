using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weaponswitch : MonoBehaviour
{
    private SpielerSteu controlls;
    private Animator animator;

    [SerializeField] private int charnumber;

    [SerializeField] private GameObject[] allweapons;
    [SerializeField] private MonoBehaviour[] weaponscripts;
    [SerializeField] private RuntimeAnimatorController[] weaponanimation;
    [SerializeField] private Sprite[] weaponimages;
    [SerializeField] private Image weaponimage;

    private Movescript movescript;

    private int firstweapon;
    private int secondweapon;
    private bool mainweaponactiv;
    void Awake()
    {
        animator = GetComponent<Animator>();
        movescript = GetComponent<Movescript>();
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        controlls.Enable();
    }
    void Update()
    {
        if (LoadCharmanager.disableattackbuttons == false)
        {
            if (controlls.Player.Weaponchange.WasPerformedThisFrame() && Statics.otheraction == false && Statics.weaponswitchbool == false)
            {
                movescript.checkforcamstate();
                Statics.otheraction = true;
                if (mainweaponactiv == true)
                {
                    spawnsecondweapon();                 
                }
                else
                {
                    spawnmainweapon();
                }
            }
        }
    }

    private void spawnmainweapon()
    {
        mainweaponactiv = true;
        allweapons[secondweapon].SetActive(false);
        allweapons[firstweapon].SetActive(true);
        animator.runtimeAnimatorController = weaponanimation[firstweapon];
        weaponscripts[secondweapon].enabled = false;
        weaponscripts[firstweapon].enabled = true;

        Statics.weaponswitchbuff = Statics.charweaponbuff[charnumber];
        GlobalCD.startweaponswitchcd();
        GlobalCD.startweaponswitchbuff(charnumber);
        weaponimageupdate();
    }
    private void spawnsecondweapon()
    {
        mainweaponactiv = false;
        allweapons[firstweapon].SetActive(false);
        allweapons[secondweapon].SetActive(true);
        animator.runtimeAnimatorController = weaponanimation[secondweapon];
        weaponscripts[firstweapon].enabled = false;
        weaponscripts[secondweapon].enabled = true;

        Statics.weaponswitchbuff = Statics.charweaponbuff[charnumber];
        GlobalCD.startweaponswitchcd();
        GlobalCD.startweaponswitchbuff(charnumber);
        weaponimageupdate();
    }

    public void setweapons()
    {
        firstweapon = Statics.firstweapon[charnumber];
        secondweapon = Statics.secondweapon[charnumber];
        foreach (MonoBehaviour attackscripts in weaponscripts)
        {
            attackscripts.enabled = false;
        }
        foreach (GameObject weapon in allweapons)
        {
            weapon.SetActive(false);
        }
        mainweaponactiv = true;
        allweapons[firstweapon].SetActive(true);
        animator.runtimeAnimatorController = weaponanimation[firstweapon];
        weaponscripts[firstweapon].enabled = true;
    }

    public void weaponimageupdate()
    {
        if (Statics.currentactiveplayer == 0)
        {
            if (mainweaponactiv == true) weaponimage.sprite = weaponimages[Statics.secondweapon[Statics.currentfirstchar]];
            else weaponimage.sprite = weaponimages[Statics.firstweapon[Statics.currentfirstchar]];
        }
        else
        {
            if (mainweaponactiv == true) weaponimage.sprite = weaponimages[Statics.secondweapon[Statics.currentsecondchar]];
            else weaponimage.sprite = weaponimages[Statics.firstweapon[Statics.currentsecondchar]];
        }
    }
}

/*public void resetmainweaponactiv()
{
    mainweaponactiv = true;
}*/