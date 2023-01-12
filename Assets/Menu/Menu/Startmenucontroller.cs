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
    //private Isaveload loadsaveinterface = new Saveloadgame();
    private Convertstatics convertstatics = new Convertstatics();

    public Color selectedcolor;
    public Color notselectedcolor;

    private void Awake()
    {
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
        PlayerPrefs.SetInt("Maincharindex", 0);
        PlayerPrefs.SetInt("Secondcharindex", 1);
        PlayerPrefs.SetInt("Thirdcharindex", 2);
        PlayerPrefs.SetInt("Forthcharindex", 3);

        PlayerPrefs.SetInt("Mariamainweaponindex", 0);
        PlayerPrefs.SetInt("Mariasecondweaponindex", 1);
        PlayerPrefs.SetInt("Erikamainweaponindex", 0);
        PlayerPrefs.SetInt("Erikasecondweaponindex", 1);
        PlayerPrefs.SetInt("Kajamainweaponindex", 0);
        PlayerPrefs.SetInt("Kajasecondweaponindex", 1);
        PlayerPrefs.SetInt("Yakumainweaponindex", 0);
        PlayerPrefs.SetInt("Yakusecondweaponindex", 1);
        PlayerPrefs.SetInt("Arissamainweaponindex", 0);
        PlayerPrefs.SetInt("Arissasecondweaponindex", 1);

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
