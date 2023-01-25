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
    [SerializeField] private GameObject shopitemprefab;
    [SerializeField] private GameObject merchantitemslots;

    private void OnEnable()
    {
        npcshopitems.Clear();
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
        }
    }
}

