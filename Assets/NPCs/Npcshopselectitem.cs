using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Npcshopselectitem : MonoBehaviour
{
    public TextMeshProUGUI newstats;
    public Npcshopcontroller npcshopstatscontroller;
    public Itemcontroller merchantitem;
    private string[] statstext = { "Health ", "Defense ", "Attack ", "Crit ", "Critchance ", "Weaponswitch ", "Charswitch ", "Basic " };
    public void statsupdate()
    {
        newstats.text = string.Empty;
        for (int i = 0; i < statstext.Length; i++)
        {
            if(merchantitem.basestats[i] > 0)
            {
                newstats.text += statstext[i] + "<pos=90%>" + "<color=green>" + merchantitem.basestats[i].ToString() + "</color>" + "\n";
            }
            else if (merchantitem.basestats[i] < 0)
            {
                newstats.text += statstext[i] + "<pos=90%>" + "<color=red>" + merchantitem.basestats[i].ToString() + "</color>" + "\n";
            }
            else
            {
                newstats.text += statstext[i] + "<pos=90%>" + merchantitem.basestats[i].ToString() + "\n";
            }
        }
    }
}
