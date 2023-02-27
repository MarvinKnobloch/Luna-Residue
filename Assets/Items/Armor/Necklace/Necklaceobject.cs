using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor/Necklace")]
public class Necklaceobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Necklace;
    }
}
