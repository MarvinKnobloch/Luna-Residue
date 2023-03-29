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
        overridedata(tutorialcomplete, areacontroller.tutorialcomplete);
        overridedata(enemychestcanopen, areacontroller.enemychestcanopen);
        overridedata(enemychestisopen, areacontroller.enemychestisopen);
        overridedata(questcomplete, areacontroller.questcomplete);
        overridedata(puzzlecomplete, areacontroller.puzzlecomplete);
        overridedata(gotpuzzlereward, areacontroller.gotpuzzlereward);
        overridedata(npcdialoguestate, areacontroller.npcdialoguestate);
        overridedata(gotgatheritem, areacontroller.gotgatheritem);
        /*for (int i = 0; i < tutorialcomplete.Length; i++)
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
        }*/
    }
    private void overridedata<T>(T[] savedataarray, T[] areaarray)
    {
        for (int i = 0; i < savedataarray.Length; i++)
        {
            areaarray[i] = savedataarray[i];
        }
    }
}

/*
if (savedataarray.Length <= areaarray.Length)           // zum rauspatchen, funktioniert aber nicht, wenn was rausgenommen wird, verschiebt sich dann die completed check anstatt das es rausgenommen wird. 
        {
            for (int i = 0; i < savedataarray.Length; i++)
            {
                areaarray[i] = savedataarray[i];
            }
        }
        else
        {
            for (int i = 0; i < areaarray.Length; i++)
            {
                areaarray[i] = savedataarray[i];
            }
        }
*/