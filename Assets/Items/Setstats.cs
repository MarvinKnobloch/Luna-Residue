using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Setstats : MonoBehaviour
{
    public GameObject slottext;
    public GameObject slotbutton;
    public TextMeshProUGUI statsnumbers;

    public Text statssworddmg;
    public Text statsbowdmg;
    public Text statsfistdmg;

    [NonSerialized] public float[] currentsworddmg = new float[5];
    [NonSerialized] public float[] currentbowdmg = new float[5];
    [NonSerialized] public float[] currentfistdmg = new float[5];


    public GameObject upgradeui;
    public Upgradeuitextcontroller upgradeuitextcontroller;
}
