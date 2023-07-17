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
    private SpielerSteu controlls;

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

    [SerializeField] private Image awakeimage;
    private bool starttimer;

    public int neededawakemats;                            //wieviel verschiedene mats zum craften benötigt sind
    public int requiredmatsamountavailable;                    //wieviel verschiedene mats vorhanden sind
    public bool neededawakematsandstone;                   //wenn alle mats vorhanden sind
    public bool neededawakecore;                          //wenn der core vorhanden ist

    public bool canawake;

    private float awaketime = 2f;             //2.5f;
    public float awaketimer;

    private DateTime startdate;
    private DateTime currentdate;
    private float seconds;

    public int awakelvl;
    public int maxawakening;
    public Awakestone[] awake;

    [SerializeField] private GameObject Uimessage;
    [SerializeField] private Menusoundcontroller menusoundcontroller;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        awakecancel();
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
                if (controlls.Elementalmenu.Awakestone.WasPressedThisFrame() && starttimer == false)
                {
                    starttimer = true;
                    checkforfirstawakes();
                }
                if (controlls.Elementalmenu.Awakestone.WasReleasedThisFrame())
                {
                    awakeimage.fillAmount = 0;
                    starttimer = false;
                    StopAllCoroutines();
                }
            }
        }
    }
    private void checkforfirstawakes()
    {
        if(Statics.groupstonedefensebonus != 0 && Statics.groupstonehealbonus != 0) StartCoroutine(awakestone());
        else
        {
            if(stonecontroller.stoneclassroll == 1) StartCoroutine(awakestone());
            else
            {
                if (stonecontroller.stoneclassroll == 2)
                {
                    if (Statics.groupstonedefensebonus != 0) StartCoroutine(awakestone());
                    else
                    {
                        Uimessage.SetActive(true);
                        Uimessage.GetComponentInChildren<TextMeshProUGUI>().text = "Awake one Guard stone first";
                    }
                }
                else if(stonecontroller.stoneclassroll == 0)
                {
                    if(Statics.groupstonedefensebonus == 0)
                    {
                        Uimessage.SetActive(true);
                        Uimessage.GetComponentInChildren<TextMeshProUGUI>().text = "Awake one Guard stone first";
                    }
                    else if (Statics.groupstonehealbonus == 0)
                    {
                        Uimessage.SetActive(true);
                        Uimessage.GetComponentInChildren<TextMeshProUGUI>().text = "Awake one Heal stone first";
                    }
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
        menusoundcontroller.playmenubuttonsound();
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
            requiredmatsamountavailable = 0;
            if (elementalstone.inventoryslot != 0)
            {
                elementalstoneamount = matsinventory.Container.Items[elementalstone.inventoryslot - 1].amount;
                if (awake[awakelvl].awakemats[1].costs <= elementalstoneamount)
                {
                    requiredmatsamountavailable++;
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
                            requiredmatsamountavailable++;
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
            if (requiredmatsamountavailable == neededawakemats)
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
