using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuemove : MonoBehaviour, Interactioninterface
{
    [SerializeField] private string actiontext = "Move";
    public string Interactiontext => actiontext;
    private Statuecontroller statuecontroller;
    public int movenumber;
    private void Awake()
    {
        statuecontroller = GetComponentInParent<Statuecontroller>();
    }
    public bool Interact(Closestinteraction interactor)
    {
        {
            if (statuecontroller.ismoving == false)
            {
                statuecontroller.startmoving(movenumber);
            }
        }
        return true;
    }
}
