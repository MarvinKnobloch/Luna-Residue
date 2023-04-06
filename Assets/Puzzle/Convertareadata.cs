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

    public bool[] gotfasttravelpoint;

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
        overridedata(gotfasttravelpoint, areacontroller.gotfasttravelpoint);
    }
    private void overridedata<T>(T[] savedataarray, T[] areaarray)
    {
        if(savedataarray != null)
        {
            for (int i = 0; i < savedataarray.Length; i++)
            {
                areaarray[i] = savedataarray[i];
            }
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
