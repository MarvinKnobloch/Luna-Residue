using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialoguestart : MonoBehaviour, Interactioninterface
{
    [SerializeField] private string interactiontext;
    public string Interactiontext => interactiontext;

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
