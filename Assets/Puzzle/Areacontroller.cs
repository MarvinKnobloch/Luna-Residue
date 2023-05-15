using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Areacontroller : MonoBehaviour
{
    private Areaobjectcontroller areaobjectcontroller;

    private Isaveload loadsaveinterface = new Saveloadgame();
    private Convertareadata convertareadata = new Convertareadata();                   //beim laden werden die daten zwischen gelagert und dann convertiert, damit ich die arraylength patchen kann
                                                                                       //areacontroller und convertarea data sollten die gleichen Variablen beinhalten, sonst treten vll fehler beim laden auf
    public string areaname;

    public bool[] tutorialcomplete;                        //die arrays müssen public(nicht Nonserialized) sein, sonst speichert json die daten nicht

    public bool[] enemychestcanopen;
    public bool[] enemychestisopen;

    public bool[] questcomplete;

    public bool[] puzzlecomplete;
    public bool[] gotpuzzlereward;

    public int[] npcdialoguestate;

    public bool[] gotgatheritem;

    public bool[] gotfasttravelpoint;

    private void Awake()
    {
        areaobjectcontroller = GetComponent<Areaobjectcontroller>();
        tutorialcomplete = new bool[areaobjectcontroller.settutorialnumber.Length];
        enemychestcanopen = new bool[areaobjectcontroller.setenemychests.Length];
        enemychestisopen = new bool[areaobjectcontroller.setenemychests.Length];
        questcomplete = new bool[areaobjectcontroller.setquestnumber.Length];
        puzzlecomplete = new bool[areaobjectcontroller.setpuzzlenumber.Length];
        gotpuzzlereward = new bool[areaobjectcontroller.setpuzzlenumber.Length];
        npcdialoguestate = new int[areaobjectcontroller.setdialoguenumber.Length];
        gotgatheritem = new bool[areaobjectcontroller.setgahteringnumber.Length];
        gotfasttravelpoint = new bool[areaobjectcontroller.fasttravelpoints.Length];

        if (Statics.currentgameslot != -1)                             // -1 = new game damit nichts geladen wird
        {
            loaddata(Statics.currentgameslot, areaname);                         //bei gameload werden hier die fortschritte geladen
            if (convertareadata != null)
            {
                convertareadata.loadareadata(this);
            }

            if (Statics.currentgameslot != 0)                                      // hier werden die geladen forschritten in den autosave slot gespeichert
            {
                autosave();
            }
        }
        for (int i = 0; i <  areaobjectcontroller.setenemychests.Length; i++)
        {
            areaobjectcontroller.setenemychests[i].GetComponent<Chestinterface>().setareachestnumber(i);
        }
        for (int i = 0; i < areaobjectcontroller.settutorialnumber.Length; i++)
        {
            areaobjectcontroller.settutorialnumber[i].areanumber = i;
        }
        for (int i = 0; i < areaobjectcontroller.setquestnumber.Length; i++)
        {
            areaobjectcontroller.setquestnumber[i].areanumber = i;
        }
        for (int i = 0; i < areaobjectcontroller.setpuzzlenumber.Length; i++)
        {
            areaobjectcontroller.setpuzzlenumber[i].areanumber = i;
        }
        for (int i = 0; i < areaobjectcontroller.setdialoguenumber.Length; i++)
        {
            areaobjectcontroller.setdialoguenumber[i].areanumber = i;
        }
        for (int i = 0; i < areaobjectcontroller.setgahteringnumber.Length; i++)
        {
            areaobjectcontroller.setgahteringnumber[i].areanumber = i;
        }
        for (int i = 0; i < areaobjectcontroller.fasttravelpoints.Length; i++)
        {
            areaobjectcontroller.fasttravelpoints[i].areanumber = i;
        }
    }
    private void loaddata(int slot, string filename)                  
    {
        string loadpath = "/" + filename + slot + ".json";
        try
        {
            convertareadata = loadsaveinterface.loaddata<Convertareadata>(loadpath);
        }
        catch (Exception e)
        {
            Debug.LogError($"error Could not load data {e.Message} {e.StackTrace}");
        }
    }

    public void autosave()                                                   // beim abschließen eines gebiets fortschritte, werden die im save slot 0 gespeichtert, dieser(slot0) wird auch beim gameover wieder geladen
    {
        Statics.currentgameslot = 0;
        string savepath = "/" + areaname + Statics.currentgameslot + ".json";
        saveinautosaveslot(savepath, this);
    }
    private bool saveinautosaveslot<T>(string dataname, T data)
    {
        string path = Application.persistentDataPath + dataname;
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string json_data = JsonUtility.ToJson(data);
            File.WriteAllText(path, json_data);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"error {e.Message} {e.StackTrace}");
            return false;
        }
    }
}

/*private void loadmonobehaviour(int slot, string filename, MonoBehaviour monobehaviour)            //bei gameload werden hier die fortschritte geladen
{
    var filePath = Path.Combine(Application.persistentDataPath, filename + slot + ".json");
    if (File.Exists(filePath))
    {
        var json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, monobehaviour);
    }

    if (Statics.currentgameslot != 0)
    {
        autosave();
    }
}*/
