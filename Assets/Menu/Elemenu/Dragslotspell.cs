using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class Dragslotspell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool gotspell;
    [SerializeField] private GameObject dragimage;
    [NonSerialized] public Color spellcolor;
    [NonSerialized] public string spelltext;
    public int spellnumber;                            //wird beim löschen nicht zurückgesetzt, sollte aber egal sein weil man sowieso nicht dragen kann

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gotspell == true)
        {
            Dragspellcontroller.drag = true;
            Dragspellcontroller.dragfromspellslot = true;                                   //für dragswitch

            Dragspellcontroller.currentspellslot = this.gameObject;                         //für dragswitch

            dragimage.SetActive(true);
            dragimage.GetComponent<Image>().color = spellcolor;
            dragimage.GetComponentInChildren<TextMeshProUGUI>().text = spelltext;
            dragimage.GetComponent<Dragspellcontroller>().spellnumber = spellnumber;
            dragimage.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (gotspell == true)
        {
            dragimage.transform.position = eventData.position;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (gotspell == true)
        {
            dragimage.GetComponent<CanvasGroup>().blocksRaycasts = true;
            dragimage.SetActive(false);
            Dragspellcontroller.drag = false;
            Dragspellcontroller.dragfromspellslot = false;
        }
    }
}
