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

    private EnemyHP healthbargameobject;


    public void sethealthbar(EnemyHP enemyhealthbar)
    {
        healthbargameobject = enemyhealthbar;
        healthbargameobject.healthpctchanged += handlehealthchange;
        float pct = enemyhealthbar.currenthealth / enemyhealthbar.maxhealth;
        handlehealthchange(pct);
        healthbargameobject.healthbar = this;
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

    private void LateUpdate()
    {
        if(Vector3.Dot(cam.transform.TransformDirection(Vector3.forward), healthbargameobject.transform.position - cam.transform.position) > 0) //cam.transform.forward,
        {
            transform.position = cam.WorldToScreenPoint(healthbargameobject.transform.position + Vector3.up * healthbargameobject.enemyheight);    //Vector3.up * positionoffset);
        }
    }
    private void OnDestroy()
    {
        healthbargameobject.healthpctchanged -= handlehealthchange;
        healthbargameobject.markcurrenttarget -= targetmarked;
        healthbargameobject.unmarkcurrenttarget -= targetunmark;
    }
}
