using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convertstatics 
{
    public Vector3 playerposition;                       //Werte m�ssen public sein sonst werde sich nicht gespeichtert
    public Quaternion playerrotation;
    public float camvalueX;
    public int charcurrentlvl;
    public float charcurrentexp;
    public float charrequiredexp;
    public string gamesavedate;
    public string gamesavetime;

    public int firstchar;
    public int secondchar;
    public int thirdchar;
    public int forthchar;

    public bool elementalmenuisactiv;

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

    public int[] firstweapon;
    public int[] secondweapon;

    public float[] charswordattack;
    public float[] charbowattack;
    public float[] charfistattack;

    public int[] swordid = new int[5];
    public int[] bowid = new int[5];
    public int[] fistid = new int[5];
    public int[] headid = new int[5];
    public int[] chestid = new int[5];
    public int[] beltid = new int[5];
    public int[] legsid = new int[5];
    public int[] shoesid = new int[5];
    public int[] necklaceid = new int[5];
    public int[] ringid = new int[5];

    public int[] charspendedskillpoints;
    public int[] charskillpoints;

    public int[] charhealthskillpoints;
    public int[] chardefenseskillpoints;
    public int[] charattackskillpoints;
    public int[] charcritchanceskillpoints;
    public int[] charcritdmgskillpoints;
    public int[] charweaponskillpoints;
    public int[] charcharswitchskillpoints;
    public int[] charbasicskillpoints;


    public int[] spellnumbers;
    public Color[] spellcolors;

    public bool[] stoneisactivated;
    public int[] charactersecondelement;
    public string[] characterclassrolltext;
    public int[] characterclassroll;
    public int maincharstoneclass;
    public int secondcharstoneclass;
    public int thirdcharstoneclass;
    public int forthcharstoneclass;
    public float groupstonehealbonus;
    public float groupstonedefensebonus;
    public float groupstonedmgbonus;
    public bool thirdcharishealer;
    public bool forthcharishealer;


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

        firstchar = Statics.currentfirstchar;
        secondchar = Statics.currentsecondchar;
        thirdchar = Statics.currentthirdchar;
        forthchar = Statics.currentforthchar;

        elementalmenuisactiv = Statics.elementalmenuisactiv;

        firstweapon = Statics.firstweapon;
        secondweapon = Statics.secondweapon;

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
        charbowattack = Statics.charbowattack;
        charfistattack = Statics.charfistattack;

        setids(swordid, Statics.charcurrentsword);
        setids(bowid, Statics.charcurrentbow);
        setids(fistid, Statics.charcurrentfist);
        setids(headid, Statics.charcurrenthead);
        setids(chestid, Statics.charcurrentchest);
        setids(beltid, Statics.charcurrentbelt);
        setids(legsid, Statics.charcurrentlegs);
        setids(shoesid, Statics.charcurrentshoes);
        setids(necklaceid, Statics.charcurrentnecklace);
        setids(ringid, Statics.charcurrentring);

        charspendedskillpoints = Statics.charspendedskillpoints;
        charskillpoints = Statics.charskillpoints;
        charhealthskillpoints = Statics.charhealthskillpoints;
        chardefenseskillpoints = Statics.chardefenseskillpoints;
        charattackskillpoints = Statics.charattackskillpoints;
        charcritchanceskillpoints = Statics.charcritchanceskillpoints;
        charcritdmgskillpoints = Statics.charcritchanceskillpoints;
        charweaponskillpoints = Statics.charweaponskillpoints;
        charcharswitchskillpoints = Statics.charcharswitchskillpoints;
        charbasicskillpoints = Statics.charbasicskillpoints;



        spellnumbers = Statics.spellnumbers;
        spellcolors = Statics.spellcolors;

        stoneisactivated = Statics.stoneisactivated;
        charactersecondelementcolor = Statics.charactersecondelementcolor;
        charactersecondelement = Statics.charactersecondelement;
        characterclassrolltext = Statics.characterclassrolltext;
        characterclassroll = Statics.characterclassroll;
        groupstonehealbonus = Statics.groupstonehealbonus;
        groupstonedefensebonus = Statics.groupstonedefensebonus;
        groupstonedmgbonus = Statics.groupstonedmgbonus;
    }

    private void setids(int[] idslot, Itemcontroller[] staticitem)
    {
        for (int i = 0; i < staticitem.Length; i++)
        {
            if (staticitem[i] != null)
            {
                idslot[i] = staticitem[i].itemid;
            }
        }
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

        Statics.currentactiveplayer = 0;
        Statics.currentfirstchar = firstchar;
        Statics.currentsecondchar = secondchar;
        Statics.currentthirdchar = thirdchar;
        Statics.currentforthchar = forthchar;

        Statics.elementalmenuisactiv = elementalmenuisactiv;

        Statics.firstweapon = firstweapon;
        Statics.secondweapon = secondweapon;
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
        Statics.charbowattack = charbowattack;
        Statics.charfistattack = charfistattack;

        Statics.swordid = swordid;
        Statics.bowid = bowid;
        Statics.fistid = fistid;
        Statics.headid = headid;
        Statics.chestid = chestid;
        Statics.beltid = beltid;
        Statics.legsid = legsid;
        Statics.shoesid = shoesid;
        Statics.necklaceid = necklaceid;
        Statics.ringid = ringid;

        Statics.charspendedskillpoints = charspendedskillpoints;
        Statics.charskillpoints = charskillpoints;
        Statics.charhealthskillpoints = charhealthskillpoints;
        Statics.chardefenseskillpoints = chardefenseskillpoints;
        Statics.charattackskillpoints = charattackskillpoints;
        Statics.charcritchanceskillpoints = charcritchanceskillpoints;
        Statics.charcritchanceskillpoints = charcritdmgskillpoints;
        Statics.charweaponskillpoints = charweaponskillpoints;
        Statics.charcharswitchskillpoints = charcharswitchskillpoints;
        Statics.charbasicskillpoints = charbasicskillpoints;

        Statics.spellnumbers = spellnumbers;
        Statics.spellcolors = spellcolors;

        Statics.stoneisactivated = stoneisactivated;
        Statics.charactersecondelementcolor = charactersecondelementcolor;
        Statics.charactersecondelement = charactersecondelement;
        Statics.characterclassrolltext = characterclassrolltext;
        Statics.characterclassroll = characterclassroll;
        Statics.groupstonehealbonus = groupstonehealbonus;
        Statics.groupstonedefensebonus = groupstonedefensebonus;
        Statics.groupstonedmgbonus = groupstonedmgbonus;
    }
}
