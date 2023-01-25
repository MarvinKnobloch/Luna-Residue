using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor/Shoes")]
public class Shoesobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Shoes;
    }
}
