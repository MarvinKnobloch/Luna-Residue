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
    [SerializeField] private Areacontroller areacontroller;

    public void savegamedata(int slot)
    {
        saveinventorys(slot);
        convertstatics.savestaticsinscript();
        savestatics(slot);
        if (areacontroller != null)
        {
            savemonobehaviour(slot, "/" + areacontroller.areaname, areacontroller.areatosave);
        }
    }
    private void savestatics(int slot)
    {
        string savepath = "/Statics" + slot + ".json";
        if (loadsaveinterface.savedata(savepath, convertstatics))
        {
            Slotvaluesarray.slotisnotempty[slot -1] = true;
            Slotvaluesarray.slotlvl[slot -1] = convertstatics.charcurrentlvl;
            Slotvaluesarray.slotdate[slot -1] = convertstatics.gamesavedate;
            Slotvaluesarray.slottime[slot -1] = convertstatics.gamesavetime;
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
    private void savemonobehaviour(int slot, string filename, MonoBehaviour monobehaviour)
    {
        string savepath = filename + slot + ".json";
        if (loadsaveinterface.savedata(savepath, monobehaviour))
        {
            //
        }
        else
        {
            Debug.Log("Error: Could not save Data");
        }
    }

    public void loadgamedate()
    {
        int slot = loadmenucontroller.selectedslot + 1;
        Statics.currentgameslot = slot;
        loadstaticdata(slot);
        if (convertstatics != null)
        {
            convertstatics.setstaticsafterload();
        }
        SceneManager.LoadScene(1);
        loadinventorys(slot);
        setitemsandinventory.resetitems();
        setitemsandinventory.updateitemsininventory();
        resetgameplaystatics();
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
    private void resetgameplaystatics()
    {
        Statics.healcdbool = false;
        Statics.dashcdbool = false;
        Statics.weapsonswitchbool = false;
        Statics.charswitchbool = false;
        Statics.characterswitchbuff = 100;
        Statics.weaponswitchbuff = 100;
        Statics.timer = false;
    }
    /*private void loadmonobehaviour(int slot, string filename, MonoBehaviour monobehaviour)
    {
        var filePath = Path.Combine(Application.persistentDataPath, filename + slot + ".json");
        if (File.Exists(filePath))
        {
            Debug.Log("load" + monobehaviour);
            var json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, monobehaviour);
        }
        else
        {
            Debug.Log("doesnt exist");
        }
    }*/
}
