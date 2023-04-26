using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gasmanspezial : MonoBehaviour
{
    [SerializeField] private Gasmancontroller spezialcontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = spezialcontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void gasmanspezial()
    {
        spezialcontroller.gameObject.SetActive(true);
    }
    private void gasmanspezialaudio() => enemyspezialsound.playgasmanspezial();
}
