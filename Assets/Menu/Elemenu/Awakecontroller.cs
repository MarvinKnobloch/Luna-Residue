using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;
using System.Linq;

public class Awakecontroller : MonoBehaviour
{
    [NonSerialized] public Stonecontroller stonecontroller;
    private SpielerSteu Steuerung;

    [SerializeField] private Inventorycontroller matsinventory;
    [SerializeField] private Craftingobject elementalstone;
    [SerializeField] private TextMeshProUGUI elemenatlstonecosttext;
    [SerializeField] private TextMeshProUGUI materialcosttext;
    [SerializeField] private TextMeshProUGUI bonusdmg;
    [SerializeField] private TextMeshProUGUI bonusdmgreduction;
    [SerializeField] private TextMeshProUGUI bonusheal;
    private int elementalstoneamount;
    private int awakematsamount;
    private int currentawakematerial;
    [SerializeField] private Craftingobject[] craftingmats;          //eine Awake kann maximal 3 verschiedene items kosten

    private InputAction awakehotkey;
    [SerializeField] private Image awakeimage;
    private bool starttimer;

    public int neededawakemats;                            //wieviel verschiedene mats zum craften benötigt sind
    public int currentawakemats;                           //wieviel verschiedene mats vorhanden sind
    public bool neededawakematsandstone;                   //wenn alle mats vorhanden sind
    public bool neededawakecore;                          //wenn der core vorhanden ist

    public bool canawake;

    private float awaketime = 0.5f;             //2.5f;
    public float awaketimer;

    private DateTime startdate;
    private DateTime currentdate;
    private float seconds;

