using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor/Gloves")]
public class Glovesobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Gloves;
    }
}
