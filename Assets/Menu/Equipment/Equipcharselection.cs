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

    [SerializeField] private TextMeshProUGUI statsnumbers;

    public Text statsswordattack;
    public Text statsbowattack;
    public Text statsfistattack;

    [SerializeField] private TextMeshProUGUI[] slotbuttontext;

    /*public TextMeshProUGUI swordbutton;
    public TextMeshProUGUI bowbutton;
    public TextMeshProUGUI fistbutton;
    public TextMeshProUGUI headbutton;
    public TextMeshProUGUI chestbutton;
    public TextMeshProUGUI glovesbutton;
    public TextMeshProUGUI beltbutton;
    public TextMeshProUGUI shoesbutton;
    public TextMeshProUGUI necklessbutton;
    public TextMeshProUGUI ringbutton;*/

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
        setselectedbutton();
    }
    private void settextandimage(int currentchar)
    {
        statsnumbers.text = string.Empty;
        statsnumbers.color = Color.white;
        statsnumbers.text = Statics.charmaxhealth[currentchar] + "\n" +
                            Statics.chardefense[currentchar] + "\n" +
                            Statics.charattack[currentchar] + "\n" +
                            Statics.charcritchance[currentchar] + "%" + "\n" +
                            Statics.charcritdmg[currentchar] + "%" + "\n" +
                            (Statics.charweaponbuff[currentchar] - 100) + "%" + "\n" +
                            Statics.charweaponbuffduration[currentchar] + "sec" + "\n" +
                            (Statics.charswitchbuff[currentchar] - 100) + "%" + "\n" +
                            Statics.charswitchbuffduration[currentchar] + "sec" + "\n" +
                            Statics.charbasiccritbuff[currentchar] + "%" + "\n" +
                            Statics.charbasicdmgbuff[currentchar] + "%";


        //swordbutton.text = Statics.charswordattack[currentint].ToString();

        statsswordattack.text = Statics.charswordattack[currentchar] + "";
        statsbowattack.text = Statics.charbowattack[currentchar] + "";
        statsfistattack.text = Statics.charfistattack[currentchar] + "";

        slotbuttontext[0].text = Statics.charcurrentswordname[currentchar].ToString();
        slotbuttontext[1].text = Statics.charcurrentbowname[currentchar].ToString();
        slotbuttontext[2].text = Statics.charcurrentfistname[currentchar].ToString();
        slotbuttontext[3].text = Statics.charcurrentheadname[currentchar].ToString();
        slotbuttontext[4].text = Statics.charcurrentchestname[currentchar].ToString();
        slotbuttontext[5].text = Statics.charcurrentglovesname[currentchar].ToString();
        slotbuttontext[6].text = Statics.charcurrentlegname[currentchar].ToString();
        slotbuttontext[7].text = Statics.charcurrentshoesname[currentchar].ToString();
        slotbuttontext[8].text = Statics.charcurrentnecklessname[currentchar].ToString();
        slotbuttontext[9].text = Statics.charcurrentringname[currentchar].ToString();

        if (Statics.activeswordslot != null)
        {
            Statics.activeswordslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentswordimage[currentchar] != null)
        {
            Statics.currentswordimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activeswordslot = Statics.currentswordimage[currentchar];
        }

        if (Statics.activebowslot != null)
        {
            Statics.activebowslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentbowimage[currentchar] != null)
        {
            Statics.currentbowimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activebowslot = Statics.currentbowimage[currentchar];
        }

        if (Statics.activefistslot != null)
        {
            Statics.activefistslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentfistimage[currentchar] != null)
        {
            Statics.currentfistimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activefistslot = Statics.currentfistimage[currentchar];
        }

        if (Statics.activeheadslot != null)
        {
            Statics.activeheadslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentheadimage[currentchar] != null)
        {
            Statics.currentheadimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activeheadslot = Statics.currentheadimage[currentchar];
        }

        if (Statics.activechestslot != null)
        {
            Statics.activechestslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentchestimage[currentchar] != null)
        {
            Statics.currentchestimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activechestslot = Statics.currentchestimage[currentchar];
        }

        if (Statics.activeglovesslot != null)
        {
            Statics.activeglovesslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentglovesimage[currentchar] != null)
        {
            Statics.currentglovesimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activeglovesslot = Statics.currentglovesimage[currentchar];
        }

        if (Statics.activebeltslot != null)
        {
            Statics.activebeltslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentlegimage[currentchar] != null)
        {
            Statics.currentlegimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activebeltslot = Statics.currentlegimage[currentchar];
        }

        if (Statics.activeshoesslot != null)
        {
            Statics.activeshoesslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentshoesimage[currentchar] != null)
        {
            Statics.currentshoesimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activeshoesslot = Statics.currentshoesimage[currentchar];
        }

        if (Statics.activenecklessslot != null)
        {
            Statics.activenecklessslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentnecklessimage[currentchar] != null)
        {
            Statics.currentnecklessimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activenecklessslot = Statics.currentnecklessimage[currentchar];
        }

        if (Statics.activeringslot != null)
        {
            Statics.activeringslot.transform.GetComponent<Image>().color = Color.white;
        }
        if (Statics.currentringimage[currentchar] != null)
        {
            Statics.currentringimage[currentchar].transform.GetComponent<Image>().color = Color.green;
            Statics.activeringslot = Statics.currentringimage[currentchar];
        }
        foreach (GameObject obj in charbuttons)
        {
            obj.GetComponent<Image>().color = Color.white;
        }
        charbuttons[Statics.currentequiptmentchar].GetComponent<Image>().color = Color.green;
    }
    
    private void setselectedbutton()
    {
        if(currentbuttonslot != null)
        {
            EventSystem.current.SetSelectedGameObject(currentbuttonslot);
        }
    }
}
