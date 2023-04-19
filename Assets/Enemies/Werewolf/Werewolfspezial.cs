using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Werewolfspezial : MonoBehaviour
{
    public GameObject werewolfcontroller;

    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = werewolfcontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void werewolfspezial()
    {
        werewolfcontroller.SetActive(true);
    }
    private void werewolfspezialaudio()
    {
        enemyspezialsound.playwerewolfspezial();
    }
}
