using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matsitemcontroller : MonoBehaviour
{
    [SerializeField] private Itemcontroller item;
    [SerializeField] private Inventorycontroller matsinventory;
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
        if(other.gameObject == LoadCharmanager.Overallmainchar && pickuponce == true)
        {
            pickuponce = false;
            matsinventory.Additem(item, 1);
            Destroy(gameObject);
        }
    }
}
