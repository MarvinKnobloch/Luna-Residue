using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savelanternstate : MonoBehaviour, Interactioninterface
{
    public string Interactiontext => "Save";

    [SerializeField] private GameObject[] lanterns;

    public bool Interact(Closestinteraction interactor)
    {
        foreach (GameObject lantern in lanterns)
        {
            lantern.GetComponent<Changeform>().savelanternstate();
        }
        return true;
    }
}
