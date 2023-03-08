using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insertitem : MonoBehaviour, Interactioninterface
{
    [SerializeField] private GameObject activateobject;

    [SerializeField] private string interactiontext;
    [SerializeField] private Inventorycontroller inventory;
    [SerializeField] private Itemcontroller neededitem;
    [SerializeField] private int neededitemamount;

    public string Interactiontext => text();
    public string text()
    {
        if(neededitem.inventoryslot == 0)
        {
            string text = interactiontext + " " + 0 + "/" + neededitemamount + " " + neededitem.name;
            return text;
        }
        else
        {
            string text = interactiontext + " " + inventory.Container.Items[neededitem.inventoryslot].amount
                   + "/" + neededitemamount + " " + neededitem.name;
            return text;
        }
    }
    public bool Interact(Closestinteraction interactor)
    {
        if(inventory.Container.Items[neededitem.inventoryslot].amount >= neededitemamount)
        {
            activateobject.SetActive(true);
        }
        return true;
    }
}
