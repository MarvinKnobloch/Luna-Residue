using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using setplattforms;

public class Checkplattform : MonoBehaviour
{
    public int plattformnumber;

    public static event Action resetplattforms;
    public static event Action puzzlefinish;

    private void OnEnable()
    {
        plattformnumber = -2;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            if(plattformnumber == Setplattforms.currentplattformnumber)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                Setplattforms.currentplattformnumber++;
                if(Setplattforms.currentplattformnumber >= Setplattforms.neededplattformnumber)
                {
                    puzzlefinish?.Invoke();
                }
            }
            else
            {
                resetplattforms?.Invoke();
                Setplattforms.currentplattformnumber = -1;
            }
        }
    }
}
