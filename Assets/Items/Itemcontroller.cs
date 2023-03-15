using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Itemtype
{
    Crafting,
    Sword,
    Bow,
    Fist,
    Head,
    Chest,
    Belt,
    Legs,
    Shoes,
    Necklace,
    Ring,
}
public abstract class Itemcontroller : ScriptableObject
{
    public Sprite Uisprite;
    public Itemtype type;
    public int itemid;
    public string itemname;
    public int inventoryslot;
    public int itemshopcosts;
    public Craftingobject seconditem;
    [TextArea(5, 20)]
    public string description;

    public float[] basestats;
    public float[] stats;

    public int upgradelvl;
    public int maxupgradelvl;
    public Upgrades[] upgrades;

    [System.Serializable]
    public class Upgrades
    {
        public float[] newstats;
        public Upgradesmats[] Upgrademats;
    }

    [System.Serializable]
    public class Upgradesmats
    {
        public Craftingobject upgrademat;
        public int costs;
    }

}
/*[System.Serializable]
public class Item
{
    public string Name;
    public Item(Itemcontroller item)
    {
        Name = item.name;
    }
}*/
