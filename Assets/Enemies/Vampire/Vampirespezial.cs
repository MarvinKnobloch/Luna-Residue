using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirespezial : MonoBehaviour
{
    [SerializeField] private Vampirecontroller vampirecontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = vampirecontroller.GetComponentInParent<Enemyspezialsound>();
    }
    public void castspezialattack()
    {
        vampirecontroller.gameObject.SetActive(true);
    }
    private void vampirespezialaudio()
    {
        enemyspezialsound.playvampirespezial();
    }
}
