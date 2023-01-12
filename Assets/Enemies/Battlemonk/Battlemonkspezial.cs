using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlemonkspezial : MonoBehaviour
{
    [SerializeField] private GameObject battlemonktimer;

    const string spezial2attackstate = "Spezial2";

    private void battlemonksecondani()
    {
        GetComponent<Enemymovement>().ChangeAnimationState(spezial2attackstate);
    }
    private void battlemonkspezial()
    {
        battlemonktimer.SetActive(true);
    }
}
