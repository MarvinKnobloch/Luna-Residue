using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twosidemirror : MonoBehaviour, Interactioninterface
{
    [SerializeField] private string actiontext = "Rotate";
    public string Interactiontext => actiontext;
    public bool Interact(Closestinteraction interactor)
    {
        transform.parent.Rotate(0, 90, 0, Space.Self);
        return true;
    }
}
