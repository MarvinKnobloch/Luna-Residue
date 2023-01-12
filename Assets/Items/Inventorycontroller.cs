using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class Inventorycontroller : ScriptableObject
{
    public Inventory Container;
    private string loottext;
    //public List<Inventoryslot> Container = new List<Inventoryslot>();
    public void Additem(GameObject player, Itemcontroller _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].Addamount(_amount);
                Container.Items[i].inventoryposi = i + 1;                             // ist hier unnötig aber ist erstmal zur absicherung drin, muss theoretisch nur bei setfirstemptyslot gecalled werden
                _item.inventoryslot = i + 1;
                return;
            }
        }
        setfirstemptyslot(_item, _amount);
        player.GetComponent<Displayloot>().displayloot(_item.name.ToString());
    }

    public void Addequipment(GameObject player, Itemcontroller _item, Itemcontroller _seconditem, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                player.GetComponent<Movescript>().matsinventory.convertedAdditem(_seconditem, _amount);
                loottext = _item.name + " convert to 1x " + _seconditem.name;
                player.GetComponent<Displayloot>().displayloot(loottext);
                return;
            }
        }
        setfirstemptyslot(_item, _amount);
        player.GetComponent<Displayloot>().displayloot(_item.name.ToString());
    }
    private void convertedAdditem(Itemcontroller _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].Addamount(_amount);
                Container.Items[i].inventoryposi = i + 1;                             // ist hier unnötig aber ist erstmal zur absicherung drin, muss theoretisch nur bei setfirstemptyslot gecalled werden
                _item.inventoryslot = i + 1;
                return;
            }
        }
        setfirstemptyslot(_item, _amount);
    }


    public Inventoryslot setfirstemptyslot(Itemcontroller _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].amount == 0)
            {
                Container.Items[i].slotupdate(_item, _amount);
                Container.Items[i].inventoryposi = i + 1;
                _item.inventoryslot = i + 1;
                return Container.Items[i];
            }
        }
        return null;
        
    }
}
[System.Serializable]
public class Inventory
{
    //public List<Inventoryslot> Items = new List<Inventoryslot>();
    public Inventoryslot[] Items = new Inventoryslot[28];
}
[System.Serializable]
public class Inventoryslot
{
    public Itemcontroller item;
    public int amount;
    public string itemname;
    public int inventoryposi;
    public Inventoryslot(Itemcontroller _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void slotupdate(Itemcontroller _item, int _amount)
    {
        item = _item;
        amount = _amount;
        itemname = item.name.ToString();       
    }

    public void Addamount(int value)
    {
        amount += value;
    }
}


