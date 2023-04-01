using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Startmenubuttoncontroller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Startmenucontroller startmenucontroller;

    private Color selectedcolor;
    private Color notselectedcolor;

    private void Awake()
    {
        selectedcolor = startmenucontroller.selectedcolor;
        notselectedcolor = startmenucontroller.notselectedcolor;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = selectedcolor;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = notselectedcolor;
    }
}
