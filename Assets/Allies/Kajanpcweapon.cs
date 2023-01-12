using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kajanpcweapon : MonoBehaviour
{
    public GameObject[] allweapons;
    public MonoBehaviour[] weaponscripts;
    public RuntimeAnimatorController[] weaponanimation;
    private int mainweaponindex;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        mainweaponindex = PlayerPrefs.GetInt("Kajamainweaponindex");

        if (PlayerPrefs.GetInt("Kajamainweaponindex") == 1)                      // bis jetzt nur 1 weil noch keine andere rangewaffe vorhanden ist
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

        allweapons[mainweaponindex].SetActive(true);
        weaponscripts[mainweaponindex].enabled = true;
        animator.runtimeAnimatorController = weaponanimation[mainweaponindex];
    }
}
