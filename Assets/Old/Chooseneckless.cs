using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Chooseneckless : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject slotbuttontext;
    private GameObject slotbutton;
    [NonSerialized] public Itemcontroller itemvalues;
    private Setstats setitemobj;

    public int currentint = 0;

    private void Awake()
    {
        setitemobj = GetComponentInParent<Setstats>();
    }
    private void OnEnable()
    {
        slotbuttontext = gameObject.GetComponentInParent<Setstats>().slottext;
        slotbutton = gameObject.GetComponentInParent<Setstats>().slotbutton;
    }
    private void OnDisable()
    {
        currentint = Statics.currentequiptmentchar;

        setitemobj.statsmaxhealth.text = Statics.charmaxhealth[currentint] + "";
        setitemobj.statsmaxhealth.color = Color.white;

        setitemobj.statsdefense.text = Statics.chardefense[currentint] + "";
        setitemobj.statsdefense.color = Color.white;

        setitemobj.statsattack.text = Statics.charattack[currentint] + "";
        setitemobj.statsattack.color = Color.white;

        setitemobj.statscritchance.text = Statics.charcritchance[currentint] + "%";
        setitemobj.statscritchance.color = Color.white;

        setitemobj.statscritdmg.text = Statics.charcritdmg[currentint] + "%";
        setitemobj.statscritdmg.color = Color.white;

        setitemobj.statsweaponbuff.text = Statics.charweaponbuff[currentint] - 100 + "%";
        setitemobj.statsweaponbuff.color = Color.white;

        setitemobj.statscharswitchbuff.text = Statics.charswitchbuff[currentint] - 100 + "%";
        setitemobj.statscharswitchbuff.color = Color.white;

        setitemobj.statsbasicdmgbuff.text = Statics.charbasicdmgbuff[currentint] + "%";
        setitemobj.statsbasicdmgbuff.color = Color.white;
    }
    public void setneckless()
    {
        if (itemvalues != null)
        {
            currentint = Statics.currentequiptmentchar;
            slotbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;
            Statics.charcurrentnecklessname[currentint] = GetComponentInChildren<Text>().text;

            Statics.charmaxhealth[currentint] += itemvalues.stats[0] - setitemobj.currentmaxhealth[currentint];
            setitemobj.currentmaxhealth[currentint] = itemvalues.stats[0];
            setitemobj.statsmaxhealth.text = Statics.charmaxhealth[currentint] + "";
            setitemobj.statsmaxhealth.color = Color.white;

            Statics.chardefense[currentint] += itemvalues.stats[1] - setitemobj.currentdefense[currentint];
            setitemobj.currentdefense[currentint] = itemvalues.stats[1];
            setitemobj.statsdefense.text = Statics.chardefense[currentint] + "";
            setitemobj.statsdefense.color = Color.white;

            Statics.charattack[currentint] += itemvalues.stats[2] - setitemobj.currentattack[currentint];
            setitemobj.currentattack[currentint] = itemvalues.stats[2];
            setitemobj.statsattack.text = Statics.charattack[currentint] + "";
            setitemobj.statsattack.color = Color.white;

            Statics.charcritchance[currentint] += itemvalues.stats[3] - setitemobj.currentcritchance[currentint];
            setitemobj.currentcritchance[currentint] = itemvalues.stats[3];
            setitemobj.statscritchance.text = Statics.charcritchance[currentint] + "%";
            setitemobj.statscritchance.color = Color.white;

            Statics.charcritdmg[currentint] += itemvalues.stats[4] - setitemobj.currentcritdmg[currentint];
            setitemobj.currentcritdmg[currentint] = itemvalues.stats[4];
            setitemobj.statscritdmg.text = Statics.charcritdmg[currentint] + "%";
            setitemobj.statscritdmg.color = Color.white;

            Statics.charweaponbuff[currentint] += itemvalues.stats[5] - setitemobj.currentweaponbuff[currentint];
            setitemobj.currentweaponbuff[currentint] = itemvalues.stats[5];
            setitemobj.statsweaponbuff.text = Statics.charweaponbuff[currentint] - 100 + "%";
            setitemobj.statsweaponbuff.color = Color.white;

            Statics.charswitchbuff[currentint] += itemvalues.stats[6] - setitemobj.currentcharswitchbuff[currentint];
            setitemobj.currentcharswitchbuff[currentint] = itemvalues.stats[6];
            setitemobj.statscharswitchbuff.text = Statics.charswitchbuff[currentint] - 100 + "%";
            setitemobj.statscharswitchbuff.color = Color.white;

            Statics.charbasicdmgbuff[currentint] += itemvalues.stats[7] - setitemobj.currentbasicdmgbuff[currentint];
            setitemobj.currentbasicdmgbuff[currentint] = itemvalues.stats[7];
            setitemobj.statsbasicdmgbuff.text = Statics.charbasicdmgbuff[currentint] + "%";
            setitemobj.statsbasicdmgbuff.color = Color.white;
        }
        EventSystem.current.SetSelectedGameObject(slotbutton);
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (itemvalues != null)
        {
            currentint = Statics.currentequiptmentchar;
            float currenthealth = setitemobj.currentmaxhealth[currentint];
            float currentdenfese = setitemobj.currentdefense[currentint];
            float currentattack = setitemobj.currentattack[currentint];
            float currentcritchance = setitemobj.currentcritchance[currentint];
            float currentcritdmg = setitemobj.currentcritdmg[currentint];
            float currentweaponbuff = setitemobj.currentweaponbuff[currentint];
            float currentcharbuff = setitemobj.currentcharswitchbuff[currentint];
            float currentbasicbuff = setitemobj.currentbasicdmgbuff[currentint];

            if (itemvalues.stats[0] > currenthealth)
            {
                setitemobj.statsmaxhealth.color = Color.green;
                setitemobj.statsmaxhealth.text = "( +" + (itemvalues.stats[0] - currenthealth) + " ) " + (itemvalues.stats[0] + Statics.charmaxhealth[currentint]);
            }
            else if (itemvalues.stats[0] < currenthealth)
            {
                setitemobj.statsmaxhealth.color = Color.red;
                setitemobj.statsmaxhealth.text = "( " + (itemvalues.stats[0] - currenthealth) + " ) " + (Statics.charmaxhealth[currentint] - currenthealth);
            }

            if (itemvalues.stats[1] > currentdenfese)
            {
                setitemobj.statsdefense.color = Color.green;
                setitemobj.statsdefense.text = "( +" + (itemvalues.stats[1] - currentdenfese) + " ) " + (itemvalues.stats[1] + Statics.chardefense[currentint]);
            }

            else if (itemvalues.stats[1] < currentdenfese)
            {
                setitemobj.statsdefense.color = Color.red;
                setitemobj.statsdefense.text = "( " + (itemvalues.stats[1] - currentdenfese) + " ) " + (Statics.chardefense[currentint] - currentdenfese);
            }

            if (itemvalues.stats[2] > currentattack)
            {
                setitemobj.statsattack.color = Color.green;
                setitemobj.statsattack.text = "( +" + (itemvalues.stats[2] - currentattack) + " ) " + (itemvalues.stats[2] + Statics.charattack[currentint]);
            }
            else if (itemvalues.stats[2] < currentattack)
            {
                setitemobj.statsattack.color = Color.red;
                setitemobj.statsattack.text = "( " + (itemvalues.stats[2] - currentattack) + " ) " + (Statics.charattack[currentint] - currentattack);
            }

            if (itemvalues.stats[3] > currentcritchance)
            {
                setitemobj.statscritchance.color = Color.green;
                setitemobj.statscritchance.text = "( +" + (itemvalues.stats[3] - currentcritchance) + "%" + " ) " + (itemvalues.stats[3] + Statics.charcritchance[currentint]) + "%";
            }
            else if (itemvalues.stats[3] < currentcritchance)
            {
                setitemobj.statscritchance.color = Color.red;
                setitemobj.statscritchance.text = "( " + (itemvalues.stats[3] - currentcritchance) + "%" + " ) " + (Statics.charcritchance[currentint] - currentcritchance) + "%";
            }

            if (itemvalues.stats[4] > currentcritdmg)
            {
                setitemobj.statscritdmg.color = Color.green;
                setitemobj.statscritdmg.text = "( +" + (itemvalues.stats[4] - currentcritdmg) + "%" + " ) " + (itemvalues.stats[4] + Statics.charcritdmg[currentint]) + "%";
            }
            else if (itemvalues.stats[4] < currentcritdmg)
            {
                setitemobj.statscritdmg.color = Color.red;
                setitemobj.statscritdmg.text = "( " + (itemvalues.stats[4] - currentcritdmg) + "%" + " ) " + (Statics.charcritdmg[currentint] - currentcritdmg) + "%";
            }

            if (itemvalues.stats[5] > currentweaponbuff)
            {
                setitemobj.statsweaponbuff.color = Color.green;
                setitemobj.statsweaponbuff.text = "( +" + (itemvalues.stats[5] - currentweaponbuff) + "%" + " ) " + (itemvalues.stats[5] + Statics.charweaponbuff[currentint] - 100) + "%";
            }
            else if (itemvalues.stats[5] < currentweaponbuff)
            {
                setitemobj.statsweaponbuff.color = Color.red;
                setitemobj.statsweaponbuff.text = "( " + (itemvalues.stats[5] - currentweaponbuff) + "%" + " ) " + (Statics.charweaponbuff[currentint] - currentweaponbuff - 100) + "%";
            }

            if (itemvalues.stats[6] > currentcharbuff)
            {
                setitemobj.statscharswitchbuff.color = Color.green;
                setitemobj.statscharswitchbuff.text = "( +" + (itemvalues.stats[6] - currentcharbuff) + "%" + " ) " + (itemvalues.stats[6] + Statics.charswitchbuff[currentint] - 100) + "%";
            }
            else if (itemvalues.stats[6] < currentcharbuff)
            {
                setitemobj.statscharswitchbuff.color = Color.red;
                setitemobj.statscharswitchbuff.text = "( " + (itemvalues.stats[6] - currentcharbuff) + "%" + " ) " + (Statics.charswitchbuff[currentint] - currentcharbuff - 100) + "%";
            }

            if (itemvalues.stats[7] > currentbasicbuff)
            {
                setitemobj.statsbasicdmgbuff.color = Color.green;
                setitemobj.statsbasicdmgbuff.text = "( +" + (itemvalues.stats[7] - currentbasicbuff) + "%" + " ) " + (itemvalues.stats[7] + Statics.charbasicdmgbuff[currentint]) + "%";
            }
            else if (itemvalues.stats[7] < currentbasicbuff)
            {
                setitemobj.statsbasicdmgbuff.color = Color.red;
                setitemobj.statsbasicdmgbuff.text = "( " + (itemvalues.stats[7] - currentbasicbuff) + "%" + " ) " + (Statics.charbasicdmgbuff[currentint] - currentbasicbuff) + "%";
            }
            setitemobj.itemtextcontroller.itemvalues = itemvalues;
            setitemobj.showitemstatsobj.SetActive(true);
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        setitemobj.statsmaxhealth.text = Statics.charmaxhealth[currentint] + "";
        setitemobj.statsmaxhealth.color = Color.white;

        setitemobj.statsdefense.text = Statics.chardefense[currentint] + "";
        setitemobj.statsdefense.color = Color.white;

        setitemobj.statsattack.text = Statics.charattack[currentint] + "";
        setitemobj.statsattack.color = Color.white;

        setitemobj.statscritchance.text = Statics.charcritchance[currentint] + "%";
        setitemobj.statscritchance.color = Color.white;

        setitemobj.statscritdmg.text = Statics.charcritdmg[currentint] + "%";
        setitemobj.statscritdmg.color = Color.white;

        setitemobj.statsweaponbuff.text = Statics.charweaponbuff[currentint] - 100 + "%";
        setitemobj.statsweaponbuff.color = Color.white;

        setitemobj.statscharswitchbuff.text = Statics.charswitchbuff[currentint] - 100 + "%";
        setitemobj.statscharswitchbuff.color = Color.white;

        setitemobj.statsbasicdmgbuff.text = Statics.charbasicdmgbuff[currentint] + "%";
        setitemobj.statsbasicdmgbuff.color = Color.white;

        setitemobj.showitemstatsobj.SetActive(false);
    }
}
