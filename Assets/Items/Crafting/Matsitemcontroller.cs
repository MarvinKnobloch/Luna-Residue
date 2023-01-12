using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matsitemcontroller : MonoBehaviour
{
    public Itemcontroller item;
    public void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponent<Movescript>())
        if(other.gameObject == LoadCharmanager.Overallmainchar)
        {
            GameObject player = other.gameObject;
            other.GetComponent<Movescript>().matsinventory.Additem(player, item, 1);
            Destroy(transform.parent.gameObject);
        }
    }
}
