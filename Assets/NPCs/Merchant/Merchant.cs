using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour, Interactioninterface
{
    public string Interactiontext => "Talk";

    public bool Interact(Closestinteraction interactor)
    {
        gameObject.GetComponent<Npcdialogue>().enabled = true;
        return true;
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("test");
        gameObject.GetComponent<Npcdialogue>().enddialogue();
    }
}
