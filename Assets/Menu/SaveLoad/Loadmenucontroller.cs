using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Loadmenucontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject menuobj;
    [SerializeField] private GameObject commitloadobj;
    [SerializeField] private TextMeshProUGUI[] slotempty;
    [SerializeField] private TextMeshProUGUI[] charlvl;
    [SerializeField] private TextMeshProUGUI[] savedate;
    public int selectedslot;

    [SerializeField] private Menusoundcontroller menusoundcontroller;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        controlls.Enable();
        commitloadobj.SetActive(false);
        setslots();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame() && commitloadobj.activeSelf == false)
        {
            menusoundcontroller.playmenubuttonsound();
            menuobj.SetActive(true);
            gameObject.SetActive(false);
        }
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame() && commitloadobj.activeSelf == true)
        {         
            closecommitload();
        }
    }
    public void setslots()
    {
        for (int i = 0; i < Slotvaluesarray.slotisnotempty.Length; i++)
        {
            if (Slotvaluesarray.slotisnotempty[i] == false)
            {
                slotempty[i].text = "Empty Slot";
                charlvl[i].text = "";
                savedate[i].text = "";
            }
            else
            {
                slotempty[i].text = "";
                charlvl[i].text = "PlayerLvL " + Slotvaluesarray.slotlvl[i].ToString();
                savedate[i].text = Slotvaluesarray.slotdate[i];
                savedate[i].text += "\n " + Slotvaluesarray.slottime[i];
            }
        }
    }
    public void opencommitload(int slot)
    {
        if (Slotvaluesarray.slotisnotempty[slot] == true)
        {
            commitloadobj.SetActive(true);
            commitloadobj.GetComponentInChildren<TextMeshProUGUI>().text = "Load Game? (Slot " + slot + ")";
            selectedslot = slot;
            menusoundcontroller.playmenubuttonsound();
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    public void closecommitload()
    {
        commitloadobj.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        menusoundcontroller.playmenubuttonsound();
    }
}
