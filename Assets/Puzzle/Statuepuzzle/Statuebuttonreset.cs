using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuebuttonreset : MonoBehaviour, Interactioninterface
{
    public GameObject[] Statues;

    [SerializeField] private string actiontext = "Reset";
    public string Interactiontext => actiontext;

    public bool Interact(Closestinteraction interactor)
    {
        {
            foreach(GameObject obj in Statues)
            {
                obj.gameObject.GetComponent<Statuecontroller>().resetstatues();
            }
        }
        return true;
    }
}
