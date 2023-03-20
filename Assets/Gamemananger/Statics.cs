using System.Collections.Generic;
using UnityEngine;

public class Statics
{
    // Statics werden momentan, beim Load, in GlobalCD zurückgesetzt

    
    //setting;
    public static int currentscreenmode;
    public static float dialoguetextspeed = 0.02f;

    //gamevalues
    public static int currentgameslot = 0;                 //save slot 0 ist autosave
    public static string gamesavedate;
    public static string gamesavetime;
    public static float currentplayerposix, currentplayerposiy, currentplayerposiz;
    public static float currentplayerrotationx, currentplayerrotationy, currentplayerrotationz, currentplayerrotationw;
    public static float savecamvalueX;

    public static Vector3 gameoverposi;                   //wird in enemyhp gesetzt weil die posi nur gespeichtert werden sollen wenn auch ein kampf gewonnen worden ist
    public static Quaternion gameoverrota;
    public static float gameovercam;
    public static bool donttriggerenemies;

    public static bool elementalmenuisactiv;

    //charvalues
    public static int currentactiveplayer;                //für hpanzeige
    public static int currentfirstchar = 0;
    public static int currentsecondchar = 1;
    public static int currentthirdchar = 2;
    public static int currentforthchar = 3;

    public static int[] firstweapon = { 0, 0, 0, 0, 0 };
    public static int[] secondweapon = { 1, 1, 1, 1, 1 };

    //presetgamevalues
    public static int playablechars = 5;

    public static float normalgamespeed;
    public static float normaltimedelta;

    public static float playermovementspeed = 10;
    public static float playerroationspeed = 700;
    public static float playerjumpheight = 13;
    public static float playergravity = 3.5f;
    public static float playerbasiccritchance = 5;
    public static float bowbasicmanarestore = 2;
    public static float bowendmanarestore = 5;

    public static float cleavedamagereduction = 3;
    public static float basicweaponheal = 3;
    public static float weaponbonushealhealthpercentage = 2;
    public static float healhealthbonuspercentage = 4;

    public static float healthperskillpoint = 20;
    public static float armorperskillpoint = 25;
    public static float defenseconvertedtoattack = 1;
    public static float attackperskillpoint = 1;
    public static float critchanceperskillpoint = 1;
    public static float critdmgperskillpoint = 2;
    public static float weaponswitchbuffperskillpoint = 2;
    public static float charswitchbuffperskillpoint = 3;
    public static int basicbuffdmgperskillpoint = 2;

    public static int presetresurrectcd = 4;
    public static int presethealcd = 5;

    //gameplaystatics
    public static bool otheraction;
    public static bool stopmovenandrotation;
    public static bool dash;
    public static bool playeriframes;

    //playercds
    public static float healcd;
    public static float healmissingtime = 10f;
    public static bool healcdbool;

    public static float dashcd = 6f;
    public static float dashcdmissingtime;
    public static float dashcost = 3f;
    public static bool dashcdbool;

    public static float weaponswitchcd = 9;
    public static float weaponswitchmissingtime;
    public static bool weapsonswitchbool; 

    public static float weaponswitchbuff = 0;
    public static float weaponswitchbuffmissingtime;

    public static float charswitchcd = 9;
    public static float charswitchmissingtime;
    public static bool charswitchbool;

    public static float characterswitchbuff = 0;
    public static float charswitchbuffmissingtime;


    //interaction
    public static List<GameObject> interactionobjects = new List<GameObject>();
    public static Transform closestinteractionobject;
    public static bool timer;

    //infight
    public static bool infight;
    public static int playertookdmgfromamount = 1;                  // wird im charmanangergesetzt/wird beim charchange geändert
    public static int[] tookdmgfromamount = { 1, 1, 1, 1 };
    public static bool oneplayerisdead;
    public static int infightresurrectcd;                           //wird im infightcontroller zurückgesetzt
    public static bool supportcanresurrect;

    //enemy
    public static float enemyspecialcd = 13;
    public static float currentenemyspecialcd = 13;
    public static float enemydebufftime = 8;
    public static float enemydmgmultiplier = 1.5f;

    public static float dazecounter;
    public static float dazekicksneeded;
    public static bool resetvaluesondeathorstun;                 //um die parameter zurückzusetzen (wird im attack script gemacht)
    public static bool enemyspezialtimescale;

