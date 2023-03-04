using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, Interactioninterface
{
    [SerializeField] private string actiontext = "Rotate";
    public string Interactiontext => actiontext;
    public bool Interact(Closestinteraction interactor)
    {
        transform.Rotate(0, 90, 0, Space.Self);
        return true;
    }
}
