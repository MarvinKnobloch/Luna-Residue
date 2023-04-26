using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblinspezial : MonoBehaviour
{
    [SerializeField] private Goblincontroller spezialcontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = spezialcontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void goblinspezial()
    {
        spezialcontroller.gameObject.SetActive(true);
    }
    private void goblinspezialaudio()
    {
        enemyspezialsound.playgoblinspezial();
    }
}