using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legitemcontroller : MonoBehaviour
{
    public Itemcontroller item;
    public Itemcontroller seconditem;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movescript>())
        {
            GameObject player = other.gameObject;
            player.GetComponent<Movescript>().beltinventory.Addequipment(player, item, seconditem, 1);
            Destroy(transform.parent.gameObject);
        }
    }
}
