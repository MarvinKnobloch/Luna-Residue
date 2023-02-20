using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Enemyhealthbar : MonoBehaviour
{
    [SerializeField] private Image healthbarimage;
    [SerializeField] private float positionoffset;
    [SerializeField] private TextMeshProUGUI enemysizetext;
    [SerializeField] private Camera cam;
    public GameObject debuffUI;
    public Image debuffbar;

    private GameObject enemyfocusgameobject;
    private Image focusdebuffbar;

    private EnemyHP healthbargameobject;

    private float debuffcd;

    public void sethealthbar(EnemyHP enemyhealthbar)
    {
        healthbargameobject = enemyhealthbar;
        healthbargameobject.healthpctchanged += handlehealthchange;
        float pct = enemyhealthbar.currenthealth / enemyhealthbar.maxhealth;
        handlehealthchange(pct);
        healthbargameobject.healthbar = this;
        //healthbargameobject.debuffstart += debuffstart;
        //healthbargameobject.debuffcdstart += debuffcdstart;
        healthbargameobject.markcurrenttarget += targetmarked;
        healthbargameobject.unmarkcurrenttarget += targetunmark;
        int size = enemyhealthbar.sizeofenemy;
        if(size == 0)
        {
            enemysizetext.text = "S" + healthbargameobject.enemylvl;
        }
        else if (size == 1)
        {
            enemysizetext.text = "M" + healthbargameobject.enemylvl;
        }
        else if (size == 2)
        {
            enemysizetext.text = "B" + healthbargameobject.enemylvl;
        }
        //focusdebuffbar = enemyhealthbar.enemyfocusdebuffbar;
        //enemyfocusgameobject = enemyhealthbar.enemyfocusbargameobject;
        cam = Camera.main;
    }
    private void handlehealthchange(float pct)
    {
        float fillhp = healthbarimage.fillAmount;
        float hFraction = pct;
        if (fillhp > hFraction)
        {
            healthbarimage.fillAmount = hFraction;
        }
        if (fillhp < hFraction)
        {
            healthbarimage.fillAmount = hFraction;
        }
    }
    private void targetmarked()
    {
        enemysizetext.color = Color.red;
    }
    private void targetunmark()
    {
        enemysizetext.color = Color.white;
    }
    public void removehealthbar()
    {
        healthbargameobject.removefromcanvas();
    }

    /*private void debuffstart()
    {
        StartCoroutine("enemydebuff");
    }
    IEnumerator enemydebuff()
    {
        //enemyfocusgameobject.gameObject.SetActive(true);
        debuffUI.SetActive(true);
        debuffbar.color = Color.blue;

        while (true)
        {
            debuffbar.fillAmount = healthbargameobject.debufffillamount;
            /*if(healthbargameobject.isfocus == true)
            {
                focusdebuffbar.fillAmount = healthbargameobject.debufftimer / Statics.enemydebufftime;
                focusdebuffbar.color = Color.blue;
            }*/

            /*if (healthbargameobject.debufftimer <= 0)
            {
                StopCoroutine("enemydebuff");
            }
            yield return null;
        }
    }
    private void debuffcdstart()
    {
        StartCoroutine("enemydebuffcd");
    }
    IEnumerator enemydebuffcd()
    {
        debuffbar.color = Color.yellow;
        while (true)
        {
            debuffbar.fillAmount = healthbargameobject.debufffillamount;

            /*if (healthbargameobject.isfocus == true)
            {
                focusdebuffbar.fillAmount = healthbargameobject.debuffcdtimer / Statics.enemydebufftime;
                focusdebuffbar.color = Color.yellow;
            }*/

            /*if (healthbargameobject.debuffcdtimer <= 0)
            {
                debuffUI.gameObject.SetActive(false);
                //enemyfocusgameobject.gameObject.SetActive(false);
                StopCoroutine("enemydebuffcd");
            }
            yield return null;
        }
    }*/

    private void LateUpdate()
    {
        if(Vector3.Dot(cam.transform.TransformDirection(Vector3.forward), healthbargameobject.transform.position - cam.transform.position) > 0) //cam.transform.forward,
        {
            transform.position = cam.WorldToScreenPoint(healthbargameobject.transform.position + Vector3.up * healthbargameobject.enemyheight);    //Vector3.up * positionoffset);
        }
    }
    private void OnDestroy()
    {
        //StopAllCoroutines();
        healthbargameobject.healthpctchanged -= handlehealthchange;
        //healthbargameobject.debuffstart -= debuffstart;
        //healthbargameobject.debuffcdstart -= debuffcdstart;
        healthbargameobject.markcurrenttarget -= targetmarked;
        healthbargameobject.unmarkcurrenttarget -= targetunmark;
    }
}
