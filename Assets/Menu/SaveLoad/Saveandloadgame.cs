using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.IO;


public class Saveandloadgame : MonoBehaviour
{
    private Isaveload loadsaveinterface = new Saveloadgame();
    [SerializeField] private Loadmenucontroller loadmenucontroller;

    private Convertstatics convertstatics = new Convertstatics();
    [SerializeField] private Setitemsandinventory setitemsandinventory;

    public void savegamedata(int slot)
    {
        saveinventorys(slot);
        savestaticsinscript();
        savestatics(slot);
    }
    private void savestaticsinscript()
    {
        convertstatics.playerposition = LoadCharmanager.savemainposi;
        convertstatics.playerrotation = LoadCharmanager.savemainrota;
        convertstatics.camvalueX = LoadCharmanager.savecamvalueX;
        convertstatics.charcurrentlvl = Statics.charcurrentlvl;
        convertstatics.charcurrentexp = Statics.charcurrentexp;
        convertstatics.charrequiredexp = Statics.charrequiredexp;
        convertstatics.gamesavedate = System.DateTime.UtcNow.ToString("dd MMMM, yyyy");
        convertstatics.gamesavetime = System.DateTime.UtcNow.ToString("HH:mm");
    }
    private void savestatics(int slot)
    {
        string savepath = "/Statics" + slot + ".json";
        if (loadsaveinterface.savedata(savepath, convertstatics))
        {
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
    private void saveinventorys(int slot)
    {
        for (int i = 0; i < setitemsandinventory.inventorys.Length; i++)
        {
            string savepath = "/" + setitemsandinventory.inventorys[i].Container.savepathname + slot + ".json";
            if (loadsaveinterface.savedata(savepath, setitemsandinventory.inventorys[i]))
            {
                continue;
            }
            else
            {
                Debug.Log("Error: Could not save Data");
            }
        }
    }

    public void loadgamedate()
    {
        int slot = loadmenucontroller.selectedslot;
        loadstaticdata(slot);
        setstaticsafterload();
        SceneManager.LoadScene(1);
        loadinventorys(slot);
        setitemsandinventory.resetitems();
        setitemsandinventory.updateitemsininventory();
    }
    private void loadstaticdata(int slot)
    {
        string loadpath = "/Statics" + slot + ".json";
        try
        {
            convertstatics = loadsaveinterface.loaddata<Convertstatics>(loadpath);
        }
        catch (Exception e)
        {
            Debug.LogError($"error Could not load data {e.Message} {e.StackTrace}");
        }
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
    private void loadinventorys(int slot)
    {
        for (int i = 0; i < setitemsandinventory.inventorys.Length; i++)
        {
            var filePath = Path.Combine(Application.persistentDataPath, setitemsandinventory.inventorys[i].Container.savepathname + slot + ".json");
            if (!File.Exists(filePath))
            {
                continue;
            }
            else
            {
                var json = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(json, setitemsandinventory.inventorys[i]);
            }
        }
    }
}
