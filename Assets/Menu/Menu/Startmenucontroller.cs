using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
//using Newtonsoft.Json;

public class Startmenucontroller : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject loadgameobj;
    [SerializeField] private GameObject settingsobj;
    private Setitemsandinventory setitemsandinventory;
    private Setstaticsnull setstaticsnull;
    //private Isaveload loadsaveinterface = new Saveloadgame();
    private Convertstatics convertstatics = new Convertstatics();

    public Color selectedcolor;
    public Color notselectedcolor;

    private void Awake()
    {
        setitemsandinventory = GetComponent<Setitemsandinventory>();
        setstaticsnull = GetComponent<Setstaticsnull>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        loadgamesettings();
        loadsaveslots();
    }
    private void OnEnable()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Image>().color = notselectedcolor;
        }
    }
    private void loadsaveslots()
    {
        for (int i = 0; i < Slotvaluesarray.slotisnotempty.Length; i++)
        {
            string loadpath = Application.persistentDataPath + "/Statics" + i + ".json";
            if (File.Exists(loadpath))
            {
                string loaded_data = File.ReadAllText(loadpath);
                convertstatics = JsonUtility.FromJson<Convertstatics>(loaded_data);
                Debug.Log("Data Slot" + i + " exists");
                saveslotvalues(i);
            }
            else
            {
                Debug.Log("Data Slot" + i + " doesn't exist");
            }
            /*try
            {
                string loadpath = "/Statics" + i + ".json";
                statics = loadsaveinterface.loaddata<Statics>(loadpath);
                Debug.Log("data has been loaded");
                saveslotvalues(i);
            }
            catch (Exception e)
            {
                Debug.LogError($"error Could not load data {e.Message} {e.StackTrace}");
            }*/
        }
    }
    private void loadgamesettings()
    {
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("Vsyncvalue");
    }
    private void saveslotvalues(int slot)
    {
        Slotvaluesarray.slotisnotempty[slot] = true;
        Slotvaluesarray.slotlvl[slot] = convertstatics.charcurrentlvl;
        Slotvaluesarray.slotdate[slot] = convertstatics.gamesavedate;
        Slotvaluesarray.slottime[slot] = convertstatics.gamesavetime;
    }
    public void newgame()
    {
        Statics.currentactiveplayer = 0;
        Statics.currentfirstchar = 0;
        Statics.currentsecondchar = 1;
        Statics.currentthirdchar = 2;
        Statics.currentforthchar = 3;

        Statics.firstweapon[0] = 0;
        Statics.secondweapon[0] = 1;
        Statics.firstweapon[1] = 0;
        Statics.secondweapon[1] = 1;
        Statics.firstweapon[2] = 0;
        Statics.secondweapon[2] = 1;
        Statics.firstweapon[3] = 0;
        Statics.secondweapon[3] = 1;
        Statics.firstweapon[4] = 0;
        Statics.secondweapon[4] = 1;

        setitemsandinventory.resetitems();
        setitemsandinventory.resetinventorys();
        setstaticsnull.resetstatics();
        SceneManager.LoadScene(1);
    }

    public void loadgame()
    {
        loadgameobj.SetActive(true);
        gameObject.SetActive(false);
    }
    public void settings()
    {
        settingsobj.SetActive(true);
        gameObject.SetActive(false);
    }
    public void endgame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}


/*private void Update()
{
    if (steuerung.Startmenu.Up.WasPerformedThisFrame())
    {
        Debug.Log("hallo");
        if(currentbutton == 0)
        {
            buttons[currentbutton].GetComponent<Image>().color = notselectedcolor;
            currentbutton = 3;
            EventSystem.current.SetSelectedGameObject(buttons[currentbutton]);
            buttons[currentbutton].GetComponent<Image>().color = selectedcolor;
        }
        else
        {
            buttons[currentbutton].GetComponent<Image>().color = notselectedcolor;
            currentbutton--;
            EventSystem.current.SetSelectedGameObject(buttons[currentbutton]);
            buttons[currentbutton].GetComponent<Image>().color = selectedcolor;
        }
    }
    if (steuerung.Startmenu.Down.WasPerformedThisFrame())
    {
        if(currentbutton == 3)
        {
            buttons[currentbutton].GetComponent<Image>().color = notselectedcolor;
            currentbutton = 0;
            EventSystem.current.SetSelectedGameObject(buttons[currentbutton]);
            buttons[currentbutton].GetComponent<Image>().color = selectedcolor;
        }
        else
        {
            buttons[currentbutton].GetComponent<Image>().color = notselectedcolor;
            currentbutton++;
            EventSystem.current.SetSelectedGameObject(buttons[currentbutton]);
            buttons[currentbutton].GetComponent<Image>().color = selectedcolor;
        }
    }
}*/
