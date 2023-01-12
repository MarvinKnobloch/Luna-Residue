using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;
using System;

public class Elemenucontroller : MonoBehaviour
{
    private SpielerSteu Steuerung;
    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject elemenu;

    [SerializeField] private GameObject[] selectablespells;
    [NonSerialized] public int elementonstoneselect;                        // gibt das element vom ausgewählten stone weiter
    [NonSerialized] public int[] currentstoneelement = { 8, 8, 8, 8};        // Anzahl der elemente + 1

    public GameObject[] charselectionimage;
    public GameObject[] charbutton;
    public GameObject[] ability1image;
    public GameObject[] ability1text;
    public GameObject[] ability2image;
    public GameObject[] ability2text;
    public GameObject[] spellimage;
    public GameObject[] spelltext;
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

    void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        Steuerung.Enable();

        firstchar = PlayerPrefs.GetInt("Maincharindex");
        secondchar = PlayerPrefs.GetInt("Secondcharindex");
        thirdchar = PlayerPrefs.GetInt("Thirdcharindex");
        forthchar = PlayerPrefs.GetInt("Forthcharindex");
        foreach (GameObject image in charselectionimage)
        {
            image.SetActive(false);
        }
        charselectionimage[0].SetActive(true);

        foreach (GameObject spell in selectablespells)
        {
            spell.SetActive(false);
        }
        if (Statics.charactersecondelement[firstchar] != 8)
        {
            selectablespells[Statics.charactersecondelement[firstchar]].SetActive(true);
            characterstoneimage[0].GetComponent<Image>().color = Statics.charactersecondelementcolor[firstchar];
            characterstonetext[0].GetComponent<TextMeshProUGUI>().text = Statics.characterclassrolltext[firstchar];
        }
        selectablespells[Statics.characterbaseelements[firstchar]].SetActive(true);

        currentelemenuchar = 0;
        charbutton[0].GetComponentInChildren<TextMeshProUGUI>().text = Statics.characternames[firstchar];
        charbutton[0].GetComponent<Image>().color = Statics.characterelementcolor[firstchar];

        charbutton[1].GetComponentInChildren<TextMeshProUGUI>().text = Statics.characternames[secondchar];
        charbutton[1].GetComponent<Image>().color = Statics.characterelementcolor[secondchar];
        if (Statics.charactersecondelement[secondchar] != 8)
        {
            characterstoneimage[1].GetComponent<Image>().color = Statics.charactersecondelementcolor[secondchar];
            characterstonetext[1].GetComponent<TextMeshProUGUI>().text = Statics.characterclassrolltext[secondchar];
        }

        if (PlayerPrefs.GetInt("Thirdcharindex") < Statics.playablechars)
        {
            charbutton[2].GetComponentInChildren<TextMeshProUGUI>().text = Statics.characternames[thirdchar];
            charbutton[2].GetComponent<Image>().color = Statics.characterelementcolor[thirdchar];
            if (Statics.charactersecondelement[thirdchar] != 8)
            {
                characterstoneimage[2].GetComponent<Image>().color = Statics.charactersecondelementcolor[thirdchar];
                characterstonetext[2].GetComponent<TextMeshProUGUI>().text = Statics.characterclassrolltext[thirdchar];
            }
        }
        else
        {
            charbutton[2].GetComponentInChildren<TextMeshProUGUI>().text = "empty";
            charbutton[2].GetComponent<Image>().color = Color.white;
            characterstonetext[2].text = "";
            characterstoneimage[2].GetComponent<Image>().color = Color.white;
        }

