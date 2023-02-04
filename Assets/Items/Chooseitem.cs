using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class Chooseitem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject slotbuttontext;
    private GameObject slotbutton;
    private TextMeshProUGUI statsnumbers;

    [NonSerialized] public Itemcontroller itemvalues;

    private Setstats setitemobj;

    [NonSerialized] public Text upgradelvltext;

    [NonSerialized] public int equipslotnumber;                       //wird im Inventoryui gesetzt

    [NonSerialized] public int selectedchar = 0;

    private void Awake()
    {
        gameObject.GetComponent<Equiparmor>().chooseitem = this;
        setitemobj = GetComponentInParent<Setstats>();
        slotbuttontext = gameObject.GetComponentInParent<Setstats>().slottext;
        slotbutton = gameObject.GetComponentInParent<Setstats>().slotbutton;
        statsnumbers = gameObject.GetComponentInParent<Setstats>().statsnumbers;
    }
    public void setitem()
    {
        if (itemvalues != null)
        {
            selectedchar = Statics.currentequipmentchar;
            slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;
            setnewitem(equipslotnumber);
            statsupdate();
            EventSystem.current.SetSelectedGameObject(slotbutton);                            //beim onselect call wird die selectfarbe gesetzt
        }
    }
    private void statsupdate()
    {
        statsnumbers.text = string.Empty;
        statsnumbers.color = Color.white;
        statsnumbers.text = Statics.charmaxhealth[selectedchar] + "\n" +
                            Statics.chardefense[selectedchar] + "\n" +
                            Statics.charattack[selectedchar] + "\n" +
                            Statics.charcritchance[selectedchar] + "%" + "\n" +
                            Statics.charcritdmg[selectedchar] + "%" + "\n" +
                            (Statics.charweaponbuff[selectedchar] - 100) + "%" + "\n" +
                            Statics.charweaponbuffduration[selectedchar] + "sec" + "\n" +
                            (Statics.charswitchbuff[selectedchar] - 100) + "%" + "\n" +
                            Statics.charswitchbuffduration[selectedchar] + "sec" + "\n" +
                            Statics.charbasiccritbuff[selectedchar] + "%" + "\n" +
                            Statics.charbasicdmgbuff[selectedchar] + "%";
    }

    public void setnewitem(int equipslot)                                           //equipslot wird im inventoryui gesetzt     

    {
        if (equipslot == 3)                //head = number3
        {
            gameObject.GetComponent<Equiparmor>().sethead();
        }
        else if (equipslot == 4)         //chest = number4
        {
            gameObject.GetComponent<Equiparmor>().setchest();
        }
        else if (equipslot == 5)        //gloves = number5
        {
            gameObject.GetComponent<Equiparmor>().setgloves();
        }
        else if (equipslot == 6)        //legs = number6
        {
            gameObject.GetComponent<Equiparmor>().setlegs();
        }
        else if (equipslot == 7)        //shoes = number7
        {
            gameObject.GetComponent<Equiparmor>().setshoes();
        }
        else if (equipslot == 8)        //neckless = number8
        {
            gameObject.GetComponent<Equiparmor>().setneckless();
        }
        else if (equipslot == 9)        //ring = number9
        {
            gameObject.GetComponent<Equiparmor>().setring();
        }
    }
    /*public void upgradeequipeditems()
    {
        transform.GetChild(2).GetComponentInChildren<Text>().text = itemvalues.upgradelvl.ToString();
        //update inventory itemlvl
        if (equipslotnumber == 3)
        {
            int currentchar = 0;
            foreach (string item in Statics.charcurrentheadname)
            {
                if (itemvalues.itemname == item)
                {
                    setstatsafterupgrade(currentchar);
                }
                currentchar++;
            }
        }
        if (equipslotnumber == 4)
        {
            int currentchar = 0;
            foreach (string item in Statics.charcurrentchestname)
            {
                if (itemvalues.itemname == item)
                {
                    setstatsafterupgrade(currentchar);
                }
                currentchar++;
            }
        }
        if (equipslotnumber == 5)
        {
            int currentchar = 0;
            foreach (string item in Statics.charcurrentglovesname)
            {
                if (itemvalues.itemname == item)
                {
                    setstatsafterupgrade(currentchar);
                }
                currentchar++;
            }
        }
        if (equipslotnumber == 6)
        {
            int currentchar = 0;
            foreach (string item in Statics.charcurrentlegname)
            {
                if (itemvalues.itemname == item)
                {
                    setstatsafterupgrade(currentchar);
                }
                currentchar++;
            }
        }
        if (equipslotnumber == 7)
        {
            int currentchar = 0;
            foreach (string item in Statics.charcurrentshoesname)
            {
                if (itemvalues.itemname == item)
                {
                    setstatsafterupgrade(currentchar);
                }
                currentchar++;
            }
        }
        if (equipslotnumber == 8)
        {
            int currentchar = 0;
            foreach (string item in Statics.charcurrentnecklessname)
            {
                if (itemvalues.itemname == item)
                {
                    setstatsafterupgrade(currentchar);
                }
                currentchar++;
            }
        }
        if (equipslotnumber == 9)
        {
            int currentchar = 0;
            foreach (string item in Statics.charcurrentringname)
            {
                if (itemvalues.itemname == item)
                {
                    setstatsafterupgrade(currentchar);
                }
                currentchar++;
            }
        }
    }
    private void setstatsafterupgrade(int currentchar)
    {
        Statics.charmaxhealth[currentchar] += itemvalues.stats[0] - setitemobj.currentmaxhealth[currentchar];
        setitemobj.currentmaxhealth[currentchar] = itemvalues.stats[0];

        Statics.chardefense[currentchar] += itemvalues.stats[1] - setitemobj.currentdefense[currentchar];
        setitemobj.currentdefense[currentchar] = itemvalues.stats[1];

        Statics.charattack[currentchar] += itemvalues.stats[2] - setitemobj.currentattack[currentchar];
        setitemobj.currentattack[currentchar] = itemvalues.stats[2];

        Statics.charcritchance[currentchar] += itemvalues.stats[3] - setitemobj.currentcritchance[currentchar];
        setitemobj.currentcritchance[selectedchar] = itemvalues.stats[3];

        Statics.charcritdmg[currentchar] += itemvalues.stats[4] - setitemobj.currentcritdmg[currentchar];
        setitemobj.currentcritdmg[currentchar] = itemvalues.stats[4];

        Statics.charweaponbuff[currentchar] += itemvalues.stats[5] - setitemobj.currentweaponbuff[currentchar];
        setitemobj.currentweaponbuff[currentchar] = itemvalues.stats[5];

        Statics.charswitchbuff[currentchar] += itemvalues.stats[6] - setitemobj.currentcharswitchbuff[currentchar];
        setitemobj.currentcharswitchbuff[currentchar] = itemvalues.stats[6];

        Statics.charbasicdmgbuff[currentchar] += itemvalues.stats[7] - setitemobj.currentbasicdmgbuff[currentchar];
        setitemobj.currentbasicdmgbuff[currentchar] = itemvalues.stats[7];
    }*/
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        statsnumbers.text = string.Empty;
        statsnumbers.color = Color.white;
        selectedchar = Statics.currentequipmentchar;
        if (itemvalues != null)
        {
            if (Statics.currentequipmentbutton == 3)
            {
                ontrigger(Statics.charcurrenthead[selectedchar]);
                /*ontriggerflatstats(Statics.charcurrenthead[selectedchar], 0, Statics.charmaxhealth[selectedchar]);
                ontriggerflatstats(Statics.charcurrenthead[selectedchar], 1, Statics.chardefense[selectedchar]);
                ontriggerflatstats(Statics.charcurrenthead[selectedchar], 2, Statics.charattack[selectedchar]);
                ontriggerpercentage(Statics.charcurrenthead[selectedchar], 3, Statics.charcritchance[selectedchar]);
                ontriggerpercentage(Statics.charcurrenthead[selectedchar], 4, Statics.charcritdmg[selectedchar]);
                ontriggerpercentage(Statics.charcurrenthead[selectedchar], 5, Statics.charweaponbuff[selectedchar] - 100);
                ontriggerduration(Statics.charweaponbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrenthead[selectedchar], 6, Statics.charswitchbuff[selectedchar] - 100);
                ontriggerduration(Statics.charswitchbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrenthead[selectedchar], 7, Statics.charbasiccritbuff[selectedchar]);
                ontriggerpercentage(Statics.charcurrenthead[selectedchar], 8, Statics.charbasicdmgbuff[selectedchar]);*/
            }

            else if (Statics.currentequipmentbutton == 4)
            {
                ontrigger(Statics.charcurrentchest[selectedchar]);
                /*ontriggerflatstats(Statics.charcurrentchest[selectedchar], 0, Statics.charmaxhealth[selectedchar]);
                ontriggerflatstats(Statics.charcurrentchest[selectedchar], 1, Statics.chardefense[selectedchar]);
                ontriggerflatstats(Statics.charcurrentchest[selectedchar], 2, Statics.charattack[selectedchar]);
                ontriggerpercentage(Statics.charcurrentchest[selectedchar], 3, Statics.charcritchance[selectedchar]);
                ontriggerpercentage(Statics.charcurrentchest[selectedchar], 4, Statics.charcritdmg[selectedchar]);
                ontriggerpercentage(Statics.charcurrentchest[selectedchar], 5, Statics.charweaponbuff[selectedchar] - 100);
                ontriggerduration(Statics.charweaponbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentchest[selectedchar], 6, Statics.charswitchbuff[selectedchar] - 100);
                ontriggerduration(Statics.charswitchbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentchest[selectedchar], 7, Statics.charbasiccritbuff[selectedchar]);
                ontriggerpercentage(Statics.charcurrentchest[selectedchar], 8, Statics.charbasicdmgbuff[selectedchar]);*/
            }
            else if (Statics.currentequipmentbutton == 5)
            {
                ontrigger(Statics.charcurrentgloves[selectedchar]);
                /*ontriggerflatstats(Statics.charcurrentgloves[selectedchar], 0, Statics.charmaxhealth[selectedchar]);
                ontriggerflatstats(Statics.charcurrentgloves[selectedchar], 1, Statics.chardefense[selectedchar]);
                ontriggerflatstats(Statics.charcurrentgloves[selectedchar], 2, Statics.charattack[selectedchar]);
                ontriggerpercentage(Statics.charcurrentgloves[selectedchar], 3, Statics.charcritchance[selectedchar]);
                ontriggerpercentage(Statics.charcurrentgloves[selectedchar], 4, Statics.charcritdmg[selectedchar]);
                ontriggerpercentage(Statics.charcurrentgloves[selectedchar], 5, Statics.charweaponbuff[selectedchar] - 100);
                ontriggerduration(Statics.charweaponbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentgloves[selectedchar], 6, Statics.charswitchbuff[selectedchar] - 100);
                ontriggerduration(Statics.charswitchbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentgloves[selectedchar], 7, Statics.charbasiccritbuff[selectedchar]);
                ontriggerpercentage(Statics.charcurrentgloves[selectedchar], 8, Statics.charbasicdmgbuff[selectedchar]);*/
            }
            else if (Statics.currentequipmentbutton == 6)
            {
                ontrigger(Statics.charcurrentlegs[selectedchar]);
                /*ontriggerflatstats(Statics.charcurrentlegs[selectedchar], 0, Statics.charmaxhealth[selectedchar]);
                ontriggerflatstats(Statics.charcurrentlegs[selectedchar], 1, Statics.chardefense[selectedchar]);
                ontriggerflatstats(Statics.charcurrentlegs[selectedchar], 2, Statics.charattack[selectedchar]);
                ontriggerpercentage(Statics.charcurrentlegs[selectedchar], 3, Statics.charcritchance[selectedchar]);
                ontriggerpercentage(Statics.charcurrentlegs[selectedchar], 4, Statics.charcritdmg[selectedchar]);
                ontriggerpercentage(Statics.charcurrentlegs[selectedchar], 5, Statics.charweaponbuff[selectedchar] - 100);
                ontriggerduration(Statics.charweaponbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentlegs[selectedchar], 6, Statics.charswitchbuff[selectedchar] - 100);
                ontriggerduration(Statics.charswitchbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentlegs[selectedchar], 7, Statics.charbasiccritbuff[selectedchar]);
                ontriggerpercentage(Statics.charcurrentlegs[selectedchar], 8, Statics.charbasicdmgbuff[selectedchar]);*/
            }
            else if (Statics.currentequipmentbutton == 7)
            {
                ontrigger(Statics.charcurrentshoes[selectedchar]);
                /*ontriggerflatstats(Statics.charcurrentshoes[selectedchar], 0, Statics.charmaxhealth[selectedchar]);
                ontriggerflatstats(Statics.charcurrentshoes[selectedchar], 1, Statics.chardefense[selectedchar]);
                ontriggerflatstats(Statics.charcurrentshoes[selectedchar], 2, Statics.charattack[selectedchar]);
                ontriggerpercentage(Statics.charcurrentshoes[selectedchar], 3, Statics.charcritchance[selectedchar]);
                ontriggerpercentage(Statics.charcurrentshoes[selectedchar], 4, Statics.charcritdmg[selectedchar]);
                ontriggerpercentage(Statics.charcurrentshoes[selectedchar], 5, Statics.charweaponbuff[selectedchar] - 100);
                ontriggerduration(Statics.charweaponbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentshoes[selectedchar], 6, Statics.charswitchbuff[selectedchar] - 100);
                ontriggerduration(Statics.charswitchbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentshoes[selectedchar], 7, Statics.charbasiccritbuff[selectedchar]);
                ontriggerpercentage(Statics.charcurrentshoes[selectedchar], 8, Statics.charbasicdmgbuff[selectedchar]);*/
            }
            else if (Statics.currentequipmentbutton == 8)
            {
                ontrigger(Statics.charcurrentneckless[selectedchar]);
                /*ontriggerflatstats(Statics.charcurrentneckless[selectedchar], 0, Statics.charmaxhealth[selectedchar]);
                ontriggerflatstats(Statics.charcurrentneckless[selectedchar], 1, Statics.chardefense[selectedchar]);
                ontriggerflatstats(Statics.charcurrentneckless[selectedchar], 2, Statics.charattack[selectedchar]);
                ontriggerpercentage(Statics.charcurrentneckless[selectedchar], 3, Statics.charcritchance[selectedchar]);
                ontriggerpercentage(Statics.charcurrentneckless[selectedchar], 4, Statics.charcritdmg[selectedchar]);
                ontriggerpercentage(Statics.charcurrentneckless[selectedchar], 5, Statics.charweaponbuff[selectedchar] - 100);
                ontriggerduration(Statics.charweaponbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentneckless[selectedchar], 6, Statics.charswitchbuff[selectedchar] - 100);
                ontriggerduration(Statics.charswitchbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentneckless[selectedchar], 7, Statics.charbasiccritbuff[selectedchar]);
                ontriggerpercentage(Statics.charcurrentneckless[selectedchar], 8, Statics.charbasicdmgbuff[selectedchar]);*/
            }
            else if (Statics.currentequipmentbutton == 9)
            {
                ontrigger(Statics.charcurrentring[selectedchar]);
                /*ontriggerflatstats(Statics.charcurrentring[selectedchar], 0, Statics.charmaxhealth[selectedchar]);
                ontriggerflatstats(Statics.charcurrentring[selectedchar], 1, Statics.chardefense[selectedchar]);
                ontriggerflatstats(Statics.charcurrentring[selectedchar], 2, Statics.charattack[selectedchar]);
                ontriggerpercentage(Statics.charcurrentring[selectedchar], 3, Statics.charcritchance[selectedchar]);
                ontriggerpercentage(Statics.charcurrentring[selectedchar], 4, Statics.charcritdmg[selectedchar]);
                ontriggerpercentage(Statics.charcurrentring[selectedchar], 5, Statics.charweaponbuff[selectedchar] - 100);
                ontriggerduration(Statics.charweaponbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentring[selectedchar], 6, Statics.charswitchbuff[selectedchar] - 100);
                ontriggerduration(Statics.charswitchbuffduration[selectedchar]);
                ontriggerpercentage(Statics.charcurrentring[selectedchar], 7, Statics.charbasiccritbuff[selectedchar]);
                ontriggerpercentage(Statics.charcurrentring[selectedchar], 8, Statics.charbasicdmgbuff[selectedchar]);*/
            }
        }
    }
    private void ontrigger(Itemcontroller slot)
    {
        ontriggerflatstats(slot, 0, Statics.charmaxhealth[selectedchar]);
        ontriggerflatstats(slot, 1, Statics.chardefense[selectedchar]);
        ontriggerflatstats(slot, 2, Statics.charattack[selectedchar]);
        ontriggerpercentage(slot, 3, Statics.charcritchance[selectedchar]);
        ontriggerpercentage(slot, 4, Statics.charcritdmg[selectedchar]);
        ontriggerpercentage(slot, 5, Statics.charweaponbuff[selectedchar] - 100);
        ontriggerduration(Statics.charweaponbuffduration[selectedchar]);
        ontriggerpercentage(slot, 6, Statics.charswitchbuff[selectedchar] - 100);
        ontriggerduration(Statics.charswitchbuffduration[selectedchar]);
        ontriggerbasiccritpercentage(Statics.charbasiccritbuff[selectedchar]);
        ontriggerpercentage(slot, 7, Statics.charbasicdmgbuff[selectedchar]);
    }
    private void ontriggerflatstats(Itemcontroller equipeditem, int itemstat, float currentstatvalue)
    {
        float difference = itemvalues.stats[itemstat] - equipeditem.stats[itemstat];
        if (itemvalues.stats[itemstat] > equipeditem.stats[itemstat])
        {
            statsnumbers.text += "<color=green>" + "( +" + difference + " ) " + (difference + currentstatvalue) + "</color>" + "\n";
        }
        else if (itemvalues.stats[itemstat] < equipeditem.stats[itemstat])
        {
            statsnumbers.text += "<color=red>" + "( +" + difference + " ) " + (difference + currentstatvalue) + "</color>" + "\n";
        }
        else
        {
            statsnumbers.text += currentstatvalue + "\n";
        }
    }
    private void ontriggerpercentage(Itemcontroller equipeditem, int itemstat, float currentstatvalue)
    {
        float difference = itemvalues.stats[itemstat] - equipeditem.stats[itemstat];
        if (itemvalues.stats[itemstat] > equipeditem.stats[itemstat])
        {
            statsnumbers.text += "<color=green>" + "( +" + difference + " ) " + (difference + currentstatvalue) + "%" + "</color>" + "\n";
        }
        else if (itemvalues.stats[itemstat] < equipeditem.stats[itemstat])
        {
            statsnumbers.text += "<color=red>" + "( +" + difference + " ) " + (difference + currentstatvalue) + "%" + "</color>" + "\n";
        }
        else
        {
            statsnumbers.text += currentstatvalue + "%" + "\n";
        }
    }
    private void ontriggerbasiccritpercentage(float currentstatvalue)
    {
        statsnumbers.text += currentstatvalue + "%" + "\n";
    }
    private void ontriggerduration(float currentstatvalue)
    {
        {
            statsnumbers.text += currentstatvalue + "sec" + "\n";
        }
    }


            /*setitemobj.itemtextcontroller.itemvalues = itemvalues;
            setitemobj.itemtextcontroller.chooseitem = this;
            setitemobj.itemtextcontroller.equipslotnumber = equipslotnumber;
            setitemobj.showitemstatsobj.SetActive(true);
        }
    }*/
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        statsupdate();
    }
}
