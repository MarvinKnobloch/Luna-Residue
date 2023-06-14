using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Npcplayerinventoryslot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Itemcontroller item;
    [SerializeField] private TextMeshProUGUI itemheader;
    [SerializeField] private TextMeshProUGUI ownitemstats;
    private string[] statstext = { "Health ", "Defense ", "Attack ", "Critchance ", "Critdamage ", "Weaponswitch ", "Charswitch ", "Basic " };

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemheader.text = item.name + " (max lvl " + item.maxupgradelvl + ")";
        ownitemstats.text = string.Empty;
        showstats(0, Statics.healthperskillpoint);
        showstats(1, Statics.defenseperskillpoint);
        showstats(2, Statics.attackperskillpoint);
        showstatsdecimal(3, Statics.critchanceperskillpoint);
        showstatsdecimal(4, Statics.critdmgperskillpoint);
        showstatsdecimal(5, Statics.weaponswitchbuffperskillpoint);
        showstatsdecimal(6, Statics.charswitchbuffperskillpoint);
        showstatsdecimal(7, Statics.basicdmgbuffperskillpoint);
    }
    private void showstats(int stat, float skillpointmultipler)
    {
        if (item.itemlvl[item.upgradelvl].stats[stat] > 0)
        {
            ownitemstats.text += "<color=green>" + item.itemlvl[item.upgradelvl].stats[stat] * skillpointmultipler + "</color>" + "\n";
        }
        else if (item.itemlvl[item.upgradelvl].stats[stat] < 0)
        {
            ownitemstats.text += "<color=red>" + item.itemlvl[item.upgradelvl].stats[stat] * skillpointmultipler + "</color>" + "\n";
        }
        else
        {
            ownitemstats.text += item.itemlvl[item.upgradelvl].stats[stat].ToString() + "\n";
        }
    }
    private void showstatsdecimal(int stat, float skillpointmultipler)
    {
        if (item.itemlvl[item.upgradelvl].stats[stat] > 0)
        {
            ownitemstats.text += "<color=green>" + string.Format("{0:0.0}", item.itemlvl[item.upgradelvl].stats[stat] * skillpointmultipler) + "</color>%" + "\n";
        }
        else if (item.itemlvl[item.upgradelvl].stats[stat] < 0)
        {
            ownitemstats.text += "<color=red>" + string.Format("{0:0.0}", item.itemlvl[item.upgradelvl].stats[stat] * skillpointmultipler) + "</color>%" + "\n";
        }
        else
        {
            ownitemstats.text += "0,0%\n";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemheader.text = string.Empty;
        ownitemstats.text = string.Empty;
        for (int i = 0; i < statstext.Length; i++)
        {
            if (i < 3) ownitemstats.text += 0 + "\n";
            else ownitemstats.text += "0,0%\n";
        }
    }
}
