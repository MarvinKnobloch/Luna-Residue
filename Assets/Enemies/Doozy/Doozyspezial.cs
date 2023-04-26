using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doozyspezial : MonoBehaviour
{
    [SerializeField] private Doozycontroller spezialcontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = spezialcontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void doozyspezial()
    {
        spezialcontroller.gameObject.SetActive(true);
    }
    private void doozyspezialaudio() => enemyspezialsound.playdoozyspezial();
}
