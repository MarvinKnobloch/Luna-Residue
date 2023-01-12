using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Setstats : MonoBehaviour
{
    public GameObject slottext;
    public GameObject slotbutton;

    public Text statsmaxhealth;
    public Text statsdefense;
    public Text statsattack;
    public Text statscritchance;
    public Text statscritdmg;
    public Text statsweaponbuff;
    public Text statsweaponbuffduration;
    public Text statscharswitchbuff;
    public Text statscharbuffduration;
    public Text statsbasiccrit;
    public Text statsbasicdmgbuff;

    public Text statssworddmg;
    public Text statsbowdmg;
    public Text statsfistdmg;

    //current player stats
    [NonSerialized] public float[] currentmaxhealth = new float[5];
    [NonSerialized] public float[] currentdefense = new float[5];
    [NonSerialized] public float[] currentattack = new float[5];
    [NonSerialized] public float[] currentcritchance = new float[5];
    [NonSerialized] public float[] currentcritdmg = new float[5];
    [NonSerialized] public float[] currentweaponbuff = new float[5];
    [NonSerialized] public float[] currentcharswitchbuff = new float[5];
    [NonSerialized] public float[] currentbasicdmgbuff = new float[5];

    [NonSerialized] public float[] currentsworddmg = new float[5];
    [NonSerialized] public float[] currentbowdmg = new float[5];
    [NonSerialized] public float[] currentfistdmg = new float[5];


    public GameObject showitemstatsobj;
    public Itemtextcontroller itemtextcontroller;
}
