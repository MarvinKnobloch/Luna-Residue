using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Itemtextcontroller : MonoBehaviour
{
    [SerializeField] private GameObject upgradeobj;
    private SpielerSteu Steuerung;

    public Itemcontroller itemvalues;
    private int neededupgrademats = 0;
    private int canupgradeint = 0;
    private TextMeshProUGUI iteminfotext;
    private string[] statsname = { "Health", "Defense", "Attack", "Critchance", "Critdmg", "Wswitchbuff", "Cswitchbuff", "Basicbuff" };

    [SerializeField] private Inventorycontroller matsinventory;
    [SerializeField] private Upgradecontroller upgradecontroller;

    public Chooseitem chooseitem;
    public Chooseweapon chooseweapon;
    [NonSerialized] public int equipslotnumber;
   

    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
        iteminfotext = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        textupdate();
        if(chooseitem != null)
        {
            upgradecontroller.chooseitem = chooseitem;
        }
        if(chooseweapon != null)
        {
            upgradecontroller.chooseweapon = chooseweapon;
        }
    }
    private void OnDisable()
    {
        upgradecontroller.itemtoupgrade = null;
        upgradecontroller.chooseitem = null;
        chooseitem = null;
        upgradecontroller.chooseweapon = null;
        chooseweapon = null;
    }

    private void Update()
    {
        if (Steuerung.Spielerboden.Attack3.WasPressedThisFrame())                //stats zurücksetzt, damit ich es nicht von hand machen muss
        {
            itemvalues.upgradelvl = 0;
            int currentstat = 0;
            foreach (float stat in itemvalues.stats)
            {
                itemvalues.stats[currentstat] = itemvalues.basestats[currentstat];
                currentstat++;
            }
            textupdate();
        }
    }
    public void textupdate()
    {
        if (itemvalues.upgradelvl < itemvalues.maxupgradelvl)
        {
            upgradeobj.SetActive(true);
            upgradecontroller.itemtoupgrade = itemvalues;
        }
        else
        {
            upgradeobj.SetActive(false);
        }
        iteminfotext.text = "";
        iteminfotext.text = itemvalues.itemname + " LvL " + itemvalues.upgradelvl + "\n";
        if(itemvalues.upgradelvl != itemvalues.maxupgradelvl)
        {
            iteminfotext.text += "(max LvL " + itemvalues.maxupgradelvl + ")" + "\n";
        }

        int currentstat = 0;
        foreach (int stat in itemvalues.stats)
        {
            if (itemvalues.upgradelvl < itemvalues.maxupgradelvl)
            {
                if (stat != 0 || itemvalues.upgrades[itemvalues.upgradelvl].newstats[currentstat] != 0)               // || damit auch die values angezeigt werden, die von 0 auf +irgendwas gehen
                {
                    if (itemvalues.stats[currentstat] < itemvalues.upgrades[itemvalues.upgradelvl].newstats[currentstat])
                    {
                        iteminfotext.text += "\n" + itemvalues.stats[currentstat] + " -> " + "<color=green>" + itemvalues.upgrades[itemvalues.upgradelvl].newstats[currentstat] + "</color>" + " " + statsname[currentstat];
                    }
                    else if (itemvalues.stats[currentstat] > itemvalues.upgrades[itemvalues.upgradelvl].newstats[currentstat])
                    {
                        iteminfotext.text += "\n" + itemvalues.stats[currentstat] + " -> " + "<color=red>" + itemvalues.upgrades[itemvalues.upgradelvl].newstats[currentstat] + "</color>" + " " + statsname[currentstat];
                    }
                    else
                    {
                        iteminfotext.text += "\n" + itemvalues.stats[currentstat] + " -> " + itemvalues.upgrades[itemvalues.upgradelvl].newstats[currentstat] + " " + statsname[currentstat];
                    }
                }
            }
            else
            {
                if (stat != 0)
                {
                    iteminfotext.text += "\n" + itemvalues.stats[currentstat] + " " + statsname[currentstat];
                }               
            }
            currentstat++;
        }
        if (itemvalues.upgradelvl < itemvalues.maxupgradelvl)
        {
            neededupgrademats = 0;
            canupgradeint = 0;
            iteminfotext.text += "\n" + "\n" + "Upradecosts"; //+ "\n";

            int currentinventoryposi = 0;
            foreach (Itemcontroller.Upgradesmats material in itemvalues.upgrades[itemvalues.upgradelvl].Upgrademats)
            {
                neededupgrademats++;
                int upgradematamount = 0;
                for (int i = 0; i < matsinventory.Container.Items.Length; i++)
                {
                    if (matsinventory.Container.Items[i].item == material.upgrademat)
                    {
                        upgradematamount = matsinventory.Container.Items[i].amount;
                        upgradecontroller.iteminventoryposi[currentinventoryposi] = i;                                    //matposition + kosten werden gespeichtert und weitergegeben damit man keinen weitern loop machen muss
                        upgradecontroller.upgrademinusvalue[currentinventoryposi] = material.costs;
                        currentinventoryposi++;
                        break;
                    }

                }
                if (upgradematamount >= material.costs)                  //text für die matkosten
                {
                    iteminfotext.text += "\n" + "<color=green>" + upgradematamount + "</color>" + "/" + material.costs + " " + material.upgrademat.itemname;
                    canupgradeint++;
                }
                else
                {
                    iteminfotext.text += "\n" + "<color=red>" + upgradematamount + "</color>" + "/" + material.costs + " " + material.upgrademat.itemname;
                }
            }
            if (neededupgrademats == canupgradeint)                  //checkt ob die mats vorhanden sind
            {
                upgradecontroller.canupgrade = true;
            }
            else
            {
                upgradecontroller.canupgrade = false;
            }
        }
    }
}
