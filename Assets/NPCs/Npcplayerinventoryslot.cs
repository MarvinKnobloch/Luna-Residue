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
        for (int i = 0; i < statstext.Length; i++)
        {
            if (item.itemlvl[item.upgradelvl].stats[i] > 0)
            {
                ownitemstats.text += statstext[i] + "<pos=75%>" + "<color=green>" + item.itemlvl[item.upgradelvl].stats[i].ToString() + "</color>" + "\n";
            }
            else if (item.itemlvl[item.upgradelvl].stats[i] < 0)
            {
                ownitemstats.text += statstext[i] + "<pos=75%>" + "<color=red>" + item.itemlvl[item.upgradelvl].stats[i].ToString() + "</color>" + "\n";
            }
            else
            {
                ownitemstats.text += statstext[i] + "<pos=75%>" + item.itemlvl[item.upgradelvl].stats[i].ToString() + "\n";
            }
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
