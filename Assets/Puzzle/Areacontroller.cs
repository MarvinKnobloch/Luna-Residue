using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Areacontroller : MonoBehaviour
{
    private Areaobjectcontroller areaobjectcontroller;

    public string areaname;

    public bool[] tutorialcomplete;                        //die arrays müssen public(nicht Nonserialized) sein, sonst speichert json die daten nicht

    public bool[] enemychestcanopen;
    public bool[] enemychestisopen;

    public bool[] questcomplete;

    public bool[] puzzlecomplete;
    public bool[] gotpuzzlereward;

    public int[] npcdialoguestate;

    public bool[] gotgatheritem;

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

        //Statics.currentgameslot = -1;
        //Debug.Log(Statics.currentgameslot);
        if (Statics.currentgameslot != -1)                             // -1 = new game damit nichts geladen wird
        {
            loadmonobehaviour(Statics.currentgameslot, areaname, this);
        }
        for (int i = 0; i <  areaobjectcontroller.setenemychests.Length; i++)
        {
            areaobjectcontroller.setenemychests[i].GetComponent<Chestreward>().areachestnumber = i;
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
    }
    private void loadmonobehaviour(int slot, string filename, MonoBehaviour monobehaviour)            //bei gameload werden hier die fortschritte geladen
    {
        var filePath = Path.Combine(Application.persistentDataPath, filename + slot + ".json");
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, monobehaviour);
        }

        if (Statics.currentgameslot != 0)                                      // hier werden die geladen forschritten in den autosave slot gespeichert
        {
            autosave();
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
