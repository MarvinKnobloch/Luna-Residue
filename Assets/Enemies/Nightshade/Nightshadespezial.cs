using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nightshadespezial : MonoBehaviour
{
    [SerializeField] private Nightshadecontroller nightshadecontroller;
    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = nightshadecontroller.GetComponentInParent<Enemyspezialsound>();
    }
    private void nightshadespezial()
    {
        nightshadecontroller.enemy = gameObject;
        nightshadecontroller.gameObject.SetActive(true);
    }
    private void nightshadespezialaudio()
    {
        enemyspezialsound.playnightshadespezial();
    }
}
