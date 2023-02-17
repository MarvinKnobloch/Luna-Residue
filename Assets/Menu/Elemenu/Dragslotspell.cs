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
    [SerializeField] private int dragedfromchar;
    private Dragspellcontroller dragspellcontroller;

    private void Awake()
    {
        dragspellcontroller = dragimage.GetComponent<Dragspellcontroller>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gotspell == true)
        {
            Dragspellcontroller.dragedfromouterspellcircle = false;
            Dragspellcontroller.dragedfromspellslot = true;                                   //für dragswitch

            Dragspellcontroller.currentspellslot = this.gameObject;                         //für dragswitch

            dragimage.SetActive(true);
            dragimage.GetComponent<Image>().color = spellcolor;
            dragimage.GetComponentInChildren<TextMeshProUGUI>().text = spelltext;
            dragspellcontroller.spellnumber = spellnumber;
            Dragspellcontroller.checkforcorrectslot = dragedfromchar;
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
            Dragspellcontroller.dragedfromouterspellcircle = false;
            Dragspellcontroller.dragedfromspellslot = false;
        }
    }
}
