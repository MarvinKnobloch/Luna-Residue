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
        swinventory.LoadDataFromFile(loadmenucontroller.selectedslot);
        int slot = loadmenucontroller.selectedslot;
        string loadpath = "/Statics" + slot + ".json";
        try
        {
            convertstatics = loadsaveinterface.loaddata<Convertstatics>(loadpath);
            Debug.Log("data has been loaded");
            setstaticsafterload();
        }
        catch (Exception e)
        {
            Debug.LogError($"error Could not load data {e.Message} {e.StackTrace}");
        }
    }


    /*public void LoadGameData(int slot)
    {
        string loadpath = Path.Combine(Application.persistentDataPath, "SW" + slot + ".json");
        if (File.Exists(loadpath))
        {
            JsonUtility.FromJsonOverwrite(loadpath, swinventory);
        }
        else
        {
            Debug.Log("file doesnt exsist");
        }
    }*/
    public void Loadsw()
    {
        //int slot = loadmenucontroller.selectedslot;
        //string loadpath = "/SW" + slot + ".json";

            //JsonUtility.FromJsonOverwrite(loadpath, swinventory);
            //swinventory = loadsaveinterface.loaddata<Swinventory>(loadpath);
            //Debug.Log("data has been loaded");

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
            SceneManager.LoadScene(1);
        }
    }
}

/*convertgamevalues.playerposix = LoadCharmanager.savemainposi.x;
convertgamevalues.playerposiy = LoadCharmanager.savemainposi.y;
convertgamevalues.playerposiz = LoadCharmanager.savemainposi.z;
convertgamevalues.playerrotax = LoadCharmanager.savemainrota.x;
convertgamevalues.playerrotay = LoadCharmanager.savemainrota.y;
convertgamevalues.playerrotaz = LoadCharmanager.savemainrota.z;
convertgamevalues.playerrotaw = LoadCharmanager.savemainrota.w;
        convertgamevalues.charcurrentlvl = Statics.charcurrentlvl;
        convertgamevalues.charcurrentexp = Statics.charcurrentexp;
        convertgamevalues.charrequiredexp = Statics.charrequiredexp;


            Statics.charcurrentlvl = convertgamevalues.charcurrentlvl;
            Statics.charcurrentexp = convertgamevalues.charcurrentexp;
            Statics.charrequiredexp = convertgamevalues.charrequiredexp;*/

/*Statics.currentplayerposix = LoadCharmanager.savemainposi.x;         //savemainposi wird gespeichter wenn man in menü geht;
Statics.currentplayerposiy = LoadCharmanager.savemainposi.y;
Statics.currentplayerposiz = LoadCharmanager.savemainposi.z;
Statics.currentplayerrotationx = LoadCharmanager.savemainrota.x;
Statics.currentplayerrotationy = LoadCharmanager.savemainrota.y;
Statics.currentplayerrotationz = LoadCharmanager.savemainrota.z;
Statics.currentplayerrotationw = LoadCharmanager.savemainrota.w;*/

//LoadCharmanager.savemainposi = new Vector3(Statics.currentplayerposix, Statics.currentplayerposiy, Statics.currentplayerposiz);
//LoadCharmanager.savemainrota.Set(Statics.currentplayerrotationx, Statics.currentplayerrotationy, Statics.currentplayerrotationz, Statics.currentplayerrotationw);
