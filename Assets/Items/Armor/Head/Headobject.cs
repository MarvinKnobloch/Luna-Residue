using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor/Head")]
public class Headobject : Itemcontroller
{
    public void Awake()
    {
        type = Itemtype.Head;
    }
}
