using System.Collections.Generic;
using UnityEngine;

public class Statics
{
    // Statics werden momentan, beim Load, in GlobalCD zurückgesetzt

    
    //setting;
    public static float mousesensitivity = 1;
    public static float rangeweaponaimsensitivity = 1;
    public static int currentscreenmode;
    public static float npcdialoguetextspeed = 0.02f;

    //gamevalues
    public static int currentgameslot = -1;
    public static string gamesavedate;
    public static string gamesavetime;
    public static float currentplayerposix, currentplayerposiy, currentplayerposiz;
    public static float currentplayerrotationx, currentplayerrotationy, currentplayerrotationz, currentplayerrotationw;
    public static float savecamvalueX;

    //charvalues
    public static int currentactiveplayer;
    public static int currentfirstchar = 0;
    public static int currentsecondchar = 1;
    public static int currentthirdchar = 2;
    public static int currentforthchar = 3;

    public static int[] firstweapon = new int[5];
    public static int[] secondweapon = { 1, 1, 1, 1, 1 };            //new int[5]   1,1,1,1,1 wegen test bei maingame scene start

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
    public static float playerlockonrange = 20;

    //gameplaystatics
    public static bool otheraction;
    public static bool stopmovenandrotation;
    public static bool dash;
    public static bool playeriframes;

    //playercds
    public static float healcd = 2f;
    public static float healmissingtime = 10f;
    public static bool healcdbool;

    public static float dashcd = 4f;
    public static float dashcdmissingtime;
    public static float dashcost = 2f;
    public static bool dashcdbool;

    public static float weaponswitchcd = 2f;
    public static float weaponswitchmissingtime;
    public static bool weapsonswitchbool;

    public static float charswitchcd = 5f;
    public static float charswitchbuffmissingtime;
    public static float charswitchmissingtime;
    public static bool charswitchbool;

    public static float charwechselbuff = 100f;
    public static float weaponswitchbuff = 100f;
    public static float weaponswitchbuffmissingtime;

    //interaction
    public static List<GameObject> interactionobjects = new List<GameObject>();
    public static Transform closestinteractionobject;
    public static bool timer;

    //infight
    public static bool infight;
    public static int playertookdmgfromamount = 1;                  //wird beim charchange geändert
    public static int thirdchartookdmgformamount = 1;
    public static int forthchartookdmgformamount = 1;

    //enemy
    public static float enemyspecialcd = 13;
    public static float currentenemyspecialcd = 13;
    public static float enemydebufftime = 8;

    public static float dazecounter;
    public static float dazekicksneeded;
    public static bool dazestunstart;                 //um die parameter zurückzusetzen (wird im attack script gemacht)
    public static bool slow;
    public static bool enemyspezialtimescale;

    //eleabilities
    public static int[] spellnumbers = { 24, 24, 24, 24, 24, 24, 24, 24 };        // 24 = anzahl der vorhanden Spells
    public static Color[] spellcolors = new Color[8];

    public static int currentelementstate;
    public static int currentcombospell;

    //stones
    public static int[] characterbaseelements = { 3, 1, 7, 6, 2 };        //Maria = ice, Erika = Water, Kaja = Earth, Yaku = Dark, Arissa = Nature}
    public static int[] charactersecondelement = { 8, 8, 8, 8, 8 };       // 8 = hat noch kein element
    public static string[] characterclassrolltext = new string[5];
    public static int maincharstoneclass = 3;
    public static int secondcharstoneclass = 3;
    public static int thirdcharstoneclass = 3;
    public static int forthcharstoneclass = 3;
    public static float groupstonehealbonus = 0;
    public static float groupstonedefensebonus = 0;
    public static float groupstonedmgbonus = 0;
    public static float guardbonushpeachlvl = 10;
    public static bool thirdcharishealer;
    public static bool forthcharishealer;
    public static float alliegrouphealspawntime = 8;

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

    //equiptment
    public static int currentequipmentchar;
    public static int currentequipmentbutton;         //wird beim button gesetzt(onclick event)

    public static float[] charswordattack = new float[5];
    public static float[] charbowattack = new float[5];
    public static float[] charfistattack = new float[5];

    public static Itemcontroller[] charcurrentsword = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentbow = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentfist = new Itemcontroller[5];
    public static Itemcontroller[] charcurrenthead = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentchest = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentgloves = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentlegs = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentshoes = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentneckless = new Itemcontroller[5];
    public static Itemcontroller[] charcurrentring = new Itemcontroller[5];

    public static float healthperskillpoint = 25;
    public static float armorperskillpoint = 25;
    public static float attackperskillpoint = 1;
    public static float critchanceperskillpoint = 1;
    public static float critdmgperskillpoint = 2;
    public static float weaponswitchbuffperskillpoint = 3;
    public static float charswitchbuffperskillpoint = 3;
    public static float basicbuffdmgperskillpoint = 4;
}
