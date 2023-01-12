using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Choosesword : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject swordbuttontext;
    private GameObject swordbutton;
    [NonSerialized] public Itemcontroller itemvalues;
    private Setsword setitemobj;

    public int currentint = 0;

    private void Awake()
    {
        setitemobj = GetComponentInParent<Setsword>();
    }
    private void OnEnable()
    {
        swordbuttontext = gameObject.GetComponentInParent<Setsword>().swordtext;
        swordbutton= gameObject.GetComponentInParent<Setsword>().swordbutton;

    }
    private void OnDisable()
    {
        currentint = Statics.currentequiptmentchar;
        setitemobj.statssworddmg.text = Statics.charswordattack[currentint] + "";
        setitemobj.statssworddmg.color = Color.white;
    }
    public void setsword()
    {
        currentint = Statics.currentequiptmentchar;
        if (itemvalues != null)
        {          
            swordbuttontext.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInChildren<Text>().text;
            Statics.charcurrentswordname[currentint] = GetComponentInChildren<Text>().text;

            Statics.charswordattack[currentint] = itemvalues.stats[2];
            setitemobj.statssworddmg.text = Statics.charswordattack[currentint] + "";
            setitemobj.statssworddmg.color = Color.white;

            if (Statics.currentswordimage[currentint] != null)
            {
                Statics.currentswordimage[currentint].transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;                //setzt das alte image beim wechseln auf weiﬂ(falls vorhanden)
            }
            transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;                                                         //setzt das neue image beim wechseln auf gr¸n
            Statics.currentswordimage[currentint] = this.gameObject;                                                                           //speichert das image vom char im static
            Statics.activeswordslot = this.gameObject;
        }
        EventSystem.current.SetSelectedGameObject(swordbutton);                                                                                // damit danach wieder der slotangew‰hlt ist
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        currentint = Statics.currentequiptmentchar;
        if(itemvalues != null)
        {
            if (itemvalues.stats[2] > Statics.charswordattack[currentint])
            {
                setitemobj.statssworddmg.color = Color.green;
                setitemobj.statssworddmg.text = "( +" + (itemvalues.stats[2] - Statics.charswordattack[currentint]) + " ) " + itemvalues.stats[2];
            }
            else if (itemvalues.stats[2] < Statics.charswordattack[currentint])
            {
                setitemobj.statssworddmg.color = Color.red;
                setitemobj.statssworddmg.text = "( " + (itemvalues.stats[2] - Statics.charswordattack[currentint]) + " ) " + itemvalues.stats[2];
            }
            else
            {
                setitemobj.statssworddmg.color = Color.white;
                setitemobj.statssworddmg.text = "( +" + (itemvalues.stats[2] - Statics.charswordattack[currentint]) + " ) " + itemvalues.stats[2];
            }
            setitemobj.showitemstats.SetActive(true);
        }     
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        setitemobj.statssworddmg.text = Statics.charswordattack[currentint] + "";
        setitemobj.statssworddmg.color = Color.white;

        setitemobj.showitemstats.SetActive(false);
    }
}
//setsword
/*if (currentint == 0)
{
    Statics.mariacurrentsword = GetComponentInChildren<Text>().text;
    Statics.mariaswordattack = weaponattack;
    Statics.charswordattack[currentint] = itemvalues.stats[2]; 
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.mariaswordattack + "";
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;

    if (Statics.mariacurrentswordimage != null)
    {
        Statics.mariacurrentswordimage.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
    }
    transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
    Statics.mariacurrentswordimage = this.gameObject;
    Statics.activeswordslot = this.gameObject;
}
if (currentint == 1)
{
    Statics.erikacurrentsword= GetComponentInChildren<Text>().text;
    Statics.erikaswordattack = weaponattack;
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.erikaswordattack + "";
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;

    if (Statics.erikacurrentswordimage != null)
    {
        Statics.erikacurrentswordimage.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
    }
    transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
    Statics.erikacurrentswordimage = this.gameObject;
    Statics.activeswordslot = this.gameObject;
}
if (currentint == 2)
{
    Statics.kajacurrentsword = GetComponentInChildren<Text>().text;
    Statics.kajaswordattack = weaponattack;
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.kajaswordattack + "";
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;

    if (Statics.kajacurrentswordimage != null)
    {
        Statics.kajacurrentswordimage.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
    }
    transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
    Statics.kajacurrentswordimage = this.gameObject;
    Statics.activeswordslot = this.gameObject;
}
if (currentint == 3)
{
    Statics.yakucurrentsword = GetComponentInChildren<Text>().text;
    Statics.yakuswordattack = weaponattack;
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.yakuswordattack + "";
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;

    if (Statics.yakucurrentswordimage != null)
    {
        Statics.yakucurrentswordimage.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
    }
    transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
    Statics.yakucurrentswordimage = this.gameObject;
    Statics.activeswordslot = this.gameObject;
}
if (currentint == 4)
{
    Statics.arissacurrentsword = GetComponentInChildren<Text>().text;
    Statics.arissaswordattack = weaponattack;
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.arissaswordattack + "";
    Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;

    if (Statics.arissacurrentswordimage != null)
    {
        Statics.arissacurrentswordimage.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
    }
    transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
    Statics.arissacurrentswordimage = this.gameObject;
    Statics.activeswordslot = this.gameObject;
}*/
//attributeupdate?.Invoke();
//swordcontrollerupdate?.Invoke();


