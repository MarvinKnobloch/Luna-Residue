using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblinspezial : MonoBehaviour
{
    [SerializeField] private GameObject goblincontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = goblincontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void goblinspezial()
    {
        goblincontroller.SetActive(true);
    }
    private void goblinspezialaudio()
    {
        enemyspezialsound.playgoblinspezial();
    }
}