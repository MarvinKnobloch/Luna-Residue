using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapons/Sword")]
public class Swordobject : Itemcontroller
{   
    public void Awake()
    {
        type = Itemtype.Sword;
    }
}
