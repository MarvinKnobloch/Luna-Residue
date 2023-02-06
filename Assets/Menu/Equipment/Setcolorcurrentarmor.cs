using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Setcolorcurrentarmor : MonoBehaviour, ISelectHandler
{
    [SerializeField] private GameObject grid;

    public void OnSelect(BaseEventData eventData)
    {
        triggerbutton();
    }
    public void triggerbutton()
    {
        foreach (Transform obj in grid.transform)
        {
            if (obj.GetComponent<Chooseitem>().itemvalues == null)
            {
                obj.GetComponent<Image>().color = Color.white;
            }
            else
            {
                if (Statics.currentequipmentbutton == 3)
                {
                    setcolor(Statics.charcurrenthead[Statics.currentequipmentchar], obj);
                }
                else if (Statics.currentequipmentbutton == 4)
                {
                    setcolor(Statics.charcurrentchest[Statics.currentequipmentchar], obj);
                }
                else if (Statics.currentequipmentbutton == 5)
                {
                    setcolor(Statics.charcurrentgloves[Statics.currentequipmentchar], obj);
                }
                else if (Statics.currentequipmentbutton == 6)
                {
                    setcolor(Statics.charcurrentlegs[Statics.currentequipmentchar], obj);
                }
                else if (Statics.currentequipmentbutton == 7)
                {
                    setcolor(Statics.charcurrentshoes[Statics.currentequipmentchar], obj);
                }
                else if (Statics.currentequipmentbutton == 8)
                {
                    setcolor(Statics.charcurrentneckless[Statics.currentequipmentchar], obj);
                }
                else if (Statics.currentequipmentbutton == 9)
                {
                    setcolor(Statics.charcurrentring[Statics.currentequipmentchar], obj);
                }
            }
        }
    }
    private void setcolor(Itemcontroller itemslot, Transform obj)
    {
        if (itemslot != null)
        {
            if (obj.GetComponent<Chooseitem>().itemvalues == itemslot)
            {
                obj.GetComponent<Image>().color = Color.green;
            }
            else obj.GetComponent<Image>().color = Color.white;
        }
        else obj.GetComponent<Image>().color = Color.white;
    }
}
