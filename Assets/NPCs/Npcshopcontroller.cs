using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Npcshopcontroller : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private Inventorycontroller matsinventory;
    [SerializeField] private Craftingobject money;
    [SerializeField] private TextMeshProUGUI moneycountdisplay;
    [SerializeField] private TextMeshProUGUI currentslotdisplay;
    public List<Itemcontroller> npcshopitems = new List<Itemcontroller>();
    private List<GameObject> instaniateditems = new List<GameObject>();
    [SerializeField] private GameObject shopitemprefab;
    [SerializeField] private GameObject merchantitemslots;

    [SerializeField] private TextMeshProUGUI newstats;
    [SerializeField] private TextMeshProUGUI comparestats;

    [SerializeField] private GameObject[] itemtypeselectionbuttons;
    private Itemtype[] itemtypes = { Itemtype.Sword, Itemtype.Bow, Itemtype.Fist, Itemtype.Head, Itemtype.Chest, Itemtype.Gloves, Itemtype.Legs, Itemtype.Shoes, Itemtype.Neckless, Itemtype.Ring };
    private int currentitemtype;
    private List<Itemtype> listoftypes = new List<Itemtype>();

    [SerializeField] private Inventorycontroller[] inventorys;
    [SerializeField] private GameObject inventoryitemprefab;
    [SerializeField] private GameObject playeritemsui;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        listoftypes.Clear();
        currentitemtype = 0;

        if (money.inventoryslot != 0)  moneycountdisplay.text = matsinventory.Container.Items[money.inventoryslot - 1].amount.ToString() + " Money";
        else  moneycountdisplay.text = 0 + " Money";
        instantiateshopitems();
        setmerchantslots();
        setcurrentitemtype();
        displayitemtyp();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menucharselectionright.WasPerformedThisFrame())
        {
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
                    currentslotdisplay.text = itemtypeselectionbuttons[currentitemtype].GetComponentInChildren<TextMeshProUGUI>().text;
                    break;
                }
            }
            displayitemtyp();
        }
        if (controlls.Menusteuerung.Menucharselectionleft.WasPerformedThisFrame())
        {
            itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.white;
            currentitemtype--;
            foreach (GameObject obj in itemtypeselectionbuttons)
            {
                if (currentitemtype < 0)
                {
                    currentitemtype = itemtypeselectionbuttons.Length -1;
                }
                if (itemtypeselectionbuttons[currentitemtype].activeSelf == false)
                {
                    currentitemtype--;
                    continue;
                }
                else
                {
                    itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.green;
                    currentslotdisplay.text = itemtypeselectionbuttons[currentitemtype].GetComponentInChildren<TextMeshProUGUI>().text;
                    break;
                }
            }
            displayitemtyp();
        }
    }
    private void  instantiateshopitems()
    {
        for (int i = 0; i < npcshopitems.Count; i++)
        {
            var obj = Instantiate(shopitemprefab, merchantitemslots.transform.position, Quaternion.identity, merchantitemslots.transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = npcshopitems[i].itemname;
            obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = npcshopitems[i].itemshopcosts.ToString();
            obj.GetComponent<Npcshopselectitem>().npcshopstatscontroller = this;
            obj.GetComponent<Npcshopselectitem>().merchantitem = npcshopitems[i];
            obj.GetComponent<Npcshopselectitem>().newstats = newstats;
            instaniateditems.Add(obj);
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
                currentslotdisplay.text = itemtypeselectionbuttons[currentitemtype].GetComponentInChildren<TextMeshProUGUI>().text;
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
                if(EventSystem.current.currentSelectedGameObject == null)
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
        /*for (int i = 0; i < inventorys[currentitemtype].Container.Items.Length; i++)
        {
            if (inventorys[currentitemtype].Container.Items[i].amount != 0)
            {
                //ar obj = Instantiate(inventoryitemprefab, merchantitemslots.transform.position, Quaternion.identity, merchantitemslots.transform);
                //obj.GetComponentInChildren<TextMeshProUGUI>().text = inventorys[currentitemtype].Container.Items[i].itemname;
            }
        }*/
    }
    public void clickitemtypebutton(int itemtype)
    {
        itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.white;
        currentitemtype = itemtype;
        itemtypeselectionbuttons[currentitemtype].GetComponent<Image>().color = Color.green;
        currentslotdisplay.text = itemtypeselectionbuttons[currentitemtype].GetComponentInChildren<TextMeshProUGUI>().text;
        displayitemtyp();
    }
}

