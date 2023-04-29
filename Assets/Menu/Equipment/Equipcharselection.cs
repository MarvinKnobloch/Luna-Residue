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

    public TextMeshProUGUI sworddmg;
    public TextMeshProUGUI bowdmg;
    public TextMeshProUGUI fistdmg;

    [SerializeField] private TextMeshProUGUI[] slotbuttontext;
    [SerializeField] private GameObject[] charbuttons;

    [SerializeField] private Menusoundcontroller menusoundcontroller;
    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        Statics.currentequipmentchar = 0;           //PlayerPrefs.GetInt("Maincharindex");
        nametext.text = Statics.characternames[Statics.currentequipmentchar] + " LvL" + Statics.charcurrentlvl;
        settextandimage(Statics.currentequipmentchar);
    }
    private void Update()
    {
        if (Steuerung.Menusteuerung.Menucharselectionleft.WasPerformedThisFrame())
        {
            selectionbackward();
        }
        if (Steuerung.Menusteuerung.Menucharselectionright.WasPerformedThisFrame())
        {
            selectionforward();
        }
    }
    private void selectionforward()
    {
        if (Statics.currentequipmentchar >= Statics.playablechars - 1)
        {
            Statics.currentequipmentchar = 0;
            choosechar(Statics.currentequipmentchar);
        }
        else
        {
            Statics.currentequipmentchar++;
            choosechar(Statics.currentequipmentchar);
        }
    }
    private void selectionbackward()
    {
        if (Statics.currentequipmentchar == 0)
        {
            Statics.currentequipmentchar = Statics.playablechars - 1;
            choosechar(Statics.currentequipmentchar);
        }
        else
        {
            Statics.currentequipmentchar--;
            choosechar(Statics.currentequipmentchar);
        }
    }
    public void choosechar(int currentchar)
    {
        Statics.currentequipmentchar = currentchar;
        nametext.text = Statics.characternames[Statics.currentequipmentchar] + " LvL" + Statics.charcurrentlvl;
        settextandimage(Statics.currentequipmentchar);
        setselectedbutton();
        menusoundcontroller.playmenubuttonsound();
    }
    private void settextandimage(int currentchar)
    {
        if (Statics.charcurrenthealth[currentchar] > Statics.charmaxhealth[currentchar])
        {
            Statics.charcurrenthealth[currentchar] = Statics.charmaxhealth[currentchar];
        }
        float healbonus;
        if (Statics.characterclassroll[currentchar] == 1)
        {
            healbonus = Mathf.Round((Statics.charmaxhealth[currentchar] - Statics.charcurrentlvl * Statics.guardbonushpeachlvl) * Statics.healhealthbonuspercentage * 0.01f);
        }
        else healbonus = Mathf.Round(Statics.charmaxhealth[currentchar] * Statics.healhealthbonuspercentage * 0.01f);
        statsnumbers.text = string.Empty;
        statsnumbers.color = Color.white;
        statsnumbers.text = Statics.charmaxhealth[currentchar] + "\n" +
                            healbonus + "\n" +
                            Statics.chardefense[currentchar] + "\n" +
                            Mathf.Round(Statics.chardefense[currentchar] * Statics.defenseconvertedtoattack * 0.01f) + "\n" +
                            Statics.charattack[currentchar] + "\n" +
                            Statics.charcritchance[currentchar] + "%" + "\n" +
                            Statics.charcritdmg[currentchar] + "%" + "\n" +
                            (Statics.charweaponbuff[currentchar]) + "%" + "\n" +
                            Statics.charweaponbuffduration[currentchar] + "sec" + "\n" +
                            (Statics.charswitchbuff[currentchar]) + "%" + "\n" +
                            Statics.charswitchbuffduration[currentchar] + "sec" + "\n" +
                            Statics.charbasiccritbuff[currentchar] + "%" + "\n" +
                            Statics.charbasicdmgbuff[currentchar] + "%";

        sworddmg.text = Statics.charswordattack[currentchar].ToString();
        bowdmg.text = Statics.charbowattack[currentchar].ToString();
        fistdmg.text = Statics.charfistattack[currentchar].ToString();


        slotbuttontext[0].text = Statics.charcurrentsword[currentchar].itemname;
        slotbuttontext[1].text = Statics.charcurrentbow[currentchar].itemname;
        slotbuttontext[2].text = Statics.charcurrentfist[currentchar].itemname;
        slotbuttontext[3].text = Statics.charcurrenthead[currentchar].itemname;
        slotbuttontext[4].text = Statics.charcurrentchest[currentchar].itemname;
        slotbuttontext[5].text = Statics.charcurrentbelt[currentchar].itemname;
        slotbuttontext[6].text = Statics.charcurrentlegs[currentchar].itemname;
        slotbuttontext[7].text = Statics.charcurrentshoes[currentchar].itemname;
        slotbuttontext[8].text = Statics.charcurrentnecklace[currentchar].itemname;
        slotbuttontext[9].text = Statics.charcurrentring[currentchar].itemname;

        foreach (GameObject obj in charbuttons)
        {
            obj.GetComponent<Image>().color = Color.white;
        }
        charbuttons[Statics.currentequipmentchar].GetComponent<Image>().color = Color.green;
    }
    
    private void setselectedbutton()
    {
        if(currentbuttonslot != null)
        {
            EventSystem.current.SetSelectedGameObject(currentbuttonslot);
            if(Statics.currentequipmentbutton < 3)
            {
                currentbuttonslot.gameObject.GetComponent<Setcolorcurrentweapon>().triggerbutton();
            }
            else
            {
                currentbuttonslot.gameObject.GetComponent<Setcolorcurrentarmor>().triggerbutton();
            }
        }
    }
}
