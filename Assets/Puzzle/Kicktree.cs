using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicktree : MonoBehaviour, Interactioninterface
{
    [SerializeField] private string actiontext = "Kick Tree";
    private bool beenkicked;
    public GameObject item;
    public string Interactiontext => actiontext;

    public bool Interact(Closestinteraction interactor)
    {
        if(beenkicked == false && item !=null)
        {
            beenkicked = true;
            item.gameObject.SetActive(true);
        }
        return true;
    }
}
