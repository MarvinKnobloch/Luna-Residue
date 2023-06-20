using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Setcolorcurrentweapon : MonoBehaviour, ISelectHandler
{
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject resetonpointerenterlayer;
    [SerializeField] private GameObject upgradeui;

    [SerializeField] private Color greencolor;
    public void OnSelect(BaseEventData eventData)
    {
        triggerbutton();
    }
    public void triggerbutton()
    {
        upgradeui.SetActive(false);
        resetonpointerenterlayer.SetActive(true);
        foreach (Transform obj in grid.transform)
        {
            if (obj.GetComponent<Chooseweapon>().itemvalues == null)
            {
                obj.GetComponent<Image>().color = Color.white;
            }
            else
            {
                if (Statics.currentequipmentbutton == 0)
                {
                    setcolor(Statics.charcurrentsword[Statics.currentequipmentchar], obj);
                }
                else if (Statics.currentequipmentbutton == 1)
                {
                    setcolor(Statics.charcurrentbow[Statics.currentequipmentchar], obj);
                }
                else if (Statics.currentequipmentbutton == 2)
                {
                    setcolor(Statics.charcurrentfist[Statics.currentequipmentchar], obj);
                }
            }
        }
    }
    private void setcolor(Itemcontroller itemslot, Transform obj)
    {
        if (itemslot != null)
        {
            if (obj.GetComponent<Chooseweapon>().itemvalues == itemslot)
            {
                obj.GetComponent<Image>().color = greencolor;
            }
            else obj.GetComponent<Image>().color = Color.white;
        }
        else obj.GetComponent<Image>().color = Color.white;
    }
}
