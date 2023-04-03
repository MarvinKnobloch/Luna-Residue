using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menucharcontroller : MonoBehaviour
{

    [SerializeField] private int charnumber;

    [SerializeField] private GameObject weaponslot1;
    [SerializeField] private GameObject weaponslot2;
    [SerializeField] private TextMeshProUGUI charstats;
    [SerializeField] private Sprite[] images;

    private int firstweapon;
    private int secondweapon;

    [SerializeField] private Menucontroller menucontroller;

    private void OnEnable()
    {
        firstweapon = Statics.firstweapon[charnumber];
        secondweapon = Statics.secondweapon[charnumber];
        weaponslot1.gameObject.GetComponent<Image>().sprite = images[firstweapon];
        weaponslot2.gameObject.GetComponent<Image>().sprite = images[secondweapon];
        charstats.text = string.Empty;
        charstats.text = "Class: " + Statics.characterclassrolltext[charnumber] + "\n" +
                         "HP " + Statics.charcurrenthealth[charnumber] + "/" + Statics.charmaxhealth[charnumber] + "\n" +
                         "Level " + Statics.charcurrentlvl + "\n" +
                         "EXP " + Statics.charcurrentexp + "/" + Statics.charrequiredexp;
    }
    public void openweapongrid(GameObject grid)
    {
        if(grid.activeSelf == true)
        {
            menucontroller.somethinginmenuisopen = false;
            grid.SetActive(false);
        }
        else
        {
            menucontroller.closeallselections();
            menucontroller.somethinginmenuisopen = true;
            grid.SetActive(true);
        }
        menucontroller.menusoundcontroller.playmenubuttonsound();
    }
    public void setmainweapon(int newweaponnumber)
    {
        if (secondweapon == newweaponnumber)
        {
            secondweapon = firstweapon;
            Statics.secondweapon[charnumber] = secondweapon;
            weaponslot2.gameObject.GetComponent<Image>().sprite = images[secondweapon];
        }
        firstweapon = newweaponnumber;
        Statics.firstweapon[charnumber] = firstweapon;
        weaponslot1.gameObject.GetComponent<Image>().sprite = images[firstweapon];
        menucontroller.closeallselections();
        menucontroller.somethinginmenuisopen = false;
        menucontroller.menusoundcontroller.playmenubuttonsound();
    }

    public void setsecondweapon(int newweaponnumber)
    {
        if (firstweapon == newweaponnumber)
        {
            firstweapon = secondweapon;
            Statics.firstweapon[charnumber] = firstweapon;
            weaponslot1.gameObject.GetComponent<Image>().sprite = images[firstweapon];
        }
        secondweapon = newweaponnumber;
        Statics.secondweapon[charnumber] = secondweapon;
        weaponslot2.gameObject.GetComponent<Image>().sprite = images[secondweapon];
        menucontroller.closeallselections();
        menucontroller.somethinginmenuisopen = false;
        menucontroller.menusoundcontroller.playmenubuttonsound();
    }
}