//onenter
/*if (currentint == 0)
{
    if(weaponattack > Statics.mariaswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.green;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.mariaswordattack) + " ) " + weaponattack;
    }
    else if (weaponattack < Statics.mariaswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.red;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( " + (weaponattack - Statics.mariaswordattack) + " ) " + weaponattack;
    }
    else
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.mariaswordattack) + " ) " + weaponattack;
    }
}
if (currentint == 1)
{
    if (weaponattack > Statics.erikaswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.green;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.erikaswordattack) + " ) " + weaponattack;
    }
    else if (weaponattack < Statics.erikaswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.red;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( " + (weaponattack - Statics.erikaswordattack) + " ) " + weaponattack;
    }
    else
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.erikaswordattack) + " ) " + weaponattack;
    }
}
if (currentint == 2)
{
    if (weaponattack > Statics.kajaswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.green;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.kajaswordattack) + " ) " + weaponattack;
    }
    else if (weaponattack < Statics.kajaswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.red;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( " + (weaponattack - Statics.kajaswordattack) + " ) " + weaponattack;
    }
    else
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.kajaswordattack) + " ) " + weaponattack;
    }
}
if (currentint == 3)
{
    if (weaponattack > Statics.yakuswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.green;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.yakuswordattack) + " ) " + weaponattack;
    }
    else if (weaponattack < Statics.yakuswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.red;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( " + (weaponattack - Statics.yakuswordattack) + " ) " + weaponattack;
    }
    else
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.yakuswordattack) + " ) " + weaponattack;
    }
}
if (currentint == 4)
{
    if (weaponattack > Statics.arissaswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.green;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.arissaswordattack) + " ) " + weaponattack;
    }
    else if (weaponattack < Statics.arissaswordattack)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.red;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( " + (weaponattack - Statics.arissaswordattack) + " ) " + weaponattack;
    }
    else
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = "( +" + (weaponattack - Statics.arissaswordattack) + " ) " + weaponattack;
    }
}*/
//setitemobj.showitemstats.SetActive(true);
/*if(itemvalues.attack != 0)
{
    if (itemvalues.upgradelvl < itemvalues.maxupgradelvl)
    {
        itemvalues.stats = itemvalues.upgrades[itemvalues.upgradelvl].newstats;
        itemvalues.upgradelvl++;
    }
    /*Debug.Log("upgrade");
    if(itemvalues.upgradelvl < itemvalues.maxupgradelvl)
    {
        Debug.Log("notmax");
        int currentstat = 0;
        foreach (float stats in itemvalues.upgrades[itemvalues.upgradelvl].choosestatposi)
        {
            itemvalues.stats[itemvalues.upgrades[itemvalues.upgradelvl].choosestatposi[currentstat]] += itemvalues.upgrades[itemvalues.upgradelvl].upgradevalues[currentstat];
            currentstat++;
        }*/
/*private void OnDisable()
{
    currentint = Statics.currentequiptmentchar;
    if (currentint == 0)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.mariaswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
    if (currentint == 1)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.erikaswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
    if (currentint == 2)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.kajaswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
    if (currentint == 3)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.yakuswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
    if (currentint == 4)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.arissaswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
}*/

/*void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
{

    if (currentint == 0)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.mariaswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
    if (currentint == 1)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.erikaswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
    if (currentint == 2)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.kajaswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
    if (currentint == 3)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.yakuswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
    if (currentint == 4)
    {
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.text = Statics.arissaswordattack + "";
        Mouseoverobj.GetComponent<Setsword>().statssworddmg.color = Color.white;
    }
    setitemobj.showitemstats.SetActive(false);
}*/