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

    [NonSerialized] public Itemcontroller itemvalues;

    private Setstats setitemobj;

    [NonSerialized] public Text upgradelvltext;

    [NonSerialized] public int equipslotnumber;                       //wird im Inventoryui gesetzt

    public int currentint = 0;

    private void Awake()
    {
        setitemobj = GetComponentInParent<Setstats>();
        slotbuttontext = gameObject.GetComponentInParent<Setstats>().slottext;
        slotbutton = gameObject.GetComponentInParent<Setstats>().slotbutton;
    }
    public void setitem()
    {
        if (itemvalues != null)
        {
            currentint = Statics.currentequiptmentchar;
            slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;
            setequipnameandimage(equipslotnumber, currentint, GetComponentInChildren<Text>().text, gameObject);
            transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;

            Statics.charmaxhealth[currentint] += itemvalues.stats[0] - setitemobj.currentmaxhealth[currentint];
            setitemobj.currentmaxhealth[currentint] = itemvalues.stats[0];
            setitemobj.statsmaxhealth.text = Statics.charmaxhealth[currentint] + "";
            setitemobj.statsmaxhealth.color = Color.white;

            Statics.chardefense[currentint] += itemvalues.stats[1] - setitemobj.currentdefense[currentint];
            setitemobj.currentdefense[currentint] = itemvalues.stats[1];
            setitemobj.statsdefense.text = Statics.chardefense[currentint] + "";
            setitemobj.statsdefense.color = Color.white;

            Statics.charattack[currentint] += itemvalues.stats[2] - setitemobj.currentattack[currentint];
            setitemobj.currentattack[currentint] = itemvalues.stats[2];
            setitemobj.statsattack.text = Statics.charattack[currentint] + "";
            setitemobj.statsattack.color = Color.white;

            Statics.charcritchance[currentint] += itemvalues.stats[3] - setitemobj.currentcritchance[currentint];
            setitemobj.currentcritchance[currentint] = itemvalues.stats[3];
            setitemobj.statscritchance.text = Statics.charcritchance[currentint] + "%";
            setitemobj.statscritchance.color = Color.white;

            Statics.charcritdmg[currentint] += itemvalues.stats[4] - setitemobj.currentcritdmg[currentint];
            setitemobj.currentcritdmg[currentint] = itemvalues.stats[4];
            setitemobj.statscritdmg.text = Statics.charcritdmg[currentint] + "%";
            setitemobj.statscritdmg.color = Color.white;

            Statics.charweaponbuff[currentint] += itemvalues.stats[5] - setitemobj.currentweaponbuff[currentint];
            setitemobj.currentweaponbuff[currentint] = itemvalues.stats[5];
            setitemobj.statsweaponbuff.text = Statics.charweaponbuff[currentint] - 100 + "%";
            setitemobj.statsweaponbuff.color = Color.white;

            Statics.charswitchbuff[currentint] += itemvalues.stats[6] - setitemobj.currentcharswitchbuff[currentint];
            setitemobj.currentcharswitchbuff[currentint] = itemvalues.stats[6];
            setitemobj.statscharswitchbuff.text = Statics.charswitchbuff[currentint] - 100 + "%";
            setitemobj.statscharswitchbuff.color = Color.white;

            Statics.charbasicdmgbuff[currentint] += itemvalues.stats[7] - setitemobj.currentbasicdmgbuff[currentint];
            setitemobj.currentbasicdmgbuff[currentint] = itemvalues.stats[7];
            setitemobj.statsbasicdmgbuff.text = Statics.charbasicdmgbuff[currentint] + "%";
            setitemobj.statsbasicdmgbuff.color = Color.white;
        }
        EventSystem.current.SetSelectedGameObject(slotbutton);
    }
    public void setequipnameandimage(int equipslot, int charnumber, string itemname, GameObject itemimageobj)             //hier werden name und image gesetzt damit ich nur ein script für chooseitem brauch, jeder equipmentslot hat eine eigene nummer
                                                                                                                          //muss im InventoryUI geändert werden falls die Reihenfolge sich ändert
    {
        if (equipslot == 3)                //head = number3
        {
            if (Statics.currentheadimage[charnumber] != null)
            {
                Statics.currentheadimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentheadname[charnumber] = itemname;
            Statics.currentheadimage[charnumber] = itemimageobj;
            Statics.activeheadslot = itemimageobj;
        }
        else if (equipslot == 4)         //chest = number4
        {
            if (Statics.currentchestimage[charnumber] != null)
            {
                Statics.currentchestimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentchestname[charnumber] = itemname;
            Statics.currentchestimage[charnumber] = itemimageobj;
            Statics.activechestslot = itemimageobj;
        }
        else if (equipslot == 5)        //gloves = number5
        {
            if (Statics.currentglovesimage[charnumber] != null)
            {
                Statics.currentglovesimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentglovesname[charnumber] = itemname;
            Statics.currentglovesimage[charnumber] = itemimageobj;
            Statics.activeglovesslot = itemimageobj;
        }
        else if (equipslot == 6)        //belt = number6
        {
            if (Statics.currentlegimage[charnumber] != null)
            {
                Statics.currentlegimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentlegname[charnumber] = itemname;
            Statics.currentlegimage[charnumber] = itemimageobj;
            Statics.activebeltslot = itemimageobj;
        }
        else if (equipslot == 7)        //shoes = number7
        {
            if (Statics.currentshoesimage[charnumber] != null)
            {
                Statics.currentshoesimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentshoesname[charnumber] = itemname;
            Statics.currentshoesimage[charnumber] = itemimageobj;
            Statics.activeshoesslot = itemimageobj;
        }
        else if (equipslot == 8)        //neckless = number8
        {
            if (Statics.currentnecklessimage[charnumber] != null)
            {
                Statics.currentnecklessimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentnecklessname[charnumber] = itemname;
            Statics.currentnecklessimage[charnumber] = itemimageobj;
            Statics.activenecklessslot = itemimageobj;
        }
        else if (equipslot == 9)        //ring = number9
        {
            if (Statics.currentringimage[charnumber] != null)
            {
                Statics.currentringimage[charnumber].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            Statics.charcurrentringname[charnumber] = itemname;
            Statics.currentringimage[charnumber] = itemimageobj;
            Statics.activeringslot = itemimageobj;
        }
    }
    public void upgradeequipeditems()
    {
        transform.GetChild(3).GetComponentInChildren<Text>().text = itemvalues.upgradelvl.ToString();
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
        setitemobj.currentcritchance[currentint] = itemvalues.stats[3];

        Statics.charcritdmg[currentchar] += itemvalues.stats[4] - setitemobj.currentcritdmg[currentchar];
        setitemobj.currentcritdmg[currentchar] = itemvalues.stats[4];

        Statics.charweaponbuff[currentchar] += itemvalues.stats[5] - setitemobj.currentweaponbuff[currentchar];
        setitemobj.currentweaponbuff[currentchar] = itemvalues.stats[5];

        Statics.charswitchbuff[currentchar] += itemvalues.stats[6] - setitemobj.currentcharswitchbuff[currentchar];
        setitemobj.currentcharswitchbuff[currentchar] = itemvalues.stats[6];

        Statics.charbasicdmgbuff[currentchar] += itemvalues.stats[7] - setitemobj.currentbasicdmgbuff[currentchar];
        setitemobj.currentbasicdmgbuff[currentchar] = itemvalues.stats[7];
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        currentint = Statics.currentequiptmentchar;
        if (itemvalues != null)
        {
            float currenthealth = setitemobj.currentmaxhealth[currentint];
            float currentdenfese = setitemobj.currentdefense[currentint];
            float currentattack = setitemobj.currentattack[currentint];
            float currentcritchance = setitemobj.currentcritchance[currentint];
            float currentcritdmg = setitemobj.currentcritdmg[currentint];
            float currentweaponbuff = setitemobj.currentweaponbuff[currentint];
            float currentcharbuff = setitemobj.currentcharswitchbuff[currentint];
            float currentbasicbuff = setitemobj.currentbasicdmgbuff[currentint];

            if (itemvalues.stats[0] > currenthealth)
            {
                setitemobj.statsmaxhealth.color = Color.green;
                setitemobj.statsmaxhealth.text = "( +" + (itemvalues.stats[0] - currenthealth) + " ) " + (itemvalues.stats[0] + Statics.charmaxhealth[currentint]);               //berechnung eventuell falsch, richtig variante ist in Chooseweapon
            }
            if (itemvalues.stats[0] < currenthealth)
            {
                setitemobj.statsmaxhealth.color = Color.red;
                setitemobj.statsmaxhealth.text = "( " + (itemvalues.stats[0] - currenthealth) + " ) " + (Statics.charmaxhealth[currentint] - currenthealth);
            }

            if (itemvalues.stats[1] > currentdenfese)
            {
                setitemobj.statsdefense.color = Color.green;
                setitemobj.statsdefense.text = "( +" + (itemvalues.stats[1] - currentdenfese) + " ) " + (itemvalues.stats[1] + Statics.chardefense[currentint]);
            }

            if (itemvalues.stats[1] < currentdenfese)
            {
                setitemobj.statsdefense.color = Color.red;
                setitemobj.statsdefense.text = "( " + (itemvalues.stats[1] - currentdenfese) + " ) " + (Statics.chardefense[currentint] - currentdenfese);
            }

            if (itemvalues.stats[2] > currentattack)
            {
                setitemobj.statsattack.color = Color.green;
                setitemobj.statsattack.text = "( +" + (itemvalues.stats[2] - currentattack) + " ) " + (itemvalues.stats[2] + Statics.charattack[currentint]);
            }
            if (itemvalues.stats[2] < currentattack)
            {
                setitemobj.statsattack.color = Color.red;
                setitemobj.statsattack.text = "( " + (itemvalues.stats[2] - currentattack) + " ) " + (Statics.charattack[currentint] - currentattack);
            }

            if (itemvalues.stats[3] > currentcritchance)
            {
                setitemobj.statscritchance.color = Color.green;
                setitemobj.statscritchance.text = "( +" + (itemvalues.stats[3] - currentcritchance) + "%" + " ) " + (itemvalues.stats[3] + Statics.charcritchance[currentint]) + "%";
            }
            if (itemvalues.stats[3] < currentcritchance)
            {
                setitemobj.statscritchance.color = Color.red;
                setitemobj.statscritchance.text = "( " + (itemvalues.stats[3] - currentcritchance) + "%" + " ) " + (Statics.charcritchance[currentint] - currentcritchance) + "%";
            }

            if (itemvalues.stats[4] > currentcritdmg)
            {
                setitemobj.statscritdmg.color = Color.green;
                setitemobj.statscritdmg.text = "( +" + (itemvalues.stats[4] - currentcritdmg) + "%" + " ) " + (itemvalues.stats[4] + Statics.charcritdmg[currentint]) + "%";
            }
            if (itemvalues.stats[4] < currentcritdmg)
            {
                setitemobj.statscritdmg.color = Color.red;
                setitemobj.statscritdmg.text = "( " + (itemvalues.stats[4] - currentcritdmg) + "%" + " ) " + (Statics.charcritdmg[currentint] - currentcritdmg) + "%";
            }

            if (itemvalues.stats[5] > currentweaponbuff)
            {
                setitemobj.statsweaponbuff.color = Color.green;
                setitemobj.statsweaponbuff.text = "( +" + (itemvalues.stats[5] - currentweaponbuff) + "%" + " ) " + (itemvalues.stats[5] + Statics.charweaponbuff[currentint] - 100) + "%";
            }
            if (itemvalues.stats[5] < currentweaponbuff)
            {
                setitemobj.statsweaponbuff.color = Color.red;
                setitemobj.statsweaponbuff.text = "( " + (itemvalues.stats[5] - currentweaponbuff) + "%" + " ) " + (Statics.charweaponbuff[currentint] - currentweaponbuff - 100) + "%";
            }

            if (itemvalues.stats[6] > currentcharbuff)
            {
                setitemobj.statscharswitchbuff.color = Color.green;
                setitemobj.statscharswitchbuff.text = "( +" + (itemvalues.stats[6] - currentcharbuff) + "%" + " ) " + (itemvalues.stats[6] + Statics.charswitchbuff[currentint] - 100) + "%";
            }
            if (itemvalues.stats[6] < currentcharbuff)
            {
                setitemobj.statscharswitchbuff.color = Color.red;
                setitemobj.statscharswitchbuff.text = "( " + (itemvalues.stats[6] - currentcharbuff) + "%" + " ) " + (Statics.charswitchbuff[currentint] - currentcharbuff - 100) + "%";
            }

            if (itemvalues.stats[7] > currentbasicbuff)
            {
                setitemobj.statsbasicdmgbuff.color = Color.green;
                setitemobj.statsbasicdmgbuff.text = "( +" + (itemvalues.stats[7] - currentbasicbuff) + "%" + " ) " + (itemvalues.stats[7] + Statics.charbasicdmgbuff[currentint]) + "%";
            }
            if (itemvalues.stats[7] < currentbasicbuff)
            {
                setitemobj.statsbasicdmgbuff.color = Color.red;
                setitemobj.statsbasicdmgbuff.text = "( " + (itemvalues.stats[7] - currentbasicbuff) + "%" + " ) " + (Statics.charbasicdmgbuff[currentint] - currentbasicbuff) + "%";
            }
            setitemobj.itemtextcontroller.itemvalues = itemvalues;
            setitemobj.itemtextcontroller.chooseitem = this;
            setitemobj.itemtextcontroller.equipslotnumber = equipslotnumber;
            setitemobj.showitemstatsobj.SetActive(true);
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        statsreset();
    }
    private void statsreset()
    {
        setitemobj.statsmaxhealth.text = Statics.charmaxhealth[currentint] + "";
        setitemobj.statsmaxhealth.color = Color.white;

        setitemobj.statsdefense.text = Statics.chardefense[currentint] + "";
        setitemobj.statsdefense.color = Color.white;

        setitemobj.statsattack.text = Statics.charattack[currentint] + "";
        setitemobj.statsattack.color = Color.white;

        setitemobj.statscritchance.text = Statics.charcritchance[currentint] + "%";
        setitemobj.statscritchance.color = Color.white;

        setitemobj.statscritdmg.text = Statics.charcritdmg[currentint] + "%";
        setitemobj.statscritdmg.color = Color.white;

        setitemobj.statsweaponbuff.text = Statics.charweaponbuff[currentint] - 100 + "%";
        setitemobj.statsweaponbuff.color = Color.white;

        setitemobj.statscharswitchbuff.text = Statics.charswitchbuff[currentint] - 100 + "%";
        setitemobj.statscharswitchbuff.color = Color.white;

        setitemobj.statsbasicdmgbuff.text = Statics.charbasicdmgbuff[currentint] + "%";
        setitemobj.statsbasicdmgbuff.color = Color.white;

        setitemobj.showitemstatsobj.SetActive(false);
    }
}
