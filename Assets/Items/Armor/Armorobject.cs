using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor")]
public class Armorobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Armor;
    }
}
