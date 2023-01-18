using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attributecontroller : MonoBehaviour
{
    [SerializeField] private int charnumber;
    [SerializeField] private SpielerHP spielerhp;

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

    [NonSerialized] public float overallstonebonusdmg;
    [NonSerialized] public float overallstonedmgreduction;
    [NonSerialized] public float overallstonehealbonus;

    [NonSerialized] public bool ishealer;
    [NonSerialized] public bool guardhpbuff;

    [NonSerialized] public float dmgfromallies;

    void Awake()
    {
        spielerhp.health = Statics.charcurrenthealth[charnumber];
    }
    private void OnEnable()
    {
        updateattributes();              //onenable für die teammates
    }
    public void updateattributes()
    {
        spielerhp.maxhealth = Statics.charmaxhealth[charnumber];

        defense = Statics.chardefense[charnumber];
        attack = Statics.charattack[charnumber];
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
            if(gameObject == LoadCharmanager.Overallthirdchar)
            {
                if(Statics.thirdcharishealer == true)
                {
                    GetComponent<Supportmovement>().ishealer = true;
                }
                else
                {
                    GetComponent<Supportmovement>().ishealer = false;
                }
            }
            if(gameObject == LoadCharmanager.Overallforthchar)
            {
                if (Statics.forthcharishealer == true)
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
    public void levelup()
    {
        if (guardhpbuff == true)
        {
            spielerhp.maxhealth += Statics.guardbonushpeachlvl;
            Statics.charmaxhealth[charnumber] = spielerhp.maxhealth;
        }
        spielerhp.UpdatehealthUI();
    }

    public void alliesdmg()
    {
        dmgfromallies = attack + (critchance - 4) + ((critdmg - 148) / Statics.critdmgperskillpoint) + ((Statics.charweaponbuff[charnumber] - 97) / Statics.weaponswitchbuffperskillpoint) + ((charswitchbuff - 97) / Statics.charswitchbuffperskillpoint) + ((basicattributedmgbuff - 146) / Statics.basicbuffdmgperskillpoint);
        dmgfromallies = dmgfromallies * 0.5f;                  //durch 2 teilen weil es sonst zu viel ist
    }       // durch die minus zahlen ergibt jeder wert bei spielstart 1
}

/*public void levelup()
{
    if (guardhpbuff == true)
    {
        if (gameObject == LoadCharmanager.Overallmainchar)
        {
            if (Statics.currentactiveplayer == 0)
            {
                Statics.charmaxhealth[PlayerPrefs.GetInt("Maincharindex")] += Statics.guardbonushpeachlvl;
            }
            else
            {
                Statics.charmaxhealth[PlayerPrefs.GetInt("Secondcharindex")] += Statics.guardbonushpeachlvl;
            }
        }
        else if (gameObject == LoadCharmanager.Overallsecondchar)
        {
            if (Statics.currentactiveplayer == 1)
            {
                Statics.charmaxhealth[PlayerPrefs.GetInt("Maincharindex")] += Statics.guardbonushpeachlvl;
            }
            else
            {
                Statics.charmaxhealth[PlayerPrefs.GetInt("Secondcharindex")] += Statics.guardbonushpeachlvl;
            }
        }
        else if (gameObject == LoadCharmanager.Overallthirdchar)
        {
            Statics.charmaxhealth[Statics.currentthirdchar] += Statics.guardbonushpeachlvl;
        }
        else if (gameObject == LoadCharmanager.Overallforthchar)
        {
            Statics.charmaxhealth[Statics.currentforthchar] += Statics.guardbonushpeachlvl;
        }
        //GetComponent<SpielerHP>().UpdatehealthUI();
    }
}*/
