using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Swordinventoryui : MonoBehaviour
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
        foreach (KeyValuePair<GameObject,Inventoryslot> _slot in itemsdisplayed)
        {
            if(_slot.Value.amount > 0)
            {
                _slot.Key.transform.GetComponent<Chooseweapon>().itemvalues = _slot.Value.item;
                _slot.Key.transform.GetComponent<Chooseweapon>().equipslotnumber = 0;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.item.Uisprite;
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Text>().text = _slot.Value.itemname.ToString();               
            }
            else
            {
                _slot.Key.transform.GetChild(1).GetComponentInChildren<Text>().text = "empty";
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
            //obj.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");                            // n0 = wenn �ber 1000 wird ein komma angezeigt
            //obj.gameObject.GetComponentInChildren<Text>().text = inventory.Container[i].item.ToString();
        }
    }
    public Vector3 getinventoryposi(int i)
    {
        return new Vector3(xStart + (x_space_between_items * (i % numberofcolumn)), yStart + (-y_space_between_items * (i / numberofcolumn)), 0f);
    }
}

/*public void creatitemlist()
{
    for (int i = 0; i < inventory.Container.Count; i++)
    {
        var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = getinventoryposi(i);
        obj.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");                            // n0 = geht �ber 1000
        obj.gameObject.GetComponentInChildren<Text>().text = inventory.Container[i].item.ToString();
    }
}*/

/*public void itemlistupdate()
{
    for (int i = 0; i < inventory.Container.Count; i++)
    {
        if (itemsdisplayed.ContainsKey(inventory.Container[i]))
        {
            itemsdisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
        }
        else
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getinventoryposi(i);
            obj.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");                            // n0 = geht �ber 1000
            obj.gameObject.GetComponentInChildren<Text>().text = inventory.Container[i].item.ToString();
            itemsdisplayed.Add(inventory.Container[i], obj);
        }
    }
}*/