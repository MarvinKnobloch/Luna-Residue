using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Werewolfspezial : MonoBehaviour
{
    public GameObject spezialcontroller;

    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = spezialcontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void werewolfspezial()
    {
        spezialcontroller.SetActive(true);
    }
    private void werewolfspezialaudio()
    {
        if(enemyspezialsound != null)                //wegen tutorial
        {
            enemyspezialsound.playwerewolfspezial();
        }
    }
}
