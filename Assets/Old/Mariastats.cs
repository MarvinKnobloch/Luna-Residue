using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mariastats : MonoBehaviour
{
    /*private int charnumber;
    private Attributecontroller attributecontroller;
    private SpielerHP spielerhp;
    private Expmanager expmanager;
    private void OnEnable()
    {
        LoadCharmanager.attributeupdate += updateattributes;
    }
    void Awake()
    {
        charnumber = 0;
        attributecontroller = GetComponent<Attributecontroller>();
        spielerhp = GetComponent<SpielerHP>();
        expmanager = GetComponent<Expmanager>();

        expmanager.charlvl = Statics.charcurrentlvl;
        expmanager.currentexp = Statics.charcurrentexp;
        expmanager.requiredexp = Statics.charrequiredexp;

        spielerhp.health = Statics.charcurrenthealth[charnumber];
        spielerhp.maxhealth = Statics.charmaxhealth[charnumber]; //(Skilltreescript.charhealthpoints[charnumber] * 25);

        attributecontroller.defense = Statics.chardefense[charnumber];
        attributecontroller.attack = Statics.charattack[charnumber];
        attributecontroller.critchance = Statics.charcritchance[charnumber];
        attributecontroller.critdmg = Statics.charcritdmg[charnumber];
        attributecontroller.charswitchbuff = Statics.charswitchbuff[charnumber];
        attributecontroller.basiccrit = Statics.charbasiccritbuff[charnumber];
        attributecontroller.basicdmgbuff = Statics.charbasicdmgbuff[charnumber];

        attributecontroller.swordattack = Statics.charswordattack[charnumber];
        attributecontroller.bowattack = Statics.charbowattack[charnumber];
        attributecontroller.fistattack = Statics.charfistattack[charnumber];

    }

    private void OnDisable()
    {
        LoadCharmanager.attributeupdate -= updateattributes;
        //Statics.mariacurrentlvl = expmanager.charlvl;
        //Statics.mariacurrentexp = expmanager.currentexp;
        //Statics.mariacurrentrequiredexp = expmanager.requiredexp;

        Statics.charcurrenthealth[charnumber] = spielerhp.health;
    }
    private void updateattributes()
    {
        spielerhp.maxhealth = Statics.charmaxhealth[charnumber];
        spielerhp.UpdatehealthUI();

        attributecontroller.defense = Statics.chardefense[charnumber];
        attributecontroller.attack = Statics.charattack[charnumber];
        attributecontroller.critchance = Statics.charcritchance[charnumber];
        attributecontroller.critdmg = Statics.charcritdmg[charnumber];
        attributecontroller.charswitchbuff = Statics.charswitchbuff[charnumber];
        attributecontroller.basiccrit = Statics.charbasiccritbuff[charnumber];
        attributecontroller.basicdmgbuff = Statics.charbasicdmgbuff[charnumber];

        attributecontroller.swordattack = Statics.charswordattack[charnumber];         //.kajaswordattack;
        attributecontroller.bowattack = Statics.charbowattack[charnumber];
        attributecontroller.fistattack = Statics.charfistattack[charnumber];
    }*/
}
