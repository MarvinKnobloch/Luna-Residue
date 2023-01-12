using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mariaskilltreeold : MonoBehaviour
{
    /*private int skillpointsperlvl = 20;

    public int[] selectetdchar;
    public int currentint;

    private int[] spendedskillpoints = new int[2];
    private int[] skillpoints = new int[2];
    private int[] healthpoints = new int[2];
    private int[] defenspoints = new int[2];
    private int[] attackpoints = new int[2];
    private int[] critchancepoints = new int[2];
    private int[] critdmgpoints = new int[2];
    private int[] weaponpoints = new int[2];
    private int[] charswitchpoints = new int[2];
    private int[] basicpoints = new int[2];
    private int[] currentlvl = new int[2];

    public Text skillpointtext;
    public Text nametext;
    public Text healthbuttonnumber;
    public Text defensebuttonnumber;
    public Text attackbuttonnumber;
    public Text critchancebuttonnumber;
    public Text critdmgbuttonnumber;
    public Text weaponbuttonnumber;
    public Text charbuttonnumber;
    public Text basicbuttonnumber;

    public Text statshealth;
    public Text statsdefense;
    public Text statsattack;
    public Text statscritchance;
    public Text statscritdmg;
    public Text statsweaponbuff;
    public Text statsweaponbuffduration;
    public Text statscharbuff;
    public Text statscharbuffduration;

    public static int mariaspendedpoints;
    public static int mariaskillpoints;
    public static int mariahealthpoints = 0;
    public static int mariadefensepoints = 0;
    public static int mariaattackpoints = 0;
    public static int mariacritchancepoints = 0;
    public static int mariacritdmgpoints = 0;
    public static int mariaweaponpoints = 0;
    public static int mariacharswitchpoints = 0;
    public static int mariabasicpoints = 0;

    public static int erikaspendedpoints;
    public static int erikaskillpoints;
    public static int erikahealthpoints = 0;
    public static int erikadefensepoints = 0;
    public static int erikaattackpoints = 0;
    public static int erikacritchancepoints = 0;
    public static int erikacritdmgpoints = 0;
    public static int erikaweaponpoints = 0;
    public static int erikacharswitchpoints = 0;
    public static int erikabasicpoints = 0;

    void Start()
    {
        /*mariaskillpoints = Statics.mariacurrentlvl * skillpointsperlvl - mariaspendedpoints;
        skillpointtext.text = "Skillpoints " + mariaskillpoints;
        nametext.text = "Maria LvL " + Statics.mariacurrentlvl;
        healthbuttonnumber.text = "" + mariahealthpoints;
        defensebuttonnumber.text = "" + mariadefensepoints;
        attackbuttonnumber.text = "" + mariaattackpoints;
        critchancebuttonnumber.text = "" + mariacritchancepoints;
        critdmgbuttonnumber.text = "" + mariacritdmgpoints;
        weaponbuttonnumber.text = "" + mariaweaponpoints;
        charbuttonnumber.text = "" + mariacharswitchpoints;
        basicbuttonnumber.text = "" + mariabasicpoints;
        statshealth.text = Statics.mariacurrenthealth + "/ " + Statics.mariamaxheatlh;
        statsdefense.text = Statics.mariadefense + "";
        statsattack.text = Statics.mariadmg + "";
        statscritchance.text = Statics.mariacritchance + "%";
        statscritdmg.text = Statics.mariacritdmg + "%";
        statsweaponbuff.text = Statics.mariaweaponbuff -100 + "%";
        statsweaponbuffduration.text = Statics.mariaweaponbuffduration + "sec";
        statscharbuff.text = Statics.mariacharwechselbuff -100 + "%";
        statscharbuffduration.text = Statics.mariacharbuffduration + "sec";

    }
    public void updatechar(int newcharacter)
    {
        currentint = newcharacter;
    }
    public void updatechar1()
    {
        spendedskillpoints[0] = mariaspendedpoints;
        skillpoints[0] = mariaskillpoints;
        healthpoints[0] = mariahealthpoints;
        defenspoints[0] = mariadefensepoints;
        attackpoints[0] = mariaattackpoints;
        critchancepoints[0] = mariacritchancepoints;
        critdmgpoints[0] = mariacritdmgpoints;
        weaponpoints[0] = mariaweaponpoints;
        charswitchpoints[0] = mariacharswitchpoints;
        basicpoints[0] = mariabasicpoints;
        currentlvl[0] = Statics.mariacurrentlvl;

        mariaskillpoints = Statics.mariacurrentlvl * skillpointsperlvl - mariaspendedpoints;
        skillpointtext.text = "Skillpoints " + mariaskillpoints;
        nametext.text = "Maria LvL " + Statics.mariacurrentlvl;
        healthbuttonnumber.text = "" + mariahealthpoints;
        defensebuttonnumber.text = "" + mariadefensepoints;
        attackbuttonnumber.text = "" + mariaattackpoints;
        critchancebuttonnumber.text = "" + mariacritchancepoints;
        critdmgbuttonnumber.text = "" + mariacritdmgpoints;
        weaponbuttonnumber.text = "" + mariaweaponpoints;
        charbuttonnumber.text = "" + mariacharswitchpoints;
        basicbuttonnumber.text = "" + mariabasicpoints;
        statshealth.text = Statics.mariacurrenthealth + "/ " + Statics.mariamaxhealth;
        statsdefense.text = Statics.mariadefense + "";
        statsattack.text = Statics.mariadmg + "";
        statscritchance.text = Statics.mariacritchance + "%";
        statscritdmg.text = Statics.mariacritdmg + "%";
        statsweaponbuff.text = Statics.mariaweaponbuff - 100 + "%";
        statsweaponbuffduration.text = Statics.mariaweaponbuffduration + "sec";
        statscharbuff.text = Statics.mariacharwechselbuff - 100 + "%";
        statscharbuffduration.text = Statics.mariacharbuffduration + "sec";
    }
    public void updatechar2()
    {
        spendedskillpoints[1] = erikaspendedpoints;
        skillpoints[1] = erikaskillpoints;
        healthpoints[1] = erikahealthpoints;
        defenspoints[1] = erikadefensepoints;
        attackpoints[1] = erikaattackpoints;
        critchancepoints[1] = erikacritchancepoints;
        critdmgpoints[1] = erikacritdmgpoints;
        weaponpoints[1] = erikaweaponpoints;
        charswitchpoints[1] = erikacharswitchpoints;
        basicpoints[1] = erikabasicpoints;
        currentlvl[1] = Statics.erikacurrentlvl;

        erikaskillpoints = Statics.erikacurrentlvl * skillpointsperlvl - erikaspendedpoints;
        skillpointtext.text = "Skillpoints " + erikaskillpoints;
        nametext.text = "Maria LvL " + Statics.erikacurrentlvl;
        healthbuttonnumber.text = "" + erikahealthpoints;
        defensebuttonnumber.text = "" + erikadefensepoints;
        attackbuttonnumber.text = "" + erikaattackpoints;
        critchancebuttonnumber.text = "" + erikacritchancepoints;
        critdmgbuttonnumber.text = "" + erikacritdmgpoints;
        weaponbuttonnumber.text = "" + erikaweaponpoints;
        charbuttonnumber.text = "" + erikacharswitchpoints;
        basicbuttonnumber.text = "" + erikabasicpoints;
        statshealth.text = Statics.erikacurrenthealth + "/ " + Statics.erikamaxhealth;
        statsdefense.text = Statics.erikadefense + "";
        statsattack.text = Statics.erikadmg + "";
        statscritchance.text = Statics.erikacritchance + "%";
        statscritdmg.text = Statics.erikacritdmg + "%";
        statsweaponbuff.text = Statics.erikaweaponbuff - 100 + "%";
        statsweaponbuffduration.text = Statics.erikaweaponbuffduration + "sec";
        statscharbuff.text = Statics.erikacharwechselbuff - 100 + "%";
        statscharbuffduration.text = Statics.erikacharbuffduration + "sec";
    }
    public void plusbutton()
    {

        spendedskillpoints[currentint] += 1;
        updateunspendpoint();
    }
    public void updateunspendpoint()
    {
        skillpoints[currentint] = currentlvl[currentint] * skillpointsperlvl - spendedskillpoints[currentint];
        skillpointtext.text = "Skillpoints" + skillpoints[currentint];
    }

    public void plusbutton()
    {
        mariaspendedpoints += 1;
        updateunspendpoint();
    }
    public void minusbutton()
    {
        mariaspendedpoints -= 1;
        updateunspendpoint();
    }
    /*public void updateunspendpoint()
    {
        mariaskillpoints = Statics.mariacurrentlvl * skillpointsperlvl - mariaspendedpoints;
        skillpointtext.text = "Skillpoints " + mariaskillpoints;
    }
    public void healthnumberplus()
    {
        if(mariaskillpoints > 0)
        {
            mariahealthpoints += 1;
            healthbuttonnumber.text = "" + mariahealthpoints;
            Statics.mariamaxhealth = Statics.mariabasichealth + (mariahealthpoints * 25);
            statshealth.text = Statics.mariacurrenthealth + "/ " + Statics.mariamaxhealth;
            plusbutton();
        }
    }
    public void healthnumberminus()
    {
        if (mariahealthpoints > 0)
        {
            mariahealthpoints -= 1;
            healthbuttonnumber.text = "" + mariahealthpoints;
            Statics.mariamaxhealth = Statics.mariabasichealth + (mariahealthpoints * 25);
            statshealth.text = Statics.mariacurrenthealth + "/ " + Statics.mariamaxhealth;
            minusbutton();
        }
    }
    public void defensenumberplus()
    {
        if (mariaskillpoints > 0)
        {
            mariadefensepoints += 1;
            Statics.mariadefense += 25;
            statsdefense.text = Statics.mariadefense + "";
            defensebuttonnumber.text = "" + mariadefensepoints;
            plusbutton();
        }
    }
    public void defensenumberminus()
    {
        if (mariadefensepoints > 0)
        {
            mariadefensepoints -= 1;
            Statics.mariadefense -= 25;
            statsdefense.text = Statics.mariadefense + "";
            defensebuttonnumber.text = "" + mariadefensepoints;
            minusbutton();
        }
    }
    public void attacknumberplus()
    {
        if (mariaskillpoints > 0)
        {
            mariaattackpoints += 1;
            Statics.mariadmg += 1;
            statsattack.text = Statics.mariadmg + "";
            attackbuttonnumber.text = "" + mariaattackpoints;
            plusbutton();
        }
    }
    public void attacknumberminus()
    {
        if (mariaattackpoints > 0)
        {
            mariaattackpoints -= 1;
            Statics.mariadmg -= 1;
            statsattack.text = Statics.mariadmg + "";
            attackbuttonnumber.text = "" + mariaattackpoints;
            minusbutton();
        }
    }
    public void critchancenumberplus()
    {
        if (mariaskillpoints > 0)
        {
            mariacritchancepoints += 1;
            Statics.mariacritchance += 1;
            statscritchance.text = Statics.mariacritchance + "%";
            critchancebuttonnumber.text = "" + mariacritchancepoints;
            plusbutton();
        }
    }
    public void critchancenumberminus()
    {
        if (mariacritchancepoints > 0)
        {
            mariacritchancepoints -= 1;
            Statics.mariacritchance -= 1;
            statscritchance.text = Statics.mariacritchance + "%";
            critchancebuttonnumber.text = "" + mariacritchancepoints;
            minusbutton();
        }
    }
    public void critdmgnumberplus()
    {
        if (mariaskillpoints > 0)
        {
            mariacritdmgpoints += 1;
            Statics.mariacritdmg += 2;
            statscritdmg.text = Statics.mariacritdmg + "%";
            critdmgbuttonnumber.text = "" + mariacritdmgpoints;
            plusbutton();
        }
    }
    public void critdmgnumberminus()
    {
        if (mariacritdmgpoints > 0)
        {
            mariacritdmgpoints -= 1;
            Statics.mariacritdmg -= 2;
            statscritdmg.text = Statics.mariacritdmg + "%";
            critdmgbuttonnumber.text = "" + mariacritdmgpoints;
            minusbutton();
        }
    }
    public void weaponnumberplus()
    {
        if (mariaskillpoints > 0)
        {
            mariaweaponpoints += 1;
            Statics.mariaweaponbuff += 5f;
            Statics.mariaweaponbuffduration += 0.05f;
            statsweaponbuff.text = Statics.mariaweaponbuff -100 + "%";
            statsweaponbuffduration.text = Statics.mariaweaponbuffduration.ToString("F2") + "sec";
            weaponbuttonnumber.text = "" + mariaweaponpoints;
            plusbutton();
        }
    }
    public void weaponnumberminus()
    {
        if (mariaweaponpoints > 0)
        {
            mariaweaponpoints -= 1;
            Statics.mariaweaponbuff -= 5f;
            Statics.mariaweaponbuffduration -= 0.05f;
            statsweaponbuff.text = Statics.mariaweaponbuff -100 + "%";
            statsweaponbuffduration.text = Statics.mariaweaponbuffduration.ToString("F2") + "sec";
            weaponbuttonnumber.text = "" + mariaweaponpoints;
            minusbutton();
        }
    }
    public void charnumberplus()
    {
        if (mariaskillpoints > 0)
        {
            mariacharswitchpoints += 1;
            Statics.mariacharwechselbuff += 5f;
            Statics.mariacharbuffduration += 0.05f;
            statscharbuff.text = Statics.mariacharwechselbuff -100 + "%";
            statscharbuffduration.text = Statics.mariacharbuffduration.ToString("F2") + "sec";
            charbuttonnumber.text = "" + mariacharswitchpoints;
            plusbutton();
        }
    }

    public void charnumberminus()
    {
        if (mariacharswitchpoints > 0)
        {
            mariacharswitchpoints -= 1;
            Statics.mariacharwechselbuff -= 5f;
            Statics.mariacharbuffduration -= 0.05f;
            statscharbuff.text = Statics.mariacharwechselbuff - 100 + "%";
            statscharbuffduration.text = Statics.mariacharbuffduration.ToString("F2") + "sec";
            charbuttonnumber.text = "" + mariacharswitchpoints;
            minusbutton();
        }
    }
    public void basicnumberplus()
    {
        if (mariaskillpoints > 0)
        {
            mariabasicpoints += 1;
            basicbuttonnumber.text = "" + mariabasicpoints;
            plusbutton();
        }
    }
    public void basicnumberminus()
    {
        if (mariabasicpoints > 0)
        {
            mariabasicpoints -= 1;
            basicbuttonnumber.text = "" + mariabasicpoints;
            minusbutton();
        }
    }*/
}
