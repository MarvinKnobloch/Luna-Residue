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

    private Armorgridvalues armorgridvalues;


    [NonSerialized] public Text upgradelvltext;
    [NonSerialized] public int selectedchar = 0;

    private void Awake()
    {
        armorgridvalues = GetComponentInParent<Armorgridvalues>();
        slotbuttontext = gameObject.GetComponentInParent<Armorgridvalues>().slottext;
        slotbutton = gameObject.GetComponentInParent<Armorgridvalues>().slotbutton;
        statsnumbers = gameObject.GetComponentInParent<Armorgridvalues>().statsnumbers;
    }
    public void setitem()
    {
        if (itemvalues != null)
        {
            selectedchar = Statics.currentequipmentchar;
            slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;
            setnewitem(Statics.currentequipmentbutton);
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
            setitemandvalues(Statics.charcurrenthead[selectedchar]);
            Statics.charcurrenthead[selectedchar] = itemvalues;
        }
        else if (equipslot == 4)         //chest = number4
        {
            setitemandvalues(Statics.charcurrentchest[selectedchar]);
            Statics.charcurrentchest[selectedchar] = itemvalues;
        }
        else if (equipslot == 5)        //gloves = number5
        {
            setitemandvalues(Statics.charcurrentgloves[selectedchar]);
            Statics.charcurrentgloves[selectedchar] = itemvalues;
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
            setitemandvalues(Statics.charcurrentneckless[selectedchar]);
            Statics.charcurrentneckless[selectedchar] = itemvalues;
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
            Statics.charmaxhealth[selectedchar] += itemvalues.stats[0] - staticsslot.stats[0];
            Statics.chardefense[selectedchar] += itemvalues.stats[1] - staticsslot.stats[1];
            Statics.charattack[selectedchar] += itemvalues.stats[2] - staticsslot.stats[2];
            Statics.charcritchance[selectedchar] += itemvalues.stats[3] - staticsslot.stats[3];
            Statics.charcritdmg[selectedchar] += itemvalues.stats[4] - staticsslot.stats[4];
            Statics.charweaponbuff[selectedchar] += itemvalues.stats[5] - staticsslot.stats[5];
            Statics.charswitchbuff[selectedchar] += itemvalues.stats[6] - staticsslot.stats[6];
            Statics.charbasicdmgbuff[selectedchar] += itemvalues.stats[7] - staticsslot.stats[7];
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
            setstaticsslot(Statics.charcurrentgloves);
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
            setstaticsslot(Statics.charcurrentneckless);
        }
        else if (Statics.currentequipmentbutton == 8)
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
    public void upgradeequipeditems()
    {
        transform.GetChild(2).GetComponentInChildren<Text>().text = itemvalues.upgradelvl.ToString();
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
            setstaticslotadd(Statics.charcurrentgloves);
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
            setstaticslotadd(Statics.charcurrentneckless);
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
    private void subtractstatsbeforeupgrade(int currentchar)
    {
        Statics.charmaxhealth[currentchar] -= itemvalues.stats[0];
        Statics.chardefense[currentchar] -= itemvalues.stats[1];
        Statics.charattack[currentchar] -= itemvalues.stats[2];
        Statics.charcritchance[currentchar] -= itemvalues.stats[3];
        Statics.charcritdmg[currentchar] -= itemvalues.stats[4];
        Statics.charweaponbuff[currentchar] -= itemvalues.stats[5];
        Statics.charswitchbuff[currentchar] -= itemvalues.stats[6];
        Statics.charbasicdmgbuff[currentchar] -= itemvalues.stats[7];
    }
    private void addstatsafterupgrade(int currentchar)
    {
        Statics.charmaxhealth[currentchar] += itemvalues.stats[0];
        Statics.chardefense[currentchar] += itemvalues.stats[1];
        Statics.charattack[currentchar] += itemvalues.stats[2];
        Statics.charcritchance[currentchar] += itemvalues.stats[3];
        Statics.charcritdmg[currentchar] += itemvalues.stats[4];
        Statics.charweaponbuff[currentchar] += itemvalues.stats[5];
        Statics.charswitchbuff[currentchar] += itemvalues.stats[6];
        Statics.charbasicdmgbuff[currentchar] += itemvalues.stats[7];
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (itemvalues != null)
        {
            selectedchar = Statics.currentequipmentchar;

            armorgridvalues.upgradeuitextcontroller.item = itemvalues;
            armorgridvalues.upgradeuitextcontroller.chooseitem = this;
            armorgridvalues.upgradeui.SetActive(true);

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
                mouseenter(Statics.charcurrentgloves[selectedchar]);
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
                mouseenter(Statics.charcurrentneckless[selectedchar]);
            }
            else if (Statics.currentequipmentbutton == 9)
            {
                mouseenter(Statics.charcurrentring[selectedchar]);
            }
        }
    }
    private void mouseenter(Itemcontroller slot)
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
            statsnumbers.text += "<color=red>" + "( " + difference + " ) " + (difference + currentstatvalue) + "</color>" + "\n";
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
            statsnumbers.text += "<color=red>" + "( " + difference + " ) " + (difference + currentstatvalue) + "%" + "</color>" + "\n";
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
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        armorgridvalues.upgradeui.SetActive(false);
        statsupdate();
    }
}
