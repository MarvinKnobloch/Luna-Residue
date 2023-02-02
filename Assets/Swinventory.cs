using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Swinventory : MonoBehaviour
{
    public string FILENAME { get; } = "Swordinventory";
    [SerializeField] private Itemtest[] items = new Itemtest[28];

    private void Awake()
    {
        //LoadDataFromFile();
    }


    /*public void Savesw(int slot)
    {
        string savepath = "/" + swinventory.FILENAME + slot + ".json";
        if (loadsaveinterface.savedata(savepath, swinventory))
        {
            Debug.Log("Data was saved");
        }
        else
        {
            Debug.Log("Error: Could not save Data");
        }
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
}