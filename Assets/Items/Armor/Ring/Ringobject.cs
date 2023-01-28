using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor/Ring")]
public class Ringobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Ring;
    }
}