    public static int currentelementstate;
    public static int currentcombospell;

    //eleabilities
    public static int[] spellnumbers = { -1, -1, -1, -1, -1, -1, -1, -1 };
    public static Color[] spellcolors = new Color[8];

    //stones
    public static bool[] stoneisactivated = new bool[24];
    public static int[] characterbaseelements = { 3, 1, 7, 6, 2 };        //Maria = ice, Erika = Water, Kaja = Earth, Yaku = Dark, Arissa = Nature}
    public static Color[] characterelementcolor = { new Color32(33, 156, 147, 255), new Color32(26, 25, 197, 255), new Color32(75, 46, 17, 255), new Color32(29, 20, 20, 255), new Color32(29, 144, 40, 255) };                      //Farben werden im Elementalmenu gesetzt
    public static Color[] charactersecondelementcolor = { new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255) };
    public static int[] charactersecondelement = { -1, -1, -1, -1, -1 };
    public static string[] characterclassrolltext = new string[5];
    public static int[] characterclassroll = { -1, -1, -1, -1, -1 };
    public static float groupstonehealbonus = 0;
    public static float groupstonedefensebonus = 0;
    public static float groupstonedmgbonus = 0;

    public static float guardbonushpeachlvl = 8;
    public static float alliegrouphealspawntime = 9;
    public static float alliegrouphealdespawntime = 5;

    //playerstats
    public static int charcurrentlvl = 1;
    public static float charcurrentexp = 0;
    public static float charrequiredexp = 70;
    public static string[] characternames = { "Maria", "Erika", "Kaja", "Yaku", "Arissa" };
    public static float[] charbasichealth = { 80, 73, 77, 73, 75 };
    public static float[] charcurrenthealth = { 80, 73, 77, 73, 75 };
    public static float[] charmaxhealth = { 80, 73, 77, 73, 75 };
    public static float[] chardefense = { 60, 60, 60, 60, 60 };
    public static float[] charattack = { 0, 0, 0, 0, 0 };
    public static float[] charcritchance = { 5, 5, 5, 5, 5 };
    public static float[] charcritdmg = { 150, 150, 150, 150, 150 };
    public static float[] charweaponbuff = { 0, 0, 0, 0, 0 };
    public static float[] charweaponbuffduration = { 5, 5, 5, 5, 5 };
    public static float[] charswitchbuff = { 0, 0, 0, 0, 0 };
    public static float[] charswitchbuffduration = { 5, 5, 5, 5, 5 };
    public static float[] charbasiccritbuff = { 1, 1, 1, 1, 1 };
    public static float[] charbasicdmgbuff = { 0, 0, 0, 0, 0 };

    //equiptment
    public static int currentequipmentchar;
    public static int currentequipmentbutton;         //wird beim button gesetzt(onclick event)

    public static float[] charswordattack = new float[5];
    public static float[] charbowattack = new float[5];
    public static float[] charfistattack = new float[5];

    public static int[] swordid = new int[5];
    public static int[] bowid = new int[5];
    public static int[] fistid = new int[5];
    public static int[] headid = new int[5];
    public static int[] chestid = new int[5];
    public static int[] beltid = new int[5];
    public static int[] legsid = new int[5];
    public static int[] shoesid = new int[5];
    public static int[] necklaceid = new int[5];
    public static int[] ringid = new int[5];

    public static Itemcontroller[] charcurrentsword = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentbow = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentfist = new Itemcontroller[5];
    public static Itemcontroller[] charcurrenthead = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentchest = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentbelt = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentlegs = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentshoes = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentnecklace = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentring = new Itemcontroller[5];

    //skillpoints
    public static int[] charspendedskillpoints = new int[5];
    public static int[] charskillpoints = new int[5];

    public static int[] charhealthskillpoints = new int[5];
    public static int[] chardefenseskillpoints = new int[5];
    public static int[] charattackskillpoints = new int[5];
    public static int[] charcritchanceskillpoints = new int[5];
    public static int[] charcritdmgskillpoints = new int[5];
    public static int[] charweaponskillpoints = new int[5];
    public static int[] charcharswitchskillpoints = new int[5];
    public static int[] charbasicskillpoints = new int[5];
}
