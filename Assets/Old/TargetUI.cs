using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetUI : MonoBehaviour
{
    //public Transform uitransform;
    public Image healthbarimage;

    [SerializeField] private TextMeshProUGUI LockonuiHPtext;
    [SerializeField] private TextMeshProUGUI nameandlvl;

    private EnemyHP healthbargameobject;

    //public float Lockonmaxhealth;
    //public GameObject Lockongameobject;
    //public float Lockonhealth;
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
        nameandlvl.text = enemyhpscript._enemyname + " " + enemyhpscript._enemylvl;
        healthbargameobject.healthpctchanged += healthuiupdate;
    }

    private void removefocus(EnemyHP enemyhpscript)
    {
        healthbargameobject.healthpctchanged -= healthuiupdate;
    }
    private void healthuiupdate(float pct)
    {
        float fillhp = healthbarimage.fillAmount;
        float hFraction = pct;
        if (fillhp > hFraction)
        {
            healthbarimage.fillAmount = hFraction;
        }
    }
}



    /*void Update()
    {
        if(Movescript.lockontarget != null)
        {
            uitransform = Movescript.lockontarget;                                                                  //Lockontransform parameter holen
            Lockongameobject = uitransform.gameObject;                                                                      //transform in gameobject verwandeln
            healthbargameobject = Lockongameobject.GetComponentInChildren<EnemyHP>();                                             //child mit dem Sricpt finden
            Lockonmaxhealth = healthbargameobject.maxhealth;                                                                      //hp parameter holen
            Lockonhealth = healthbargameobject.currenthealth;
            //UpdatehealthUI();
        }
    }
    public void healthuiupdate(float pct)
    {
        float fillhp = Lockuihealthbar.fillAmount;
        float hFraction = Lockonhealth / Lockonmaxhealth;
        if (fillhp > hFraction)
        {
            Lockuihealthbar.fillAmount = hFraction;
        }
        if (fillhp < hFraction)
        {
            Lockuihealthbar.fillAmount = hFraction;
        }
        LockonuiHPtext.text = "HP " + Lockonhealth + " / " + Lockonmaxhealth;
    }*/

