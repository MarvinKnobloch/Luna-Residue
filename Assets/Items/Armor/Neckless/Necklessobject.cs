using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor/Neckless")]
public class Necklessobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Neckless;
    }
}