        if (PlayerPrefs.GetInt("Forthcharindex") < Statics.playablechars)
        {
            charbutton[3].GetComponentInChildren<TextMeshProUGUI>().text = Statics.characternames[forthchar];
            charbutton[3].GetComponent<Image>().color = Statics.characterelementcolor[forthchar];
            if (Statics.charactersecondelement[forthchar] != 8)
            {
                characterstoneimage[3].GetComponent<Image>().color = Statics.charactersecondelementcolor[forthchar];
                characterstonetext[3].GetComponent<TextMeshProUGUI>().text = Statics.characterclassrolltext[forthchar];
            }
        }
        else
        {
            charbutton[3].GetComponentInChildren<TextMeshProUGUI>().text = "empty";
            charbutton[3].GetComponent<Image>().color = Color.white;
            characterstonetext[3].text = "";
            characterstoneimage[3].GetComponent<Image>().color = Color.white;
        }
    }
    private void OnDisable()
    {
        charbutton[currentelemenuchar].gameObject.GetComponent<Image>().color = Color.white;
    }
    private void Update()
    {
        if (Steuerung.Elementalmenu._1.WasPerformedThisFrame())
        {
            choosechar(0);
        }
        if (Steuerung.Elementalmenu._2.WasPerformedThisFrame())
        {
            choosechar(1);
        }
        if (Steuerung.Elementalmenu._3.WasPerformedThisFrame())
        {
            if(PlayerPrefs.GetInt("Thirdcharindex") < Statics.playablechars)
            {
                choosechar(2);
            }
        }
        if (Steuerung.Elementalmenu._4.WasPerformedThisFrame())
        {
            if(PlayerPrefs.GetInt("Forthcharindex") < Statics.playablechars)
            {
                choosechar(3);
            }
        }
        if (Steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            overview.SetActive(true);
            elemenu.SetActive(false);
        }
    }
    public void choosechar(int newchar)
    {
        if (newchar == 0)
        {
            foreach (GameObject spell in selectablespells)
            {
                spell.SetActive(false);
            }
            if (Statics.charactersecondelement[firstchar] != 8)
            {
                selectablespells[Statics.charactersecondelement[firstchar]].SetActive(true);
            }
            selectablespells[Statics.characterbaseelements[firstchar]].SetActive(true);

            foreach (GameObject image in charselectionimage)
            {
                image.SetActive(false);
            }
            charselectionimage[newchar].SetActive(true);
        }
        else if (newchar == 1)
        {
            foreach (GameObject spell in selectablespells)
            {
                spell.SetActive(false);
            }
            if (Statics.charactersecondelement[secondchar] != 8)
            {
                selectablespells[Statics.charactersecondelement[secondchar]].SetActive(true);
            }
            selectablespells[Statics.characterbaseelements[secondchar]].SetActive(true);

            foreach (GameObject image in charselectionimage)
            {
                image.SetActive(false);
            }
            charselectionimage[newchar].SetActive(true);
        }
        else if (newchar == 2)
        {
            if (PlayerPrefs.GetInt("Thirdcharindex") >= Statics.playablechars)              //damit man die spell nicht auswählen kann falls kein 3/4 char ausgewählt ist
            {
                return;
            }
            else
            {
                foreach (GameObject spell in selectablespells)
                {
                    spell.SetActive(false);
                }
                if (Statics.charactersecondelement[thirdchar] != 8)
                {
                    selectablespells[Statics.charactersecondelement[thirdchar]].SetActive(true);
                }
                selectablespells[Statics.characterbaseelements[thirdchar]].SetActive(true);

                if (PlayerPrefs.GetInt("Forthcharindex") < Statics.playablechars)
                {
                    if (Statics.charactersecondelement[forthchar] != 8)
                    {
                        selectablespells[Statics.charactersecondelement[forthchar]].SetActive(true);
                    }
                    selectablespells[Statics.characterbaseelements[forthchar]].SetActive(true);
                }
                foreach (GameObject image in charselectionimage)
                {
                    image.SetActive(false);
                }
                charselectionimage[newchar].SetActive(true);
            }

        }
        else if (newchar == 3)
        {
            if (PlayerPrefs.GetInt("Forthcharindex") >= Statics.playablechars)
            {
                return;
            }
            else
            {
                foreach (GameObject spell in selectablespells)
                {
                    spell.SetActive(false);
                }
                if (Statics.charactersecondelement[forthchar] != 8)
                {
                    selectablespells[Statics.charactersecondelement[forthchar]].SetActive(true);
                }
                selectablespells[Statics.characterbaseelements[forthchar]].SetActive(true);

                if (PlayerPrefs.GetInt("Thirdcharindex") < Statics.playablechars)
                {
                    if (Statics.charactersecondelement[thirdchar] != 8)
                    {
                        selectablespells[Statics.charactersecondelement[thirdchar]].SetActive(true);
                    }
                    selectablespells[Statics.characterbaseelements[thirdchar]].SetActive(true);
                }
                foreach (GameObject image in charselectionimage)
                {
                    image.SetActive(false);
                }
                charselectionimage[newchar].SetActive(true);
            }         
        }
        currentelemenuchar = newchar;
    }

    public void choosestone(GameObject stoneobj)
    {
        characterstoneimage[currentelemenuchar].gameObject.GetComponent<Image>().color = stonecolor;
        characterstonetext[currentelemenuchar].text = stonetext.ToString();


        if (currentelemenuchar == 0)
        {
            Statics.charactersecondelementcolor[PlayerPrefs.GetInt("Maincharindex")] = stonecolor;
            Statics.characterclassrolltext[PlayerPrefs.GetInt("Maincharindex")] = stonetext;
            Statics.charactersecondelement[firstchar] = elementonstoneselect;
            Statics.maincharstoneclass = stoneclassroll;

            foreach (GameObject spell in selectablespells)
            {
                spell.SetActive(false);
            }
            if (Statics.charactersecondelement[firstchar] != 8)
            {
                selectablespells[Statics.charactersecondelement[firstchar]].SetActive(true);
            }
            selectablespells[Statics.characterbaseelements[firstchar]].SetActive(true);
        }
        else if (currentelemenuchar == 1)
        {
            Statics.charactersecondelementcolor[PlayerPrefs.GetInt("Secondcharindex")] = stonecolor;
            Statics.characterclassrolltext[PlayerPrefs.GetInt("Secondcharindex")] = stonetext;
            Statics.charactersecondelement[secondchar] = elementonstoneselect;
            Statics.secondcharstoneclass = stoneclassroll;

            foreach (GameObject spell in selectablespells)
            {
                spell.SetActive(false);
            }
            if (Statics.charactersecondelement[secondchar] != 8)
            {
                selectablespells[Statics.charactersecondelement[secondchar]].SetActive(true);
            }
            selectablespells[Statics.characterbaseelements[secondchar]].SetActive(true);

        }
        else if (currentelemenuchar == 2)
        {
            Statics.charactersecondelementcolor[PlayerPrefs.GetInt("Thirdcharindex")] = stonecolor;
            Statics.characterclassrolltext[PlayerPrefs.GetInt("Thirdcharindex")] = stonetext;
            Statics.charactersecondelement[thirdchar] = elementonstoneselect;
            Statics.thirdcharstoneclass = stoneclassroll;

            foreach (GameObject spell in selectablespells)
            {
                spell.SetActive(false);
            }
            if (Statics.charactersecondelement[thirdchar] != 8)
            {
                selectablespells[Statics.charactersecondelement[thirdchar]].SetActive(true);
            }
            selectablespells[Statics.characterbaseelements[thirdchar]].SetActive(true);

            if (PlayerPrefs.GetInt("Forthcharindex") < Statics.playablechars)
            {
                if (Statics.charactersecondelement[forthchar] != 8)
                {
                    selectablespells[Statics.charactersecondelement[forthchar]].SetActive(true);
                }
                selectablespells[Statics.characterbaseelements[forthchar]].SetActive(true);
            }
        }
        else if (currentelemenuchar == 3)
        {
            Statics.charactersecondelementcolor[PlayerPrefs.GetInt("Forthcharindex")] = stonecolor;
            Statics.characterclassrolltext[PlayerPrefs.GetInt("Forthcharindex")] = stonetext;
            Statics.charactersecondelement[forthchar] = elementonstoneselect;
            Statics.forthcharstoneclass = stoneclassroll;

            foreach (GameObject spell in selectablespells)
            {
                spell.SetActive(false);
            }
            if (Statics.charactersecondelement[forthchar] != 8)
            {
                selectablespells[Statics.charactersecondelement[forthchar]].SetActive(true);
            }
            selectablespells[Statics.characterbaseelements[forthchar]].SetActive(true);

            if (PlayerPrefs.GetInt("Thirdcharindex") < Statics.playablechars)
            {
                if (Statics.charactersecondelement[thirdchar] != 8)
                {
                    selectablespells[Statics.charactersecondelement[thirdchar]].SetActive(true);
                }
                selectablespells[Statics.characterbaseelements[thirdchar]].SetActive(true);
            }
        }
    }
}


