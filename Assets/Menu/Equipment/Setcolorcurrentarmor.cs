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
                if (Statics.currentequipmentbutton == 3 && Statics.charcurrenthead != null)
                {
                    if (obj.GetComponent<Chooseitem>().itemvalues == Statics.charcurrenthead[Statics.currentequipmentchar])
                    {
                        obj.GetComponent<Image>().color = Color.green;
                    }
                    else obj.GetComponent<Image>().color = Color.white;
                }
                else if (Statics.currentequipmentbutton == 4 && Statics.charcurrentchest != null)
                {
                    if (obj.GetComponent<Chooseitem>().itemvalues == Statics.charcurrentchest[Statics.currentequipmentchar])
                    {
                        obj.GetComponent<Image>().color = Color.green;
                    }
                    else obj.GetComponent<Image>().color = Color.white;
                }
                else if (Statics.currentequipmentbutton == 5 && Statics.charcurrentgloves != null)
                {
                    if (obj.GetComponent<Chooseitem>().itemvalues == Statics.charcurrentgloves[Statics.currentequipmentchar])
                    {
                        obj.GetComponent<Image>().color = Color.green;
                    }
                    else obj.GetComponent<Image>().color = Color.white;
                }
                else if (Statics.currentequipmentbutton == 6 && Statics.charcurrentlegs != null)
                {
                    if (obj.GetComponent<Chooseitem>().itemvalues == Statics.charcurrentlegs[Statics.currentequipmentchar])
                    {
                        obj.GetComponent<Image>().color = Color.green;
                    }
                    else obj.GetComponent<Image>().color = Color.white;
                }
                else if (Statics.currentequipmentbutton == 7 && Statics.charcurrentshoes != null)
                {
                    if (obj.GetComponent<Chooseitem>().itemvalues == Statics.charcurrentshoes[Statics.currentequipmentchar])
                    {
                        obj.GetComponent<Image>().color = Color.green;
                    }
                    else obj.GetComponent<Image>().color = Color.white;
                }
                else if (Statics.currentequipmentbutton == 8 && Statics.charcurrentneckless != null)
                {
                    if (obj.GetComponent<Chooseitem>().itemvalues == Statics.charcurrentneckless[Statics.currentequipmentchar])
                    {
                        obj.GetComponent<Image>().color = Color.green;
                    }
                    else obj.GetComponent<Image>().color = Color.white;
                }
                else if (Statics.currentequipmentbutton == 9 && Statics.charcurrentring != null)
                {
                    if (obj.GetComponent<Chooseitem>().itemvalues == Statics.charcurrentring[Statics.currentequipmentchar])
                    {
                        obj.GetComponent<Image>().color = Color.green;
                    }
                    else obj.GetComponent<Image>().color = Color.white;
                }
                else obj.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
