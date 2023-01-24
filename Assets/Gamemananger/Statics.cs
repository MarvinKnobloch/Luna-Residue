using System.Collections.Generic;
using UnityEngine;

public class Statics
{
    // Statics werden momentan, beim Load, in GlobalCD zurückgesetzt

    
    //setting;
    public static float mousesensitivity = 1;
    public static float rangeweaponaimsensitivity = 1;
    public static int currentscreenmode;

    //gamevalues
    public Vector3 test = new Vector3(1, 1, 1);
    public static string gamesavedate;
    public static string gamesavetime;
    public static float currentplayerposix, currentplayerposiy, currentplayerposiz;
    public static float currentplayerrotationx, currentplayerrotationy, currentplayerrotationz, currentplayerrotationw;
    public static float savecamvalueX;
    public static float npcdialoguetextspeed = 0.02f;

    //player

    public static int currentactiveplayer;
    public static int currentthirdchar;
    public static int currentforthchar;
    public static int playablechars = 5;

    public static bool otheraction;
    public static bool stopmovenandrotation;
    public static bool dash;
    public static float playerlockonrange = 20;
    public static bool playeriframes;
    public static float playermovementspeed = 10;
    public static float playergravity = 4;

    public static float playerbasiccritchance = 5;
    public static float bowbasicmanarestore = 2;
    public static float bowendmanarestore = 5;

    public static float healcd = 2f;
    public static float healmissingtime = 10f;
    public static bool healcdbool;

    public static float dashcd = 4f;
    public static float dashcdmissingtime;
    public static float dashcost = 2f;
    public static bool dashcdbool;

    public static float kickcd = 5f;
    public static float kickcdmissingtime;
    public static bool kickcdbool;

    public static float weaponswitchcd = 2f;
    public static float weaponswitchmissingtime;
    public static bool weapsonswitchbool;

    //public static float charswitchbuffbasicduration = 4f;
    public static float charswitchbuffmissingtime;
    public static float charswitchcd = 5f;
    public static float charswitchmissingtime;
    public static bool charswitchbool;

    public static float charwechselbuff = 100f;
    public static float weaponswitchbuff = 100f;
    public static float weaponswitchbuffmissingtime;

    public static int currentelementstate;
    public static int currentcombospell;

    public static float dazecounter;
    public static float dazekicksneeded;
    public static bool dazestunstart;                 //um die parameter zurückzusetzen (wird im attack script gemacht)
    public static bool slow;
    public static bool enemyspezialtimescale;

    public static float normalgamespeed;
    public static float normaltimedelta;

    //equiptment
    public static int currentequiptmentchar;

    //infight
    public static bool infight;

    //puzzles
    public static int memorycurrentplattform = 1;
    public static bool memoryisrunning;
    public static bool stopshowing;

    //interaction
    public static List<GameObject> interactionobjects = new List<GameObject>();
    public static Transform closestinteractionobject;
    public static bool timer;

    //enemy
    public static float enemyspecialcd = 13;
    public static float currentenemyspecialcd = 13;
    public static float enemydebufftime = 8;

    //abilities
    public static int[] spellnumbers = { 24, 24, 24, 24, 24, 24, 24, 24 };        // 24 = anzahl der vorhanden Spells
    public static Color[] spellcolors = new Color[8];

    //stones
    public static int[] characterbaseelements = { 3, 1, 7, 6, 2 };        //Maria = ice, Erika = Water, Kaja = Earth, Yaku = Dark, Arissa = Nature}
    public static int[] charactersecondelement = { 8, 8, 8, 8, 8 };       // 8 = hat noch kein element
    public static string[] characterclassrolltext = new string[5];
    public static int maincharstoneclass = 3;
    public static int secondcharstoneclass = 3;
    public static int thirdcharstoneclass = 3;
    public static int forthcharstoneclass = 3;
    public static int playertookdmgfromamount = 1;                  //wird beim charchange geändert
    public static int thirdchartookdmgformamount = 1;
    public static int forthchartookdmgformamount = 1;
    public static float groupstonehealbonus = 0;
    public static float groupstonedefensebonus = 0;
    public static float groupstonedmgbonus = 0;
    public static float guardbonushpeachlvl = 10;
    public static bool thirdcharishealer;
    public static bool forthcharishealer;
    public static float alliegrouphealspawntime = 8;

    //itemgrid
    public static GameObject activeswordslot;
    public static GameObject activebowslot;
    public static GameObject activefistslot;
    public static GameObject activeheadslot;
    public static GameObject activechestslot;
    public static GameObject activeglovesslot;
    public static GameObject activeshoesslot;
    public static GameObject activebeltslot;
    public static GameObject activenecklessslot;
    public static GameObject activeringslot;

