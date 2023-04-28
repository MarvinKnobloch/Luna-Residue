using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;

public class Upgradecontroller : MonoBehaviour
{
    private SpielerSteu Steuerung;
    private InputAction upgradehotkey;
    [SerializeField] private GameObject resetonpointenterlayer;
    [NonSerialized] public Chooseitem chooseitem;
    [NonSerialized] public Chooseweapon chooseweapon;

    public Itemcontroller itemtoupgrade;
    private Image upgradeimage;
    private TextMeshProUGUI upgradetext;
    public bool canupgrade;

    [SerializeField] Upgradeuitextcontroller itemtextcontroller;

    [SerializeField] private Inventorycontroller[] inventorys;
    public Inventorycontroller matsinventory;
    public int[] craftingmatinventoryposi = new int[5];                  //es dürfen maximal 5 mats zum übergraden verwendet werden
    public int[] upgrademinusvalue = new int[5];

    private float upgradetime = 1.5f;
    public float upgradetimer;
    private bool starttimer;

    private DateTime startdate;
    private DateTime currentdate;
    private float seconds;

    [SerializeField] private Menusoundcontroller menusoundcontroller;
    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
        upgradehotkey = Steuerung.Equipmentmenu.Upgradeitem;
        upgradeimage = GetComponent<Image>();
        upgradeimage.fillAmount = 0;
        upgradetext = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        StopAllCoroutines();
        upgradetext.text = "hold " + upgradehotkey.GetBindingDisplayString() + " to Upgrade";
        upgradeimage.fillAmount = 0;
        starttimer = false;
    }
    private void Update()
    {
        if(canupgrade == true)
        {
            if (Steuerung.Equipmentmenu.Upgradeitem.WasPressedThisFrame() && starttimer == false)
            {
                starttimer = true;
                StartCoroutine(upgradeitemstart());
            }
            if (Steuerung.Equipmentmenu.Upgradeitem.WasReleasedThisFrame())
            {
                upgradeimage.fillAmount = 0;
                starttimer = false;
                StopAllCoroutines();
            }
        }
    }
    IEnumerator upgradeitemstart()
    {
        startdate = DateTime.Now;
        upgradeimage.fillAmount = 0;
        upgradetimer = 0f;
        while (upgradetimer < upgradetime)
        {
            currentdate = DateTime.Now;
            seconds = currentdate.Ticks - startdate.Ticks;
            upgradetimer = seconds * 0.0000001f;
            upgradeimage.fillAmount = upgradetimer / upgradetime;
            yield return null;
        }
        upgradeimage.fillAmount = 0;
        if (itemtoupgrade.upgradelvl < itemtoupgrade.maxupgradelvl)
        {
            upgradeitem();
        }
        starttimer = false;
    }
    private void upgradeitem()
    {
        chooseitem.subtractequipeditem();      
        removemats();
        itemtoupgrade.upgradelvl++;
        chooseitem.upgradeequipeditems();
        itemtextcontroller.valuesupdate();
        resetonpointenterlayer.SetActive(true);
        inventorys[Statics.currentequipmentbutton -3].Container.Items[itemtoupgrade.inventoryslot -1].itemlvl++;                    //-3 weil die waffeninventory nicht dabei sind / -1 weil array
        menusoundcontroller.playmenubuttonsound();
    }
    private void removemats()
    {
        for (int i = 0; i < itemtoupgrade.itemlvl[itemtoupgrade.upgradelvl].Upgrademats.Length; i++)
        {
            matsinventory.Container.Items[craftingmatinventoryposi[i]].amount -= upgrademinusvalue[i];
        }

    }
}