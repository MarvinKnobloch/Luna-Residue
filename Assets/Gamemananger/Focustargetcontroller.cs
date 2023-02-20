using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Focustargetcontroller : MonoBehaviour
{
    public Image healthbarimage;

    [SerializeField] private TextMeshProUGUI LockonuiHPtext;
    [SerializeField] private TextMeshProUGUI nameandlvl;
    [SerializeField] private TextMeshProUGUI enemysizetext;

    //private EnemyHP healthbargameobject;

    [SerializeField] private GameObject focustargetui;
    [SerializeField] private GameObject debuffbar;

    void Awake()
    {
        EnemyHP.setfocustargetui += setfocus;
        EnemyHP.deselectfocustargetui += removefocus;
    }
    private void OnDisable()
    {
        EnemyHP.setfocustargetui -= setfocus;
        EnemyHP.deselectfocustargetui -= removefocus;
    }
    private void setfocus(EnemyHP enemyhpscript)
    {
        focustargetui.SetActive(true);
        enemyhpscript.focustargetuihptext += healthuiupdate;
        nameandlvl.text = "LvL" + enemyhpscript.enemylvl + " " + enemyhpscript.enemyname;
        LockonuiHPtext.text = "HP " + enemyhpscript.currenthealth + " / " + enemyhpscript.maxhealth;
        healthuiupdate(enemyhpscript.currenthealth, enemyhpscript.maxhealth);
        if(enemyhpscript.enemydebuffcd == true)
        {
            debuffbar.SetActive(true);
        }
        else
        {
            debuffbar.SetActive(false);
        }
        int size = enemyhpscript.sizeofenemy;
        if (size == 0)
        {
            enemysizetext.text = "S";
        }
        else if (size == 1)
        {
            enemysizetext.text = "M";
        }
        else if (size == 2)
        {
            enemysizetext.text = "B";
        }
    }

    private void removefocus(EnemyHP enemyhpscript)
    {        
        enemyhpscript.focustargetuihptext -= healthuiupdate;
        focustargetui.SetActive(false);
    }
    private void healthuiupdate(float currenthp, float maxhp)
    {
        float fillhp = healthbarimage.fillAmount;
        float hFraction = currenthp / maxhp;
        if (fillhp > hFraction)
        {
            healthbarimage.fillAmount = hFraction;
        }
        if (fillhp < hFraction)
        {
            healthbarimage.fillAmount = hFraction;
        }
        LockonuiHPtext.text = "HP " + currenthp + " / " + maxhp;
    }  
}
