using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convertstatics 
{
    public Vector3 playerposition;
    public Quaternion playerrotation;
    public float camvalueX;
    public int charcurrentlvl;
    public float charcurrentexp;
    public float charrequiredexp;
    public string gamesavedate;
    public string gamesavetime;

    public Color[] characterelementcolor;
    public Color[] charactersecondelementcolor;
    public float[] charbasichealth;
    public float[] charcurrenthealth;
    public float[] charmaxhealth;
    public float[] chardefense;
    public float[] charattack;
    public float[] charcritchance;
    public float[] charcritdmg;
    public float[] charweaponbuff;
    public float[] charweaponbuffduration;
    public float[] charswitchbuff;
    public float[] charswitchbuffduration;
    public float[] charbasiccritbuff;
    public float[] charbasicdmgbuff;

    public float[] charswordattack;
    public string[] charcurrentswordname;
    public float[] charbowattack;
    public string[] charcurrentbowname;
    public float[] charfistattack;
    public string[] charcurrentfistname;

    public string[] charcurrentheadname;
    public string[] charcurrentchestname;
    public string[] charcurrentglovesname;
    public string[] charcurrentshoesname;
    public string[] charcurrentlegname;
    public string[] charcurrentnecklessname;
    public string[] charcurrentringname;

    public GameObject[] currentswordimage;
    public GameObject[] currentbowimage;
    public GameObject[] currentfistimage;
    public GameObject[] currentheadimage;
    public GameObject[] currentchestimage;
    public GameObject[] currentglovesimage;
    public GameObject[] currentshoesimage;
    public GameObject[] currentlegimage;
    public GameObject[] currentnecklessimage;
    public GameObject[] currentringimage;

    public void savestaticsinscript()
    {
        playerposition = LoadCharmanager.savemainposi;
        playerrotation = LoadCharmanager.savemainrota;
        camvalueX = LoadCharmanager.savecamvalueX;
        gamesavedate = System.DateTime.UtcNow.ToString("dd MMMM, yyyy");
        gamesavetime = System.DateTime.UtcNow.ToString("HH:mm");

        charcurrentlvl = Statics.charcurrentlvl;
        charcurrentexp = Statics.charcurrentexp;
        charrequiredexp = Statics.charrequiredexp;

        characterelementcolor = Statics.characterelementcolor;
        charactersecondelementcolor = Statics.charactersecondelementcolor;
        charbasichealth = Statics.charbasichealth;
        charcurrenthealth = Statics.charcurrenthealth;
        charmaxhealth = Statics.charmaxhealth;
        chardefense = Statics.chardefense;
        charattack = Statics.charattack;
        charcritchance = Statics.charcritchance;
        charcritdmg = Statics.charcritdmg;
        charweaponbuff = Statics.charweaponbuff;
        charweaponbuffduration = Statics.charweaponbuffduration;
        charswitchbuff = Statics.charswitchbuff;
        charswitchbuffduration = Statics.charswitchbuffduration;
        charbasiccritbuff = Statics.charbasiccritbuff;
        charbasicdmgbuff = Statics.charbasicdmgbuff;

        charswordattack = Statics.charswordattack;
        charcurrentswordname = Statics.charcurrentswordname;
        charbowattack = Statics.charbowattack;
        charcurrentbowname = Statics.charcurrentbowname;
        charfistattack = Statics.charfistattack;
        charcurrentfistname = Statics.charcurrentfistname;

        charcurrentheadname = Statics.charcurrentheadname;
        charcurrentchestname = Statics.charcurrentchestname;
        charcurrentglovesname = Statics.charcurrentglovesname;
        charcurrentshoesname = Statics.charcurrentshoesname;
        charcurrentlegname = Statics.charcurrentlegname;
        charcurrentnecklessname = Statics.charcurrentnecklessname;
        charcurrentringname = Statics.charcurrentringname;

        currentswordimage = Statics.currentswordimage;
        currentbowimage = Statics.currentbowimage;
        currentfistimage = Statics.currentfistimage;
        currentheadimage = Statics.currentheadimage;
        currentchestimage = Statics.currentchestimage;
        currentglovesimage = Statics.currentglovesimage;
        currentshoesimage = Statics.currentshoesimage;
        currentlegimage = Statics.currentlegimage;
        currentnecklessimage = Statics.currentnecklessimage;
        currentringimage = Statics.currentringimage;

            /*public static int currentactiveplayer;
    public static int currentthirdchar;
    public static int currentforthchar;
    public static float charwechselbuff = 100f;
    public static float weaponswitchbuff = 100f;
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
    public static bool forthcharishealer;*/
}

    public void setstaticsafterload()
    {
        LoadCharmanager.savemainposi = playerposition;
        LoadCharmanager.savemainrota = playerrotation;
        LoadCharmanager.savecamvalueX = camvalueX;

        Statics.charcurrentlvl = charcurrentlvl;
        Statics.charcurrentexp = charcurrentexp;
        Statics.charrequiredexp = charrequiredexp;

        charcurrentlvl = Statics.charcurrentlvl;
        charcurrentexp = Statics.charcurrentexp;
        charrequiredexp = Statics.charrequiredexp;

        Statics.characterelementcolor = characterelementcolor;
        Statics.charactersecondelementcolor = charactersecondelementcolor;
        Statics.charbasichealth = charbasichealth;
        Statics.charcurrenthealth = charcurrenthealth;
        Statics.charmaxhealth = charmaxhealth;
        Statics.chardefense = chardefense;
        Statics.charattack = charattack;
        Statics.charcritchance = charcritchance;
        Statics.charcritdmg = charcritdmg;
        Statics.charweaponbuff = charweaponbuff;
        Statics.charweaponbuffduration = charweaponbuffduration;
        Statics.charswitchbuff = charswitchbuff;
        Statics.charswitchbuffduration = charswitchbuffduration;
        Statics.charbasiccritbuff = charbasiccritbuff;
        Statics.charbasicdmgbuff = charbasicdmgbuff;

        Statics.charswordattack = charswordattack;
        Statics.charcurrentswordname = charcurrentswordname;
        Statics.charbowattack = charbowattack;
        Statics.charcurrentbowname = charcurrentbowname;
        Statics.charfistattack = charfistattack;
        Statics.charcurrentfistname = charcurrentfistname;

        Statics.charcurrentheadname = charcurrentheadname;
        Statics.charcurrentchestname = charcurrentchestname;
        Statics.charcurrentglovesname = charcurrentglovesname;
        Statics.charcurrentshoesname = charcurrentshoesname;
        Statics.charcurrentlegname = charcurrentlegname;
        Statics.charcurrentnecklessname = charcurrentnecklessname;
        Statics.charcurrentringname = charcurrentringname;

        Statics.currentswordimage = currentswordimage;
        Statics.currentbowimage = currentbowimage;
        Statics.currentfistimage = currentfistimage;
        Statics.currentheadimage = currentheadimage;
        Statics.currentchestimage = currentchestimage;
        Statics.currentglovesimage = currentglovesimage;
        Statics.currentshoesimage = currentshoesimage;
        Statics.currentlegimage = currentlegimage;
        Statics.currentnecklessimage = currentnecklessimage;
        Statics.currentringimage = currentringimage;
    }
}

/*public float playerposix;               //Loadcharmanager werte, werden beim menu öffnen gespeichert
public float playerposiy;
public float playerposiz;
public float playerrotax;
public float playerrotay;
public float playerrotaz;
public float playerrotaw;*/
