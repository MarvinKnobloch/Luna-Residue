using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlemonkspezial : MonoBehaviour
{
    [SerializeField] private GameObject battlemonkcontroller;

    const string spezial2attackstate = "Spezial2";

    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = battlemonkcontroller.GetComponentInParent<Enemyspezialsound>();
    }

    private void battlemonksecondani()
    {
        GetComponent<Enemymovement>().ChangeAnimationState(spezial2attackstate);
    }
    private void battlemonkspezial()
    {
        battlemonkcontroller.SetActive(true);
    }
    private void battlemonkspezialaudio() => enemyspezialsound.playbattlemonkspezial();
}
