using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Npcshopcontroller : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private Inventorycontroller matsinventory;
    [SerializeField] private Craftingobject money;
    private int moneyamount;
    [SerializeField] private TextMeshProUGUI moneycountdisplay;
    [SerializeField] private TextMeshProUGUI currentslotdisplay;
    public List<Itemcontroller> npcshopitems = new List<Itemcontroller>();
    private List<GameObject> instaniateditems = new List<GameObject>();
    [SerializeField] private GameObject shopitemprefab;
    [SerializeField] private GameObject merchantitemslots;
    public GameObject currentselecteditem;


    [SerializeField] private TextMeshProUGUI newitemheader;
    [SerializeField] private TextMeshProUGUI newstats;
    [SerializeField] private TextMeshProUGUI ownitemstats;
    private string[] statstext = { "Health ", "Defense ", "Attack ", "Critchance ", "Critdamage ", "Weaponswitch ", "Charswitch ", "Basic " };

    [SerializeField] private GameObject[] itemtypeselectionbuttons;
    private Itemtype[] itemtypes = { Itemtype.Sword, Itemtype.Bow, Itemtype.Fist, Itemtype.Head, Itemtype.Belt, Itemtype.Chest, Itemtype.Legs, Itemtype.Shoes, Itemtype.Necklace, Itemtype.Ring };
    private int currentitemtype;
    private List<Itemtype> listoftypes = new List<Itemtype>();

    [SerializeField] private Inventorycontroller[] inventorys;
    [SerializeField] private GameObject inventoryitemprefab;
    [SerializeField] private GameObject playeritemsui;

    [SerializeField] private GameObject mouseresetlayer;

    //Upgrade
    private float buyitemtime = 1.5f;
    private float buyitemtimer;
    private DateTime startdate;
    private DateTime currentdate;
    private float seconds;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        listoftypes.Clear();
        instaniateditems.Clear();
        currentitemtype = 0;
        resetownitemstatstext();

        setmoneyamount();
        instantiateshopitems();
        setmerchantslots();
        setcurrentitemtype();
        displayitemtyp();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menucharselectionright.WasPerformedThisFrame())
        {
            itemtypeselectionforward();
            resetownitemstatstext();
            displayitemtyp();
        }
        if (controlls.Menusteuerung.Menucharselectionleft.WasPerformedThisFrame())
        {
            itemtypeselectionbackwards();
            resetownitemstatstext();
            displayitemtyp();
        }
        if(currentselecteditem.GetComponent<Npcshopselectitem>().canbuy == true)
        {
            if (controlls.Equipmentmenu.Upgradeitem.WasPressedThisFrame())
            {
                if(checkformoney() == true)
                {
                    StartCoroutine(buyitemstart());
                }
            }
        }
    }
    private void itemtypeselectionforward()
    {
        StopAllCoroutines();
        EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Npcshopselectitem>().buyitembar.fillAmount = 0;

        itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.white;
        currentitemtype++;
        foreach (GameObject obj in itemtypeselectionbuttons)
        {
            if (currentitemtype >= itemtypeselectionbuttons.Length)
            {
                currentitemtype = 0;
            }
            if (itemtypeselectionbuttons[currentitemtype].activeSelf == false)
            {
                currentitemtype++;
                continue;
            }
            else
            {
                itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.green;
                break;
            }
        }
    }
    private void itemtypeselectionbackwards()
    {
        StopAllCoroutines();
        EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Npcshopselectitem>().buyitembar.fillAmount = 0;

        itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.white;
        currentitemtype--;
        foreach (GameObject obj in itemtypeselectionbuttons)
        {
            if (currentitemtype < 0)
            {
                currentitemtype = itemtypeselectionbuttons.Length - 1;
            }
            if (itemtypeselectionbuttons[currentitemtype].activeSelf == false)
            {
                currentitemtype--;
                continue;
            }
            else
            {
                itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.green;
                break;
            }
        }
    }
    private void  instantiateshopitems()
    {
        for (int i = 0; i < npcshopitems.Count; i++)
        {
            var obj = Instantiate(shopitemprefab, merchantitemslots.transform.position, Quaternion.identity, merchantitemslots.transform);
            obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = npcshopitems[i].itemname + " (" + npcshopitems[i].maxupgradelvl + ")";
            obj.GetComponent<Npcshopselectitem>().buyitembar.gameObject.SetActive(false);
            if(npcshopitems[i].inventoryslot == 0)
            {
                obj.GetComponent<Npcshopselectitem>().canbuy = true;
                obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = npcshopitems[i].itemshopcosts.ToString();
            }
            else
            {
                obj.GetComponent<Npcshopselectitem>().canbuy = false;
                obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "own Item";
            }
            obj.GetComponent<Npcshopselectitem>().npcshopstatscontroller = this;
            obj.GetComponent<Npcshopselectitem>().merchantitem = npcshopitems[i];
            obj.GetComponent<Npcshopselectitem>().newitemheader = newitemheader;
            obj.GetComponent<Npcshopselectitem>().newstats = newstats;
            instaniateditems.Add(obj);
        }
    }
    private void setmoneyamount()
    {
        if (money.inventoryslot != 0)
        {
            moneyamount = matsinventory.Container.Items[money.inventoryslot - 1].amount;
            moneycountdisplay.text = "Money: " + moneyamount.ToString();
        }
        else
        {
            moneyamount = 0;
            moneycountdisplay.text = moneyamount + " Money";
        }
    }
    private void setmerchantslots()
    {
        foreach(GameObject obj in itemtypeselectionbuttons)
        {
            obj.GetComponent<Image>().color = Color.white;
            obj.SetActive(false);
        }
        for (int i = 0; i < instaniateditems.Count; i++)
        {
            if(listoftypes.Contains(instaniateditems[i].GetComponent<Npcshopselectitem>().merchantitem.type) == false)
            {
                listoftypes.Add(instaniateditems[i].GetComponent<Npcshopselectitem>().merchantitem.type);
            }
        }
        for (int i = 0; i < itemtypes.Length; i++)
        {
            if (listoftypes.Contains(itemtypes[i])) itemtypeselectionbuttons[i].SetActive(true);
        }
    }
    private void setcurrentitemtype()
    {
        for (int i = 0; i < itemtypeselectionbuttons.Length; i++)
        {
            if (itemtypeselectionbuttons[i].activeSelf == true)
            {
                currentitemtype = i;
                itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.green;
                return;
            }
            else continue;
        }
    }
    private void displayitemtyp()
    {
        EventSystem.current.SetSelectedGameObject(null);
        for (int i = 0; i < instaniateditems.Count; i++)
        {
            if (instaniateditems[i].GetComponent<Npcshopselectitem>().merchantitem.type != itemtypes[currentitemtype])
            {
                instaniateditems[i].SetActive(false);
            }
            else
            {           
                instaniateditems[i].SetActive(true);
                checkitemformoney(i);
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(instaniateditems[i]);
                    instaniateditems[i].GetComponent<Npcshopselectitem>().statsupdate();
                }
            }
        }
        displayinventoryitems();
    }

    private void displayinventoryitems()
    {
        foreach (Transform obj in playeritemsui.transform)
        {
            obj.gameObject.SetActive(false);
        }
        for (int i = 0; i < inventorys[currentitemtype].Container.Items.Length; i++)
        {
            if (inventorys[currentitemtype].Container.Items[i].amount != 0)
            {
                GameObject obj = playeritemsui.transform.GetChild(i).gameObject;
                obj.SetActive(true);
                obj.transform.GetChild(0).GetComponent<Image>().sprite = inventorys[currentitemtype].Container.Items[i].item.Uisprite;
                obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventorys[currentitemtype].Container.Items[i].itemname;
                obj.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = inventorys[currentitemtype].Container.Items[i].itemlvl.ToString();
                obj.GetComponent<Npcplayerinventoryslot>().item = inventorys[currentitemtype].Container.Items[i].item;
            }
        }
        mouseresetlayer.SetActive(true);
    }
    private void checkitemformoney(int i)
    {
        if (instaniateditems[i].GetComponent<Npcshopselectitem>().merchantitem.inventoryslot == 0)
        {
            if (instaniateditems[i].GetComponent<Npcshopselectitem>().merchantitem.itemshopcosts <= moneyamount)
            {
                instaniateditems[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.green;
            }
            else
            {
                instaniateditems[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            instaniateditems[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.blue;
        }
    }
    public void clickitemtypebutton(int itemtype)
    {
        itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.white;
        currentitemtype = itemtype;
        itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.green;
        displayitemtyp();
    }
    private bool checkformoney()
    {
        if (money.inventoryslot != 0)
        {
            if (matsinventory.Container.Items[money.inventoryslot - 1].amount >= EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Npcshopselectitem>().merchantitem.itemshopcosts)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }
    IEnumerator buyitemstart()
    {
        startdate = DateTime.Now;
        Image upgradebar = currentselecteditem.GetComponent<Npcshopselectitem>().buyitembar;
        upgradebar.gameObject.SetActive(true);
        upgradebar.fillAmount = 0;
        buyitemtimer = 0f;
        while (buyitemtimer < buyitemtime)
        {
            if (controlls.Equipmentmenu.Upgradeitem.WasReleasedThisFrame())
            {
                upgradebar.gameObject.SetActive(false);
                StopAllCoroutines();
            }
            currentdate = DateTime.Now;
            seconds = currentdate.Ticks - startdate.Ticks;
            buyitemtimer = seconds * 0.0000001f;
            upgradebar.fillAmount = buyitemtimer / buyitemtime;
            yield return null;
        }
        upgradebar.fillAmount = 0;
        buyitem();
    }
    private void buyitem()
    {
        Itemcontroller itemtobuy = EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Npcshopselectitem>().merchantitem;
        matsinventory.Container.Items[money.inventoryslot - 1].amount -= itemtobuy.itemshopcosts;
        setmoneyamount();

        inventorys[currentitemtype].Additem(itemtobuy, 1);
        EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Npcshopselectitem>().canbuy = false;
        EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "own Item";
        EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.blue;
        displayinventoryitems();
    }
    private void resetownitemstatstext()
    {
        ownitemstats.text = string.Empty;
        for (int i = 0; i<statstext.Length; i++)
        {
            ownitemstats.text += statstext[i] + "<pos=75%>" + 0 + "\n";
        }
    }

    public void removeitemswhenclose()
    {
        StopAllCoroutines();
        foreach (Transform obj in merchantitemslots.transform)
        {
            Destroy(obj.gameObject);
        }
        listoftypes.Clear();
        instaniateditems.Clear();
    }
}

