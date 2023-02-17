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

    [SerializeField] private Setspells[] spellslots;

    private int firstchar;
    private int secondchar;
    private int thirdchar;
    private int forthchar;

    private void Awake()
    {
        setstonesafterload();
    }
    private void setstonesafterload()
    {
        int i = 0;
        foreach (Transform obj in transform)
        {
            obj.GetComponentInChildren<Stonecontroller>().stonenumber = i;
            if (Statics.stoneisactivated[i] == true)
            {
                obj.GetComponentInChildren<Stonecontroller>().isactiv = true;
                Color colornew = obj.GetComponentInChildren<Image>().color;
                colornew.a = 1;
                obj.GetComponentInChildren<Image>().color = colornew;
            }
            i++;
        }
    }
    private void OnEnable()
    {
        firstchar = Statics.currentfirstchar;
        secondchar = Statics.currentsecondchar;
        thirdchar = Statics.currentthirdchar;
        forthchar = Statics.currentforthchar;
    }
    public void resetslotifnewelement(int newstoneelement)
    {
        if (Elemenucontroller.currentelemenuchar == 0)
        {
            checkslotforreset(0, newstoneelement, Statics.characterbaseelements[firstchar]);          //stoneelement = das element das neu zugewiesen wird. Statics = das baseelement vom character
            checkslotforreset(1, newstoneelement, Statics.characterbaseelements[firstchar]);
        }
        else if (Elemenucontroller.currentelemenuchar == 1)
        {
            checkslotforreset(2, newstoneelement, Statics.characterbaseelements[secondchar]);
            checkslotforreset(3, newstoneelement, Statics.characterbaseelements[secondchar]);
        }
        else if (Elemenucontroller.currentelemenuchar == 2)
        {
            if (Statics.currentforthchar != -1)
            {
                checksupportcharsslotforreset(4, newstoneelement, Statics.characterbaseelements[thirdchar], Statics.characterbaseelements[forthchar], Statics.charactersecondelement[forthchar]);
                checksupportcharsslotforreset(5, newstoneelement, Statics.characterbaseelements[thirdchar], Statics.characterbaseelements[forthchar], Statics.charactersecondelement[forthchar]);
                checksupportcharsslotforreset(6, newstoneelement, Statics.characterbaseelements[thirdchar], Statics.characterbaseelements[forthchar], Statics.charactersecondelement[forthchar]);
                checksupportcharsslotforreset(7, newstoneelement, Statics.characterbaseelements[thirdchar], Statics.characterbaseelements[forthchar], Statics.charactersecondelement[forthchar]);
            }
            else                    //falls kein 4. char vorhanden muss es trotzdem für den 3. upgedated werden
            {
                checkslotforreset(4, newstoneelement, Statics.characterbaseelements[thirdchar]);
                checkslotforreset(5, newstoneelement, Statics.characterbaseelements[thirdchar]);
                checkslotforreset(6, newstoneelement, Statics.characterbaseelements[thirdchar]);
                checkslotforreset(8, newstoneelement, Statics.characterbaseelements[thirdchar]);
            }
        }
        else if (Elemenucontroller.currentelemenuchar == 3)
        {
            if (Statics.currentthirdchar != -1)
            {
                checksupportcharsslotforreset(4, newstoneelement, Statics.characterbaseelements[forthchar], Statics.characterbaseelements[thirdchar], Statics.charactersecondelement[thirdchar]);
                checksupportcharsslotforreset(5, newstoneelement, Statics.characterbaseelements[forthchar], Statics.characterbaseelements[thirdchar], Statics.charactersecondelement[thirdchar]);
                checksupportcharsslotforreset(6, newstoneelement, Statics.characterbaseelements[forthchar], Statics.characterbaseelements[thirdchar], Statics.charactersecondelement[thirdchar]);
                checksupportcharsslotforreset(7, newstoneelement, Statics.characterbaseelements[forthchar], Statics.characterbaseelements[thirdchar], Statics.charactersecondelement[thirdchar]);
            }
            else
            {
                checkslotforreset(4, newstoneelement, Statics.characterbaseelements[forthchar]);
                checkslotforreset(5, newstoneelement, Statics.characterbaseelements[forthchar]);
                checkslotforreset(6, newstoneelement, Statics.characterbaseelements[forthchar]);
                checkslotforreset(8, newstoneelement, Statics.characterbaseelements[forthchar]);
            }
        }
    }
    private void checkslotforreset(int spellslot, int newstoneelement, int charbaseelement)
    {
        if (spellslots[spellslot].spellelement != newstoneelement && spellslots[spellslot].spellelement != charbaseelement)
        {
            Statics.spellnumbers[spellslot] = -1;
            Statics.spellcolors[spellslot] = Color.white;
            spellslots[spellslot].GetComponent<Image>().color = Color.white;
            spellslots[spellslot].GetComponentInChildren<TextMeshProUGUI>().text = "";
            spellslots[spellslot].GetComponent<Dragslotspell>().gotspell = false;
            spellslots[spellslot].GetComponent<Setspells>().spellnumber = -1;
            spellslots[spellslot].spellelement = -1;
        }
    }
    private void checksupportcharsslotforreset(int spellslot, int newstonelement, int charbaseelement, int othercharbaseelement, int otercharsecondelement)
    {
        int slotint = spellslots[spellslot].spellelement;
        if (slotint != newstonelement && slotint != charbaseelement && slotint != othercharbaseelement && slotint != otercharsecondelement)
        {
            Statics.spellnumbers[spellslot] = -1;
            Statics.spellcolors[spellslot] = Color.white;
            spellslots[spellslot].GetComponent<Image>().color = Color.white;
            spellslots[spellslot].GetComponentInChildren<TextMeshProUGUI>().text = "";
            spellslots[spellslot].GetComponent<Dragslotspell>().gotspell = false;
            spellslots[spellslot].GetComponent<Setspells>().spellnumber = -1;
            spellslots[spellslot].spellelement = -1;
        }
    }
}