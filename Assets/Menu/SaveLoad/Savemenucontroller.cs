using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Savemenucontroller : MonoBehaviour
{
    private SpielerSteu steuerung;
    [SerializeField] private GameObject menuobj;
    [SerializeField] private TextMeshProUGUI[] slotempty;
    [SerializeField] private TextMeshProUGUI[] charlvl;
    [SerializeField] private TextMeshProUGUI[] savedate;
    public int selectedslot;

    [SerializeField] private GameObject commitsaveobj;

    [SerializeField] private Menusoundcontroller menusoundcontroller;

    private void Awake()
    {
        steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        steuerung.Enable();
        setslots();
    }
    private void Update()
    {
        if (steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame() && commitsaveobj.activeSelf == false)
        {
            menuobj.SetActive(true);
            gameObject.SetActive(false);
            menusoundcontroller.playmenubuttonsound();
        }
        if (steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame() && commitsaveobj.activeSelf == true)
        {
            closecommitsave();
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
    public void opencommitsave(int slot)
    {
        //if (Slotvaluesarray.slotisnotempty[slot - 1] == true)
        commitsaveobj.SetActive(true);
        commitsaveobj.GetComponentInChildren<TextMeshProUGUI>().text = "Save Game? (Slot " + slot + ")";
        selectedslot = slot;
        menusoundcontroller.playmenubuttonsound();
    }
    public void closecommitsave()
    {
        commitsaveobj.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        menusoundcontroller.playmenubuttonsound();
    }
}
