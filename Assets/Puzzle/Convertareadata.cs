using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convertareadata
{
    public string areaname;

    public bool[] tutorialcomplete;

    public bool[] enemychestcanopen;
    public bool[] enemychestisopen;

    public bool[] questcomplete;

    public bool[] puzzlecomplete;
    public bool[] gotpuzzlereward;

    public int[] npcdialoguestate;

    public bool[] gotgatheritem;

    public void loadareadata(Areacontroller areacontroller)
    {
        for (int i = 0; i < tutorialcomplete.Length; i++)
        {
            areacontroller.tutorialcomplete[i] = tutorialcomplete[i];
        }

        for (int i = 0; i < enemychestcanopen.Length; i++)
        {
            areacontroller.enemychestcanopen[i] = enemychestcanopen[i];
        }

        for (int i = 0; i < enemychestisopen.Length; i++)
        {
            areacontroller.enemychestisopen[i] = enemychestisopen[i];
        }

        for (int i = 0; i < questcomplete.Length; i++)
        {
            areacontroller.questcomplete[i] = questcomplete[i];
        }

        for (int i = 0; i < puzzlecomplete.Length; i++)
        {
            areacontroller.puzzlecomplete[i] = puzzlecomplete[i];
        }

        for (int i = 0; i < gotpuzzlereward.Length; i++)
        {
            areacontroller.gotpuzzlereward[i] = gotpuzzlereward[i];
        }

        for (int i = 0; i < npcdialoguestate.Length; i++)
        {
            areacontroller.npcdialoguestate[i] = npcdialoguestate[i];
        }

        for (int i = 0; i < gotgatheritem.Length; i++)
        {
            areacontroller.gotgatheritem[i] = gotgatheritem[i];
        }
    }
}
