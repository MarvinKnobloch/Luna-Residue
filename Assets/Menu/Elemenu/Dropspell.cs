using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Dropspell : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject dragimage;
    [SerializeField] private int charslotdrop;
    [SerializeField] private int secondcharslotdrop;    //wegen slot 3 und 4
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (Dragspellcontroller.dragedfromspellslot == true)                                //f�r dragswitch
        {
            if (charslotdrop == Dragspellcontroller.checkforcorrectslot || secondcharslotdrop == Dragspellcontroller.checkforcorrectslot)
            {
                Dragspellcontroller.currentspellslot.GetComponent<Image>().color = GetComponent<Image>().color;
                Dragspellcontroller.currentspellslot.GetComponentInChildren<TextMeshProUGUI>().text = GetComponentInChildren<TextMeshProUGUI>().text;
                Dragspellcontroller.currentspellslot.GetComponent<Setspells>().spellnumber = GetComponent<Setspells>().spellnumber;
                Dragspellcontroller.currentspellslot.GetComponent<Setspells>().ondragswitch();
                GetComponent<Image>().color = dragimage.GetComponent<Image>().color;
                GetComponentInChildren<TextMeshProUGUI>().text = dragimage.GetComponentInChildren<TextMeshProUGUI>().text;
                GetComponent<Setspells>().setspell(dragimage.GetComponent<Dragspellcontroller>().spellnumber, dragimage.GetComponent<Image>().color, dragimage.GetComponentInChildren<TextMeshProUGUI>().text);
                dragimage.GetComponent<Dragspellcontroller>().menusoundcontroller.playmenubuttonsound();
            }
        }

        if( Dragspellcontroller.dragedfromouterspellcircle == true)
        {
            if (charslotdrop == Elemenucontroller.currentelemenuchar || secondcharslotdrop == Elemenucontroller.currentelemenuchar)
            {
                GetComponent<Image>().color = dragimage.GetComponent<Image>().color;
                GetComponentInChildren<TextMeshProUGUI>().text = dragimage.GetComponentInChildren<TextMeshProUGUI>().text;
                GetComponent<Setspells>().setspell(dragimage.GetComponent<Dragspellcontroller>().spellnumber, dragimage.GetComponent<Image>().color, dragimage.GetComponentInChildren<TextMeshProUGUI>().text);
                dragimage.GetComponent<Dragspellcontroller>().menusoundcontroller.playmenubuttonsound();
            }
        }
    }
}
