using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laseractivate : MonoBehaviour, Interactioninterface
{
    [SerializeField] private GameObject Laser;
    [SerializeField] private string actiontext = "Activate";
    public string Interactiontext => actiontext;

    public bool Interact(Closestinteraction interactor)
    {
        if(Laser.activeSelf == false)
        {
            Laser.SetActive(true);
        }
        else
        {
            Laser.SetActive(false);
        }
        return true;
    }
}
