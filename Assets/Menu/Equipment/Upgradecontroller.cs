using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;

public class Upgradecontroller : MonoBehaviour
{
    private SpielerSteu Steuerung;
    private InputAction upgradehotkey;
    [SerializeField] private GameObject resetonpointenterlayer;
    [NonSerialized] public Chooseitem chooseitem;
    [NonSerialized] public Chooseweapon chooseweapon;

    public Itemcontroller itemtoupgrade;
    private Image upgradeimage;
    private TextMeshProUGUI upgradetext;
    public bool canupgrade;

    [SerializeField] Itemtextcontroller itemtextcontroller;

    public Inventorycontroller matsinventory;
    public int[] iteminventoryposi = new int[5];                  //es dürfen maximal 5 mats zum übergraden verwendet werden
    public int[] upgrademinusvalue = new int[5];

    private float upgradetime = 2.5f;
    public float upgradetimer;
    private bool starttimer;

    private DateTime startdate;
    private DateTime currentdate;
    private float seconds;

    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
        upgradehotkey = Steuerung.Equipmentmenu.Upgradeitem;
        upgradeimage = GetComponent<Image>();
        upgradeimage.fillAmount = 0;
        upgradetext = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        upgradetext.text = "hold " + upgradehotkey.GetBindingDisplayString() + " to Upgrade";
        upgradeimage.fillAmount = 0;
    }
    private void Update()
    {
        if(canupgrade == true)
        {
            if (Steuerung.Equipmentmenu.Upgradeitem.WasPressedThisFrame() && starttimer == false)
            {
                starttimer = true;
                StartCoroutine(upgradeitemstart());
            }
            if (Steuerung.Equipmentmenu.Upgradeitem.WasReleasedThisFrame())
            {
                upgradeimage.fillAmount = 0;
                starttimer = false;
                StopAllCoroutines();
            }
        }
    }
    IEnumerator upgradeitemstart()
    {
        startdate = DateTime.Now;
        upgradeimage.fillAmount = 0;
        upgradetimer = 0f;
        while (upgradetimer < upgradetime)
        {
            currentdate = DateTime.Now;
            seconds = currentdate.Ticks - startdate.Ticks;
            upgradetimer = seconds * 0.0000001f;
            upgradeimage.fillAmount = upgradetimer / upgradetime;
            yield return null;
        }
        upgradeimage.fillAmount = 0;
        if (itemtoupgrade.upgradelvl < itemtoupgrade.maxupgradelvl)
        {
            upgradeitem();
        }
        starttimer = false;
    }
    private void upgradeitem()
    {
        int currentint = 0;
        foreach (float stat in itemtoupgrade.stats)
        {
            itemtoupgrade.stats[currentint] = itemtoupgrade.upgrades[itemtoupgrade.upgradelvl].newstats[currentint];
            currentint++;
        }
        removemats();
        itemtoupgrade.upgradelvl++;
        chooseitem.upgradeequipeditems();
        itemtextcontroller.textupdate();
        resetonpointenterlayer.SetActive(true);
    }
    private void removemats()
    {
        for (int i = 0; i < itemtoupgrade.upgrades[itemtoupgrade.upgradelvl].Upgrademats.Length; i++)
        {
            matsinventory.Container.Items[iteminventoryposi[i]].amount -= upgrademinusvalue[i];
        }

    }
}


//public float deltaTime;
//public DateTime tp1;
//private DateTime tp2;
/*
IEnumerator upgraditem()
{
    tp1 = DateTime.Now;
    tp2 = DateTime.Now;
    Debug.Log("hallo");
    deltaTime = 0f;
    upgradetimer = 0f;
    while (upgradetimer < upgradetime)
    {
        tp2 = DateTime.Now;
        deltaTime = (float)((tp2.Ticks - tp1.Ticks) / 10000000.0);
        tp1 = tp2;
        upgradetimer += deltaTime;
        yield return null;
    }
    Debug.Log("upgradecomplete");
    starttimer = false;

}*/

/*IEnumerator upgraditem()
{
    //private Stopwatch stopwatch = new Stopwatch();
    //public string time;

        //stopwatch.Start();
        //stopwatch.Stop();
        //stopwatch.Reset();

    upgradeimage.fillAmount = 0;
    stopwatch.Reset();
    UnityEngine.Debug.Log("hallo");
    upgradetimer = 0f;
    while (upgradetimer < upgradetime)
    {
        stopwatch.Start();
        stopwatch.Stop();
        TimeSpan ts = stopwatch.Elapsed;
        seconds = Convert.ToSingle(ts.TotalMilliseconds);
        upgradetimer = seconds * 120;
        upgradeimage.fillAmount = upgradetimer / upgradetime;
        yield return null;
    }
    UnityEngine.Debug.Log("upgradecomplete");
    upgradeimage.fillAmount = 0;
    starttimer = false;
}*/
