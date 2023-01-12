using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chestinventoryui : MonoBehaviour
{
    public Inventorycontroller inventory;
    public GameObject inventoryicon;                               // das icon das im UI angezeigwird

    public int x_space_between_items;
    public int xStart;
    public int y_space_between_items;
    public int yStart;
    public int numberofcolumn;
    Dictionary<GameObject, Inventoryslot> itemsdisplayed = new Dictionary<GameObject, Inventoryslot>();                         // Das Dictionary welches die Inventoryslots speichert

    private void Awake()
    {
        Creatslots();
    }
    private void OnEnable()
    {
        slotsupdate();
    }
    public void slotsupdate()
    {
        foreach (KeyValuePair<GameObject, Inventoryslot> _slot in itemsdisplayed)
        {
            if (_slot.Value.amount > 0)
            {
                _slot.Key.transform.GetComponent<Chooseitem>().itemvalues = _slot.Value.item;
                _slot.Key.transform.GetComponent<Chooseitem>().equipslotnumber = 4;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.item.Uisprite;
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Text>().text = _slot.Value.itemname.ToString();
                _slot.Key.transform.GetComponent<Chooseitem>().upgradelvltext = _slot.Key.transform.GetChild(3).GetComponentInChildren<Text>();
                _slot.Key.transform.GetChild(3).GetComponentInChildren<Text>().text = _slot.Value.item.upgradelvl.ToString();
            }
            else
            {
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Text>().text = "empty";
                _slot.Key.transform.GetChild(2).gameObject.SetActive(false);                  // Image und lvl der Waffe
                _slot.Key.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
    public void Creatslots()
    {
        itemsdisplayed = new Dictionary<GameObject, Inventoryslot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryicon, Vector3.zero, Quaternion.identity, transform);                                                   // spawnt das icon im inventar
            obj.GetComponent<RectTransform>().localPosition = getinventoryposi(i);

            itemsdisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }
    public Vector3 getinventoryposi(int i)
    {
        return new Vector3(xStart + (x_space_between_items * (i % numberofcolumn)), yStart + (-y_space_between_items * (i / numberofcolumn)), 0f);
    }
}
