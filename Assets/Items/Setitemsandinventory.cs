using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setitemsandinventory : MonoBehaviour
{
    [SerializeField] public Inventorycontroller[] inventorys;
    [SerializeField] private Craftingobject[] craftingitems;
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
            for (int t = 0; t < items[i].stats.Length; t++)
            {
                items[i].stats[t] = items[i].basestats[t];
            }
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

        //nicht schön, aber erfüllt erstmal den zweck
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
                        if (item.upgradelvl != 0)
                        {
                            item.stats = item.upgrades[item.upgradelvl - 1].newstats;
                        }
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
            if (staticid[i] == inventory.Container.Items[i].itemid)
            {
                staticslot[i] = inventory.Container.Items[i].item;
            }
        }
    }
    public void setstartitems()
    {
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            Statics.charswordattack[i] = startingsword.stats[2];
            Statics.charcurrentsword[i] = startingsword;
        }
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            Statics.charbowattack[i] = startingbow.stats[2];
            Statics.charcurrentbow[i] = startingbow;
        }
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            Statics.charfistattack[i] = startingfist.stats[2];
            Statics.charcurrentfist[i] = startingfist;
        }
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            setitemandvalues(startinghead, i);
            Statics.charcurrenthead[i] = startinghead;
        }
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            setitemandvalues(startingchest, i);
            Statics.charcurrentchest[i] = startingchest;
        }
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            setitemandvalues(startingbelt, i);
            Statics.charcurrentbelt[i] = startingbelt;
        }
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            setitemandvalues(startinglegs, i);
            Statics.charcurrentlegs[i] = startinglegs;
        }
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            setitemandvalues(startingshoes, i);
            Statics.charcurrentshoes[i] = startingshoes;
        }
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            setitemandvalues(startingneckless, i);
            Statics.charcurrentnecklace[i] = startingneckless;
        }
        for (int i = 0; i < Statics.characternames.Length; i++)
        {
            setitemandvalues(startingring, i);
            Statics.charcurrentring[i] = startingring;
        }
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
    private void setitemandvalues(Itemcontroller item, int character)
    {
        if(item != null)
        {
            Statics.charmaxhealth[character] += item.stats[0];
            Statics.chardefense[character] += item.stats[1];
            Statics.charattack[character] += item.stats[2];
            Statics.charcritchance[character] += item.stats[3];
            Statics.charcritdmg[character] += item.stats[4];
            Statics.charweaponbuff[character] += item.stats[5];
            Statics.charswitchbuff[character] += item.stats[6];
            Statics.charbasicdmgbuff[character] += item.stats[7];
        }
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