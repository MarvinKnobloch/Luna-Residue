using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mathmanspezial : MonoBehaviour
{
    [SerializeField] private Mathmancontroller spezialcontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = spezialcontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void mathspezial()
    {
        if (Statics.dash == false)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtostun();
            spezialcontroller.gameObject.SetActive(true);
        }
    }
    private void mathmanspezialaudio()
    {
        enemyspezialsound.playmathmanspezial();
    }
}
