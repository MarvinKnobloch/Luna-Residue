using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Npcshopselectitem : MonoBehaviour, ISelectHandler
{
    public TextMeshProUGUI newstats;
    public Npcshopcontroller npcshopstatscontroller;
    public Itemcontroller merchantitem;
    public bool canbuy;
    public Image buyitembar;
    private string[] statstext = { "Health ", "Defense ", "Attack ", "Crit ", "Critchance ", "Weaponswitch ", "Charswitch ", "Basic " };
    public void OnSelect(BaseEventData eventData)
    {
        npcshopstatscontroller.currentselecteditem = gameObject;
        statsupdate();
    }

    public void statsupdate()
    {
        newstats.text = string.Empty;
        for (int i = 0; i < statstext.Length; i++)
        {
            if(merchantitem.basestats[i] > 0)
            {
                newstats.text += statstext[i] + "<pos=75%>" + "<color=green>" + merchantitem.basestats[i].ToString() + "</color>" + "\n";
            }
            else if (merchantitem.basestats[i] < 0)
            {
                newstats.text += statstext[i] + "<pos=75%>" + "<color=red>" + merchantitem.basestats[i].ToString() + "</color>" + "\n";
            }
            else
            {
                newstats.text += statstext[i] + "<pos=75%>" + merchantitem.basestats[i].ToString() + "\n";
            }
        }
    }
}
