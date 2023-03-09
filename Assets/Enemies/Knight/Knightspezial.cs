using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knightspezial : MonoBehaviour
{
    [SerializeField] private GameObject knightcontroller;

    const string spezial2attackstate = "Spezial2";

    private void secondani()
    {
        GetComponent<Enemymovement>().ChangeAnimationState(spezial2attackstate);
    }
    private void knightspezial()
    {
        knightcontroller.SetActive(true);
        Statics.dash = true;
    }
}
