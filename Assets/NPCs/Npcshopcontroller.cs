using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Npcshopcontroller : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private Inventorycontroller matsinventory;
    [SerializeField] private Craftingobject money;
    [SerializeField] private TextMeshProUGUI moneycount;
    public List<Itemcontroller> npcshopitems = new List<Itemcontroller>();
    private List<GameObject> instaniateditems = new List<GameObject>();
    [SerializeField] private GameObject shopitemprefab;
    [SerializeField] private GameObject merchantitemslots;

    [SerializeField] private TextMeshProUGUI newstats;
    [SerializeField] private TextMeshProUGUI comparestats;

    [SerializeField] private GameObject[] itemslots;
    private Itemtype[] itemtypes = { Itemtype.Sword, Itemtype.Bow, Itemtype.Fist, Itemtype.Head, Itemtype.Chest, Itemtype.Gloves, Itemtype.Legs, Itemtype.Shoes, Itemtype.Neckless, Itemtype.Ring };
    private int currentitemtype;
    private int maxitemtype;
    private List<Itemtype> listoftypes = new List<Itemtype>();


    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        listoftypes.Clear();
        currentitemtype = 0;
        maxitemtype = 0;
        if (money.inventoryslot != 0)  moneycount.text = matsinventory.Container.Items[money.inventoryslot - 1].amount.ToString() + " Money";
        else  moneycount.text = 0 + " Money";
        instantiateshopitems();
        merchantslots();
        setcurrentitemtype();
        displayitemtyp();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menucharselectionleft.WasPerformedThisFrame())
        {
            if (currentitemtype == maxitemtype) currentitemtype = 0;
            else currentitemtype++;
            displayitemtyp();
        }
        if (controlls.Menusteuerung.Menucharselectionright.WasPerformedThisFrame())
        {
            if (currentitemtype == 0) currentitemtype = maxitemtype;
            else currentitemtype--;
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
    private void merchantslots()
    {
        foreach(GameObject obj in itemslots)
        {
            obj.SetActive(false);
        }
        for (int i = 0; i < instaniateditems.Count; i++)
        {
            if(listoftypes.Contains(instaniateditems[i].GetComponent<Npcshopselectitem>().merchantitem.type) == false)
            {
                listoftypes.Add(instaniateditems[i].GetComponent<Npcshopselectitem>().merchantitem.type);
                maxitemtype++;
            }
        }
        Debug.Log(maxitemtype);
        for (int i = 0; i < listoftypes.Count; i++)
        {
            if (listoftypes.Contains(itemtypes[i])) itemslots[i].SetActive(true);
        }
    }
    private void setcurrentitemtype()
    {
        for (int i = 0; i < itemslots.Length; i++)
        {
            if (itemslots[i].activeSelf == true)
            {
                currentitemtype = i;
                return;
            }
            else continue;
        }
    }
    private void displayitemtyp()
    {
        for (int i = 0; i < instaniateditems.Count; i++)
        {
            if (instaniateditems[i].GetComponent<Npcshopselectitem>().merchantitem.type != itemtypes[currentitemtype])
            {
                instaniateditems[i].SetActive(false);
            }
        }
    }
}

