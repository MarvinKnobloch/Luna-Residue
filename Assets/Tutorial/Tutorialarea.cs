using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialarea : MonoBehaviour
{
    [SerializeField] private Areacontroller areacontroller;
    private int tutorialnumber;
    void Start()
    {
        tutorialnumber = GetComponent<Areanumber>().areanumber;
        if (areacontroller.tutorialcomplete[tutorialnumber] == true)
        {
            gameObject.SetActive(false);
        }
    }
}
