using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setitemsandinventory : MonoBehaviour
{
    [SerializeField] public Inventorycontroller[] inventorys;
    [SerializeField] private Swordobject[] sworditems;
    [SerializeField] private Bowobject[] bowitems;
    [SerializeField] private Fistobject[] fistitems;
    [SerializeField] private Headobject[] headitems;
    [SerializeField] private Chestobject[] chestitems;
    [SerializeField] private Glovesobject[] glovesitems;
    [SerializeField] private Legsobject[] legitems;
    [SerializeField] private Shoesobject[] shoesitems;
    [SerializeField] private Necklessobject[] necklessitems;
    [SerializeField] private Ringobject[] ringitems;
    [SerializeField] private Craftingobject[] craftingitems;


    public void resetitems()
    {
        resetitems(sworditems);
        resetitems(bowitems);
        resetitems(fistitems);
        resetitems(headitems);
        resetitems(chestitems);
        resetitems(glovesitems);
        resetitems(legitems);
        resetitems(shoesitems);
        resetitems(necklessitems);
        resetitems(ringitems);
    }

    private void resetitems(Itemcontroller[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].inventoryslot = 0;
            items[i].upgradelvl = 0;
            items[i].stats = items[i].basestats;
        }
    }
    public void updateitemsininventory()
    {
        updateitems();
    }
    public void resetinventorys()
    {
        for (int i = 0; i < inventorys.Length; i++)
        {
            for (int t = 0; t < inventorys[i].Container.Items.Length; t++)
            {
                inventorys[i].Container.Items[t].item = null;
                inventorys[i].Container.Items[t].amount = 0;
                inventorys[i].Container.Items[t].inventoryposi = 0;
                inventorys[i].Container.Items[t].itemname = "";
                inventorys[i].Container.Items[t].itemlvl = 0;
            }
        }
    }
    private void updateitems()
    {
        for (int i = 0; i < inventorys.Length; i++)
        {
            for (int t = 0; t < inventorys[i].Container.Items.Length; t++)
            {
                if (inventorys[i].Container.Items[t].amount != 0)
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
    }
}
