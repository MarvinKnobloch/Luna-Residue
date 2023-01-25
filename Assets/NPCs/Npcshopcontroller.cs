using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Npcshopcontroller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneycount;
    [SerializeField] private Inventorycontroller matsinventory;
    [SerializeField] private Craftingobject money;
    public List<Itemcontroller> npcshopitems = new List<Itemcontroller>();
    private List<GameObject> instaniateditems = new List<GameObject>();
    [SerializeField] private GameObject shopitemprefab;
    [SerializeField] private GameObject merchantitemslots;
    [SerializeField] private TextMeshProUGUI newstats;
    [SerializeField] private TextMeshProUGUI comparestats;
    private Itemtype[] itemtypes = { Itemtype.Sword, Itemtype.Bow, Itemtype.Fist, Itemtype.Head, Itemtype.Chest, Itemtype.Gloves, Itemtype.Belt, Itemtype.Legs, Itemtype.Shoes, Itemtype.Neckless, Itemtype.Ring };
    private int currentitemtype;

    private void OnEnable()
    {
        npcshopitems.Clear();
        instaniateditems.Clear();
        currentitemtype = 0;
        if (money.inventoryslot != 0)
        {
            moneycount.text = matsinventory.Container.Items[money.inventoryslot -1].amount.ToString() + " Money";
        }
        else
        {
            moneycount.text = 0 + " Money";
        }
        StartCoroutine(displayshopitems());
    }
    IEnumerator displayshopitems()
    {
        yield return null;
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
        for (int i = 0; i < instaniateditems.Count; i++)
        {
            if(instaniateditems[i].GetComponent<Npcshopselectitem>().merchantitem.type != itemtypes[currentitemtype])
            {
                instaniateditems[i].SetActive(false);
            }
        }
    }
}