/*public void chooseability1(int newspell)
{
    clickedspell = newspell;
    ability1image[currentchar].gameObject.GetComponent<Image>().color = spellimage[newspell].gameObject.GetComponent<Image>().color;
    ability1text[currentchar].gameObject.GetComponent<TextMeshProUGUI>().text = spelltext[newspell].gameObject.GetComponent<TextMeshProUGUI>().text;
    ability1text[currentchar].gameObject.GetComponent<TextMeshProUGUI>().color = spelltext[newspell].gameObject.GetComponent<TextMeshProUGUI>().color;
    if (currentchar == 0)
    {
        Statics.mainability1 = newspell;
        Statics.mainability1Image = spellimage[newspell].gameObject.GetComponent<Image>().color;
        if (Statics.currentactiveplayer == 0)
        {
            Spellbarabilities[0].color = Statics.mainability1Image;
        }
    }
    if (currentchar == 1)
    {
        Statics.secondability1 = newspell;
        Statics.secondability1Image = spellimage[newspell].gameObject.GetComponent<Image>().color;
        if (Statics.currentactiveplayer == 1)
        {
            Spellbarabilities[0].color = Statics.secondability1Image;
        }
    }
    if (currentchar == 2)
    {
        Statics.thirdability1 = newspell;
        Statics.thirdability1Image = spellimage[newspell].gameObject.GetComponent<Image>().color;
        Spellbarabilities[2].color = Statics.thirdability1Image;
    }
    if (currentchar == 3)
    {
        Statics.forthability1 = newspell;
        Statics.forthability1Image = spellimage[newspell].gameObject.GetComponent<Image>().color;
        Spellbarabilities[4].color = Statics.forthability1Image;
    }
}

public void chooseability2(int newspell)
{
    clickedspell2 = newspell;
    ability2image[currentchar].gameObject.GetComponent<Image>().color = spellimage[newspell].gameObject.GetComponent<Image>().color;
    ability2text[currentchar].gameObject.GetComponent<TextMeshProUGUI>().text = spelltext[newspell].gameObject.GetComponent<TextMeshProUGUI>().text;
    ability2text[currentchar].gameObject.GetComponent<TextMeshProUGUI>().color = spelltext[newspell].gameObject.GetComponent<TextMeshProUGUI>().color;
    if (currentchar == 0)
    {
        Statics.mainability2 = newspell;
        Statics.mainability2Image = spellimage[newspell].gameObject.GetComponent<Image>().color;
        if (Statics.currentactiveplayer == 0)
        {
            Spellbarabilities[1].color = Statics.mainability2Image;
        }
    }
    if (currentchar == 1)
    {
        Statics.secondability2 = newspell;
        Statics.secondability2Image = spellimage[newspell].gameObject.GetComponent<Image>().color;
        if (Statics.currentactiveplayer == 1)
        {
            Spellbarabilities[1].color = Statics.secondability2Image;
        }
    }
    if (currentchar == 2)
    {
        Statics.thirdability2 = newspell;
        Statics.thirdability2Image = spellimage[newspell].gameObject.GetComponent<Image>().color;
        Spellbarabilities[3].color = Statics.thirdability2Image;
    }
    if (currentchar == 3)
    {
        Statics.forthability2 = newspell;
        Statics.forthability2Image = spellimage[newspell].gameObject.GetComponent<Image>().color;
        Spellbarabilities[5].color = Statics.forthability2Image;
    }
}*/

