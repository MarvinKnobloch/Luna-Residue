using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mathmanspezial : MonoBehaviour
{
    [SerializeField] private GameObject mathmancontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = mathmancontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void mathspezial()
    {
        if (Statics.dash == false)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtostun();
            mathmancontroller.SetActive(true);
        }
    }
    private void mathmanspezialaudio()
    {
        enemyspezialsound.playmathmanspezial();
    }
}
