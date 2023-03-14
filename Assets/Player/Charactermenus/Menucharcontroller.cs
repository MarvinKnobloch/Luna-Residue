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
    [SerializeField] private GameObject weapongrid1;
    [SerializeField] private GameObject weapongrid2;
    [SerializeField] private TextMeshProUGUI charstats;
    [SerializeField] private Sprite[] images;

    private int firstweapon;
    private int secondweapon;

    private void OnEnable()
    {
        weapongrid1.SetActive(false);
        weapongrid2.SetActive(false);
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
            grid.SetActive(false);
        }
        else
        {
            foreach (GameObject obj in gameObject.GetComponentInParent<Closeweapongrids>().weaponselectiongrids)
            {
                obj.SetActive(false);
            }
            grid.SetActive(true);
        }
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
        weapongrid1.SetActive(false);
        weapongrid2.SetActive(false);
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
        weapongrid1.SetActive(false);
        weapongrid2.SetActive(false);
    }
}
