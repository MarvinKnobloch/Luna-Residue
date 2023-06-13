using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Npcshopselectitem : MonoBehaviour, ISelectHandler                     //instantiate shop item benutzt dieses script
{
    public TextMeshProUGUI newitemheader;
    public TextMeshProUGUI newstats;
    public Npcshopcontroller npcshopstatscontroller;
    public Itemcontroller merchantitem;
    public bool canbuy;
    public Image buyitembar;
    private string[] statstext = { "Health ", "Defense ", "Attack ", "Critchance ", "Critdamage ", "Weaponswitch ", "Charswitch ", "Basic " };
    public void OnSelect(BaseEventData eventData)
    {
        npcshopstatscontroller.currentselecteditem = gameObject;
        statsupdate();
    }

    public void statsupdate()
    {
        newitemheader.text = merchantitem.itemname + " (max lvl" + merchantitem.maxupgradelvl + ")";
        newstats.text = string.Empty;
        showstats(0, Statics.healthperskillpoint);
        showstats(1, Statics.defenseperskillpoint);
        showstats(2, Statics.attackperskillpoint);
        showstats(3, Statics.critchanceperskillpoint);
        showstats(4, Statics.critdmgperskillpoint);
        showstats(5, Statics.weaponswitchbuffperskillpoint);
        showstats(6, Statics.charswitchbuffperskillpoint);
        showstats(7, Statics.basicdmgbuffperskillpoint);
    }
    private void showstats(int stat, float skillpointmultipler)
    {
        if (merchantitem.itemlvl[merchantitem.upgradelvl].stats[stat] > 0)
        {
            newstats.text += statstext[stat] + "<pos=75%>" + "<color=green>" + merchantitem.itemlvl[merchantitem.upgradelvl].stats[stat] * skillpointmultipler + "</color>" + "\n";
        }
        else if (merchantitem.itemlvl[merchantitem.upgradelvl].stats[stat] < 0)
        {
            newstats.text += statstext[stat] + "<pos=75%>" + "<color=red>" + merchantitem.itemlvl[merchantitem.upgradelvl].stats[stat] * skillpointmultipler + "</color>" + "\n";
        }
        else
        {
            newstats.text += statstext[stat] + "<pos=75%>" + merchantitem.itemlvl[merchantitem.upgradelvl].stats[stat].ToString() + "\n";
        }
    }
}
