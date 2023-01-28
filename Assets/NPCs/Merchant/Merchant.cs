using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour, Interactioninterface
{
    public string Interactiontext => "Talk";

    public bool Interact(Closestinteraction interactor)
    {
        if(LoadCharmanager.interaction == false)
        {
            gameObject.GetComponent<Npcdialogue>().enabled = true;
        }
        return true;
    }
    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<Npcdialogue>().enddialogue();
    }
}