    public int awakelvl;
    public int maxawakening;
    public Awakestone[] awake;

    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
        awakehotkey = Steuerung.Elementalmenu.Awakestone;
        GetComponentInChildren<TextMeshProUGUI>().text = "Hold " + awakehotkey.GetBindingDisplayString() + " to awake Stone";
    }
    private void OnEnable()
    {
        awakeimage.fillAmount = 0;
        elementalstonetextupdate();
        bonusdmg.text = "Bonusdmg (" + Statics.groupstonedmgbonus + "%)";
        bonusdmgreduction.text = "Dmgreduction (" + Statics.groupstonedefensebonus + "%)";
        bonusheal.text = "Bonusheal (" + Statics.groupstonehealbonus + "%)";
    }
    private void Update()
    {
        if(stonecontroller != null)
        {
            if(canawake == true)
            {
                if (Steuerung.Elementalmenu.Awakestone.WasPressedThisFrame() && starttimer == false)
                {
                    starttimer = true;
                    StartCoroutine(awakestone());
                }
                if (Steuerung.Elementalmenu.Awakestone.WasReleasedThisFrame())
                {
                    awakeimage.fillAmount = 0;
                    starttimer = false;
                    StopAllCoroutines();
                }
            }
        }
    }
    public void awakecancel()
    {
        awakeimage.fillAmount = 0;
        starttimer = false;
        StopAllCoroutines();
    }
    IEnumerator awakestone()
    {
        startdate = DateTime.Now;
        awakeimage.fillAmount = 0;
        awaketimer = 0f;
        while (awaketimer < awaketime)
        {
            currentdate = DateTime.Now;
            seconds = currentdate.Ticks - startdate.Ticks;
            awaketimer = seconds * 0.0000001f;
            awakeimage.fillAmount = awaketimer / awaketime;
            yield return null;
        }
        awakeimage.fillAmount = 0;
        stonecontroller.isactiv = true;
        Statics.stoneisactivated[stonecontroller.stonenumber] = true;
        Color colornew = stonecontroller.gameObject.GetComponent<Image>().color;
        colornew.a = 1;
        stonecontroller.gameObject.GetComponent<Image>().color = colornew;
        stonecontroller.stonecontrollerupdate();
        removeitems();
        awakelvl++;
        elementalstonetextupdate();
        starttimer = false;
    }

    public void elementalstonetextupdate()
    {
        neededawakematsandstone = false;
        canawake = false;
        if (awakelvl == maxawakening)
        {
            elemenatlstonecosttext.text = "";
            materialcosttext.text = "";
            stonecontroller.elementalcorecosttext.text = "";
        }
        else
        {
            neededawakemats = 1;
            currentawakemats = 0;
            if (elementalstone.inventoryslot != 0)
            {
                elementalstoneamount = matsinventory.Container.Items[elementalstone.inventoryslot - 1].amount;
                if (awake[awakelvl].awakemats[1].costs <= elementalstoneamount)
                {
                    currentawakemats++;
                    elemenatlstonecosttext.text = "(" + "<color=green>" + elementalstoneamount + "</color>" + "/" + awake[awakelvl].awakemats[1].costs + ")" + elementalstone.itemname;
                }
                else
                {
                    elemenatlstonecosttext.text = "(" + "<color=red>" + elementalstoneamount + "</color>" + "/" + awake[awakelvl].awakemats[1].costs + ")" + elementalstone.itemname;
                }
            }
            else
            {
                elemenatlstonecosttext.text = "(" + "<color=red>" + "0" + "</color>" + "/" + awake[awakelvl].awakemats[1].costs + ")" + elementalstone.itemname;
            }

            materialcosttext.text = "";
            awakematsamount = awake[awakelvl].awakemats.Length;
            currentawakematerial = 2;                                          // 2, weil 1. und 2. immer cores + stone sind
            int currentposi = 0;
            foreach (Craftingobject obj in craftingmats)
            {
                if (currentawakematerial < awakematsamount)
                {
                    neededawakemats++;
                    craftingmats[currentposi] = awake[awakelvl].awakemats[currentawakematerial].awakemat;                
                    if(craftingmats[currentposi].inventoryslot != 0)
                    {
                        int amount = matsinventory.Container.Items[craftingmats[currentposi].inventoryslot - 1].amount;                          //findet das item in inventroy durch die inventoryslot nummer
                        if (awake[awakelvl].awakemats[currentawakematerial].costs <= amount)
                        {
                            currentawakemats++;
                            materialcosttext.text +=  "(" + "<color=green>" + amount + "</color>" + "/" + awake[awakelvl].awakemats[currentawakematerial].costs + ")" + craftingmats[currentposi].itemname + "\n";
                        }
                        else
                        {
                            materialcosttext.text += "(" + "<color=red>" + amount + "</color>" + "/" + awake[awakelvl].awakemats[currentawakematerial].costs + ")" + craftingmats[currentposi].itemname + "\n";
                        }
                    }
                    else
                    {
                        materialcosttext.text += "(" + "<color=red>" + "0" + "</color>" + "/" + awake[awakelvl].awakemats[currentawakematerial].costs + ")" + craftingmats[currentposi].itemname + "\n";
                    }
                    currentposi++;
                    currentawakematerial++;
                }
                else
                {
                    break;
                }
            }
            if (currentawakemats == neededawakemats)
            {
                neededawakematsandstone = true;
            }
        }
    }
    public void checkforawake()
    {
        if (neededawakematsandstone == true && neededawakecore == true)
        {
            canawake = true;
        }
        else
        {
            canawake = false;
        }
    }
    private void removeitems()
    {
        matsinventory.Container.Items[elementalstone.inventoryslot - 1].amount -= awake[awakelvl].awakemats[1].costs;             //remove stone

        awakematsamount = awake[awakelvl].awakemats.Length;
        currentawakematerial = 2;                                          // 2, weil 1. und 2. immer cores + stone sind
        int currentposi = 0;
        foreach (Craftingobject obj in craftingmats)
        {
            if (currentawakematerial < awakematsamount)
            {
                matsinventory.Container.Items[craftingmats[currentposi].inventoryslot - 1].amount -= awake[awakelvl].awakemats[currentawakematerial].costs;         //  matsinventory.Container.Items[craftingmats[currentposi].inventoryslot - 1], findet das item in inventroy durch die inventoryslot nummer
                currentposi++;
                currentawakematerial++;
            }
            else
            {
                return;
            }
        }
    }


    [System.Serializable]
    public class Awakestone
    {
        public Awakestonemats[] awakemats;                    // 0 sind immer die Cores, 1 ist immer der Stone
    }

    [System.Serializable]
    public class Awakestonemats
    {
        public Craftingobject awakemat;
        public int costs;
    }
}
