using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Firstarea : MonoBehaviour
{
    private Areacontroller areacontroller;

    public bool laserpuzzlecomplete;
    public bool boxpuzzlecomplete;
    public bool attacktutorialcomplete;
    public bool healtutorialcomplete;
    public bool startingzonechest;
    private void Awake()
    {
        areacontroller = GetComponent<Areacontroller>();
        areacontroller.areatosave = this;
        if(Statics.currentgameslot != -1)                             // -1 = new game damit nichts geladen wird
        {
            loadmonobehaviour(Statics.currentgameslot, areacontroller.areaname, this);
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

        if(Statics.currentgameslot != 0)                                      // hier werden die geladen forschritten in den autosave slot gespeichert
        {
            autosave();
        }
    }

    public void autosave()                                                   // beim abschlieﬂen eines gebiets fortschritte, werden die im save slot 0 gespeichtert, dieser(slot0) wird auch beim gameover wieder geladen
    {
        Statics.currentgameslot = 0;
        string savepath = "/" + areacontroller.areaname + Statics.currentgameslot + ".json";
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
