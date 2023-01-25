using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapons/Bow")]
public class Bowobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Bow;
    }
}
