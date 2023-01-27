using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Npcplayerinventoryslot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Itemcontroller item;
    [SerializeField] private TextMeshProUGUI ownitemstats;
    private string[] statstext = { "Health ", "Defense ", "Attack ", "Crit ", "Critchance ", "Weaponswitch ", "Charswitch ", "Basic " };

    public void OnPointerEnter(PointerEventData eventData)
    {
        ownitemstats.text = string.Empty;
        for (int i = 0; i < statstext.Length; i++)
        {
            if (item.stats[i] > 0)
            {
                ownitemstats.text += statstext[i] + "<pos=90%>" + "<color=green>" + item.stats[i].ToString() + "</color>" + "\n";
            }
            else if (item.stats[i] < 0)
            {
                ownitemstats.text += statstext[i] + "<pos=90%>" + "<color=red>" + item.stats[i].ToString() + "</color>" + "\n";
            }
            else
            {
                ownitemstats.text += statstext[i] + "<pos=90%>" + item.stats[i].ToString() + "\n";
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ownitemstats.text = string.Empty;
        for (int i = 0; i < statstext.Length; i++)
        {
            ownitemstats.text += statstext[i] + "<pos=90%>" + 0 + "\n";
        }
    }
}
