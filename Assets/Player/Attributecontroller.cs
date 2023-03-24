using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attributecontroller : MonoBehaviour
{
    [SerializeField] private int charnumber;
    [SerializeField] private Playerhp playerhp;

    [SerializeField] private float testingbonusattack;

    [NonSerialized] public float maxhealth;
    [NonSerialized] public float attack;
    [NonSerialized] public float defense;
    [NonSerialized] public float critchance;
    [NonSerialized] public float critdmg;
    [NonSerialized] public float charswitchbuff;               //void Awake in GlobalCD
    [NonSerialized] public float basicattributecritbuff;
    [NonSerialized] public float basicattributedmgbuff;

    [NonSerialized] public float swordattack;
    [NonSerialized] public float bowattack;
    [NonSerialized] public float fistattack;

    [NonSerialized] public bool isdmgclassroll;
    [NonSerialized] public float stoneclassbonusdmg;

    [NonSerialized] public bool isguardclassroll;
    [NonSerialized] public float stoneclassdmgreduction;

    [NonSerialized] public bool ishealerclassroll;
    [NonSerialized] public float stoneclassbonusheal;

    [NonSerialized] public bool guardhpbuff;
    [NonSerialized] public float dmgfromallies;

    public void classrollupdate()
    {
        if (isdmgclassroll == true)
        {
            if (Statics.characterclassroll[Statics.currentthirdchar] == 0 || Statics.characterclassroll[Statics.currentforthchar] == 0)
            {
                stoneclassbonusdmg = Statics.groupstonedmgbonus;
            }
            else
            {
                stoneclassbonusdmg = Statics.groupstonedmgbonus * 0.5f;
            }
            stoneclassbonusheal = 0;
            stoneclassdmgreduction = 0;
        }
        else if (isguardclassroll)
        {
            checkforsupportdmgbuff();
            stoneclassbonusheal = 0;
            stoneclassdmgreduction = Statics.groupstonedefensebonus / 2;
        }
        else if (ishealerclassroll)
        {
            checkforsupportdmgbuff();
            stoneclassbonusheal = Statics.groupstonehealbonus / 2;
            stoneclassdmgreduction = 0;
        }
        else
        {
            checkforsupportdmgbuff();
            stoneclassbonusheal = 0;
            stoneclassdmgreduction = 0;
        }
        updateattributes();
    }
    private void checkforsupportdmgbuff()
    {
        if(Statics.currentthirdchar != -1)
        {
            if(Statics.characterclassroll[Statics.currentthirdchar] == 0)
            {
                stoneclassbonusdmg = Statics.groupstonedmgbonus * 0.5f;
                return;
            }
        }
        if (Statics.currentforthchar != -1)
        {
            if (Statics.characterclassroll[Statics.currentforthchar] == 0)
            {
                stoneclassbonusdmg = Statics.groupstonedmgbonus * 0.5f;
                return;
            }
            else
            {
                stoneclassbonusdmg = 0;
            }
        }
        else
        {
            stoneclassbonusdmg = 0;
        }
        
    }
    public void updateattributes()
    {
        playerhp.health = Statics.charcurrenthealth[charnumber];
        playerhp.maxhealth = Statics.charmaxhealth[charnumber];
        maxhealth = Statics.charmaxhealth[charnumber];

        defense = Statics.chardefense[charnumber];
        attack = Statics.charattack[charnumber] + Statics.chardefense[charnumber] * (Statics.defenseconvertedtoattack * 0.01f);
#if UNITY_EDITOR
        attack += testingbonusattack;
#endif
        critchance = Statics.charcritchance[charnumber];
        critdmg = Statics.charcritdmg[charnumber];
        charswitchbuff = Statics.charswitchbuff[charnumber];
        basicattributecritbuff = Statics.charbasiccritbuff[charnumber];
        basicattributedmgbuff = Statics.charbasicdmgbuff[charnumber];

        swordattack = Statics.charswordattack[charnumber];
        bowattack = Statics.charbowattack[charnumber];
        fistattack = Statics.charfistattack[charnumber];
        if(gameObject == LoadCharmanager.Overallthirdchar || gameObject == LoadCharmanager.Overallforthchar)
        {
            supportcharupdate();
        }
    }
    private void supportcharupdate()
    {
        if (gameObject == LoadCharmanager.Overallthirdchar || gameObject == LoadCharmanager.Overallforthchar)
        {
            if (gameObject == LoadCharmanager.Overallthirdchar)
            {
                if (ishealerclassroll == true)
                {
                    GetComponent<Supportmovement>().ishealer = true;
                }
                else
                {
                    GetComponent<Supportmovement>().ishealer = false;
                }
            }
            if (gameObject == LoadCharmanager.Overallforthchar)
            {
                if (ishealerclassroll == true)
                {
                    GetComponent<Supportmovement>().ishealer = true;
                }
                else
                {
                    GetComponent<Supportmovement>().ishealer = false;
                }
            }
            alliesdmg();
        }
    }
    public void alliesdmg()
    {
        float basicdmg = 5;
        dmgfromallies = basicdmg + attack + (critchance - Statics.playerbasiccritchance) + ((critdmg - 150) / Statics.critdmgperskillpoint) + (Statics.charweaponbuff[charnumber] / Statics.weaponswitchbuffperskillpoint) + (charswitchbuff / Statics.charswitchbuffperskillpoint) + (basicattributedmgbuff / Statics.basicbuffdmgperskillpoint);
    }
}