    //playerstats
    public static int charcurrentlvl = 1;
    public static float charcurrentexp = 0;
    public static float charrequiredexp = 52;
    public static string[] characternames = { "Maria", "Erika", "Kaja", "Yaku", "Arissa" };
    public static Color[] characterelementcolor = { new Color32(33, 156, 147, 255), new Color32(26, 25, 197, 255), new Color32(75, 46, 17, 255), new Color32(29, 20, 20, 255), new Color32(29, 144, 40, 255) };                      //Farben werden im Elementalmenu gesetzt
    public static Color[] charactersecondelementcolor = new Color[5];
    public static float[] charbasichealth = { 100, 93, 97, 91, 95 };
    public static float[] charcurrenthealth = { 100, 93, 97, 91, 95 };
    public static float[] charmaxhealth = { 100, 93, 97, 91, 95 };
    public static float[] chardefense = { 100, 100, 100, 100, 100 };
    public static float[] charattack = { 1, 1, 1, 1, 1 };
    public static float[] charcritchance = { 5, 5, 5, 5, 5 };
    public static float[] charcritdmg = { 150, 150, 150, 150, 150 };
    public static float[] charweaponbuff = { 100, 100, 100, 100, 100 };
    public static float[] charweaponbuffduration = { 5, 5, 5, 5, 5 };
    public static float[] charswitchbuff = { 100, 100, 100, 100, 100 };
    public static float[] charswitchbuffduration = { 5, 5, 5, 5, 5 };
    public static float[] charbasiccritbuff = { 1, 1, 1, 1, 1 };
    public static float[] charbasicdmgbuff = { 150, 150, 150, 150, 150 };

    public static float[] charswordattack = new float[5];
    public static string[] charcurrentswordname = { "empty", "empty", "empty", "empty", "empty" };
    public static float[] charbowattack = new float[5];
    public static string[] charcurrentbowname = { "empty", "empty", "empty", "empty", "empty" };
    public static float[] charfistattack = new float[5];
    public static string[] charcurrentfistname = { "empty", "empty", "empty", "empty", "empty" };

    public static string[] charcurrentheadname = { "empty", "empty", "empty", "empty", "empty" };
    public static string[] charcurrentchestname = { "empty", "empty", "empty", "empty", "empty" };
    public static string[] charcurrentglovesname = { "empty", "empty", "empty", "empty", "empty" };
    public static string[] charcurrentshoesname = { "empty", "empty", "empty", "empty", "empty" };
    public static string[] charcurrentlegname = { "empty", "empty", "empty", "empty", "empty" };
    public static string[] charcurrentnecklessname = { "empty", "empty", "empty", "empty", "empty" };
    public static string[] charcurrentringname = { "empty", "empty", "empty", "empty", "empty" };

    public static GameObject[] currentswordimage = new GameObject[5];
    public static GameObject[] currentbowimage = new GameObject[5];
    public static GameObject[] currentfistimage = new GameObject[5];
    public static GameObject[] currentheadimage = new GameObject[5];
    public static GameObject[] currentchestimage = new GameObject[5];
    public static GameObject[] currentglovesimage = new GameObject[5];
    public static GameObject[] currentshoesimage = new GameObject[5];
    public static GameObject[] currentlegimage = new GameObject[5];
    public static GameObject[] currentnecklessimage = new GameObject[5];
    public static GameObject[] currentringimage = new GameObject[5];

    public static float healthperskillpoint = 25;
    public static float armorperskillpoint = 25;
    public static float attackperskillpoint = 1;
    public static float critchanceperskillpoint = 1;
    public static float critdmgperskillpoint = 2;
    public static float weaponswitchbuffperskillpoint = 3;
    public static float charswitchbuffperskillpoint = 3;
    public static float basicbuffdmgperskillpoint = 4;


    // Mariastats
    /*public static int mariacurrentlvl = 1;
    public static float mariacurrentexp = 10f;
    public static float mariacurrentrequiredexp = 52f;

    //Erikastats
    public static int erikacurrentlvl = 1;
    public static float erikacurrentexp = 0f;
    public static float erikacurrentrequiredexp = 52f;

    //Kajawstats
    public static int kajacurrentlvl = 1;
    public static float kajacurrentexp = 0f;
    public static float kajacurrentrequiredexp = 52f;

    //Yakustats yaku
    public static int yakucurrentlvl = 1;
    public static float yakucurrentexp = 0f;
    public static float yakucurrentrequiredexp = 52f;

    //Arissastats arrisa
    public static int arissacurrentlvl = 1;
    public static float arissacurrentexp = 0f;
    public static float arissacurrentrequiredexp = 52f;*/
}
