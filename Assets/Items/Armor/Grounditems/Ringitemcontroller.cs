using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringitemcontroller : MonoBehaviour
{
    public Itemcontroller item;
    public Itemcontroller seconditem;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Movescript>())
        {
            GameObject player = other.gameObject;
            player.GetComponent<Movescript>().ringinventory.Addequipment(player, item, seconditem, 1);
            Destroy(transform.parent.gameObject);
        }
    }
}
