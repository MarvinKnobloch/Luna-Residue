using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Openselectedmenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject menuoverview;
    private Menucontroller menucontroller;
    [SerializeField] private GameObject menu;

    [SerializeField] private string uimessage;

    private Color selectedcolor;
    private Color notselectedcolor;

    private void Awake()
    {
        menucontroller = menuoverview.GetComponent<Menucontroller>();
        selectedcolor = menucontroller.selectedcolor;
        notselectedcolor = menucontroller.notselectedcolor;
    }
    private void OnEnable()
    {
        GetComponent<Image>().color = notselectedcolor;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = selectedcolor;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = notselectedcolor;
    }
    public void opennormalmenu()
    {
        menu.SetActive(true);
        menuoverview.SetActive(false);
    }
    public void openelemenu()
    {
        if (Statics.elementalmenuisactiv == true)
        {
            menu.SetActive(true);
            menuoverview.SetActive(false);
        }
        else
        {
            menucontroller.cantopenuimessage(uimessage);
        }
    }
    public void opensavegamemenu()
    {
        if (LoadCharmanager.cantsavehere == false)
        {
            menu.SetActive(true);
            menuoverview.SetActive(false);
        }
        else
        {
            menucontroller.cantopenuimessage(uimessage);
        }
    }
    public void closegame()
    {
        if(menu.activeSelf == false)
        {
            menucontroller.closeallselections();
            menucontroller.somethinginmenuisopen = true;
            menu.SetActive(true);
        }
        else
        {
            menucontroller.somethinginmenuisopen = false;
            menu.SetActive(false);
        }
    }
}
