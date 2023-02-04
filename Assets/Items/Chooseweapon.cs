using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class Chooseweapon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject slotbuttontext;
    private GameObject slotbutton;

    [NonSerialized] public Itemcontroller itemvalues;

    private Setstats setitemobj;

    [NonSerialized] public Text upgradelvltext;

    [NonSerialized] public int equipslotnumber;                       //wird im Inventoryui gesetzt

    public int currentint = 0;

    private void Awake()
    {
        setitemobj = GetComponentInParent<Setstats>();
        slotbuttontext = gameObject.GetComponentInParent<Setstats>().slottext;
        slotbutton = gameObject.GetComponentInParent<Setstats>().slotbutton;
    }
    public void setweapon()
    {
        currentint = Statics.currentequipmentchar;
        if (equipslotnumber == 0)
        {
            if (itemvalues != null)
            {
                slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;

                Statics.charswordattack[currentint] += itemvalues.stats[2] - setitemobj.currentsworddmg[currentint];                               //falls ich items hinzufügen will die + weapondmg geben, ansonsten müsste ich nicht den currentstat speichern weil der weapondmg nur von einer scoure kommt
                setitemobj.currentsworddmg[currentint] = itemvalues.stats[2];
                setitemobj.statssworddmg.text = itemvalues.stats[2] + "";
                setitemobj.statssworddmg.color = Color.white;

                if (Statics.currentswordimage[currentint] != null)
                {
                    Statics.currentswordimage[currentint].transform.GetComponent<Image>().color = Color.white;
                    //Statics.currentswordimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;                //setzt das alte image beim wechseln auf weiß(falls vorhanden)
                }
                gameObject.GetComponent<Image>().color = Color.green;                                                         //setzt das neue image beim wechseln auf grün
                Statics.charcurrentswordname[currentint] = GetComponentInChildren<Text>().text;
                Statics.currentswordimage[currentint] = this.gameObject;                                                                           //speichert das image vom char im static
                Statics.activeswordslot = this.gameObject;
            }
            EventSystem.current.SetSelectedGameObject(slotbutton);
        }
        else if (equipslotnumber == 1)
        {
            if (itemvalues != null)
            {
                slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;

                Statics.charbowattack[currentint] += itemvalues.stats[2] - setitemobj.currentbowdmg[currentint];
                setitemobj.currentbowdmg[currentint] = itemvalues.stats[2];
                setitemobj.statsbowdmg.text = itemvalues.stats[2] + "";
                setitemobj.statsbowdmg.color = Color.white;

                if (Statics.currentbowimage[currentint] != null)
                {
                    Statics.currentbowimage[currentint].transform.GetComponent<Image>().color = Color.white;
                }
                gameObject.GetComponent<Image>().color = Color.green;
                Statics.charcurrentbowname[currentint] = GetComponentInChildren<Text>().text;
                Statics.currentbowimage[currentint] = this.gameObject;
                Statics.activebowslot = this.gameObject;
            }
            EventSystem.current.SetSelectedGameObject(slotbutton);
        }
        else if (equipslotnumber == 2)
        {
            if (itemvalues != null)
            {
                slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;

                Statics.charfistattack[currentint] += itemvalues.stats[2] - setitemobj.currentfistdmg[currentint];
                setitemobj.currentfistdmg[currentint] = itemvalues.stats[2];
                setitemobj.statsfistdmg.text = itemvalues.stats[2] + "";
                setitemobj.statsfistdmg.color = Color.white;

                if (Statics.currentfistimage[currentint] != null)
                {
                    Statics.currentfistimage[currentint].transform.GetComponent<Image>().color = Color.white;
                }
                gameObject.GetComponent<Image>().color = Color.green;
                Statics.charcurrentfistname[currentint] = GetComponentInChildren<Text>().text;
                Statics.currentfistimage[currentint] = this.gameObject;
                Statics.activefistslot = this.gameObject;
            }
            EventSystem.current.SetSelectedGameObject(slotbutton);
        }
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //gameObject.GetComponent<Image>().color = Color.gray;
        currentint = Statics.currentequipmentchar;
        if (equipslotnumber == 0)
        {
            float currentsword = setitemobj.currentsworddmg[currentint];
            if (itemvalues != null)
            {
                if (itemvalues.stats[2] > currentsword)
                {
                    setitemobj.statssworddmg.color = Color.green;
                    setitemobj.statssworddmg.text = "( +" + (itemvalues.stats[2] - currentsword) + " ) " + (itemvalues.stats[2] + Statics.charswordattack[currentint] - currentsword);
                }
                else if (itemvalues.stats[2] < currentsword)
                {
                    setitemobj.statssworddmg.color = Color.red;
                    setitemobj.statssworddmg.text = "( " + (itemvalues.stats[2] - currentsword) + " ) " + (itemvalues.stats[2] + Statics.charswordattack[currentint] - currentsword);
                }
            }
        }
        else if (equipslotnumber == 1)
        {
            float currentbow = setitemobj.currentbowdmg[currentint];
            if (itemvalues != null)
            {
                if (itemvalues.stats[2] > currentbow)
                {
                    setitemobj.statsbowdmg.color = Color.green;
                    setitemobj.statsbowdmg.text = "( +" + (itemvalues.stats[2] - currentbow) + " ) " + (itemvalues.stats[2] + Statics.charbowattack[currentint] - currentbow);
                }
                else if (itemvalues.stats[2] < currentbow)
                {
                    setitemobj.statsbowdmg.color = Color.red;
                    setitemobj.statsbowdmg.text = "( " + (itemvalues.stats[2] - currentbow) + " ) " + (itemvalues.stats[2] + Statics.charbowattack[currentint] - currentbow);
                }
            }
        }
        else if (equipslotnumber == 2)
        {
            float currentfist = setitemobj.currentfistdmg[currentint];
            if (itemvalues != null)
            {
                if (itemvalues.stats[2] > currentfist)
                {
                    setitemobj.statsfistdmg.color = Color.green;
                    setitemobj.statsfistdmg.text = "( +" + (itemvalues.stats[2] - currentfist) + " ) " + (itemvalues.stats[2] + Statics.charfistattack[currentint] - currentfist);
                }
                else if (itemvalues.stats[2] < currentfist)
                {
                    setitemobj.statsfistdmg.color = Color.red;
                    setitemobj.statsfistdmg.text = "( " + (itemvalues.stats[2] - currentfist) + " ) " + (itemvalues.stats[2] + Statics.charfistattack[currentint] - currentfist);
                }
            }
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //gameObject.GetComponent<Image>().color = Color.white;
        statsreset();
    }
    private void OnDisable()
    {
        statsreset();
    }
    private void statsreset()
    {
        if (equipslotnumber == 0)
        {
            setitemobj.statssworddmg.text = Statics.charswordattack[currentint] + "";
            setitemobj.statssworddmg.color = Color.white;

            setitemobj.showitemstatsobj.SetActive(false);
        }
        else if (equipslotnumber == 1)
        {
            setitemobj.statsbowdmg.text = Statics.charbowattack[currentint] + "";
            setitemobj.statsbowdmg.color = Color.white;

            setitemobj.showitemstatsobj.SetActive(false);
        }
        else if (equipslotnumber == 2)
        {
            setitemobj.statsfistdmg.text = Statics.charfistattack[currentint] + "";
            setitemobj.statsfistdmg.color = Color.white;

            setitemobj.showitemstatsobj.SetActive(false);
        }
    }
}
