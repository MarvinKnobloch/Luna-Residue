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

    private int skillpointsperlvl = 1;

    [SerializeField] private Image[] charselectionimage;
    [SerializeField] private int currentchar;

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
        currentchar = PlayerPrefs.GetInt("Maincharindex");
        charselectionimage[currentchar].color = Color.green;
        choosechar(currentchar);
    }
    private void closeskilltree()
    {
        skilltree.SetActive(false);
        overview.SetActive(true);
    }
    public void choosechar(int currentchar)
    {
        this.currentchar = currentchar;           // falls man mit click den char auswählt
        nametext.text = Statics.characternames[this.currentchar] + " LvL" + Statics.charcurrentlvl;
        settextandpoints();
    }

    private void selectionforward() 
    {
        if(currentchar >= charselectionimage.Length -1)
        {
            currentchar = 0;
            choosechar(currentchar);
        }
        else
        {
            currentchar++;
            choosechar(currentchar);
        }
    }
    private void selectionbackward()
    {
        if (currentchar == 0)
        {
            currentchar = charselectionimage.Length -1;
            choosechar(currentchar);
        }
        else
        {
            currentchar--;
            choosechar(currentchar);
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

        charselectionimage[currentchar].color = Color.green;
        Statics.charskillpoints[currentchar] = Statics.charcurrentlvl * skillpointsperlvl - Statics.charspendedskillpoints[currentchar];
        skillpointtext.text = "Skillpoints " + Statics.charskillpoints[currentchar];

        healthbuttonnumber.text = "" + Statics.charhealthskillpoints[currentchar];
        defensebuttonnumber.text = "" + Statics.chardefenseskillpoints[currentchar];
        attackbuttonnumber.text = "" + Statics.charattackskillpoints[currentchar];
        critchancebuttonnumber.text = "" + Statics.charcritchanceskillpoints[currentchar];
        critdmgbuttonnumber.text = "" + Statics.charcritdmgskillpoints[currentchar];
        weaponbuttonnumber.text = "" + Statics.charweaponskillpoints[currentchar];
        charbuttonnumber.text = "" + Statics.charcharswitchskillpoints[currentchar];
        basicbuttonnumber.text = "" + Statics.charbasicskillpoints[currentchar];
        statshealth.text = Statics.charcurrenthealth[currentchar] + "/ " + Statics.charmaxhealth[currentchar];
        statsdefense.text = Statics.chardefense[currentchar] + "";
        statsattack.text = Statics.charattack[currentchar] + "";
        statscritchance.text = Statics.charcritchance[currentchar] + "%";
        statscritdmg.text = Statics.charcritdmg[currentchar] + "%";
        statsweaponbuff.text = Statics.charweaponbuff[currentchar] - 100 + "%";
        statsweaponbuffduration.text = Statics.charweaponbuffduration[currentchar] + "sec";
        statscharbuff.text = Statics.charswitchbuff[currentchar] - 100 + "%";
        statscharbuffduration.text = Statics.charswitchbuffduration[currentchar] + "sec";
        statsbasiccrit.text = Statics.charbasiccritbuff[currentchar] + "%";
        statsbasicbuffdmg.text = Statics.charbasicdmgbuff[currentchar] + "%";
    }

    public void plusbutton()
    {
        Statics.charspendedskillpoints[currentchar] += 1;
        updateunspendpoint();
    }

    public void minusbutton()
    {
        Statics.charspendedskillpoints[currentchar] -= 1;

        updateunspendpoint();
    }
    public void updateunspendpoint()
    {
        Statics.charskillpoints[currentchar] = Statics.charcurrentlvl * skillpointsperlvl - Statics.charspendedskillpoints[currentchar];
        skillpointtext.text = "Skillpoints" + Statics.charskillpoints[currentchar];
    }

    public void healthnumberplus()
    {
        if(Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charhealthskillpoints[currentchar] += 1;
            Statics.charmaxhealth[currentchar] += Statics.healthperskillpoint;
            if(Statics.charcurrenthealth[currentchar] > Statics.charmaxhealth[currentchar])
            {
                Statics.charcurrenthealth[currentchar] = Statics.charmaxhealth[currentchar];
            }
            statshealth.text = Statics.charcurrenthealth[currentchar] + "/ " + Statics.charmaxhealth[currentchar];
            healthbuttonnumber.text = "" + Statics.charhealthskillpoints[currentchar];
            plusbutton();
        }
    }
    public void healthnumberminus()
    {
        if (Statics.charhealthskillpoints[currentchar] > 0)
        {
            Statics.charhealthskillpoints[currentchar] -= 1;
            Statics.charmaxhealth[currentchar] -= Statics.healthperskillpoint;
            if (Statics.charcurrenthealth[currentchar] > Statics.charmaxhealth[currentchar])
            {
                Statics.charcurrenthealth[currentchar] = Statics.charmaxhealth[currentchar];
            }
            statshealth.text = Statics.charcurrenthealth[currentchar] + "/ " + Statics.charmaxhealth[currentchar];
            healthbuttonnumber.text = "" + Statics.charhealthskillpoints[currentchar];                   
            minusbutton();
        }
    }
    public void defensenumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.chardefenseskillpoints[currentchar] += 1;
            Statics.chardefense[currentchar] += Statics.armorperskillpoint;
            statsdefense.text = Statics.chardefense[currentchar] + "";
            defensebuttonnumber.text = "" + Statics.chardefenseskillpoints[currentchar];
            plusbutton();
        }
    }
    public void defensenumberminus()
    {
        if (Statics.chardefenseskillpoints[currentchar] > 0)
        {
            Statics.chardefenseskillpoints[currentchar] -= 1;
            Statics.chardefense[currentchar] -= Statics.armorperskillpoint;
            statsdefense.text = Statics.chardefense[currentchar] + "";
            defensebuttonnumber.text = "" + Statics.chardefenseskillpoints[currentchar];
            minusbutton();
        }
    }
    public void attacknumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charattackskillpoints[currentchar] += 1;
            Statics.charattack[currentchar] += Statics.attackperskillpoint;
            statsattack.text = Statics.charattack[currentchar] + "";
            attackbuttonnumber.text = "" + Statics.charattackskillpoints[currentchar];
            plusbutton();
        }
    }
    public void attacknumberminus()
    {
        if (Statics.charattackskillpoints[currentchar] > 0)
        {
            Statics.charattackskillpoints[currentchar] -= 1;
            Statics.charattack[currentchar] -= Statics.attackperskillpoint;
            statsattack.text = Statics.charattack[currentchar] + "";
            attackbuttonnumber.text = "" + Statics.charattackskillpoints[currentchar];
            minusbutton();
        }
    }
    public void critchancenumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charcritchanceskillpoints[currentchar] += 1;
            Statics.charcritchance[currentchar] += Statics.critchanceperskillpoint;
            statscritchance.text = Statics.charcritchance[currentchar] + "%";
            critchancebuttonnumber.text = "" + Statics.charcritchanceskillpoints[currentchar];
            plusbutton();
        }
    }
    public void critchancenumberminus()
    {
        if (Statics.charcritchanceskillpoints[currentchar] > 0)
        {
            Statics.charcritchanceskillpoints[currentchar] -= 1;
            Statics.charcritchance[currentchar] -= Statics.critchanceperskillpoint;
            statscritchance.text = Statics.charcritchance[currentchar] + "%";
            critchancebuttonnumber.text = "" + Statics.charcritchanceskillpoints[currentchar];
            minusbutton();
        }
    }
    public void critdmgnumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charcritdmgskillpoints[currentchar] += 1;
            Statics.charcritdmg[currentchar] += Statics.critdmgperskillpoint;
            statscritdmg.text = Statics.charcritdmg[currentchar] + "%";
            critdmgbuttonnumber.text = "" + Statics.charcritdmgskillpoints[currentchar];
            plusbutton();
        }
    }
    public void critdmgnumberminus()
    {
        if (Statics.charcritdmgskillpoints[currentchar] > 0)
        {
            Statics.charcritdmgskillpoints[currentchar] -= 1;
            Statics.charcritdmg[currentchar] -= Statics.critdmgperskillpoint;
            statscritdmg.text = Statics.charcritdmg[currentchar] + "%";
            critdmgbuttonnumber.text = "" + Statics.charcritdmgskillpoints[currentchar];
            minusbutton();
        }
    }
    public void weaponnumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charweaponskillpoints[currentchar] += 1;
            Statics.charweaponbuff[currentchar] += Statics.weaponswitchbuffperskillpoint;
            Statics.charweaponbuffduration[currentchar] += 0.05f;
            statsweaponbuff.text = Statics.charweaponbuff[currentchar] - 100 + "%";
            statsweaponbuffduration.text = Statics.charweaponbuffduration[currentchar].ToString("F2") + "sec";
            weaponbuttonnumber.text = "" + Statics.charweaponskillpoints[currentchar];
            plusbutton();
        }
    }
    public void weaponnumberminus()
    {
        if (Statics.charweaponskillpoints[currentchar] > 0)
        {
            Statics.charweaponskillpoints[currentchar] -= 1;
            Statics.charweaponbuff[currentchar] -= Statics.weaponswitchbuffperskillpoint;
            Statics.charweaponbuffduration[currentchar] -= 0.05f;
            statsweaponbuff.text = Statics.charweaponbuff[currentchar] - 100 + "%";
            statsweaponbuffduration.text = Statics.charweaponbuffduration[currentchar].ToString("F2") + "sec";
            weaponbuttonnumber.text = "" + Statics.charweaponskillpoints[currentchar];
            minusbutton();
        }
    }
    public void charnumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charcharswitchskillpoints[currentchar] += 1;
            Statics.charswitchbuff[currentchar] += Statics.charswitchbuffperskillpoint;
            Statics.charswitchbuffduration[currentchar] += 0.05f;
            statscharbuff.text = Statics.charswitchbuff[currentchar] - 100 + "%";
            statscharbuffduration.text = Statics.charswitchbuffduration[currentchar].ToString("F2") + "sec";
            charbuttonnumber.text = "" + Statics.charcharswitchskillpoints[currentchar];
            plusbutton();
        }
    }

    public void charnumberminus()
    {
        if (Statics.charcharswitchskillpoints[currentchar] > 0)
        {
            Statics.charcharswitchskillpoints[currentchar] -= 1;
            Statics.charswitchbuff[currentchar] -= Statics.charswitchbuffperskillpoint;
            Statics.charswitchbuffduration[currentchar] -= 0.05f;
            statscharbuff.text = Statics.charswitchbuff[currentchar] - 100 + "%";
            statscharbuffduration.text = Statics.charswitchbuffduration[currentchar].ToString("F2") + "sec";
            charbuttonnumber.text = "" + Statics.charcharswitchskillpoints[currentchar];
            minusbutton();
        }
    }
    public void basicnumberplus()
    {
        if (Statics.charskillpoints[currentchar] > 0)
        {
            Statics.charbasicskillpoints[currentchar] += 1;
            Statics.charbasiccritbuff[currentchar] += 1;
            Statics.charbasicdmgbuff[currentchar] += Statics.basicbuffdmgperskillpoint;
            statsbasiccrit.text = Statics.charbasiccritbuff[currentchar] + "%";
            statsbasicbuffdmg.text = Statics.charbasicdmgbuff[currentchar] + "%";
            basicbuttonnumber.text = "" + Statics.charbasicskillpoints[currentchar];
            plusbutton();
        }
    }
    public void basicnumberminus()
    {
        if (Statics.charbasicskillpoints[currentchar] > 0)
        {
            Statics.charbasicskillpoints[currentchar] -= 1;
            Statics.charbasiccritbuff[currentchar] -= 1;
            Statics.charbasicdmgbuff[currentchar] -= Statics.basicbuffdmgperskillpoint;
            statsbasiccrit.text = Statics.charbasiccritbuff[currentchar] + "%";
            statsbasicbuffdmg.text = Statics.charbasicdmgbuff[currentchar] + "%";
            basicbuttonnumber.text = "" + Statics.charbasicskillpoints[currentchar];
            minusbutton();
        }
    }
}
