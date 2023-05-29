using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class Saveandloadgame : MonoBehaviour
{
    [SerializeField] private GameObject Loadingscreen;
    [SerializeField] private Image loadingscreenbar;

    private Isaveload loadsaveinterface = new Saveloadgame();
    [SerializeField] private Savemenucontroller savemenucontroller;
    [SerializeField] private Loadmenucontroller loadmenucontroller;

    private Convertstatics convertstatics = new Convertstatics();
    [SerializeField] private Setitemsandinventory setitemsandinventory;
    [SerializeField] private Areacontroller areacontroller;

    public void savegamedata()
    {
        Infightcontroller.instance.savegameoverposi(LoadCharmanager.savemainposi);
        int slot;
        if (savemenucontroller == null) slot = 0;                //für autosave
        else slot = savemenucontroller.selectedslot ;
        saveinventorys(slot);
        convertstatics.savestaticsinscript();
        savestatics(slot);
        if (areacontroller != null)
        {
            areacontroller.convertquestdata();
            savemonobehaviour(slot, "/" + areacontroller.areaname, areacontroller);
        }
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
        int slot = loadmenucontroller.selectedslot;
        Statics.currentgameslot = slot;
        loadstaticdata(slot);
        if (convertstatics != null)
        {
            convertstatics.setstaticsafterload();
        }
        StartCoroutine(loadgameloadingscreen());
        loadinventorys(slot);
        setitemsandinventory.resetplayerstats();
        setitemsandinventory.resetitems();
        setitemsandinventory.updateitemsininventory();
        setitemsandinventory.setequipeditemsafterload();
        setitemsandinventory.setallitemstats();
        setitemsandinventory.addskillpoints();
        setitemsandinventory.addguardhp();
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
    IEnumerator loadgameloadingscreen()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        Loadingscreen.SetActive(true);

        while (!operation.isDone)
        {
            float loadingbaramount = Mathf.Clamp01(operation.progress / 0.9f);
            loadingscreenbar.fillAmount = loadingbaramount;
            yield return null;
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
        Statics.dashcd = Statics.presetdashcd;
        Statics.dashcost = Statics.presetdashcost;
        Statics.healcd = Statics.presethealcd;
        Statics.healcdbool = false;
        Statics.dashcdbool = false;
        Statics.weapsonswitchbool = false;
        Statics.charswitchbool = false;
        Statics.characterswitchbuff = 0;
        Statics.weaponswitchbuff = 0;
        Statics.timer = false;
        Fasttravelpoints.travelpoints.Clear();
    }
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

