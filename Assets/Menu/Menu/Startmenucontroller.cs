using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Audio;

public class Startmenucontroller : MonoBehaviour
{
    [SerializeField] private GameObject Loadingscreen;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject loadgameobj;
    [SerializeField] private GameObject settingsobj;
    [SerializeField] private GameObject creditsobj;
    private Convertstatics convertstatics = new Convertstatics();

    [SerializeField] private GameObject difficultyui;
    [SerializeField] private GameObject fpscounter;

    public Color selectedcolor;
    public Color notselectedcolor;

    [SerializeField] private Menusoundcontroller menusoundcontroller;

    [SerializeField] private AudioMixer audiomixer;
    [SerializeField] private string gamevolume;
    [SerializeField] private string musicvolume;
    [SerializeField] private string soundeffecsvolume;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        loadgamesettings();
        loadsaveslots();
        if(PlayerPrefs.GetFloat("mousesensitivity") <= 0) PlayerPrefs.SetFloat("mousesensitivity", 20);
        if(PlayerPrefs.GetFloat("rangeweaponaimsensitivity") <= 0) PlayerPrefs.SetFloat("rangeweaponaimsensitivity", 20);
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("audiohasbeenchange") == 0)
        {
            PlayerPrefs.SetFloat(gamevolume, -10);
            audiomixer.SetFloat(gamevolume, PlayerPrefs.GetFloat(gamevolume));
            PlayerPrefs.SetFloat(musicvolume, -15);
            audiomixer.SetFloat(musicvolume, PlayerPrefs.GetFloat(musicvolume));
            PlayerPrefs.SetFloat(soundeffecsvolume, 5);
            audiomixer.SetFloat(soundeffecsvolume, PlayerPrefs.GetFloat(soundeffecsvolume));
        }
        else
        {
            setvolume(gamevolume, 0);
            setvolume(musicvolume, 0);
            setvolume(soundeffecsvolume, 10);
        }
        fpscounter.SetActive(true);
    }
    private void setvolume(string volumename, float maxdb)
    {
        if (PlayerPrefs.GetFloat(volumename + "ismuted") == 1)
        {
            audiomixer.SetFloat(volumename, -80);
        }
        else
        {
            audiomixer.SetFloat(volumename, PlayerPrefs.GetFloat(volumename));
            bool gotvalue = audiomixer.GetFloat(volumename, out float soundvalue);
            if (gotvalue == true)
            {
                if (soundvalue > maxdb)
                {
                    audiomixer.SetFloat(volumename, maxdb);
                }
            }
        }

    }
    private void OnEnable()
    {
        Loadingscreen.SetActive(false);
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Image>().color = notselectedcolor;
        }
        Mouseactivate.enablemouse();
    }
    private void loadsaveslots()
    {
        for (int i = 0; i < Slotvaluesarray.slotisnotempty.Length; i++)
        {
            int slot = i;
            string loadpath = Application.persistentDataPath + "/Statics" + slot + ".json";
            if (File.Exists(loadpath))
            {
                string loaded_data = File.ReadAllText(loadpath);
                convertstatics = JsonUtility.FromJson<Convertstatics>(loaded_data);
                //Debug.Log("Data Slot" + slot + " exists");
                saveslotvalues(i);
            }
            else
            {
                //Debug.Log("Data Slot" + slot + " doesn't exist");
            }
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
    public void opendifficultyui()
    {
        menusoundcontroller.playmenubuttonsound();
        difficultyui.SetActive(true);
        gameObject.SetActive(false);
    }
    public void loadgame()
    {
        menusoundcontroller.playmenubuttonsound();
        loadgameobj.SetActive(true);
        gameObject.SetActive(false);
    }
    public void settings()
    {
        settingsobj.SetActive(true);
        gameObject.SetActive(false);
    }
    public void credits()
    {
        menusoundcontroller.playmenubuttonsound();
        creditsobj.SetActive(true);
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
