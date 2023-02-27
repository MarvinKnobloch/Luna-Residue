using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Golditemcontroller : MonoBehaviour
{
    [SerializeField] private Itemcontroller item;
    [SerializeField] private Inventorycontroller matsinventory;
    [NonSerialized] public int golddropamount;
    private bool pickuponce;

    private void Awake()
    {
        GetComponent<SphereCollider>().radius = 1;
    }
    private void OnEnable()
    {
        pickuponce = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && pickuponce == true)
        {
            pickuponce = false;
            matsinventory.Additem(item, golddropamount);
            gameObject.SetActive(false);
        }
    }
}
