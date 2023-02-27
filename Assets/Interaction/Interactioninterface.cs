using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactioninterface
{
    public string Interactiontext { get; }
    public bool Interact(Closestinteraction interactor);
}
