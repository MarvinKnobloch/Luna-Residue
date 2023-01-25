using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor/Chest")]
public class Chestobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Chest;
    }
}
