using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.IO;


public class Saveloadstatics : MonoBehaviour
{
    private Isaveload loadsaveinterface = new Saveloadgame();
    private Convertstatics convertstatics = new Convertstatics();
    [SerializeField] private Inventorycontroller fistinventory;
    [SerializeField] private Swinventory swinventory;
    [SerializeField] GameObject test;
    [SerializeField] private Loadmenucontroller loadmenucontroller;

    public void savestaticsinscript(int slot)
    {
        convertstatics.playerposition = LoadCharmanager.savemainposi;
        convertstatics.playerrotation = LoadCharmanager.savemainrota;
        convertstatics.camvalueX = LoadCharmanager.savecamvalueX;
        convertstatics.charcurrentlvl = Statics.charcurrentlvl;
        convertstatics.charcurrentexp = Statics.charcurrentexp;
        convertstatics.charrequiredexp = Statics.charrequiredexp;
        convertstatics.gamesavedate = System.DateTime.UtcNow.ToString("dd MMMM, yyyy");
        convertstatics.gamesavetime = System.DateTime.UtcNow.ToString("HH:mm");
        Savestaticsdata(slot);
    }
    private void Savestaticsdata(int slot)
    {
        //swinventory.SaveToFile(slot);
        Savesw(slot);
        saveinventory(slot);
        string savepath = "/Statics" + slot + ".json";
        if (loadsaveinterface.savedata(savepath, convertstatics))
        {
            Debug.Log("Data was saved");
            Slotvaluesarray.slotisnotempty[slot] = true;
            Slotvaluesarray.slotlvl[slot] = convertstatics.charcurrentlvl;
            Slotvaluesarray.slotdate[slot] = convertstatics.gamesavedate;
            Slotvaluesarray.slottime[slot] = convertstatics.gamesavetime;
        }
        else
        {
            Debug.Log("Error: Could not save Data");
        }
    }
    public void saveinventory(int slot)
    {
        string savepath = "/Fistinventory" + slot + ".json";
        if (loadsaveinterface.savedata(savepath, fistinventory))
        {
            Debug.Log("Data was saved");
        }
        else
        {
            Debug.Log("Error: Could not save Data");
        }
    }
    public void Savesw(int slot)
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
    }

    public void Loadstaticdata()
    {
        int slot = loadmenucontroller.selectedslot;
        string loadpath = "/Statics" + slot + ".json";
        try
        {
            convertstatics = loadsaveinterface.loaddata<Convertstatics>(loadpath);
            Debug.Log("data has been loaded");
        }
        catch (Exception e)
        {
            Debug.LogError($"error Could not load data {e.Message} {e.StackTrace}");
        }
        setstaticsafterload();
        SceneManager.LoadScene(1);
        swinventory.LoadDataFromFile(slot);
        Loadfistinventory(slot);
    }
    public void Loadfistinventory(int slot)
    {
        var filePath = Path.Combine(Application.persistentDataPath, "Fistinventory" + slot + ".json");

        if (!File.Exists(filePath))
        {
            return;
        }
        Debug.Log("loadfistinventory");
        var json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, fistinventory);
    }



    private void setstaticsafterload()
    {
        if (convertstatics != null)
        {
            LoadCharmanager.savemainposi = convertstatics.playerposition;
            LoadCharmanager.savemainrota = convertstatics.playerrotation;
            LoadCharmanager.savecamvalueX = convertstatics.camvalueX;
            Statics.charcurrentlvl = convertstatics.charcurrentlvl;
            Statics.charcurrentexp = convertstatics.charcurrentexp;
            Statics.charrequiredexp = convertstatics.charrequiredexp;
        }
    }
}
