using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Choosefist : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject fistbuttontext;
    private GameObject fistbutton;
    [NonSerialized] public Itemcontroller itemvalues;
    private Setfist setitemobj;

    public int currentint = 0;

    private void Awake()
    {
        setitemobj = GetComponentInParent<Setfist>();
    }

    private void OnEnable()
    {
        fistbuttontext = gameObject.GetComponentInParent<Setfist>().fisttext;
        fistbutton = gameObject.GetComponentInParent<Setfist>().fistbutton;
    }
    private void OnDisable()
    {
        currentint = Statics.currentequiptmentchar;
        setitemobj.statsfistdmg.text = Statics.charfistattack[currentint] + "";
        setitemobj.statsfistdmg.color = Color.white;
    }
    public void setfist()
    {
        currentint = Statics.currentequiptmentchar;
        if (itemvalues != null)
        {           
            fistbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;
            Statics.charcurrentfistname[currentint] = GetComponentInChildren<Text>().text;

            Statics.charfistattack[currentint] = itemvalues.stats[2];
            setitemobj.statsfistdmg.text = Statics.charfistattack[currentint] + "";
            setitemobj.statsfistdmg.color = Color.white;

            if (Statics.currentfistimage[currentint] != null)
            {
                Statics.currentfistimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.currentfistimage[currentint] = this.gameObject;
            Statics.activefistslot = this.gameObject;
        }
        EventSystem.current.SetSelectedGameObject(fistbutton);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        currentint = Statics.currentequiptmentchar;
        if (itemvalues != null)
        {
            if (itemvalues.stats[2] > Statics.charfistattack[currentint])
            {
                setitemobj.statsfistdmg.color = Color.green;
                setitemobj.statsfistdmg.text = "( +" + (itemvalues.stats[2] - Statics.charfistattack[currentint]) + " ) " + itemvalues.stats[2];
            }
            else if (itemvalues.stats[2] < Statics.charfistattack[currentint])
            {
                setitemobj.statsfistdmg.color = Color.red;
                setitemobj.statsfistdmg.text = "( " + (itemvalues.stats[2] - Statics.charfistattack[currentint]) + " ) " + itemvalues.stats[2];
            }
            else
            {
                setitemobj.statsfistdmg.color = Color.white;
                setitemobj.statsfistdmg.text = "( +" + (itemvalues.stats[2] - Statics.charfistattack[currentint]) + " ) " + itemvalues.stats[2];
            }
            //setitemobj.showitemstats.SetActive(true);
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        setitemobj.statsfistdmg.text = Statics.charfistattack[currentint] + "";
        setitemobj.statsfistdmg.color = Color.white;

        //setitemobj.showitemstats.SetActive(false);
    }
}
