using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updateitemsininventory : MonoBehaviour
{
    [SerializeField] private Inventorycontroller[] inventorys;
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
        inventorysupdate();
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
    /*private void chestitemreset()
    {
        for (int i = 0; i < chestitems.Length; i++)
        {
            chestitems[i].inventoryslot = 0;
            chestitems[i].upgradelvl = 0;
            chestitems[i].stats = chestitems[i].basestats;
        }
    }*/
    public void inventorysupdate()
    {
        for (int i = 0; i < inventorys.Length; i++)
        {
            for (int t = 0; t < inventorys[i].Container.Items.Length; t++)
            {
                if (inventorys[t].Container.Items[t].amount != 0)
                {
                    Itemcontroller item = inventorys[t].Container.Items[t].item;
                    item.inventoryslot = inventorys[t].Container.Items[t].inventoryposi;
                    item.upgradelvl = inventorys[t].Container.Items[t].itemlvl;
                    if (item.upgradelvl != 0)
                    {
                        item.stats = item.upgrades[item.upgradelvl].newstats;
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
