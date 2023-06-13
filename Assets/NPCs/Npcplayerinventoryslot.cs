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
        itemheader.text = item.name + " (max lvl" + item.maxupgradelvl + ")";
        ownitemstats.text = string.Empty;
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
        if (item.itemlvl[item.upgradelvl].stats[stat] > 0)
        {
            ownitemstats.text += statstext[stat] + "<pos=75%>" + "<color=green>" + item.itemlvl[item.upgradelvl].stats[stat] * skillpointmultipler + "</color>" + "\n";
        }
        else if (item.itemlvl[item.upgradelvl].stats[stat] < 0)
        {
            ownitemstats.text += statstext[stat] + "<pos=75%>" + "<color=red>" + item.itemlvl[item.upgradelvl].stats[stat] * skillpointmultipler + "</color>" + "\n";
        }
        else
        {
            ownitemstats.text += statstext[stat] + "<pos=75%>" + item.itemlvl[item.upgradelvl].stats[stat].ToString() + "\n";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemheader.text = string.Empty;
        ownitemstats.text = string.Empty;
        for (int i = 0; i < statstext.Length; i++)
        {
            ownitemstats.text += statstext[i] + "<pos=75%>" + 0 + "\n";
        }
    }
}
