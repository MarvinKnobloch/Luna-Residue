using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactioninterface
{
    public string Interactiontext { get; }
    public bool Interact(Closestinteraction interactor);

    //public void interact(Closestinteraction interactor);                     sollte auch als ganz normal funktion gehen anstatt bool, keine ahnung wieso das im video so gemacht worden ist  
}
