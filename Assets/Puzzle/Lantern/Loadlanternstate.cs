using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadlanternstate : MonoBehaviour, Interactioninterface
{
    public string Interactiontext => "Load";

    [SerializeField] GameObject[] lanterns;

    public bool Interact(Closestinteraction interactor)
    {
        foreach (GameObject lantern in lanterns)
        {
            lantern.GetComponent<Changeform>().loadlanternstate();
        }
        return true;
    }

}
