using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class Weaponobject : Itemcontroller
{   
    public void Awake()
    {
        type = Itemtype.Weapon;
    }
}
