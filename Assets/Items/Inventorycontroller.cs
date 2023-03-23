using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class Inventorycontroller : ScriptableObject
{
    public Inventorycontroller matsinventory;
    public Inventory Container;
    private string loottext;
    public void Additem(Itemcontroller item, int amount)
    {
        if (item.inventoryslot != 0)
        {
            matsinventory.Container.Items[item.inventoryslot - 1].amount += amount;
            loottext = amount + "x " + item.name;
            LoadCharmanager.Overallmainchar.GetComponent<Displayloot>().displayloot(loottext);
        }
        else
        {
            setfirstemptyslot(item, amount);
            LoadCharmanager.Overallmainchar.GetComponent<Displayloot>().displayloot(item.name.ToString());
        }
    }

    public void Addequipment(Itemcontroller item, Itemcontroller seconditem, int seconditemamount)
    {
        if (item.inventoryslot != 0)
        {
            if(seconditem.inventoryslot != 0)
            {
                matsinventory.Container.Items[item.inventoryslot - 1].amount += seconditemamount;
                loottext = item.name + " convert to " + seconditemamount + "x " + seconditem.name;
                LoadCharmanager.Overallmainchar.GetComponent<Displayloot>().displayloot(loottext);
            }
            else
            {
                loottext = item.name + " convert to " + seconditemamount + "x " + seconditem.name;
                LoadCharmanager.Overallmainchar.GetComponent<Displayloot>().displayloot(loottext);
                setfirstitemifconverted(seconditem, seconditemamount);
            }
        }
        else
        {
            setfirstemptyslot(item, 1);
            if(LoadCharmanager.Overallmainchar.TryGetComponent(out Displayloot displayloot))
            {
                displayloot.displayloot(item.name.ToString());
            }
        }
    }
    public void addstartequiptment(Itemcontroller item)
    {
        setfirstemptyslot(item, 1);
    }
    public Inventoryslot setfirstemptyslot(Itemcontroller item, int amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].amount == 0)
            {
                Container.Items[i].slotupdate(item, amount);
                Container.Items[i].inventoryposi = i + 1;
                item.inventoryslot = i + 1;
                return Container.Items[i];
            }
        }
        return null;       
    }
    private Inventoryslot setfirstitemifconverted(Itemcontroller item, int amount)
    {
        for (int i = 0; i < matsinventory.Container.Items.Length; i++)
        {
            if (matsinventory.Container.Items[i].amount == 0)
            {
                matsinventory.Container.Items[i].slotupdate(item, amount);
                matsinventory.Container.Items[i].inventoryposi = i + 1;
                item.inventoryslot = i + 1;
                return matsinventory.Container.Items[i];
            }
        }
        return null;
    }
}
[System.Serializable]
public class Inventory
{
    public string savepathname;
    public Inventoryslot[] Items = new Inventoryslot[28];
}
[System.Serializable]
public class Inventoryslot
{
    public Itemcontroller item;
    public int itemid;
    public int amount;
    public string itemname;               //itemname ist vielleicht unnötig, hab ich benutzt um die items beim laden zu suchen(macht jetzt die item id)
    public int inventoryposi;
    public int itemlvl;
    public Inventoryslot(Itemcontroller _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void slotupdate(Itemcontroller addeditem, int addedamount)
    {
        itemid = addeditem.itemid;
        item = addeditem;
        amount = addedamount;
        itemname = addeditem.name.ToString();       
    }

    public void Addamount(int value)
    {
        amount += value;
    }
}


