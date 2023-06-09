using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Difficultyuicontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject Loadingscreen;
    [SerializeField] private Image loadingscreenbar;
    private Setitemsandinventory setitemsandinventory;

    [SerializeField] private Menusoundcontroller menusoundcontroller;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        setitemsandinventory = GetComponent<Setitemsandinventory>();
    }
    private void OnEnable()
    {
        controlls.Enable();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            menusoundcontroller.playmenubuttonsound();
            menu.SetActive(true);
            gameObject.SetActive(false);
        }
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
        Statics.weaponswitchbool = false;
        Statics.charswitchbool = false;
        Statics.characterswitchbuff = 0;
        Statics.weaponswitchbuff = 0;
        Statics.timer = false;
        Fasttravelpoints.travelpoints.Clear();

        Statics.spellnumbers = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };
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
}
