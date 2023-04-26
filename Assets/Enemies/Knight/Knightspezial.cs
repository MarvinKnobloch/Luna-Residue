using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knightspezial : MonoBehaviour
{
    [SerializeField] private Knightcontroller spezialcontroller;

    const string spezial2attackstate = "Spezial2";

    private void secondani()
    {
        GetComponent<Enemymovement>().ChangeAnimationState(spezial2attackstate);
    }
    private void knightspezial()
    {
        spezialcontroller.gameObject.SetActive(true);
    }
}
