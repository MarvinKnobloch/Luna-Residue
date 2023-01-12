using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stonegridcontroller : MonoBehaviour
{
    public Elemenucontroller elemenucontroller;
    public GameObject awakemessage;
    public Inventorycontroller matsinventory;
    public TextMeshProUGUI elementalcorecosttext;

    [SerializeField] private Setspells spellslot1;
    [SerializeField] private Setspells spellslot2;
    [SerializeField] private Setspells spellslot3;
    [SerializeField] private Setspells spellslot4;
    [SerializeField] private Setspells spellslot5;
    [SerializeField] private Setspells spellslot6;
    [SerializeField] private Setspells spellslot7;
    [SerializeField] private Setspells spellslot8;

    private int firstchar;
    private int secondchar;
    private int thirdchar;
    private int forthchar;

    private void OnEnable()
    {
        firstchar = PlayerPrefs.GetInt("Maincharindex");
        secondchar = PlayerPrefs.GetInt("Secondcharindex");
        thirdchar = PlayerPrefs.GetInt("Thirdcharindex");
        forthchar = PlayerPrefs.GetInt("Forthcharindex");
    }
    public void checkforcorrectelementinslot(int stoneelement)
    {
        if(Elemenucontroller.currentelemenuchar == 0)
        {
            if (spellslot1.spellelement != stoneelement && Statics.characterbaseelements[firstchar] != spellslot1.spellelement)
            {
                Statics.spellnumbers[0] = 24;
                Statics.spellcolors[0] = Color.white;
                spellslot1.GetComponent<Image>().color = Color.white;
                spellslot1.GetComponentInChildren<TextMeshProUGUI>().text = "";
                spellslot1.GetComponent<Dragslotspell>().gotspell = false;
                spellslot1.GetComponent<Setspells>().spellnumber = 24;
                spellslot1.spellelement = 8;
            }
            if (spellslot2.spellelement != stoneelement && Statics.characterbaseelements[firstchar] != spellslot2.spellelement)
            {
                Statics.spellnumbers[1] = 24;
                Statics.spellcolors[1] = Color.white;
                spellslot2.GetComponent<Image>().color = Color.white;
                spellslot2.GetComponent<Dragslotspell>().gotspell = false;
                spellslot2.GetComponentInChildren<TextMeshProUGUI>().text = "";
                spellslot2.GetComponent<Setspells>().spellnumber = 24;
                spellslot2.spellelement = 8;
            }
        }
        else if (Elemenucontroller.currentelemenuchar == 1)
        {
            if (spellslot3.spellelement != stoneelement && Statics.characterbaseelements[secondchar] != spellslot3.spellelement)
            {
                Statics.spellnumbers[2] = 24;
                Statics.spellcolors[2] = Color.white;
                spellslot3.GetComponent<Image>().color = Color.white;
                spellslot3.GetComponentInChildren<TextMeshProUGUI>().text = "";
                spellslot3.GetComponent<Dragslotspell>().gotspell = false;
                spellslot3.GetComponent<Setspells>().spellnumber = 24;
                spellslot3.spellelement = 8;
            }
            if (spellslot4.spellelement != stoneelement && Statics.characterbaseelements[secondchar] != spellslot4.spellelement)
            {
                Statics.spellnumbers[3] = 24;
                Statics.spellcolors[3] = Color.white;
                spellslot4.GetComponent<Image>().color = Color.white;
                spellslot4.GetComponentInChildren<TextMeshProUGUI>().text = "";
                spellslot4.GetComponent<Dragslotspell>().gotspell = false;
                spellslot4.GetComponent<Setspells>().spellnumber = 24;
                spellslot4.spellelement = 8;
            }
        }
        else if (Elemenucontroller.currentelemenuchar == 2)
        {
            if (spellslot5.spellelement != stoneelement && spellslot5.spellelement != Statics.characterbaseelements[thirdchar])
            {
                if (PlayerPrefs.GetInt("Forthcharindex") < Statics.playablechars)
                {
                    if(spellslot5.spellelement != Statics.characterbaseelements[forthchar] && spellslot5.spellelement != Statics.charactersecondelement[forthchar])
                    {
                        Statics.spellnumbers[4] = 24;
                        Statics.spellcolors[4] = Color.white;
                        spellslot5.GetComponent<Image>().color = Color.white;
                        spellslot5.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        spellslot5.GetComponent<Dragslotspell>().gotspell = false;
                        spellslot5.spellelement = 8;
                    }
                }
                else
                {
                    Statics.spellnumbers[4] = 24;
                    Statics.spellcolors[4] = Color.white;
                    spellslot5.GetComponent<Image>().color = Color.white;
                    spellslot5.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    spellslot5.GetComponent<Dragslotspell>().gotspell = false;
                    spellslot5.spellelement = 8;
                }
            }
            if (spellslot6.spellelement != stoneelement && spellslot6.spellelement != Statics.characterbaseelements[thirdchar])
            {
                if (PlayerPrefs.GetInt("Forthcharindex") < Statics.playablechars)
                {
                    if (spellslot6.spellelement != Statics.characterbaseelements[forthchar] && spellslot6.spellelement != Statics.charactersecondelement[forthchar])
                    {
                        Statics.spellnumbers[5] = 24;
                        Statics.spellcolors[5] = Color.white;
                        spellslot6.GetComponent<Image>().color = Color.white;
                        spellslot6.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        spellslot6.GetComponent<Dragslotspell>().gotspell = false;
                        spellslot6.GetComponent<Setspells>().spellnumber = 24;
                        spellslot6.spellelement = 8;
                    }
                }
                else
                {
                    Statics.spellnumbers[5] = 24;
                    Statics.spellcolors[5] = Color.white;
                    spellslot6.GetComponent<Image>().color = Color.white;
                    spellslot6.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    spellslot6.GetComponent<Dragslotspell>().gotspell = false;
                    spellslot6.GetComponent<Setspells>().spellnumber = 24;
                    spellslot6.spellelement = 8;
                }
            }
            if (spellslot7.spellelement != stoneelement && spellslot7.spellelement != Statics.characterbaseelements[thirdchar])
            {
                if (PlayerPrefs.GetInt("Forthcharindex") < Statics.playablechars)
                {
                    if (spellslot7.spellelement != Statics.characterbaseelements[forthchar] && spellslot7.spellelement != Statics.charactersecondelement[forthchar])
                    {
                        Statics.spellnumbers[6] = 24;
                        Statics.spellcolors[6] = Color.white;
                        spellslot7.GetComponent<Image>().color = Color.white;
                        spellslot7.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        spellslot7.GetComponent<Dragslotspell>().gotspell = false;
                        spellslot7.GetComponent<Setspells>().spellnumber = 24;
                        spellslot7.spellelement = 8;
                    }
                }
                else
                {
                    Statics.spellnumbers[6] = 24;
                    Statics.spellcolors[6] = Color.white;
                    spellslot7.GetComponent<Image>().color = Color.white;
                    spellslot7.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    spellslot7.GetComponent<Dragslotspell>().gotspell = false;
                    spellslot7.GetComponent<Setspells>().spellnumber = 24;
                    spellslot7.spellelement = 8;
                }
            }
            if (spellslot8.spellelement != stoneelement && spellslot8.spellelement != Statics.characterbaseelements[thirdchar])
            {
                if (PlayerPrefs.GetInt("Forthcharindex") < Statics.playablechars)
                {
                    if (spellslot8.spellelement != Statics.characterbaseelements[forthchar] && spellslot8.spellelement != Statics.charactersecondelement[forthchar])
                    {
                        Statics.spellnumbers[7] = 24;
                        Statics.spellcolors[7] = Color.white;
                        spellslot8.GetComponent<Image>().color = Color.white;
                        spellslot8.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        spellslot8.GetComponent<Dragslotspell>().gotspell = false;
                        spellslot8.GetComponent<Setspells>().spellnumber = 24;
                        spellslot8.spellelement = 8;
                    }
                }
                else
                {
                    Statics.spellnumbers[7] = 24;
                    Statics.spellcolors[7] = Color.white;
                    spellslot8.GetComponent<Image>().color = Color.white;
                    spellslot8.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    spellslot8.GetComponent<Dragslotspell>().gotspell = false;
                    spellslot8.GetComponent<Setspells>().spellnumber = 24;
                    spellslot8.spellelement = 8;
                }
            }
        }
        else if (Elemenucontroller.currentelemenuchar == 3)
        {
            if (spellslot5.spellelement != stoneelement && spellslot5.spellelement != Statics.characterbaseelements[forthchar])
            {
                if (PlayerPrefs.GetInt("Thirdcharindex") < Statics.playablechars)
                {
                    if(spellslot5.spellelement != Statics.characterbaseelements[thirdchar] && spellslot5.spellelement != Statics.charactersecondelement[thirdchar])
                    {
                        Statics.spellnumbers[4] = 24;
                        Statics.spellcolors[4] = Color.white;
                        spellslot5.GetComponent<Image>().color = Color.white;
                        spellslot5.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        spellslot5.GetComponent<Dragslotspell>().gotspell = false;
                        spellslot5.GetComponent<Setspells>().spellnumber = 24;
                        spellslot5.spellelement = 8;
                    }
                }
                else
                {
                    Statics.spellnumbers[4] = 24;
                    Statics.spellcolors[4] = Color.white;
                    spellslot5.GetComponent<Image>().color = Color.white;
                    spellslot5.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    spellslot5.GetComponent<Dragslotspell>().gotspell = false;
                    spellslot5.GetComponent<Setspells>().spellnumber = 24;
                    spellslot5.spellelement = 8;
                }
            }
            if (spellslot6.spellelement != stoneelement && spellslot6.spellelement != Statics.characterbaseelements[forthchar])
            {
                if (PlayerPrefs.GetInt("Thirdcharindex") < Statics.playablechars)
                {
                    if(spellslot6.spellelement != Statics.characterbaseelements[thirdchar] && spellslot6.spellelement != Statics.charactersecondelement[thirdchar])
                    {
                        Statics.spellnumbers[5] = 24;
                        Statics.spellcolors[5] = Color.white;
                        spellslot6.GetComponent<Image>().color = Color.white;
                        spellslot6.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        spellslot6.GetComponent<Dragslotspell>().gotspell = false;
                        spellslot6.GetComponent<Setspells>().spellnumber = 24;
                        spellslot6.spellelement = 8;
                    }
                }
                else
                {
                    Statics.spellnumbers[5] = 24;
                    Statics.spellcolors[5] = Color.white;
                    spellslot6.GetComponent<Image>().color = Color.white;
                    spellslot6.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    spellslot6.GetComponent<Dragslotspell>().gotspell = false;
                    spellslot6.GetComponent<Setspells>().spellnumber = 24;
                    spellslot6.spellelement = 8;
                }
            }
            if (spellslot7.spellelement != stoneelement && spellslot7.spellelement != Statics.characterbaseelements[forthchar])
            {
                if (PlayerPrefs.GetInt("Thirdcharindex") < Statics.playablechars)
                {
                    if(spellslot7.spellelement != Statics.characterbaseelements[thirdchar] && spellslot7.spellelement != Statics.charactersecondelement[thirdchar])
                    {
                        Statics.spellnumbers[6] = 24;
                        Statics.spellcolors[6] = Color.white;
                        spellslot7.GetComponent<Image>().color = Color.white;
                        spellslot7.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        spellslot7.GetComponent<Dragslotspell>().gotspell = false;
                        spellslot7.GetComponent<Setspells>().spellnumber = 24;
                        spellslot7.spellelement = 8;
                    }
                }
                else
                {
                    Statics.spellnumbers[6] = 24;
                    Statics.spellcolors[6] = Color.white;
                    spellslot7.GetComponent<Image>().color = Color.white;
                    spellslot7.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    spellslot7.GetComponent<Dragslotspell>().gotspell = false;
                    spellslot7.GetComponent<Setspells>().spellnumber = 24;
                    spellslot7.spellelement = 8;
                }
            }
            if (spellslot8.spellelement != stoneelement && spellslot8.spellelement != Statics.characterbaseelements[forthchar])
            {
                if (PlayerPrefs.GetInt("Thirdcharindex") < Statics.playablechars)
                {
                    if(spellslot8.spellelement != Statics.characterbaseelements[thirdchar] && spellslot8.spellelement != Statics.charactersecondelement[thirdchar])
                    {
                        Statics.spellnumbers[7] = 24;
                        Statics.spellcolors[7] = Color.white;
                        spellslot8.GetComponent<Image>().color = Color.white;
                        spellslot8.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        spellslot8.GetComponent<Dragslotspell>().gotspell = false;
                        spellslot8.GetComponent<Setspells>().spellnumber = 24;
                        spellslot8.spellelement = 8;
                    }
                }
                else
                {
                    Statics.spellnumbers[7] = 24;
                    Statics.spellcolors[7] = Color.white;
                    spellslot8.GetComponent<Image>().color = Color.white;
                    spellslot8.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    spellslot8.GetComponent<Dragslotspell>().gotspell = false;
                    spellslot8.GetComponent<Setspells>().spellnumber = 24;
                    spellslot8.spellelement = 8;
                }
            }
        }
    }
}
/*if (spellslot6.spellelement != stoneelement && spellslot6.spellelement != Statics.characterbaseelements[thirdchar] && spellslot6.spellelement != Statics.characterbaseelements[forthchar] && spellslot6.spellelement != Statics.charactersecondelement[forthchar])
{
    Statics.spellnumbers[5] = 24;
    Statics.spellcolors[5] = Color.white;
    spellslot6.GetComponent<Image>().color = Color.white;
    spellslot6.GetComponentInChildren<TextMeshProUGUI>().text = "";
    spellslot6.GetComponent<Dragslotspell>().gotspell = false;
    spellslot6.GetComponent<Setspells>().spellnumber = 24;
    spellslot6.spellelement = 8;
}
if (spellslot7.spellelement != stoneelement && spellslot7.spellelement != Statics.characterbaseelements[thirdchar] && spellslot7.spellelement != Statics.characterbaseelements[forthchar] && spellslot7.spellelement != Statics.charactersecondelement[forthchar])
{
    Statics.spellnumbers[6] = 24;
    Statics.spellcolors[6] = Color.white;
    spellslot7.GetComponent<Image>().color = Color.white;
    spellslot7.GetComponentInChildren<TextMeshProUGUI>().text = "";
    spellslot7.GetComponent<Dragslotspell>().gotspell = false;
    spellslot7.GetComponent<Setspells>().spellnumber = 24;
    spellslot7.spellelement = 8;
}
if (spellslot8.spellelement != stoneelement && spellslot8.spellelement != Statics.characterbaseelements[thirdchar] && spellslot8.spellelement != Statics.characterbaseelements[forthchar] && spellslot8.spellelement != Statics.charactersecondelement[forthchar])
{
    Statics.spellnumbers[7] = 24;
    Statics.spellcolors[7] = Color.white;
    spellslot8.GetComponent<Image>().color = Color.white;
    spellslot8.GetComponentInChildren<TextMeshProUGUI>().text = "";
    spellslot8.GetComponent<Dragslotspell>().gotspell = false;
    spellslot8.GetComponent<Setspells>().spellnumber = 24;
    spellslot8.spellelement = 8;
}*/