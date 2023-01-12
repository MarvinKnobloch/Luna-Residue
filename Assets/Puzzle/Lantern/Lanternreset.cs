using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanternreset : MonoBehaviour, Interactioninterface
{
    [SerializeField] private string actiontext = "Reset";
    public string Interactiontext => actiontext;

    public GameObject[] lanterns;

    public bool Interact(Closestinteraction interactor)
    {
        foreach(GameObject obj in lanterns)
        {
            obj.GetComponent<Changeform>().lanternreset();
        }
        return true;
    }
}
