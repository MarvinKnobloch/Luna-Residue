using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doozyspezial : MonoBehaviour
{
    [SerializeField] private GameObject doozycontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = doozycontroller.GetComponentInParent<Enemyspezialsound>();
    }

    private void doozyspezial()
    {
        doozycontroller.SetActive(true);
    }
    private void doozyspezialaudio() => enemyspezialsound.playdoozyspezial();
}
