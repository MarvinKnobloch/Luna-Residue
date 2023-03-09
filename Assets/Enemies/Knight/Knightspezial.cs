using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knightspezial : MonoBehaviour
{
    [SerializeField] private GameObject knightcontroller;

    const string spezial2attackstate = "Spezial2";
    const string slowdazestate = "Slowdaze";

    private void secondani()
    {
        GetComponent<Enemymovement>().ChangeAnimationState(spezial2attackstate);
    }
    private void knightspezial()
    {
        knightcontroller.SetActive(true);
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().slowplayer(4);
        Statics.resetvaluesondeathorstun = true;
        Statics.dash = true;
    }
}
