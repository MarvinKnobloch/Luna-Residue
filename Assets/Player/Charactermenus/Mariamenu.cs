using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mariamenu : MonoBehaviour
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
        mainindex = PlayerPrefs.GetInt("Mariamainweaponindex");
        secondindex = PlayerPrefs.GetInt("Mariasecondweaponindex");
        Mainslot.gameObject.GetComponent<Image>().sprite = images[mainindex];
        Seclondslot.gameObject.GetComponent<Image>().sprite = images[secondindex];
        oldmainindex = mainindex;
        oldsecondindex = secondindex;
        lvltext.text = "Stufe " + Statics.charcurrentlvl;
        exp.text = "EXP " + Statics.charcurrentexp + "/" + Statics.charrequiredexp;
        hpanzeige.text = "HP " + Statics.charcurrenthealth[0] + "/" + Statics.charmaxhealth[0];
    }
    public void menuslot()
    {
        if(waffenchangeopen == false) 
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
            PlayerPrefs.SetInt("Mariamainweaponindex", mainindex);
            Mainslot.gameObject.GetComponent<Image>().sprite = images[mainindex];
            oldmainindex = mainindex;
            waffenmenu.SetActive(false);
            waffenmenu2.SetActive(false);
            waffenchangeopen = false;
        }
    }
    public void choosesecondweapon(int mariasecondweapon)
    {
        secondindex = mariasecondweapon;
        if (mainindex == secondindex)
        {
            changemainindex();
        }
        else
        {
            PlayerPrefs.SetInt("Mariasecondweaponindex", secondindex);
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
        PlayerPrefs.SetInt("Mariamainweaponindex", mainindex);
        Mainslot.gameObject.GetComponent<Image>().sprite = images[mainindex];

        PlayerPrefs.SetInt("Mariasecondweaponindex", secondindex);
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
        PlayerPrefs.SetInt("Mariasecondweaponindex", secondindex);
        Seclondslot.gameObject.GetComponent<Image>().sprite = images[secondindex];

        PlayerPrefs.SetInt("Mariamainweaponindex", mainindex);
        oldmainindex = mainindex;
        Mainslot.gameObject.GetComponent<Image>().sprite = images[mainindex];
        waffenmenu.SetActive(false);
        waffenmenu2.SetActive(false);
        waffenchangeopen = false;
    }
}
