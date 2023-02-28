using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Areacontroller : MonoBehaviour
{
    private Areaobjectcontroller areaobjectcontroller;

    [SerializeField] public string areaname;

    public bool[] tutorialcomplete;

    public bool[] enemychestcanopen;           //muss anzahl der chest sein
    public bool[] enemychestisopen;

    public bool[] puzzlecomplete;
    public bool[] gotpuzzlereward;

    private void Awake()
    {
        areaobjectcontroller = GetComponent<Areaobjectcontroller>();
        if (Statics.currentgameslot != -1)                             // -1 = new game damit nichts geladen wird
        {
            Debug.Log("loadarea");
            loadmonobehaviour(Statics.currentgameslot, areaname, this);
        }
        for (int i = 0; i <  areaobjectcontroller.enemychests.Length; i++)
        {
            areaobjectcontroller.enemychests[i].GetComponent<Chestreward>().areachestnumber = i;
        }
        for (int i = 0; i < areaobjectcontroller.settutorialnumber.Length; i++)
        {
            areaobjectcontroller.settutorialnumber[i].areanumber = i;
        }
        for (int i = 0; i < areaobjectcontroller.setpuzzlenumber.Length; i++)
        {
            areaobjectcontroller.setpuzzlenumber[i].areanumber = i;
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
