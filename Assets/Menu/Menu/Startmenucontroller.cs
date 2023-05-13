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
    [SerializeField] private Image loadingscreenbar;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject loadgameobj;
    [SerializeField] private GameObject settingsobj;
    private Setitemsandinventory setitemsandinventory;
    private Convertstatics convertstatics = new Convertstatics();

    [SerializeField] private GameObject difficultyui;

    public Color selectedcolor;
    public Color notselectedcolor;

    [SerializeField] private Menusoundcontroller menusoundcontroller;

    [SerializeField] private AudioMixer audiomixer;
    [SerializeField] private string gamevolume;
    [SerializeField] private string musicvolume;
    [SerializeField] private string soundeffecsvolume;

    private void Awake()
    {
        setitemsandinventory = GetComponent<Setitemsandinventory>();
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
            int slot = i + 1;
            string loadpath = Application.persistentDataPath + "/Statics" + slot + ".json";
            if (File.Exists(loadpath))
            {
                string loaded_data = File.ReadAllText(loadpath);
                convertstatics = JsonUtility.FromJson<Convertstatics>(loaded_data);
                Debug.Log("Data Slot" + slot + " exists");
                saveslotvalues(i);
            }
            else
            {
                Debug.Log("Data Slot" + slot + " doesn't exist");
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
    }
    public void newgame(int difficulty)
    {
        menusoundcontroller.playmenubuttonsound();
        Statics.difficulty = difficulty;
        Statics.currentgameslot = -1;             //damit bei new game nichts geladen wird
        Statics.currentfirstchar = 0;
        Statics.currentsecondchar = 1;
        Statics.currentthirdchar = 2;
        Statics.currentforthchar = 3;

        Statics.currentzonemusicint = 0;
        Statics.elementalmenuunlocked = false;

        Statics.charcurrentlvl = 1;
        Statics.charcurrentexp = 0;
        Statics.charrequiredexp = 70;

        //resetgameplaystatics
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

        Statics.spellnumbers = new int[]{ -1, -1, -1, -1, -1, -1, -1, -1 };
        Statics.spellcolors = new Color[8];

        Statics.stoneisactivated = new bool[24];
        Statics.charactersecondelement = new int[] { -1, -1, -1, -1, -1 };
        Statics.charactersecondelementcolor = new Color[] { new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255) };
        Statics.characterclassrolltext = new string[5];
        Statics.characterclassroll = new int[] { -1, -1, -1, -1, -1 };
        Statics.groupstonehealbonus = 0;
        Statics.groupstonedefensebonus = 0;
        Statics.groupstonedmgbonus = 0;

        for (int i = 0; i < Statics.playablechars; i++)
        {
            Statics.firstweapon[i] = 0;
            Statics.secondweapon[i] = 1;
        }
        for (int i = 0; i < Statics.playablechars; i++)
        {
            Statics.charspendedskillpoints[i] = 0;
            Statics.charskillpoints[i] = 0;
            Statics.charhealthskillpoints[i] = 0;
            Statics.chardefenseskillpoints[i] = 0;
            Statics.charattackskillpoints[i] = 0;
            Statics.charcritchanceskillpoints[i] = 0;
            Statics.charcritchanceskillpoints[i] = 0;
            Statics.charweaponskillpoints[i] = 0;
            Statics.charcharswitchskillpoints[i] = 0;
            Statics.charbasicskillpoints[i] = 0;
        }

        Statics.charcurrentsword = new Itemcontroller[5];
        Statics.charcurrentbow = new Itemcontroller[5];
        Statics.charcurrentfist = new Itemcontroller[5];
        Statics.charcurrenthead = new Itemcontroller[5];
        Statics.charcurrentchest = new Itemcontroller[5];
        Statics.charcurrentbelt = new Itemcontroller[5];
        Statics.charcurrentlegs = new Itemcontroller[5];
        Statics.charcurrentshoes = new Itemcontroller[5];
        Statics.charcurrentnecklace = new Itemcontroller[5];
        Statics.charcurrentring = new Itemcontroller[5];

        Statics.swordid = new int[5] { 1, 1, 1, 1, 1 };
        Statics.bowid = new int[5] { 1, 1, 1, 1, 1 };
        Statics.fistid = new int[5] { 1, 1, 1, 1, 1 };
        Statics.headid = new int[5] { 1, 1, 1, 1, 1 };
        Statics.chestid = new int[5] { 1, 1, 1, 1, 1 };
        Statics.beltid = new int[5] { 1, 1, 1, 1, 1 };
        Statics.legsid = new int[5] { 1, 1, 1, 1, 1 };
        Statics.shoesid = new int[5] { 1, 1, 1, 1, 1 };
        Statics.necklaceid = new int[5] { 1, 1, 1, 1, 1 };
        Statics.ringid = new int[5] { 1, 1, 1, 1, 1 };

        setitemsandinventory.resetplayerstats();
        setitemsandinventory.resetitems();
        setitemsandinventory.resetinventorys();
        setitemsandinventory.setstartitems();

        //setstaticsnull.equipmentstatics();
        StartCoroutine("loadgameloadingscreen");
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
    public void endgame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
