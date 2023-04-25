using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class Chooseweapon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject slotbuttontext;
    private GameObject slotbutton;

    [NonSerialized] public Itemcontroller itemvalues;
    private Gridvalues gridvalues;

    public int selectedchar = 0;

    private void Awake()
    {
        gridvalues = GetComponentInParent<Gridvalues>();
        slotbuttontext = gameObject.GetComponentInParent<Gridvalues>().slottext;
        slotbutton = gameObject.GetComponentInParent<Gridvalues>().slotbutton;
    }
    private void OnEnable()
    {
        if(itemvalues != null)
        {
            gameObject.GetComponent<Button>().enabled = true;
        }
        else gameObject.GetComponent<Button>().enabled = false;
    }
    public void setweapon()
    {
        if (itemvalues != null)
        {
            selectedchar = Statics.currentequipmentchar;
            slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<TextMeshProUGUI>().text;
            setnewitem(Statics.currentequipmentbutton);
            statsupdate();
            EventSystem.current.SetSelectedGameObject(slotbutton);                            //beim onselect call wird die selectfarbe gesetzt + der sound gespielt
        }     
    }
    private void statsupdate()
    {
        gridvalues.statsnumbers.text = string.Empty;
        gridvalues.statsnumbers.color = Color.white;
        gridvalues.statsnumbers.text = Statics.charmaxhealth[selectedchar] + "\n" +
                            Mathf.Round(Statics.charmaxhealth[selectedchar] * Statics.healhealthbonuspercentage * 0.01f) + "\n" +
                            Statics.chardefense[selectedchar] + "\n" +
                            Mathf.Round(Statics.chardefense[selectedchar] * Statics.defenseconvertedtoattack * 0.01f) + "\n" +
                            Statics.charattack[selectedchar] + "\n" +
                            Statics.charcritchance[selectedchar] + "%" + "\n" +
                            Statics.charcritdmg[selectedchar] + "%" + "\n" +
                            (Statics.charweaponbuff[selectedchar]) + "%" + "\n" +
                            Statics.charweaponbuffduration[selectedchar] + "sec" + "\n" +
                            (Statics.charswitchbuff[selectedchar]) + "%" + "\n" +
                            Statics.charswitchbuffduration[selectedchar] + "sec" + "\n" +
                            Statics.charbasiccritbuff[selectedchar] + "%" + "\n" +
                            Statics.charbasicdmgbuff[selectedchar] + "%";

        gridvalues.sworddmg.text = Statics.charswordattack[selectedchar].ToString();
        gridvalues.bowdmg.text = Statics.charbowattack[selectedchar].ToString();
        gridvalues.fistdmg.text = Statics.charfistattack[selectedchar].ToString();
    }
    private void setnewitem(int equipslot)
    {

        if (equipslot == 0)                //sword = number0
        {
            Statics.charswordattack[selectedchar] = itemvalues.stats[2];
            Statics.charcurrentsword[selectedchar] = itemvalues;
        }
        else if (equipslot == 1)         //bow = number1
        {
            Statics.charbowattack[selectedchar] = itemvalues.stats[2];
            Statics.charcurrentbow[selectedchar] = itemvalues;
        }
        else if (equipslot == 2)        //fist = number2
        {
            Statics.charfistattack[selectedchar] = itemvalues.stats[2];
            Statics.charcurrentfist[selectedchar] = itemvalues;
        }
        statsupdate();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        selectedchar = Statics.currentequipmentchar;
        if (itemvalues != null)
        {
            statsupdate();
            if (Statics.currentequipmentbutton == 0)
            {
                ontriggerstats(Statics.charcurrentsword[selectedchar], gridvalues.sworddmg);
            }
            else if (Statics.currentequipmentbutton == 1)
            {
                ontriggerstats(Statics.charcurrentbow[selectedchar], gridvalues.bowdmg);
            }
            else if (Statics.currentequipmentbutton == 2)
            {
                ontriggerstats(Statics.charcurrentfist[selectedchar], gridvalues.fistdmg);
            }
        }
        else
        {
            statsupdate();
        }
    }
    private void ontriggerstats(Itemcontroller equipeditem, TextMeshProUGUI weapondmgtext)
    {
        float difference = itemvalues.stats[2] - equipeditem.stats[2];
        if (difference > 0)
        {
            weapondmgtext.text = "<color=green>" + "( +" + difference + " ) " + itemvalues.stats[2] + "</color>";
        }
        else if (difference < 0)
        {
            weapondmgtext.text = "<color=red>" + "( " + difference + " ) " + itemvalues.stats[2] + "</color>";
        }
        else
        {
            weapondmgtext.text = itemvalues.stats[2].ToString();
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        selectedchar = Statics.currentequipmentchar;
        statsupdate();
    }
}

