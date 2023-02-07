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
    private Weapongridvalues weapongridvalues;

    public int selectedchar = 0;

    private void Awake()
    {
        weapongridvalues = GetComponentInParent<Weapongridvalues>();
        slotbuttontext = gameObject.GetComponentInParent<Weapongridvalues>().slottext;
        slotbutton = gameObject.GetComponentInParent<Weapongridvalues>().slotbutton;
    }
    public void setweapon()
    {
        if(itemvalues != null)
        {
            selectedchar = Statics.currentequipmentchar;
            slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;
            setnewitem(Statics.currentequipmentbutton);
            statsupdate();
        }
        EventSystem.current.SetSelectedGameObject(slotbutton);                            //beim onselect call wird die selectfarbe gesetzt
    }
    private void statsupdate()
    {
        weapongridvalues.sworddmg.text = Statics.charswordattack[selectedchar].ToString();
        weapongridvalues.bowdmg.text = Statics.charbowattack[selectedchar].ToString();
        weapongridvalues.fistdmg.text = Statics.charfistattack[selectedchar].ToString();
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
        if (itemvalues != null)
        {
            selectedchar = Statics.currentequipmentchar;

            if (Statics.currentequipmentbutton == 0)
            {
                ontriggerstats(Statics.charcurrentsword[selectedchar], weapongridvalues.sworddmg);
            }

            else if (Statics.currentequipmentbutton == 1)
            {
                ontriggerstats(Statics.charcurrentbow[selectedchar], weapongridvalues.bowdmg);
            }
            else if (Statics.currentequipmentbutton == 2)
            {
                ontriggerstats(Statics.charcurrentfist[selectedchar], weapongridvalues.fistdmg);
            }
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
        statsupdate();
    }
}

