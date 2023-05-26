using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convertareadata
{
    public string areaname;

    public bool[] tutorialcomplete;

    public bool[] enemychestcanopen;
    public bool[] enemychestisopen;

    public bool[] puzzlecomplete;
    public bool[] gotpuzzlereward;

    public int[] npcdialoguestate;

    public bool[] gotgatheritem;

    public bool[] gotfasttravelpoint;

    public int[] questids;
    public bool[] questactiv;
    public bool[] questcomplete;

    public void loadareadata(Areacontroller areacontroller)
    {
        overridedata(tutorialcomplete, areacontroller.tutorialcomplete);
        overridedata(enemychestcanopen, areacontroller.enemychestcanopen);
        overridedata(enemychestisopen, areacontroller.enemychestisopen);
        overridedata(puzzlecomplete, areacontroller.puzzlecomplete);
        overridedata(gotpuzzlereward, areacontroller.gotpuzzlereward);
        overridedata(npcdialoguestate, areacontroller.npcdialoguestate);
        overridedata(gotgatheritem, areacontroller.gotgatheritem);
        overridedata(gotfasttravelpoint, areacontroller.gotfasttravelpoint);
        overridequestdata(questids, areacontroller);
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
    private void overridequestdata(int[] savedataarray, Areacontroller areacontroller)
    {
        if (savedataarray != null)
        {
            for (int i = 0; i < savedataarray.Length; i++)                 //durch jede gespeicherte quests wird geloopt
            {
                for (int t = 0; t < areacontroller.quests.Length; t++)                //durch jede quest in der area wird geloopt
                {
                    if(savedataarray[i] == areacontroller.quests[t].questid)           //wenn die ids der 2 geloopten gleich sind werden sie geladen, ansonsten verfallen sie beim nächsten speichern
                    {
                        areacontroller.quests[t].questactiv = questactiv[i];
                        areacontroller.questactiv[t] = questactiv[i];
                        areacontroller.quests[t].questcomplete = questcomplete[i];
                        areacontroller.questcomplete[t] = questcomplete[i];
                        areacontroller.questids[t] = questids[i];                 //unnötiger call, ist nur für übersicht im editor
                    }
                }
            }
        }
    }
}
