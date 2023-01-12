using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Savemenucontroller : MonoBehaviour
{
    private SpielerSteu steuerung;
    [SerializeField] private GameObject menuobj;
    [SerializeField] private TextMeshProUGUI[] slotempty;
    [SerializeField] private TextMeshProUGUI[] charlvl;
    [SerializeField] private TextMeshProUGUI[] savedate;


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
        if (steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            menuobj.SetActive(true);
            gameObject.SetActive(false);
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
}
