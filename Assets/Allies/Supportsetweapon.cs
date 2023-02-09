using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportsetweapon : MonoBehaviour
{
    [SerializeField] private int charnumber;

    [SerializeField] private GameObject[] allweapons;
    [SerializeField] private MonoBehaviour[] weaponscripts;
    [SerializeField] private RuntimeAnimatorController[] weaponanimation;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        LoadCharmanager.setweapons += setweapon;
    }
    private void OnDisable()
    {
        LoadCharmanager.setweapons -= setweapon;
    }
    private void setweapon()
    {
        if (Statics.firstweapon[charnumber] == 1)                      // bis jetzt nur 1 weil noch keine andere rangewaffe vorhanden ist
        {
            GetComponent<Supportmovement>().rangeweaponequiped = true;
        }
        else
        {
            GetComponent<Supportmovement>().rangeweaponequiped = false;
        }

        foreach (MonoBehaviour setweapon in weaponscripts)
        {
            setweapon.enabled = false;
        }
        foreach (GameObject weapons in allweapons)
        {
            weapons.SetActive(false);
        }

        allweapons[Statics.firstweapon[charnumber]].SetActive(true);
        weaponscripts[Statics.firstweapon[charnumber]].enabled = true;
        animator.runtimeAnimatorController = weaponanimation[Statics.firstweapon[charnumber]];
    }
}
