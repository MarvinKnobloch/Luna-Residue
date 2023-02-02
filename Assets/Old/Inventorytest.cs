using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Inventorytest
{
    public int hallo { get; }
    //public Itemtest[] items { get; }
}

[System.Serializable]
public class Itemtest
{
    public Itemcontroller item;
    public string itemname;
    public int inventoryslot;
    public int amount;
    public int upgradelvl;
}
