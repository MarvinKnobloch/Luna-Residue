using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirespezial : MonoBehaviour
{
    [SerializeField] private GameObject vampirecontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = vampirecontroller.GetComponentInParent<Enemyspezialsound>();
    }
    public void castspezialattack()
    {
        vampirecontroller.SetActive(true);
    }
    private void vampirespezialaudio()
    {
        enemyspezialsound.playvampirespezial();
    }
}
