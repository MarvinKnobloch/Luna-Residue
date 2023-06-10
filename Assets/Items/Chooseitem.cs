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

    private Gridvalues gridvalues;

    [NonSerialized] public Text upgradelvltext;
    [NonSerialized] public int selectedchar = 0;

    private void Awake()
    {
        gridvalues = GetComponentInParent<Gridvalues>();
        slotbuttontext = gameObject.GetComponentInParent<Gridvalues>().slottext;
        slotbutton = gameObject.GetComponentInParent<Gridvalues>().slotbutton;
        statsnumbers = gameObject.GetComponentInParent<Gridvalues>().statsnumbers;
    }
    public void buttonupdate()
    {
        if (itemvalues != null)
        {
            gameObject.GetComponent<Button>().enabled = true;
        }
        else gameObject.GetComponent<Button>().enabled = false;
    }
    public void setitem()
    {
        if (itemvalues != null)
        {
            selectedchar = Statics.currentequipmentchar;
            slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<TextMeshProUGUI>().text;
            setnewitem(Statics.currentequipmentbutton);
            statsupdate();
            EventSystem.current.SetSelectedGameObject(slotbutton);                            //beim onselect call wird die selectfarbe gesetzt
        }
    }
    private void statsupdate()
    {
        if (Statics.charcurrenthealth[selectedchar] > Statics.charmaxhealth[selectedchar]) 
        {
            Statics.charcurrenthealth[selectedchar] = Statics.charmaxhealth[selectedchar];
        }
        float healbonus;
        if (Statics.characterclassroll[selectedchar] == 1)
        {
            healbonus = Mathf.Round((Statics.charmaxhealth[selectedchar] - Statics.charcurrentlvl * Statics.guardbonushpeachlvl) * Statics.healhealthbonuspercentage * 0.01f);
        }
        else healbonus = Mathf.Round(Statics.charmaxhealth[selectedchar] * Statics.healhealthbonuspercentage * 0.01f);
        statsnumbers.text = string.Empty;
        statsnumbers.color = Color.white;
        statsnumbers.text = Statics.charmaxhealth[selectedchar] + "\n" +
                            healbonus + "\n" +
                            Statics.chardefense[selectedchar] + "\n" +
                            Mathf.Round(Statics.chardefense[selectedchar] * Statics.defenseconvertedtoattack * 0.01f) + "\n" +
                            Statics.charattack[selectedchar] + "\n" +
                            string.Format("{0:0.0}", Statics.charcritchance[selectedchar]) + "%" + "\n" +
                            string.Format("{0:0.0}", Statics.charcritdmg[selectedchar]) + "%" + "\n" +
                            string.Format("{0:0.0}", Statics.charweaponbuff[selectedchar]) + "%" + "\n" +
                            string.Format("{0:0.0}", Statics.charweaponbuffduration[selectedchar]) + "sec" + "\n" +
                            string.Format("{0:0.0}", Statics.charswitchbuff[selectedchar]) + "%" + "\n" +
                            string.Format("{0:0.0}", Statics.charswitchbuffduration[selectedchar]) + "sec" + "\n" +
                            string.Format("{0:0.0}", Statics.charbasiccritbuff[selectedchar]) + "%" + "\n" +
                            string.Format("{0:0.0}", Statics.charbasicdmgbuff[selectedchar]) + "%";

        gridvalues.sworddmg.text = Statics.charswordattack[selectedchar].ToString();
        gridvalues.bowdmg.text = Statics.charbowattack[selectedchar].ToString();
        gridvalues.fistdmg.text = Statics.charfistattack[selectedchar].ToString();
    }

    public void setnewitem(int equipslot)                                           //equipslot wird im inventoryui gesetzt     

    {
        if (equipslot == 3)                //head = number3
        {
            setitemandvalues(Statics.charcurrenthead[selectedchar]);
            Statics.charcurrenthead[selectedchar] = itemvalues;
        }
        else if (equipslot == 4)         //chest = number4
        {
            setitemandvalues(Statics.charcurrentchest[selectedchar]);
            Statics.charcurrentchest[selectedchar] = itemvalues;
        }
        else if (equipslot == 5)        //belt = number5
        {
            setitemandvalues(Statics.charcurrentbelt[selectedchar]);
            Statics.charcurrentbelt[selectedchar] = itemvalues;
        }
        else if (equipslot == 6)        //legs = number6
        {
            setitemandvalues(Statics.charcurrentlegs[selectedchar]);
            Statics.charcurrentlegs[selectedchar] = itemvalues;
        }
        else if (equipslot == 7)        //shoes = number7
        {
            setitemandvalues(Statics.charcurrentshoes[selectedchar]);
            Statics.charcurrentshoes[selectedchar] = itemvalues;
        }
        else if (equipslot == 8)        //neckless = number8
        {
            setitemandvalues(Statics.charcurrentnecklace[selectedchar]);
            Statics.charcurrentnecklace[selectedchar] = itemvalues;
        }
        else if (equipslot == 9)        //ring = number9
        {
            setitemandvalues(Statics.charcurrentring[selectedchar]);
            Statics.charcurrentring[selectedchar] = itemvalues;
        }
    }
    private void setitemandvalues(Itemcontroller staticsslot)
    {
        if (staticsslot != null)
        {
            Statics.charmaxhealth[selectedchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[0] * Statics.healthperskillpoint - staticsslot.itemlvl[staticsslot.upgradelvl].stats[0] * Statics.healthperskillpoint;
            Statics.chardefense[selectedchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[1] * Statics.defenseperskillpoint - staticsslot.itemlvl[staticsslot.upgradelvl].stats[1] * Statics.defenseperskillpoint;
            Statics.charattack[selectedchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[2] - staticsslot.itemlvl[staticsslot.upgradelvl].stats[2];
            Statics.charcritchance[selectedchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[3] * Statics.critchanceperskillpoint - staticsslot.itemlvl[staticsslot.upgradelvl].stats[3] * Statics.critchanceperskillpoint;
            Statics.charcritdmg[selectedchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[4] * Statics.critdmgperskillpoint - staticsslot.itemlvl[staticsslot.upgradelvl].stats[4] * Statics.critdmgperskillpoint;
            Statics.charweaponbuff[selectedchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[5] * Statics.weaponswitchbuffperskillpoint - staticsslot.itemlvl[staticsslot.upgradelvl].stats[5] * Statics.weaponswitchbuffperskillpoint;
            Statics.charswitchbuff[selectedchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[6] * Statics.charswitchbuffperskillpoint - staticsslot.itemlvl[staticsslot.upgradelvl].stats[6] * Statics.charswitchbuffperskillpoint;
            Statics.charbasicdmgbuff[selectedchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[7] * Statics.basicdmgbuffperskillpoint - staticsslot.itemlvl[staticsslot.upgradelvl].stats[7] * Statics.basicdmgbuffperskillpoint;
        }
        else
        {
            Debug.Log("noitem");
        }
    }
    public void subtractequipeditem()
    {
        if (Statics.currentequipmentbutton == 3)
        {
            setstaticsslot(Statics.charcurrenthead);
        }
        else if (Statics.currentequipmentbutton == 4)
        {
            setstaticsslot(Statics.charcurrentchest);
        }
        else if (Statics.currentequipmentbutton == 5)
        {
            setstaticsslot(Statics.charcurrentbelt);
        }
        else if (Statics.currentequipmentbutton == 6)
        {
            setstaticsslot(Statics.charcurrentlegs);
        }
        else if (Statics.currentequipmentbutton == 7)
        {
            setstaticsslot(Statics.charcurrentshoes);
        }
        else if (Statics.currentequipmentbutton == 8)
        {
            setstaticsslot(Statics.charcurrentnecklace);
        }
        else if (Statics.currentequipmentbutton == 9)
        {
            setstaticsslot(Statics.charcurrentring);
        }
    }
    public void setstaticsslot(Itemcontroller[] staticslotitem)
    {
        for (int i = 0; i < staticslotitem.Length; i++)
        {
            if (itemvalues == staticslotitem[i])
            {
                subtractstatsbeforeupgrade(i);
            }
        }
    }
    private void subtractstatsbeforeupgrade(int currentchar)
    {
        Statics.charmaxhealth[currentchar] -= itemvalues.itemlvl[itemvalues.upgradelvl].stats[0] * Statics.healthperskillpoint;
        Statics.chardefense[currentchar] -= itemvalues.itemlvl[itemvalues.upgradelvl].stats[1] * Statics.defenseperskillpoint;
        Statics.charattack[currentchar] -= itemvalues.itemlvl[itemvalues.upgradelvl].stats[2];
        Statics.charcritchance[currentchar] -= itemvalues.itemlvl[itemvalues.upgradelvl].stats[3] * Statics.critchanceperskillpoint;
        Statics.charcritdmg[currentchar] -= itemvalues.itemlvl[itemvalues.upgradelvl].stats[4] * Statics.critdmgperskillpoint;
        Statics.charweaponbuff[currentchar] -= itemvalues.itemlvl[itemvalues.upgradelvl].stats[5] * Statics.weaponswitchbuffperskillpoint;
        Statics.charswitchbuff[currentchar] -= itemvalues.itemlvl[itemvalues.upgradelvl].stats[6] * Statics.charswitchbuffperskillpoint;
        Statics.charbasicdmgbuff[currentchar] -= itemvalues.itemlvl[itemvalues.upgradelvl].stats[7] * Statics.basicdmgbuffperskillpoint;
    }
    public void upgradeequipeditems()
    {
        transform.GetChild(3).GetComponentInChildren<Text>().text = itemvalues.upgradelvl.ToString();
        if (Statics.currentequipmentbutton == 3)
        {
            setstaticslotadd(Statics.charcurrenthead);
        }
        if (Statics.currentequipmentbutton == 4)
        {
            setstaticslotadd(Statics.charcurrentchest);
        }
        if (Statics.currentequipmentbutton == 5)
        {
            setstaticslotadd(Statics.charcurrentbelt);
        }
        if (Statics.currentequipmentbutton == 6)
        {
            setstaticslotadd(Statics.charcurrentlegs);
        }
        if (Statics.currentequipmentbutton == 7)
        {
            setstaticslotadd(Statics.charcurrentshoes);
        }
        if (Statics.currentequipmentbutton == 8)
        {
            setstaticslotadd(Statics.charcurrentnecklace);
        }
        if (Statics.currentequipmentbutton == 9)
        {
            setstaticslotadd(Statics.charcurrentring);
        }
        statsupdate();
    }
    private void setstaticslotadd(Itemcontroller[] staticslotitem)
    {
        for (int i = 0; i < staticslotitem.Length; i++)
        {
            if (itemvalues == staticslotitem[i])
            {
                addstatsafterupgrade(i);
            }
        }
    }
    private void addstatsafterupgrade(int currentchar)
    {
        Statics.charmaxhealth[currentchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[0] * Statics.healthperskillpoint;
        Statics.chardefense[currentchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[1] * Statics.defenseperskillpoint;
        Statics.charattack[currentchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[2];
        Statics.charcritchance[currentchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[3] * Statics.critchanceperskillpoint;
        Statics.charcritdmg[currentchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[4] * Statics.critdmgperskillpoint;
        Statics.charweaponbuff[currentchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[5] * Statics.weaponswitchbuffperskillpoint;
        Statics.charswitchbuff[currentchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[6] * Statics.charswitchbuffperskillpoint;
        Statics.charbasicdmgbuff[currentchar] += itemvalues.itemlvl[itemvalues.upgradelvl].stats[7] * Statics.basicdmgbuffperskillpoint;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        selectedchar = Statics.currentequipmentchar;
        if (itemvalues != null)
        {
            gridvalues.upgradeuitextcontroller.item = itemvalues;
            gridvalues.upgradeuitextcontroller.chooseitem = this;
            gridvalues.upgradeui.SetActive(true);

            gridvalues.sworddmg.text = Statics.charswordattack[selectedchar].ToString();
            gridvalues.bowdmg.text = Statics.charbowattack[selectedchar].ToString();
            gridvalues.fistdmg.text = Statics.charfistattack[selectedchar].ToString();

            statsnumbers.text = string.Empty;
            statsnumbers.color = Color.white;
            if (Statics.currentequipmentbutton == 3)
            {
                mouseenter(Statics.charcurrenthead[selectedchar]);
            }

            else if (Statics.currentequipmentbutton == 4)
            {
                mouseenter(Statics.charcurrentchest[selectedchar]);
            }
            else if (Statics.currentequipmentbutton == 5)
            {
                mouseenter(Statics.charcurrentbelt[selectedchar]);
            }
            else if (Statics.currentequipmentbutton == 6)
            {
                mouseenter(Statics.charcurrentlegs[selectedchar]);
            }
            else if (Statics.currentequipmentbutton == 7)
            {
                mouseenter(Statics.charcurrentshoes[selectedchar]);
            }
            else if (Statics.currentequipmentbutton == 8)
            {
                mouseenter(Statics.charcurrentnecklace[selectedchar]);
            }
            else if (Statics.currentequipmentbutton == 9)
            {
                mouseenter(Statics.charcurrentring[selectedchar]);
            }
        }
        else
        {
            statsupdate();
        }
    }
    private void mouseenter(Itemcontroller slot)
    {
        ontriggerflatstats(slot, 0, Statics.charmaxhealth[selectedchar], Statics.healthperskillpoint);
        healbonus(slot, 0, Statics.charmaxhealth[selectedchar]);
        ontriggerflatstats(slot, 1, Statics.chardefense[selectedchar], Statics.defenseperskillpoint);
        defensebonus(slot, 1, Statics.chardefense[selectedchar]);
        ontriggerflatstats(slot, 2, Statics.charattack[selectedchar], Statics.attackperskillpoint);
        ontriggerpercentage(slot, 3, Statics.charcritchance[selectedchar] , Statics.critchanceperskillpoint);
        ontriggerpercentage(slot, 4, Statics.charcritdmg[selectedchar], Statics.critdmgperskillpoint);
        ontriggerpercentage(slot, 5, Statics.charweaponbuff[selectedchar], Statics.weaponswitchbuffperskillpoint);
        ontriggerduration(Statics.charweaponbuffduration[selectedchar]);
        ontriggerpercentage(slot, 6, Statics.charswitchbuff[selectedchar], Statics.charswitchbuffperskillpoint);
        ontriggerduration(Statics.charswitchbuffduration[selectedchar]);
        ontriggerbasiccritpercentage(Statics.charbasiccritbuff[selectedchar]);
        ontriggerpercentage(slot, 7, Statics.charbasicdmgbuff[selectedchar], Statics.basicdmgbuffperskillpoint);
    }
    private void ontriggerflatstats(Itemcontroller equipeditem, int itemstat, float currentstatvalue, float skillpointmultipler)
    {
        float difference = (itemvalues.itemlvl[itemvalues.upgradelvl].stats[itemstat] - equipeditem.itemlvl[equipeditem.upgradelvl].stats[itemstat]) * skillpointmultipler;
        if (itemvalues.itemlvl[itemvalues.upgradelvl].stats[itemstat] > equipeditem.itemlvl[equipeditem.upgradelvl].stats[itemstat])
        {
            statsnumbers.text += "<color=green>" + "( +" + difference + " ) " + (difference + currentstatvalue) + "</color>" + "\n";
        }
        else if (itemvalues.itemlvl[itemvalues.upgradelvl].stats[itemstat] < equipeditem.itemlvl[equipeditem.upgradelvl].stats[itemstat])
        {
            statsnumbers.text += "<color=red>" + "( " + difference + " ) " + (difference + currentstatvalue) + "</color>" + "\n";
        }
        else
        {
            statsnumbers.text += currentstatvalue + "\n";
        }
    }
    private void ontriggerpercentage(Itemcontroller equipeditem, int itemstat, float currentstatvalue, float skillpointmultipler)
    {
        float difference = (itemvalues.itemlvl[itemvalues.upgradelvl].stats[itemstat] - equipeditem.itemlvl[equipeditem.upgradelvl].stats[itemstat]) * skillpointmultipler;
        if (itemvalues.itemlvl[itemvalues.upgradelvl].stats[itemstat] > equipeditem.itemlvl[equipeditem.upgradelvl].stats[itemstat])
        {
            statsnumbers.text += "<color=green>" + "( +" + string.Format("{0:0.0}", difference) + " ) " + string.Format("{0:0.0}", difference + currentstatvalue) + "%" + "</color>" + "\n";
        }
        else if (itemvalues.itemlvl[itemvalues.upgradelvl].stats[itemstat] < equipeditem.itemlvl[equipeditem.upgradelvl].stats[itemstat])
        {
            statsnumbers.text += "<color=red>" + "( " + string.Format("{0:0.0}", difference) + " ) " + string.Format("{0:0.0}", difference + currentstatvalue) + "%" + "</color>" + "\n";
        }
        else
        {
            statsnumbers.text += string.Format("{0:0.0}", currentstatvalue) + "%" + "\n";
        }
    }
    private void ontriggerbasiccritpercentage(float currentstatvalue)
    {
        statsnumbers.text += string.Format("{0:0.0}", currentstatvalue) + "%" + "\n";
    }
    private void ontriggerduration(float currentstatvalue)
    {
        {
            statsnumbers.text += string.Format("{0:0.0}", currentstatvalue) + "sec" + "\n";
        }
    }
    private void healbonus(Itemcontroller equipeditem, int itemstat, float currentstatvalue)
    {
        float health;
        if (Statics.characterclassroll[selectedchar] == 1)
        {
            health = Mathf.Round(currentstatvalue - Statics.charcurrentlvl * Statics.guardbonushpeachlvl);
        }
        else health = currentstatvalue;
        float currentstats = Mathf.Round(health * Statics.healhealthbonuspercentage * 0.01f);
        float newstats = Mathf.Round((health + itemvalues.itemlvl[itemvalues.upgradelvl].stats[itemstat] - equipeditem.itemlvl[equipeditem.upgradelvl].stats[itemstat]) * Statics.healhealthbonuspercentage * 0.01f);
        float difference = newstats - currentstats;
        if (newstats > currentstats)
        {
            statsnumbers.text += "<color=green>" + "( +" + difference + " ) " + (difference + currentstats) + "</color>" + "\n";
        }
        else if (newstats < currentstats)
        {
            statsnumbers.text += "<color=red>" + "( " + difference + " ) " + (difference + currentstats) + "</color>" + "\n";
        }
        else
        {
            statsnumbers.text += Mathf.Round(health * Statics.healhealthbonuspercentage * 0.01f) + "\n";
        }
    }
    private void defensebonus(Itemcontroller equipeditem, int itemstat, float currentstatvalue)
    {
        float currentstats = Mathf.Round(currentstatvalue * Statics.defenseconvertedtoattack * 0.01f);
        float newstats = Mathf.Round((currentstatvalue + itemvalues.itemlvl[itemvalues.upgradelvl].stats[itemstat] - equipeditem.itemlvl[equipeditem.upgradelvl].stats[itemstat]) * Statics.defenseconvertedtoattack * 0.01f);
        float difference = newstats - currentstats;
        if (newstats > currentstats)
        {
            statsnumbers.text += "<color=green>" + "( +" + difference + " ) " + (difference + currentstats) + "</color>" + "\n";
        }
        else if (newstats < currentstats)
        {
            statsnumbers.text += "<color=red>" + "( " + difference + " ) " + (difference + currentstats) + "</color>" + "\n";
        }
        else
        {
            statsnumbers.text += Mathf.Round(Statics.chardefense[selectedchar] * Statics.defenseconvertedtoattack * 0.01f) + "\n";
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        gridvalues.upgradeui.SetActive(false);
        selectedchar = Statics.currentequipmentchar;
        statsupdate();
    }
}
