using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insertitem : MonoBehaviour, Interactioninterface
{
    [SerializeField] private Areacontroller areacontroller;
    public int areapuzzlenumber;

    [SerializeField] private GameObject activateobject;

    [SerializeField] private string interactiontext;
    [SerializeField] private Inventorycontroller inventory;
    [SerializeField] private Itemcontroller neededitem;
    [SerializeField] private int neededitemamount;

    public string Interactiontext => textupdate();
    public string textupdate()
    {
        string text;
        if(neededitem.inventoryslot == 0)
        {
            text = interactiontext + " " + 0 + "/" + neededitemamount + " " + neededitem.name;
        }
        else
        {
            if(inventory.Container.Items[neededitem.inventoryslot].amount >= neededitemamount)
            {
                text = interactiontext + " " + "<color=green>" + inventory.Container.Items[neededitem.inventoryslot].amount + "</color>"
                   + "/" + neededitemamount + " " + neededitem.name;
            }
            else
            {
                text = interactiontext + " " + "<color=red>" + inventory.Container.Items[neededitem.inventoryslot].amount + "</color>"
                              + "/" + neededitemamount + " " + neededitem.name;
            }           
        }
        return text;
    }
    public bool Interact(Closestinteraction interactor)
    {
        if (areacontroller.enemychestisopen[areapuzzlenumber] == false)
        {
            if (inventory.Container.Items[neededitem.inventoryslot].amount >= neededitemamount)
            {
                inventory.Container.Items[neededitem.inventoryslot].amount -= neededitemamount;
                activateobject.SetActive(true);
                areacontroller.enemychestcanopen[areapuzzlenumber] = true;
                areacontroller.autosave();
            }
        }
        return true;
    }
}
