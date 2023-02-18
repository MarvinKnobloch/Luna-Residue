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
        //LoadCharmanager.Overallmainchar.GetComponent<Movescript>().ChangeAnimationStateInstant(slowdazestate);
        //LoadCharmanager.Overallmainchar.GetComponent<Movescript>().state = Movescript.State.Slow;
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().slowplayer(4);
        Statics.resetvaluesondeathorstun = true;                //triggert eine funktion die alle werte zurück setzt(im waffen script)
        Statics.otheraction = true;
        Statics.slow = true;
        Statics.dash = true;
    }
}
