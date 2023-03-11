using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insertitem : MonoBehaviour, Interactioninterface
{
    [SerializeField] private Areacontroller areacontroller;
    public int areaquestnumber;

    [SerializeField] private GameObject activateobject;

    [SerializeField] private string interactiontext;
    [SerializeField] private Inventorycontroller inventory;
    [SerializeField] private Itemcontroller neededitem;
    [SerializeField] private int neededitemamount;

    private void Start()
    {
        areaquestnumber = GetComponent<Areanumber>().areanumber;
    }
    public string Interactiontext => textupdate();
    public string textupdate()
    {
        if (areacontroller.questcomplete[areaquestnumber] == false)
        {
            string text;
            if (neededitem.inventoryslot == 0)
            {
                Debug.Log("noitem");
                text = interactiontext + " " + "<color=red>" + "0" + "</color>" + "/" + neededitemamount + " " + neededitem.name;
            }
            else
            {
                if (inventory.Container.Items[neededitem.inventoryslot - 1].amount >= neededitemamount)
                {
                    text = interactiontext + " " + "<color=green>" + inventory.Container.Items[neededitem.inventoryslot - 1].amount + "</color>"
                       + "/" + neededitemamount + " " + neededitem.name;
                }
                else
                {
                    text = interactiontext + " " + "<color=red>" + inventory.Container.Items[neededitem.inventoryslot - 1].amount + "</color>"
                                  + "/" + neededitemamount + " " + neededitem.name;
                }
            }
            return text;
        }
        else return "";
    }
    public bool Interact(Closestinteraction interactor)
    {
        if (neededitem.inventoryslot == 0)
        {
            return true;
        }
        else
        {
            if (areacontroller.questcomplete[areaquestnumber] == false)
            {
                if (inventory.Container.Items[neededitem.inventoryslot -1 ].amount >= neededitemamount)
                {
                    inventory.Container.Items[neededitem.inventoryslot -1 ].amount -= neededitemamount;
                    activateobject.SetActive(true);
                    areacontroller.questcomplete[areaquestnumber] = true;
                    areacontroller.autosave();
                }
            }
            return true;
        }
    }
}
