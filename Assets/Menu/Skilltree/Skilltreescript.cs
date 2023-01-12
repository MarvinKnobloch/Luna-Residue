using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skilltreescript : MonoBehaviour
{
    private SpielerSteu Steuerung;
    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject skilltree;

    private int skillpointsperlvl = 2;

    [SerializeField] private Image[] charselectionimage;
    [SerializeField] private int currentint;

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
    public Text statsbasiccrit;
    public Text statsbasicbuffdmg;

    private static int[] charspendedskillpoints = new int[5];
    private static int[] charskillpoints = new int[5];

    public static int[] charhealthpoints = new int[5];
    private static int[] chardefensepoints = new int[5];
    private static int[] charattackpoints = new int[5];
    private static int[] charcritchancepoints = new int[5];
    private static int[] charcritdmgpoints = new int[5];
    private static int[] charweaponpoints = new int[5];
    private static int[] charcharswitchpoints = new int[5];
    private static int[] charbasicpoints = new int[5];

    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
    }

    private void Update()
    {
        if (Steuerung.Menusteuerung.Menucharselectionleft.WasPerformedThisFrame())
        {
            selectionbackward();
        }
        if (Steuerung.Menusteuerung.Menucharselectionright.WasPerformedThisFrame())
        {
            selectionforward();
        }
        if (Steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            closeskilltree();
        }
    }
    private void OnEnable()
    {
        Steuerung.Enable();
        foreach (Image chars in charselectionimage)
        {
            chars.color = Color.white;
        }
        currentint = PlayerPrefs.GetInt("Maincharindex");
        charselectionimage[currentint].color = Color.green;
        choosechar(currentint);
    }
    private void closeskilltree()
    {
        skilltree.SetActive(false);
        overview.SetActive(true);
    }
    public void choosechar(int currentchar)
    {
        currentint = currentchar;           // falls man mit click den char auswählt
        nametext.text = Statics.characternames[currentint] + " LvL" + Statics.charcurrentlvl;
        settextandpoints();
    }

    private void selectionforward() 
    {
        if(currentint >= charselectionimage.Length -1)
        {
            currentint = 0;
            choosechar(currentint);
        }
        else
        {
            currentint++;
            choosechar(currentint);
        }
    }
    private void selectionbackward()
    {
        if (currentint == 0)
        {
            currentint = charselectionimage.Length -1;
            choosechar(currentint);
        }
        else
        {
            currentint--;
            choosechar(currentint);
        }
    }
    public void imagewhite()
    {
        foreach (Image image in charselectionimage)
        {
            image.color = Color.white;
        }
    }

    private void settextandpoints()
    {
        imagewhite();

        charselectionimage[currentint].color = Color.green;
        charskillpoints[currentint] = Statics.charcurrentlvl * skillpointsperlvl - charspendedskillpoints[currentint];
        skillpointtext.text = "Skillpoints " + charskillpoints[currentint];

        healthbuttonnumber.text = "" + charhealthpoints[currentint];
        defensebuttonnumber.text = "" + chardefensepoints[currentint];
        attackbuttonnumber.text = "" + charattackpoints[currentint];
        critchancebuttonnumber.text = "" + charcritchancepoints[currentint];
        critdmgbuttonnumber.text = "" + charcritdmgpoints[currentint];
        weaponbuttonnumber.text = "" + charweaponpoints[currentint];
        charbuttonnumber.text = "" + charcharswitchpoints[currentint];
        basicbuttonnumber.text = "" + charbasicpoints[currentint];
        statshealth.text = Statics.charcurrenthealth[currentint] + "/ " + Statics.charmaxhealth[currentint];
        statsdefense.text = Statics.chardefense[currentint] + "";
        statsattack.text = Statics.charattack[currentint] + "";
        statscritchance.text = Statics.charcritchance[currentint] + "%";
        statscritdmg.text = Statics.charcritdmg[currentint] + "%";
        statsweaponbuff.text = Statics.charweaponbuff[currentint] - 100 + "%";
        statsweaponbuffduration.text = Statics.charweaponbuffduration[currentint] + "sec";
        statscharbuff.text = Statics.charswitchbuff[currentint] - 100 + "%";
        statscharbuffduration.text = Statics.charswitchbuffduration[currentint] + "sec";
        statsbasiccrit.text = Statics.charbasiccritbuff[currentint] + "%";
        statsbasicbuffdmg.text = Statics.charbasicdmgbuff[currentint] + "%";
    }

    public void plusbutton()
    {
        charspendedskillpoints[currentint] += 1;
        updateunspendpoint();
    }

    public void minusbutton()
    {
        charspendedskillpoints[currentint] -= 1;

        updateunspendpoint();
    }
    public void updateunspendpoint()
    {
        charskillpoints[currentint] = Statics.charcurrentlvl * skillpointsperlvl - charspendedskillpoints[currentint];
        skillpointtext.text = "Skillpoints" + charskillpoints[currentint];
    }

    public void healthnumberplus()
    {
        if(charskillpoints[currentint] > 0)
        {
            charhealthpoints[currentint] += 1;
            Statics.charmaxhealth[currentint] += Statics.healthperskillpoint;
            if(Statics.charcurrenthealth[currentint] > Statics.charmaxhealth[currentint])
            {
                Statics.charcurrenthealth[currentint] = Statics.charmaxhealth[currentint];
            }
            statshealth.text = Statics.charcurrenthealth[currentint] + "/ " + Statics.charmaxhealth[currentint];
            healthbuttonnumber.text = "" + charhealthpoints[currentint];
            plusbutton();
        }
    }
    public void healthnumberminus()
    {
        if (charhealthpoints[currentint] > 0)
        {
            charhealthpoints[currentint] -= 1;
            Statics.charmaxhealth[currentint] -= Statics.healthperskillpoint;
            if (Statics.charcurrenthealth[currentint] > Statics.charmaxhealth[currentint])
            {
                Statics.charcurrenthealth[currentint] = Statics.charmaxhealth[currentint];
            }
            statshealth.text = Statics.charcurrenthealth[currentint] + "/ " + Statics.charmaxhealth[currentint];
            healthbuttonnumber.text = "" + charhealthpoints[currentint];                   
            minusbutton();
        }
    }
    public void defensenumberplus()
    {
        if (charskillpoints[currentint] > 0)
        {
            chardefensepoints[currentint] += 1;
            Statics.chardefense[currentint] += Statics.armorperskillpoint;
            statsdefense.text = Statics.chardefense[currentint] + "";
            defensebuttonnumber.text = "" + chardefensepoints[currentint];
            plusbutton();
        }
    }
    public void defensenumberminus()
    {
        if (chardefensepoints[currentint] > 0)
        {
            chardefensepoints[currentint] -= 1;
            Statics.chardefense[currentint] -= Statics.armorperskillpoint;
            statsdefense.text = Statics.chardefense[currentint] + "";
            defensebuttonnumber.text = "" + chardefensepoints[currentint];
            minusbutton();
        }
    }
    public void attacknumberplus()
    {
        if (charskillpoints[currentint] > 0)
        {
            charattackpoints[currentint] += 1;
            Statics.charattack[currentint] += Statics.attackperskillpoint;
            statsattack.text = Statics.charattack[currentint] + "";
            attackbuttonnumber.text = "" + charattackpoints[currentint];
            plusbutton();
        }
    }
    public void attacknumberminus()
    {
        if (charattackpoints[currentint] > 0)
        {
            charattackpoints[currentint] -= 1;
            Statics.charattack[currentint] -= Statics.attackperskillpoint;
            statsattack.text = Statics.charattack[currentint] + "";
            attackbuttonnumber.text = "" + charattackpoints[currentint];
            minusbutton();
        }
    }
    public void critchancenumberplus()
    {
        if (charskillpoints[currentint] > 0)
        {
            charcritchancepoints[currentint] += 1;
            Statics.charcritchance[currentint] += Statics.critchanceperskillpoint;
            statscritchance.text = Statics.charcritchance[currentint] + "%";
            critchancebuttonnumber.text = "" + charcritchancepoints[currentint];
            plusbutton();
        }
    }
    public void critchancenumberminus()
    {
        if (charcritchancepoints[currentint] > 0)
        {
            charcritchancepoints[currentint] -= 1;
            Statics.charcritchance[currentint] -= Statics.critchanceperskillpoint;
            statscritchance.text = Statics.charcritchance[currentint] + "%";
            critchancebuttonnumber.text = "" + charcritchancepoints[currentint];
            minusbutton();
        }
    }
    public void critdmgnumberplus()
    {
        if (charskillpoints[currentint] > 0)
        {
            charcritdmgpoints[currentint] += 1;
            Statics.charcritdmg[currentint] += Statics.critdmgperskillpoint;
            statscritdmg.text = Statics.charcritdmg[currentint] + "%";
            critdmgbuttonnumber.text = "" + charcritdmgpoints[currentint];
            plusbutton();
        }
    }
    public void critdmgnumberminus()
    {
        if (charcritdmgpoints[currentint] > 0)
        {
            charcritdmgpoints[currentint] -= 1;
            Statics.charcritdmg[currentint] -= Statics.critdmgperskillpoint;
            statscritdmg.text = Statics.charcritdmg[currentint] + "%";
            critdmgbuttonnumber.text = "" + charcritdmgpoints[currentint];
            minusbutton();
        }
    }
    public void weaponnumberplus()
    {
        if (charskillpoints[currentint] > 0)
        {
            charweaponpoints[currentint] += 1;
            Statics.charweaponbuff[currentint] += Statics.weaponswitchbuffperskillpoint;
            Statics.charweaponbuffduration[currentint] += 0.05f;
            statsweaponbuff.text = Statics.charweaponbuff[currentint] - 100 + "%";
            statsweaponbuffduration.text = Statics.charweaponbuffduration[currentint].ToString("F2") + "sec";
            weaponbuttonnumber.text = "" + charweaponpoints[currentint];
            plusbutton();
        }
    }
    public void weaponnumberminus()
    {
        if (charweaponpoints[currentint] > 0)
        {
            charweaponpoints[currentint] -= 1;
            Statics.charweaponbuff[currentint] -= Statics.weaponswitchbuffperskillpoint;
            Statics.charweaponbuffduration[currentint] -= 0.05f;
            statsweaponbuff.text = Statics.charweaponbuff[currentint] - 100 + "%";
            statsweaponbuffduration.text = Statics.charweaponbuffduration[currentint].ToString("F2") + "sec";
            weaponbuttonnumber.text = "" + charweaponpoints[currentint];
            minusbutton();
        }
    }
    public void charnumberplus()
    {
        if (charskillpoints[currentint] > 0)
        {
            charcharswitchpoints[currentint] += 1;
            Statics.charswitchbuff[currentint] += Statics.charswitchbuffperskillpoint;
            Statics.charswitchbuffduration[currentint] += 0.05f;
            statscharbuff.text = Statics.charswitchbuff[currentint] - 100 + "%";
            statscharbuffduration.text = Statics.charswitchbuffduration[currentint].ToString("F2") + "sec";
            charbuttonnumber.text = "" + charcharswitchpoints[currentint];
            plusbutton();
        }
    }

    public void charnumberminus()
    {
        if (charcharswitchpoints[currentint] > 0)
        {
            charcharswitchpoints[currentint] -= 1;
            Statics.charswitchbuff[currentint] -= Statics.charswitchbuffperskillpoint;
            Statics.charswitchbuffduration[currentint] -= 0.05f;
            statscharbuff.text = Statics.charswitchbuff[currentint] - 100 + "%";
            statscharbuffduration.text = Statics.charswitchbuffduration[currentint].ToString("F2") + "sec";
            charbuttonnumber.text = "" + charcharswitchpoints[currentint];
            minusbutton();
        }
    }
    public void basicnumberplus()
    {
        if (charskillpoints[currentint] > 0)
        {
            charbasicpoints[currentint] += 1;
            Statics.charbasiccritbuff[currentint] += 1;
            Statics.charbasicdmgbuff[currentint] += Statics.basicbuffdmgperskillpoint;
            statsbasiccrit.text = Statics.charbasiccritbuff[currentint] + "%";
            statsbasicbuffdmg.text = Statics.charbasicdmgbuff[currentint] + "%";
            basicbuttonnumber.text = "" + charbasicpoints[currentint];
            plusbutton();
        }
    }
    public void basicnumberminus()
    {
        if (charbasicpoints[currentint] > 0)
        {
            charbasicpoints[currentint] -= 1;
            Statics.charbasiccritbuff[currentint] -= 1;
            Statics.charbasicdmgbuff[currentint] -= Statics.basicbuffdmgperskillpoint;
            statsbasiccrit.text = Statics.charbasiccritbuff[currentint] + "%";
            statsbasicbuffdmg.text = Statics.charbasicdmgbuff[currentint] + "%";
            basicbuttonnumber.text = "" + charbasicpoints[currentint];
            minusbutton();
        }
    }
}
