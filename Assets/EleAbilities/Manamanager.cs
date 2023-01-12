using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manamanager : MonoBehaviour
{
    public static Manamanager manamanager;
    public Image Manabar;
    public TextMeshProUGUI Manatext;
    public static float mana;
    private float maxmana = 100;

    private void Awake()
    {
        manamanager = this;
        mana = 100;
        UpdatemanaUI();
    }
    public void UpdatemanaUI()
    {
    float fillhp = Manabar.fillAmount;
    float hFraction = mana / maxmana;
    if (fillhp > hFraction)
    {
        Manabar.fillAmount = hFraction;
    }
    if (fillhp < hFraction)
    {
        Manabar.fillAmount = hFraction;
    }
        Manatext.text = "MP " + mana;
    }
    public void Managemana(float handlemana)                                               // static kann von jedem anderen script aufgerufen werden (classname+voidname)
    {                                                                                  // sting kann mit texten verbunden werden. Kann z.b einen text umändern
    mana += handlemana;                                                              // array: sind []. in diesen klammern können mehrere values bzw. zahlen zusammen gerechtnet werden
    if (mana > maxmana)
    {
        mana = maxmana;
    }
    UpdatemanaUI();
    }
}
