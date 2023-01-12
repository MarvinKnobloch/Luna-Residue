using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Choosebow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject bowbuttontext;
    private GameObject bowbutton;
    [NonSerialized] public Itemcontroller itemvalues;
    private Setbow setitemobj;

    public int currentint = 0;

    private void Awake()
    {
        setitemobj = GetComponentInParent<Setbow>();
    }

    private void OnEnable()
    {
        bowbuttontext = gameObject.GetComponentInParent<Setbow>().bowtext;
        bowbutton = gameObject.GetComponentInParent<Setbow>().bowbutton;
    }
    private void OnDisable()
    {
        currentint = Statics.currentequiptmentchar;
        setitemobj.statsbowdmg.text = Statics.charbowattack[currentint] + "";
        setitemobj.statsbowdmg.color = Color.white;
    }
    public void setbow()
    {
        currentint = Statics.currentequiptmentchar;
        if (itemvalues != null)
        {
            bowbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;
            Statics.charcurrentbowname[currentint] = GetComponentInChildren<Text>().text;

            Statics.charbowattack[currentint] = itemvalues.stats[2];
            setitemobj.statsbowdmg.text = Statics.charbowattack[currentint] + "";
            setitemobj.statsbowdmg.color = Color.white;

            if (Statics.currentbowimage[currentint] != null)
            {
                Statics.currentbowimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            }
            transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
            Statics.currentbowimage[currentint] = this.gameObject;
            Statics.activebowslot = this.gameObject;
        }
        EventSystem.current.SetSelectedGameObject(bowbutton);
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        currentint = Statics.currentequiptmentchar;
        if (itemvalues != null)
        {
            if (itemvalues.stats[2] > Statics.charbowattack[currentint])
            {
                setitemobj.statsbowdmg.color = Color.green;
                setitemobj.statsbowdmg.text = "( +" + (itemvalues.stats[2] - Statics.charbowattack[currentint]) + " ) " + itemvalues.stats[2];
            }
            else if (itemvalues.stats[2] < Statics.charbowattack[currentint])
            {
                setitemobj.statsbowdmg.color = Color.red;
                setitemobj.statsbowdmg.text = "( " + (itemvalues.stats[2] - Statics.charbowattack[currentint]) + " ) " + itemvalues.stats[2];
            }
            else
            {
                setitemobj.statsbowdmg.color = Color.white;
                setitemobj.statsbowdmg.text = "( +" + (itemvalues.stats[2] - Statics.charbowattack[currentint]) + " ) " + itemvalues.stats[2];
            }
            //setitemobj.showitemstats.SetActive(true);
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        setitemobj.statsbowdmg.text = Statics.charbowattack[currentint] + "";
        setitemobj.statsbowdmg.color = Color.white;

        //setitemobj.showitemstats.SetActive(false);
    }
}
