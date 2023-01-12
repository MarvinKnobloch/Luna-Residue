using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yakumenu : MonoBehaviour
{
    public GameObject Mainslot;
    public GameObject Seclondslot;
    public GameObject waffenmenu;
    public GameObject waffenmenu2;
    public Text lvltext;
    public Text exp;
    public Text hpanzeige;

    private bool waffenchangeopen;
    public Sprite[] images;
    private int mainindex;
    private int secondindex;
    private int oldmainindex;
    private int oldsecondindex;

    private void OnEnable()
    {
        waffenchangeopen = false;
        mainindex = PlayerPrefs.GetInt("Yakumainweaponindex");
        secondindex = PlayerPrefs.GetInt("Yakusecondweaponindex");
        Mainslot.gameObject.GetComponent<Image>().sprite = images[mainindex];
        Seclondslot.gameObject.GetComponent<Image>().sprite = images[secondindex];
        oldmainindex = mainindex;
        oldsecondindex = secondindex;
        lvltext.text = "Stufe " + Statics.charcurrentlvl;
        exp.text = "EXP " + Statics.charcurrentexp + "/" + Statics.charrequiredexp;
        hpanzeige.text = "HP " + Statics.charcurrenthealth[3] + "/" + Statics.charmaxhealth[3];
    }
    public void menuslot()
    {
        if (waffenchangeopen == false)
        {
            waffenmenu.SetActive(true);
            waffenchangeopen = true;
        }
        else
        {
            waffenmenu.SetActive(false);
            waffenchangeopen = false;
        }
    }
    public void menuslot2()
    {
        if (waffenchangeopen == false)
        {
            waffenmenu2.SetActive(true);
            waffenchangeopen = true;
        }
        else
        {
            waffenmenu2.SetActive(false);
            waffenchangeopen = false;
        }
    }
    public void choosemainweapon(int mariaweapon)
    {
        mainindex = mariaweapon;
        if (secondindex == mainindex)
        {
            changesecondindex();
        }
        else
        {
            PlayerPrefs.SetInt("Yakumainweaponindex", mainindex);
            Mainslot.gameObject.GetComponent<Image>().sprite = images[mainindex];
            oldmainindex = mainindex;
            waffenmenu.SetActive(false);
            waffenmenu2.SetActive(false);
            waffenchangeopen = false;
        }
    }
    public void choosesecondweapon(int yakusecondweapon)
    {
        secondindex = yakusecondweapon;
        if (mainindex == secondindex)
        {
            changemainindex();
        }
        else
        {
            PlayerPrefs.SetInt("Yakusecondweaponindex", secondindex);
            Seclondslot.gameObject.GetComponent<Image>().sprite = images[secondindex];
            oldsecondindex = secondindex;
            waffenmenu.SetActive(false);
            waffenmenu2.SetActive(false);
            waffenchangeopen = false;
        }
    }
    public void changemainindex()
    {
        mainindex = oldsecondindex;
        oldmainindex = mainindex;
        PlayerPrefs.SetInt("Yakumainweaponindex", mainindex);
        Mainslot.gameObject.GetComponent<Image>().sprite = images[mainindex];

        PlayerPrefs.SetInt("Yakusecondweaponindex", secondindex);
        oldsecondindex = secondindex;
        Seclondslot.gameObject.GetComponent<Image>().sprite = images[secondindex];
        waffenmenu.SetActive(false);
        waffenmenu2.SetActive(false);
        waffenchangeopen = false;
    }
    public void changesecondindex()
    {
        secondindex = oldmainindex;
        oldsecondindex = secondindex;
        PlayerPrefs.SetInt("Yakusecondweaponindex", secondindex);
        Seclondslot.gameObject.GetComponent<Image>().sprite = images[secondindex];

        PlayerPrefs.SetInt("Yakumainweaponindex", mainindex);
        oldmainindex = mainindex;
        Mainslot.gameObject.GetComponent<Image>().sprite = images[mainindex];
        waffenmenu.SetActive(false);
        waffenmenu2.SetActive(false);
        waffenchangeopen = false;
    }
}
