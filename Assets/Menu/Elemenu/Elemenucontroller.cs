using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class Elemenucontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject elemenu;

    [SerializeField] private GameObject[] selectablespells;
    [NonSerialized] public int elementonstoneselect;                        // gibt das element vom ausgewählten stone weiter
    [NonSerialized] public int[] currentstoneelement = { -1, -1, -1, -1};        // Anzahl der elemente + 1

    public GameObject[] charselectionimage;
    public GameObject[] charbutton;
    [SerializeField] private Image[] abilityimages;
    [SerializeField] private TextMeshProUGUI[] spelltext;
    public GameObject[] characterstoneimage;
    public TextMeshProUGUI[] characterstonetext;
    public static int currentelemenuchar;
    public int clickedspell;
    public int clickedspell2;

    private int firstchar;
    private int secondchar;
    private int thirdchar;
    private int forthchar;

    [NonSerialized] public int stoneclassroll;                          // roll(Damage, Tank , Healer)
    [NonSerialized] public Color stonecolor;
    [NonSerialized] public string stonetext;

    [SerializeField] private TextMeshProUGUI switchchartext;
    private string switchleft;
    private string switchright;

    [SerializeField] private GameObject Uimessage;
    [SerializeField] private Menusoundcontroller menusoundcontroller;
    void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        setspellslotsafterload();
    }
    private void setspellslotsafterload()
    {
        for (int i = 0; i < Statics.spellnumbers.Length; i++)
        {
            if (Statics.spellnumbers[i] != -1)
            {
                abilityimages[i].color = Statics.spellcolors[i];
                TextMeshProUGUI spellname = spelltext[Statics.spellnumbers[i]];
                abilityimages[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = spellname.text;
                abilityimages[i].gameObject.GetComponent<Setspells>().setspellafterload(i, spellname.text);
            }
            else
            {
                abilityimages[i].color = Color.white;
                abilityimages[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
            }
        }
    }
    private void OnEnable()
    {
        controlls.Enable();

        Uimessage.SetActive(false);
        currentelemenuchar = 0;
        firstchar = Statics.currentfirstchar;
        secondchar = Statics.currentsecondchar;
        thirdchar = Statics.currentthirdchar;
        forthchar = Statics.currentforthchar;

        foreach (GameObject image in charselectionimage)
        {
            image.SetActive(false);
        }
        charselectionimage[0].SetActive(true);

        foreach (GameObject spell in selectablespells)
        {
            spell.SetActive(false);
        }
        abilityimages[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = controlls.Player.Ability1.GetBindingDisplayString();
        abilityimages[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = controlls.Player.Ability2.GetBindingDisplayString();
        abilityimages[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = controlls.Player.Ability1.GetBindingDisplayString();
        abilityimages[3].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = controlls.Player.Ability2.GetBindingDisplayString();
        abilityimages[4].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = controlls.Player.Ability3.GetBindingDisplayString();
        abilityimages[5].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = controlls.Player.Ability4.GetBindingDisplayString();

        settextandsecondelement(0, firstchar);
        selectablespells[Statics.characterbaseelements[firstchar]].SetActive(true);
        if (Statics.charactersecondelement[firstchar] != -1)
        {
            selectablespells[Statics.charactersecondelement[firstchar]].SetActive(true);
        }

        checkforactivchar(1, secondchar);
        checkforactivchar(2, thirdchar);
        checkforactivchar(3, forthchar);

        switchleft = controlls.Equipmentmenu.Switchcharleft.GetBindingDisplayString();
        switchright = controlls.Equipmentmenu.Switchcharright.GetBindingDisplayString();
        switchchartext.text = "(" + switchleft + "/" + switchright + ") or click on char name)";
    }
    private void checkforactivchar(int charslot, int charstaticint)
    {
        if(charstaticint != -1)
        {
            settextandsecondelement(charslot, charstaticint);
        }
        else setnochartext(charslot);
    }
    private void settextandsecondelement(int charslot, int charstaticint)
    {
        charbutton[charslot].GetComponentInChildren<TextMeshProUGUI>().text = Statics.characternames[charstaticint];
        charbutton[charslot].GetComponent<Image>().color = Statics.characterelementcolor[charstaticint];

        characterstoneimage[charslot].GetComponent<Image>().color = Statics.charactersecondelementcolor[charstaticint];
        characterstonetext[charslot].GetComponent<TextMeshProUGUI>().text = Statics.characterclassrolltext[charstaticint];
    }
    private void setnochartext(int charslot)
    {
        charbutton[charslot].GetComponentInChildren<TextMeshProUGUI>().text = "empty";
        charbutton[charslot].GetComponent<Image>().color = Color.white;
        characterstonetext[charslot].text = string.Empty;
        characterstoneimage[charslot].GetComponent<Image>().color = Color.white;
    }
    private void OnDisable()
    {
        charbutton[currentelemenuchar].gameObject.GetComponent<Image>().color = Color.white;
    }
    private void Update()
    {
        if (controlls.Equipmentmenu.Switchcharleft.WasPerformedThisFrame())
        {
            if (currentelemenuchar > 0) currentelemenuchar--;
            else currentelemenuchar = 3;
            choosecharbuttonpress(currentelemenuchar);
        }
        if (controlls.Equipmentmenu.Switchcharright.WasPerformedThisFrame())
        {
            if (currentelemenuchar < 3) currentelemenuchar++;
            else currentelemenuchar = 0;
            choosecharbuttonpress(currentelemenuchar);
        }
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            overview.SetActive(true);
            elemenu.SetActive(false);
            menusoundcontroller.playmenubuttonsound();
        }
    }
    public void choosecharbuttonpress(int slot)
    {
        currentelemenuchar = slot;
        if(currentelemenuchar == 0) choosefirstandsecondchar(firstchar);
        else if(currentelemenuchar == 1) choosefirstandsecondchar(secondchar);
        else if(currentelemenuchar == 2) choosethirdandforthchar(thirdchar, forthchar);
        else if(currentelemenuchar == 3) choosethirdandforthchar(forthchar, thirdchar);
    }
    public void choosefirstandsecondchar(int charstaticint)
    {
        if (charstaticint == -1)
        {
            return;
        }
        else
        {
            foreach (GameObject spell in selectablespells)
            {
                spell.SetActive(false);
            }
            if (Statics.charactersecondelement[charstaticint] != -1)
            {
                selectablespells[Statics.charactersecondelement[charstaticint]].SetActive(true);
            }
            selectablespells[Statics.characterbaseelements[charstaticint]].SetActive(true);

            foreach (GameObject image in charselectionimage)
            {
                image.SetActive(false);
            }
            charselectionimage[currentelemenuchar].SetActive(true);
            menusoundcontroller.playmenubuttonsound();
        }
    }
    public void choosethirdandforthchar(int charstaticint, int othercharstaticint)
    {
        if (charstaticint == -1)
        {
            return;
        }
        else
        {
            foreach (GameObject spell in selectablespells)
            {
                spell.SetActive(false);
            }
            if (Statics.charactersecondelement[charstaticint] != -1)
            {
                selectablespells[Statics.charactersecondelement[charstaticint]].SetActive(true);
            }
            selectablespells[Statics.characterbaseelements[charstaticint]].SetActive(true);

            if (othercharstaticint != -1)
            {
                if (Statics.charactersecondelement[othercharstaticint] != -1)
                {
                    selectablespells[Statics.charactersecondelement[othercharstaticint]].SetActive(true);
                }
                selectablespells[Statics.characterbaseelements[othercharstaticint]].SetActive(true);
            }
            foreach (GameObject image in charselectionimage)
            {
                image.SetActive(false);
            }
            charselectionimage[currentelemenuchar].SetActive(true);
            menusoundcontroller.playmenubuttonsound();
        }
    }
    
    public void choosestone()
    {
        characterstoneimage[currentelemenuchar].gameObject.GetComponent<Image>().color = stonecolor;
        characterstonetext[currentelemenuchar].text = stonetext.ToString();
        if (currentelemenuchar == 0)
        {
            setstonefirstandsecondchar(firstchar);
            Statics.characterclassroll[firstchar] = stoneclassroll;
        }
        else if(currentelemenuchar == 1)
        {
            setstonefirstandsecondchar(secondchar);
            Statics.characterclassroll[secondchar] = stoneclassroll;
        }
        else if(currentelemenuchar == 2)
        {
            setstonethirdandforthchar(thirdchar, forthchar);
            Statics.characterclassroll[thirdchar] = stoneclassroll;

        }
        else if(currentelemenuchar == 3)
        {
            setstonethirdandforthchar(forthchar, thirdchar);
            Statics.characterclassroll[forthchar] = stoneclassroll;
        }
        menusoundcontroller.playmenubuttonsound();
    }
    private void setstonefirstandsecondchar(int charstaticint)
    {
        Statics.charactersecondelementcolor[charstaticint] = stonecolor;
        Statics.characterclassrolltext[charstaticint] = stonetext;
        Statics.charactersecondelement[charstaticint] = elementonstoneselect;

        if (stoneclassroll == 1 && Statics.characterclassroll[charstaticint] != 1)               //falls der char vorher nicht guard war
        {
            Statics.charmaxhealth[charstaticint] += Statics.charcurrentlvl * Statics.guardbonushpeachlvl;
        }
        else if (stoneclassroll != 1 && Statics.characterclassroll[charstaticint] == 1)  //falls der char vorher guard war
        {
            Statics.charmaxhealth[charstaticint] -= Statics.charcurrentlvl * Statics.guardbonushpeachlvl;
            if(Statics.charcurrenthealth[charstaticint] > Statics.charmaxhealth[charstaticint])
            {
                Statics.charcurrenthealth[charstaticint] = Statics.charmaxhealth[charstaticint];
            }
        }

        foreach (GameObject spell in selectablespells)
        {
            spell.SetActive(false);
        }
        if (Statics.charactersecondelement[charstaticint] != -1)
        {
            selectablespells[Statics.charactersecondelement[charstaticint]].SetActive(true);
        }
        selectablespells[Statics.characterbaseelements[charstaticint]].SetActive(true);
    }
    private void setstonethirdandforthchar(int charstaticint, int othercharstaticint)
    {
        Statics.charactersecondelementcolor[charstaticint] = stonecolor;
        Statics.characterclassrolltext[charstaticint] = stonetext;
        Statics.charactersecondelement[charstaticint] = elementonstoneselect;

        if (stoneclassroll == 1 && Statics.characterclassroll[charstaticint] != 1)               //falls der char vorher nicht guard war
        {
            Statics.charmaxhealth[charstaticint] += Statics.charcurrentlvl * Statics.guardbonushpeachlvl;
        }
        else if (stoneclassroll != 1 && Statics.characterclassroll[charstaticint] == 1)  //falls der char vorher guard war
        {
            Statics.charmaxhealth[charstaticint] -= Statics.charcurrentlvl * Statics.guardbonushpeachlvl;
            if (Statics.charcurrenthealth[charstaticint] > Statics.charmaxhealth[charstaticint])
            {
                Statics.charcurrenthealth[charstaticint] = Statics.charmaxhealth[charstaticint];
            }
        }

        foreach (GameObject spell in selectablespells)
        {
            spell.SetActive(false);
        }
        if (Statics.charactersecondelement[charstaticint] != -1)
        {
            selectablespells[Statics.charactersecondelement[charstaticint]].SetActive(true);
        }
        selectablespells[Statics.characterbaseelements[charstaticint]].SetActive(true);

        if (othercharstaticint != -1)
        {
            if (Statics.charactersecondelement[othercharstaticint] != -1)
            {
                selectablespells[Statics.charactersecondelement[othercharstaticint]].SetActive(true);
            }
            selectablespells[Statics.characterbaseelements[othercharstaticint]].SetActive(true);
        }
    }
}