/*public void disableoldspells()
{
    if (currentelemenuchar == 0)
    {
        if (Statics.charactersecondelement[firstchar] != 8)
        {
            if (Statics.charactersecondelement[firstchar] != Statics.characterbaseelements[firstchar])
            {
                selectablespells[Statics.charactersecondelement[firstchar]].SetActive(false);
            }
        }
    }
    else if (currentelemenuchar == 1)
    {
        if (Statics.charactersecondelement[secondchar] != 8)
        {
            if (Statics.charactersecondelement[secondchar] != Statics.characterbaseelements[secondchar])
            {
                selectablespells[Statics.charactersecondelement[secondchar]].SetActive(false);
            }
        }
    }
    else if (currentelemenuchar == 2)
    {

        if (Statics.charactersecondelement[thirdchar] != 8)
        {
            if (Statics.charactersecondelement[thirdchar] != Statics.characterbaseelements[thirdchar])
            {
                selectablespells[Statics.charactersecondelement[thirdchar]].SetActive(false);
            }
        }
    }
    else if (currentelemenuchar == 3)
    {
        if (Statics.charactersecondelement[forthchar] != 8)
        {
            if (Statics.charactersecondelement[forthchar] != Statics.characterbaseelements[forthchar])
            {
                selectablespells[Statics.charactersecondelement[forthchar]].SetActive(false);
            }
        }
    }
}*/
