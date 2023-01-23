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

    public bool isportal;
    public Vector3 portalend;

    const string fly = "Stormchainlightning";

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
                if (isportal == true)
                {
                    Statics.otheraction = true;
                    LoadCharmanager.Overallmainchar.GetComponent<Movescript>().movetowardsposition = portalend;
                    LoadCharmanager.Overallmainchar.GetComponent<Movescript>().ChangeAnimationState(fly);
                    LoadCharmanager.Overallmainchar.GetComponent<Movescript>().state = Movescript.State.Movetowards;
                }
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
