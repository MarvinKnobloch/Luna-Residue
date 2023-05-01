using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Armorinventoryui : MonoBehaviour
{
    [SerializeField] private Inventorycontroller inventory;

    Dictionary<GameObject, Inventoryslot> itemsdisplayed = new Dictionary<GameObject, Inventoryslot>();                         // Das Dictionary welches die Inventoryslots speichert

    private void Awake()
    {
        itemsdisplayed = new Dictionary<GameObject, Inventoryslot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            itemsdisplayed.Add(gameObject.transform.GetChild(i).gameObject, inventory.Container.Items[i]);
        }
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

                _slot.Key.transform.GetChild(0).GetComponent<Image>().sprite = _slot.Value.item.Uisprite;
                _slot.Key.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.itemname;
                _slot.Key.transform.GetChild(2).gameObject.SetActive(true);
                _slot.Key.transform.GetChild(3).gameObject.SetActive(true);
                _slot.Key.transform.GetChild(3).GetComponentInChildren<Text>().text = _slot.Value.item.upgradelvl.ToString();

                _slot.Key.transform.GetComponent<Chooseitem>().upgradelvltext = _slot.Key.transform.GetChild(2).GetComponentInChildren<Text>();          //hier wird chooseitem der upgradetext zugewiesen
            }
            else
            {
                _slot.Key.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "???";
                _slot.Key.transform.GetChild(2).gameObject.SetActive(false);                  // Image und lvl der Waffe
                _slot.Key.transform.GetChild(3).gameObject.SetActive(false);
            }
            _slot.Key.transform.GetComponent<Chooseitem>().buttonupdate();
        }
    }
}
