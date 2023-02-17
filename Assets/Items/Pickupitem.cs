using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupitem : MonoBehaviour
{
    [SerializeField] private Itemcontroller item;
    [SerializeField] private Itemcontroller seconditem;
    [SerializeField] private Inventorycontroller inventory;
    private bool pickuponce;

    private void OnEnable()
    {
        pickuponce = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject && pickuponce == true)
        {
            pickuponce = false;
            inventory.Addequipment(item, seconditem, 1);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
