using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Setcolorcurrentweapon : MonoBehaviour, ISelectHandler
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
            if (obj.GetComponent<Chooseweapon>().itemvalues == null)
            {
                obj.GetComponent<Image>().color = Color.white;
            }
            else
            {
                if (Statics.currentequipmentbutton == 0 && Statics.charcurrentsword != null)
                {
                    if (obj.GetComponent<Chooseweapon>().itemvalues == Statics.charcurrentsword[Statics.currentequipmentchar])
                    {
                        obj.GetComponent<Image>().color = Color.green;
                    }
                    else obj.GetComponent<Image>().color = Color.white;
                }
                else if (Statics.currentequipmentbutton == 1 && Statics.charcurrentbow != null)
                {
                    if (obj.GetComponent<Chooseweapon>().itemvalues == Statics.charcurrentbow[Statics.currentequipmentchar])
                    {
                        obj.GetComponent<Image>().color = Color.green;
                    }
                    else obj.GetComponent<Image>().color = Color.white;
                }
                else if (Statics.currentequipmentbutton == 2 && Statics.charcurrentfist != null)
                {
                    if (obj.GetComponent<Chooseweapon>().itemvalues == Statics.charcurrentfist[Statics.currentequipmentchar])
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
