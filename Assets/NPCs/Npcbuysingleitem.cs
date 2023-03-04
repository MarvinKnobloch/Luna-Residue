using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System;

public class Npcbuysingleitem : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject buysingleitemwindow;
    [SerializeField] private TextMeshProUGUI itemtobuytext;
    [SerializeField] private TextMeshProUGUI itemcosttext;
    [SerializeField] private TextMeshProUGUI goldamount;
    [SerializeField] private TextMeshProUGUI interaction;
    [SerializeField] private Image buybar;

    [SerializeField] private Inventorycontroller matsinventory;
    [SerializeField] private Inventorycontroller inventroytoplacetoitem;
    [SerializeField] private Itemcontroller itemtobuy;
    [SerializeField] private Craftingobject gold;
    [SerializeField] private int itemprice;
    private bool canbuyitem;

    private float timetobuyitem = 1.5f;
    private float buyitemtimer;

    private Areacontroller areacontroller;
    private int dialoguenumber;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        areacontroller = GetComponent<Npcupdatedialogue>().areacontroller;
    }
    private void Start()
    {
        dialoguenumber = GetComponent<Areanumber>().areanumber;
    }
    private void OnEnable()
    {
        buysingleitemwindow.SetActive(true);
        itemtobuytext.text = itemtobuy.itemname;
        itemcosttext.text = itemprice.ToString(); 
        if (gold.inventoryslot == 0) 
        {
            itemcosttext.color = Color.red;
            canbuyitem = false;
        }
        else
        {
            if(matsinventory.Container.Items[gold.inventoryslot - 1].amount < itemprice)
            {
                itemcosttext.color = Color.red;
                canbuyitem = false;
            }
            else
            {
                itemcosttext.color = Color.green;
                canbuyitem = true;
            }
        }
        goldamount.text = "Gold: " + matsinventory.Container.Items[gold.inventoryslot - 1].amount.ToString();
        interaction.text = "Hold " + controlls.Equipmentmenu.Upgradeitem.GetBindingDisplayString() + " to buy item";
        buybar.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame()) //|| controlls.Menusteuerung.Leftclick.WasPerformedThisFrame())
        {
            buysingleitemwindow.SetActive(false);

            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
            LoadCharmanager.disableattackbuttons = false;
            LoadCharmanager.interaction = false;
            enabled = false;
        }
        if(canbuyitem == true)
        {
            if (controlls.Equipmentmenu.Upgradeitem.WasPressedThisFrame())
            {
                StartCoroutine(buyitemstart());
            }
        }
    }
    IEnumerator buyitemstart()
    {
        buyitemtimer = 0;
        buybar.gameObject.SetActive(true);
        buybar.fillAmount = 0;
        buyitemtimer = 0f;
        while (buyitemtimer < timetobuyitem)
        {
            if (controlls.Equipmentmenu.Upgradeitem.WasReleasedThisFrame())
            {
                buybar.gameObject.SetActive(false);
                StopAllCoroutines();
            }
            buyitemtimer += Time.deltaTime;
            buybar.fillAmount = buyitemtimer / timetobuyitem;
            yield return null;
        }
        buybar.fillAmount = 0;
        buyitem();
    }
    private void buyitem()
    {
        if(inventroytoplacetoitem == matsinventory)
        {
            inventroytoplacetoitem.Additem(itemtobuy, 1);
        }
        else
        {
            inventroytoplacetoitem.Addequipment(itemtobuy, itemtobuy.seconditem, 1);
        }
        matsinventory.Container.Items[gold.inventoryslot - 1].amount -= itemprice;
        endbuyitem();
    }
    private void endbuyitem()
    {
        areacontroller.npcdialoguestate[dialoguenumber]++;
        areacontroller.autosave();
        buysingleitemwindow.SetActive(false);
        GetComponent<Npcupdatedialogue>().enabled = true;
        if(TryGetComponent(out Npcafterinteraction npcafterinteraction))
        {
            npcafterinteraction.enabled = true;
        }
        else
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
            LoadCharmanager.disableattackbuttons = false;
            LoadCharmanager.interaction = false;
        }
        enabled = false;
    }
}
