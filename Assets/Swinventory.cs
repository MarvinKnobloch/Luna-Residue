using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Swinventory : MonoBehaviour
{
    public int test;
    public string FILENAME = "SW";

    private void Awake()
    {
        //LoadDataFromFile();
    }


    /*public void SaveToFile(int slot)
    {
        var filePath = Path.Combine(Application.persistentDataPath, FILENAME + slot + ".json");

        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }

        var json = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, json);
    }*/


    public void LoadDataFromFile(int slot)
    {
        var filePath = Path.Combine(Application.persistentDataPath, FILENAME + slot + ".json");

        if (!File.Exists(filePath))
        {
            return;
        }
        Debug.Log("load");
        var json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, this);
    }
    /*
    public void SaveGameData(int slot)
    {
        string savepath = Path.Combine(Application.persistentDataPath, FILENAME);
        if (!File.Exists(savepath))
        {
            File.Create(savepath);
        }

        var json = JsonUtility.ToJson(this);
        File.WriteAllText(savepath, json);
    }
    public void LoadGameData(int slot)
    {
        string loadpath = Path.Combine(Application.persistentDataPath, FILENAME);
        if (File.Exists(loadpath))
        {
            JsonUtility.FromJsonOverwrite(loadpath, this);
        }
        else
        {
            Debug.Log("file doesnt exsist");
        }
    }*/
}