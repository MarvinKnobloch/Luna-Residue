using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor/Legs")]
public class Legsobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Legs;
    }
}
