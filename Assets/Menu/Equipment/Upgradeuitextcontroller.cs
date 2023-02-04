using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Upgradeuitextcontroller : MonoBehaviour
{
    [SerializeField] private GameObject upgradeobj;
    private SpielerSteu Steuerung;

    public Chooseitem chooseitem;
    public Itemcontroller item;

    private int neededupgrademats = 0;
    private int canupgradeint = 0;
    private TextMeshProUGUI iteminfotext;
    private string[] statsname = { "Health", "Defense", "Attack", "Critchance", "Critdmg", "Wswitchbuff", "Cswitchbuff", "Basicbuff" };

    [SerializeField] private Inventorycontroller matsinventory;
    [SerializeField] private Upgradecontroller upgradecontroller;


    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
        iteminfotext = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        valuesupdate();
        if(chooseitem != null)
        {
            upgradecontroller.chooseitem = chooseitem;
        }
    }
    private void OnDisable()
    {
        upgradecontroller.itemtoupgrade = null;
        upgradecontroller.chooseitem = null;
        chooseitem = null;
        upgradecontroller.chooseweapon = null;
    }

    private void Update()
    {
        if (Steuerung.Player.Attack3.WasPressedThisFrame())                //stats zurücksetzt, damit ich es nicht von hand machen muss
        {
            item.upgradelvl = 0;
            int currentstat = 0;
            foreach (float stat in item.stats)
            {
                item.stats[currentstat] = item.basestats[currentstat];
                currentstat++;
            }
        }
    }
    public void valuesupdate()
    {
        itemheaderandcheckforupgrade();
        itemstats();
        upgradedetails();
    }
    private void itemheaderandcheckforupgrade()
    {
        iteminfotext.text = string.Empty;
        iteminfotext.text = item.itemname + " LvL " + item.upgradelvl + "\n";
        if (item.upgradelvl != item.maxupgradelvl)
        {
            iteminfotext.text += "(max LvL " + item.maxupgradelvl + ")" + "\n";
        }

        if (item.upgradelvl < item.maxupgradelvl)
        {
            upgradeobj.SetActive(true);
            upgradecontroller.itemtoupgrade = item;
        }
        else
        {
            upgradeobj.SetActive(false);
        }
    }
    private void itemstats()
    {
        int currentstat = 0;
        foreach (int stat in item.stats)
        {
            if (item.upgradelvl < item.maxupgradelvl)
            {
                if (stat != 0 || item.upgrades[item.upgradelvl].newstats[currentstat] != 0)               // || damit auch die values angezeigt werden, die von 0 auf +irgendwas gehen
                {
                    if (item.stats[currentstat] < item.upgrades[item.upgradelvl].newstats[currentstat])
                    {
                        iteminfotext.text += "\n" + item.stats[currentstat] + " -> " + "<color=green>" + item.upgrades[item.upgradelvl].newstats[currentstat] + "</color>" + " " + statsname[currentstat];
                    }
                    else if (item.stats[currentstat] > item.upgrades[item.upgradelvl].newstats[currentstat])
                    {
                        iteminfotext.text += "\n" + item.stats[currentstat] + " -> " + "<color=red>" + item.upgrades[item.upgradelvl].newstats[currentstat] + "</color>" + " " + statsname[currentstat];
                    }
                    else
                    {
                        iteminfotext.text += "\n" + item.stats[currentstat] + " -> " + item.upgrades[item.upgradelvl].newstats[currentstat] + " " + statsname[currentstat];
                    }
                }
            }
            else
            {
                if (stat != 0)
                {
                    iteminfotext.text += "\n" + item.stats[currentstat] + " " + statsname[currentstat];
                }
            }
            currentstat++;
        }
    }
    private void upgradedetails()
    {
        if (item.upgradelvl < item.maxupgradelvl)
        {
            neededupgrademats = 0;
            canupgradeint = 0;
            iteminfotext.text += "\n" + "\n" + "Upradecosts"; //+ "\n";

            int currentinventoryposi = 0;
            foreach (Itemcontroller.Upgradesmats material in item.upgrades[item.upgradelvl].Upgrademats)
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
