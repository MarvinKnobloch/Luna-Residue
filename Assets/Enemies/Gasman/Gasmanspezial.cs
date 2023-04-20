using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gasmanspezial : MonoBehaviour
{
    [SerializeField] private GameObject gasmancontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = gasmancontroller.GetComponentInParent<Enemyspezialsound>();
    }

    private void gasmanspezial()
    {
        gasmancontroller.SetActive(true);
    }
    private void gasmanspezialaudio() => enemyspezialsound.playgasmanspezial();
}
