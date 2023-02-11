using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class Dragspell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [NonSerialized] public GameObject dragimage;
    private Color spellcolor;
    private string spelltext;
    public int spellnumber;

    private void Awake()
    {
        spellcolor = GetComponent<Image>().color;
        spelltext = GetComponentInChildren<TextMeshProUGUI>().text;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Dragspellcontroller.dragedfromspellslot = false;
        Dragspellcontroller.dragedfromouterspellcircle = true;
        dragimage.SetActive(true);
        dragimage.GetComponent<Image>().color = spellcolor;
        dragimage.GetComponentInChildren<TextMeshProUGUI>().text = spelltext;
        dragimage.GetComponent<Dragspellcontroller>().spellnumber = spellnumber;
        dragimage.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        dragimage.transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        dragimage.GetComponent<CanvasGroup>().blocksRaycasts = true;
        dragimage.SetActive(false);
        Dragspellcontroller.dragedfromouterspellcircle = false;
    }
}
