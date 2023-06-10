using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setitemsandinventory : MonoBehaviour
{
    [SerializeField] public Inventorycontroller[] inventorys;
    [SerializeField] private Craftingobject[] craftingitems;
    [SerializeField] private Itemcontroller gold;
    [SerializeField] private Swordobject[] sworditems;
    [SerializeField] private Bowobject[] bowitems;
    [SerializeField] private Fistobject[] fistitems;
    [SerializeField] private Headobject[] headitems;
    [SerializeField] private Chestobject[] chestitems;
    [SerializeField] private Beltobject[] beltitems;
    [SerializeField] private Legsobject[] legitems;
    [SerializeField] private Shoesobject[] shoesitems;
    [SerializeField] private Necklaceobject[] necklessitems;
    [SerializeField] private Ringobject[] ringitems;

    [SerializeField] private Itemcontroller startingsword;
    [SerializeField] private Itemcontroller startingbow;
    [SerializeField] private Itemcontroller startingfist;
    [SerializeField] private Itemcontroller startinghead;
    [SerializeField] private Itemcontroller startingchest;
    [SerializeField] private Itemcontroller startingbelt;
    [SerializeField] private Itemcontroller startinglegs;
    [SerializeField] private Itemcontroller startingshoes;
    [SerializeField] private Itemcontroller startingneckless;
    [SerializeField] private Itemcontroller startingring;

    private void Awake()
    {
/*#if UNITY_EDITOR
        resetitems();
        resetinventorys();
#endif*/
    }

    public void resetplayerstats()
    {
        //Statics.charbasichealth = new float[] { 80, 73, 77, 73, 75 };
        //Statics.charcurrenthealth = new float[] { 80, 73, 77, 73, 75 };
        Statics.charmaxhealth = new float[] { 80, 73, 77, 73, 75 };
        Statics.chardefense = new float[] { 60, 60, 60, 60, 60 };
        Statics.charattack = new float[] { 0, 0, 0, 0, 0 };
        Statics.charcritchance = new float[] { 5, 5, 5, 5, 5 };
        Statics.charcritdmg = new float[] { 150, 150, 150, 150, 150 };
        Statics.charweaponbuff = new float[] { 0, 0, 0, 0, 0 };
        Statics.charweaponbuffduration = new float[] { 8, 8, 8, 8, 8 };
        Statics.charswitchbuff = new float[] { 0, 0, 0, 0, 0 };
        Statics.charswitchbuffduration = new float[] { 8, 8, 8, 8, 8 };
        Statics.charbasiccritbuff = new float[] { 1, 1, 1, 1, 1 };
        Statics.charbasicdmgbuff = new float[] { 0, 0, 0, 0, 0 };
    }
    public void resetitems()
    {
        resetitemvalues(sworditems);
        resetitemvalues(bowitems);
        resetitemvalues(fistitems);
        resetitemvalues(headitems);
        resetitemvalues(chestitems);
        resetitemvalues(beltitems);
        resetitemvalues(legitems);
        resetitemvalues(shoesitems);
        resetitemvalues(necklessitems);
        resetitemvalues(ringitems);
        resetitemvalues(craftingitems);
    }

    private void resetitemvalues(Itemcontroller[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].inventoryslot = 0;
            items[i].upgradelvl = 0;
            //items[i].stats = items[i].basestats;                synrchonisiert die beiden arrays???????(wenn ich stats umändere, werden die basestats auch geändert)
        }
    }
    public void resetinventorys()
    {
        for (int i = 0; i < inventorys.Length; i++)
        {
            inventorys[i].matsinventory = inventorys[0];
            for (int t = 0; t < inventorys[i].Container.Items.Length; t++)
            {
                inventorys[i].Container.Items[t].item = null;
                inventorys[i].Container.Items[t].itemid = 0;
                inventorys[i].Container.Items[t].amount = 0;
                inventorys[i].Container.Items[t].inventoryposi = 0;
                inventorys[i].Container.Items[t].itemname = string.Empty;
                inventorys[i].Container.Items[t].itemlvl = 0;
            }
        }
    }
    public void updateitemsininventory()
    {
        for (int i = 0; i < inventorys.Length; i++)
        {
            inventorys[i].matsinventory = inventorys[0];
        }

        updateitems(craftingitems, inventorys[0]);
        updateitems(sworditems, inventorys[1]);
        updateitems(bowitems, inventorys[2]);
        updateitems(fistitems, inventorys[3]);
        updateitems(headitems, inventorys[4]);
        updateitems(chestitems, inventorys[5]);
        updateitems(beltitems, inventorys[6]);
        updateitems(legitems, inventorys[7]);
        updateitems(shoesitems, inventorys[8]);
        updateitems(necklessitems, inventorys[9]);
        updateitems(ringitems, inventorys[10]);
    }
    private void updateitems(Itemcontroller[] allitems, Inventorycontroller inventory)
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if(inventory.Container.Items[i].itemid != 0)
            {
                bool couldfinditem = false;
                for (int t = 0; t < allitems.Length; t++)
                {
                    if (allitems[t].itemid == inventory.Container.Items[i].itemid)
                    {
                        inventory.Container.Items[i].item = allitems[t];
                        Itemcontroller item = inventory.Container.Items[i].item;
                        item.inventoryslot = inventory.Container.Items[i].inventoryposi;
                        item.upgradelvl = inventory.Container.Items[i].itemlvl;
                        couldfinditem = true;
                        break;
                    }
                }
                if(couldfinditem == false)
                {
                    Debug.Log("itemgotremovedfromgame");
                    inventory.Container.Items[i].item = null;
                    inventory.Container.Items[i].itemid = 0;
                    inventory.Container.Items[i].itemname = string.Empty;
                    inventory.Container.Items[i].inventoryposi = 0;
                    inventory.Container.Items[i].amount = 0;
                    inventory.Container.Items[i].itemlvl = 0;
                }
            }
        }
    }
    public void setequipeditemsafterload()
    {
        setequipditems(Statics.swordid, inventorys[1], Statics.charcurrentsword);
        setequipditems(Statics.bowid, inventorys[2], Statics.charcurrentbow);
        setequipditems(Statics.fistid, inventorys[3], Statics.charcurrentfist);
        setequipditems(Statics.headid, inventorys[4], Statics.charcurrenthead);
        setequipditems(Statics.chestid, inventorys[5], Statics.charcurrentchest);
        setequipditems(Statics.beltid, inventorys[6], Statics.charcurrentbelt);
        setequipditems(Statics.legsid, inventorys[7], Statics.charcurrentlegs);
        setequipditems(Statics.shoesid, inventorys[8], Statics.charcurrentshoes);
        setequipditems(Statics.necklaceid, inventorys[9], Statics.charcurrentnecklace);
        setequipditems(Statics.ringid, inventorys[10], Statics.charcurrentring);
    }
    private void setequipditems(int[] staticid, Inventorycontroller inventory, Itemcontroller[] staticslot)
    {
        for (int i = 0; i < staticid.Length; i++)
        {
            for (int t = 0; t < inventory.Container.Items.Length; t++)
            {
                if (inventory.Container.Items[t].itemid == staticid[i])
                {
                    staticslot[i] = inventory.Container.Items[t].item;
                    break;
                }
            }
        }
    }
    public void setallitemstats()
    {
        setweaoponstats(Statics.charcurrentsword, Statics.charcurrentbow, Statics.charcurrentfist);
        setarmorstats(Statics.charcurrenthead);
        setarmorstats(Statics.charcurrentchest);
        setarmorstats(Statics.charcurrentbelt);
        setarmorstats(Statics.charcurrentlegs);
        setarmorstats(Statics.charcurrentshoes);
        setarmorstats(Statics.charcurrentring);
        setarmorstats(Statics.charcurrentnecklace);
    }
    public void setweaoponstats(Itemcontroller[] swords, Itemcontroller[] bows, Itemcontroller[] fists)
    {
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            Statics.charswordattack[i] = swords[i].itemlvl[swords[i].upgradelvl].stats[2];
            Statics.charbowattack[i] = bows[i].itemlvl[bows[i].upgradelvl].stats[2];
            Statics.charfistattack[i] = fists[i].itemlvl[fists[i].upgradelvl].stats[2];
        }
    }
    public void setarmorstats(Itemcontroller[] staticitem)
    {
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            setitemandvalues(staticitem[i], i);
        }
    }
    private void setitemandvalues(Itemcontroller item, int character)
    {
        if (item != null)
        {
            Statics.charmaxhealth[character] += item.itemlvl[item.upgradelvl].stats[0] * Statics.healthperskillpoint;
            Statics.chardefense[character] += item.itemlvl[item.upgradelvl].stats[1] * Statics.defenseperskillpoint;
            Statics.charattack[character] += item.itemlvl[item.upgradelvl].stats[2];
            Statics.charcritchance[character] += item.itemlvl[item.upgradelvl].stats[3] * Statics.critchanceperskillpoint;
            Statics.charcritdmg[character] += item.itemlvl[item.upgradelvl].stats[4] * Statics.critdmgperskillpoint;
            Statics.charweaponbuff[character] += item.itemlvl[item.upgradelvl].stats[5] * Statics.weaponswitchbuffperskillpoint;
            Statics.charswitchbuff[character] += item.itemlvl[item.upgradelvl].stats[6] * Statics.charswitchbuffperskillpoint;
            Statics.charbasicdmgbuff[character] += item.itemlvl[item.upgradelvl].stats[7] * Statics.basicdmgbuffperskillpoint;
        }
    }
    public void addskillpoints()
    {
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            Statics.charmaxhealth[i] += Statics.charhealthskillpoints[i] * Statics.healthperskillpoint;
            Statics.chardefense[i] += Statics.chardefenseskillpoints[i] * Statics.defenseperskillpoint;
            //Statics.charattack[i] += Statics.charattackskillpoints[i] * Statics.attackperskillpoint;
            Statics.charattack[i] += Statics.charcurrentlvl;
            Statics.charcritchance[i] += Statics.charcritchanceskillpoints[i] * Statics.critchanceperskillpoint;
            Statics.charcritdmg[i] += Statics.charcritdmgskillpoints[i] * Statics.critdmgperskillpoint;
            Statics.charweaponbuffduration[i] += Statics.charweaponskillpoints[i] * Statics.weaonswitchbuffdurationperskillpoint;
            Statics.charweaponbuff[i] += Statics.charweaponskillpoints[i] * Statics.weaponswitchbuffperskillpoint;
            Statics.charswitchbuffduration[i] += Statics.charcharswitchskillpoints[i] * Statics.charswitchbuffdurationperskillpoint;
            Statics.charswitchbuff[i] += Statics.charcharswitchskillpoints[i] * Statics.charswitchbuffperskillpoint;
            Statics.charbasiccritbuff[i] += Statics.charbasicskillpoints[i] * Statics.basiccritbuffperskillpoint;
            Statics.charbasicdmgbuff[i] += Statics.charbasicskillpoints[i] * Statics.basicdmgbuffperskillpoint;
        }
    }
    public void addguardhp()
    {
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            if(Statics.characterclassroll[i] == 1)
            {
                Statics.charmaxhealth[i] += Statics.charcurrentlvl * Statics.guardbonushpeachlvl;
            }
        }
    }
    public void setstartitems()
    {
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            Statics.charcurrentsword[i] = startingsword;
            Statics.charcurrentbow[i] = startingbow;
            Statics.charcurrentfist[i] = startingfist;
            Statics.charcurrenthead[i] = startinghead;
            Statics.charcurrentchest[i] = startingchest;
            Statics.charcurrentbelt[i] = startingbelt;
            Statics.charcurrentlegs[i] = startinglegs;
            Statics.charcurrentshoes[i] = startingshoes;
            Statics.charcurrentnecklace[i] = startingneckless;
            Statics.charcurrentring[i] = startingring;
        }
        setallitemstats();
        inventorys[0].Additem(gold, 1);
        inventorys[1].addstartequiptment(startingsword);
        inventorys[2].addstartequiptment(startingbow);
        inventorys[3].addstartequiptment(startingfist);
        inventorys[4].addstartequiptment(startinghead);
        inventorys[5].addstartequiptment(startingchest);
        inventorys[6].addstartequiptment(startingbelt);
        inventorys[7].addstartequiptment(startinglegs);
        inventorys[8].addstartequiptment(startingshoes);
        inventorys[9].addstartequiptment(startingneckless);
        inventorys[10].addstartequiptment(startingring);
    }
}

/*private void updateitems()
{
    for (int i = 0; i < inventorys.Length; i++)
    {
        for (int t = 0; t < inventorys[i].Container.Items.Length; t++)
        {
            if (inventorys[i].Container.Items[t].inventoryposi != 0)
            {
                Itemcontroller item = inventorys[i].Container.Items[t].item;
                item.inventoryslot = inventorys[i].Container.Items[t].inventoryposi;
                item.upgradelvl = inventorys[i].Container.Items[t].itemlvl;
                if (item.upgradelvl != 0)
                {
                    item.stats = item.upgrades[item.upgradelvl - 1].newstats;
                }
            }
            else
            {
                continue;
            }
        }
    }
}*/