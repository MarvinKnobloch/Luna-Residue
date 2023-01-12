using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class Equipcharselection : MonoBehaviour
{
    public Text nametext;

    private SpielerSteu Steuerung;
    public static GameObject currentbuttonslot;

    public Text statshealth;
    public Text statsdefense;
    public Text statsattack;
    public Text statscritchance;
    public Text statscritdmg;
    public Text statsweaponbuff;
    public Text statsweaponbuffduration;
    public Text statscharbuff;
    public Text statscharbuffduration;
    public Text statsbasiccrit;
    public Text statsbasicbuffdmg;

    public Text statsswordattack;
    public Text statsbowattack;
    public Text statsfistattack;

    public TextMeshProUGUI swordbutton;
    public TextMeshProUGUI bowbutton;
    public TextMeshProUGUI fistbutton;
    public TextMeshProUGUI headbutton;
    public TextMeshProUGUI chestbutton;
    public TextMeshProUGUI glovesbutton;
    public TextMeshProUGUI beltbutton;
    public TextMeshProUGUI shoesbutton;
    public TextMeshProUGUI necklessbutton;
    public TextMeshProUGUI ringbutton;

    public GameObject[] charbuttons;
    [SerializeField] private GameObject resetonpointerenterlayer;

    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        Statics.currentequiptmentchar = PlayerPrefs.GetInt("Maincharindex");
        nametext.text = Statics.characternames[Statics.currentequiptmentchar] + " LvL" + Statics.charcurrentlvl;
        settextandimage(Statics.currentequiptmentchar);
    }
    private void Update()
    {
        if (Steuerung.Menusteuerung.Menucharselectionleft.WasPerformedThisFrame())
        {
            resetonpointerenterlayer.SetActive(true);
            selectionbackward();
        }
        if (Steuerung.Menusteuerung.Menucharselectionright.WasPerformedThisFrame())
        {
            resetonpointerenterlayer.SetActive(true);
            selectionforward();
        }
    }
    private void selectionforward()
    {
        if (Statics.currentequiptmentchar >= Statics.playablechars - 1)
        {
            Statics.currentequiptmentchar = 0;
            choosechar(Statics.currentequiptmentchar);
        }
        else
        {
            Statics.currentequiptmentchar++;
            choosechar(Statics.currentequiptmentchar);
        }
    }
    private void selectionbackward()
    {
        if (Statics.currentequiptmentchar == 0)
        {
            Statics.currentequiptmentchar = Statics.playablechars - 1;
            choosechar(Statics.currentequiptmentchar);
        }
        else
        {
            Statics.currentequiptmentchar--;
            choosechar(Statics.currentequiptmentchar);
        }
    }
    public void choosechar(int currentchar)
    {
        Statics.currentequiptmentchar = currentchar;
        nametext.text = Statics.characternames[Statics.currentequiptmentchar] + " LvL" + Statics.charcurrentlvl;
        settextandimage(Statics.currentequiptmentchar);
    }
    private void settextandimage(int currentint)
    {
        statshealth.text = Statics.charmaxhealth[currentint] + "";
        statsdefense.text = Statics.chardefense[currentint] + "";
        statsattack.text = Statics.charattack[currentint] + "";
        statscritchance.text = Statics.charcritchance[currentint] + "%";
        statscritdmg.text = Statics.charcritdmg[currentint] + "%";
        statsweaponbuff.text = Statics.charweaponbuff[currentint] - 100 + "%";
        statsweaponbuffduration.text = Statics.charweaponbuffduration[currentint] + "sec";
        statscharbuff.text = Statics.charswitchbuff[currentint] - 100 + "%";
        statscharbuffduration.text = Statics.charswitchbuffduration[currentint] + "sec";
        statsbasiccrit.text = Statics.charbasiccritbuff[currentint] + "%";
        statsbasicbuffdmg.text = Statics.charbasicdmgbuff[currentint] + "%";

        swordbutton.text = Statics.charswordattack[currentint].ToString();

        statsswordattack.text = Statics.charswordattack[currentint] + "";
        statsbowattack.text = Statics.charbowattack[currentint] + "";
        statsfistattack.text = Statics.charfistattack[currentint] + "";

        swordbutton.text = Statics.charcurrentswordname[currentint].ToString();
        bowbutton.text = Statics.charcurrentbowname[currentint].ToString();
        fistbutton.text = Statics.charcurrentfistname[currentint].ToString();
        headbutton.text = Statics.charcurrentheadname[currentint].ToString();
        chestbutton.text = Statics.charcurrentchestname[currentint].ToString();
        glovesbutton.text = Statics.charcurrentglovesname[currentint].ToString();
        beltbutton.text = Statics.charcurrentlegname[currentint].ToString();
        shoesbutton.text = Statics.charcurrentshoesname[currentint].ToString();
        necklessbutton.text = Statics.charcurrentnecklessname[currentint].ToString();
        ringbutton.text = Statics.charcurrentringname[currentint].ToString();

        if (Statics.activeswordslot != null)
        {
            Statics.activeswordslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentswordimage[currentint] != null)
        {
            Statics.currentswordimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activeswordslot = Statics.currentswordimage[currentint];
        }

        if (Statics.activebowslot != null)
        {
            Statics.activebowslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentbowimage[currentint] != null)
        {
            Statics.currentbowimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activebowslot = Statics.currentbowimage[currentint];
        }

        if (Statics.activefistslot != null)
        {
            Statics.activefistslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentfistimage[currentint] != null)
        {
            Statics.currentfistimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activefistslot = Statics.currentfistimage[currentint];
        }

        if (Statics.activeheadslot != null)
        {
            Statics.activeheadslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentheadimage[currentint] != null)
        {
            Statics.currentheadimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activeheadslot = Statics.currentheadimage[currentint];
        }

        if (Statics.activechestslot != null)
        {
            Statics.activechestslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentchestimage[currentint] != null)
        {
            Statics.currentchestimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activechestslot = Statics.currentchestimage[currentint];
        }

        if (Statics.activeglovesslot != null)
        {
            Statics.activeglovesslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentglovesimage[currentint] != null)
        {
            Statics.currentglovesimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activeglovesslot = Statics.currentglovesimage[currentint];
        }

        if (Statics.activebeltslot != null)
        {
            Statics.activebeltslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentlegimage[currentint] != null)
        {
            Statics.currentlegimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activebeltslot = Statics.currentlegimage[currentint];
        }

        if (Statics.activeshoesslot != null)
        {
            Statics.activeshoesslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentshoesimage[currentint] != null)
        {
            Statics.currentshoesimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activeshoesslot = Statics.currentshoesimage[currentint];
        }

        if (Statics.activenecklessslot != null)
        {
            Statics.activenecklessslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentnecklessimage[currentint] != null)
        {
            Statics.currentnecklessimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activenecklessslot = Statics.currentnecklessimage[currentint];
        }

        if (Statics.activeringslot != null)
        {
            Statics.activeringslot.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (Statics.currentringimage[currentint] != null)
        {
            Statics.currentringimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.activeringslot = Statics.currentringimage[currentint];
        }
        foreach (GameObject obj in charbuttons)
        {
            obj.GetComponent<Image>().color = Color.white;
        }
        charbuttons[Statics.currentequiptmentchar].GetComponent<Image>().color = Color.green;
        if(currentbuttonslot != null)
        {
            EventSystem.current.SetSelectedGameObject(currentbuttonslot);
        }
    }
}
