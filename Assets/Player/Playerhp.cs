using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Playerhp : MonoBehaviour
{
    [SerializeField] private int charnumber;
    [NonSerialized] public float health;
    [NonSerialized] public float maxhealth;
    [NonSerialized] public int playerhpuislot;
    [SerializeField] HealthUImanager healthUImanager;
    private Attributecontroller attributecontroller;

    void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>(); 
    }
    public void TakeDamage(float damage)
    {
        if (Statics.playeriframes == true && gameObject == LoadCharmanager.Overallmainchar)
        {
            return;
        }

        float dmgtodeal = Mathf.Round(damage - ((((Statics.groupstonedefensebonus + attributecontroller.stoneclassdmgreduction) * 0.01f) + attributecontroller.defense / 40) * damage));
        health -= dmgtodeal;
        handlehealth();
    }
    public void playerheal(float heal)
    {
        health += Mathf.Round(heal + (Statics.groupstonehealbonus + attributecontroller.stoneclassbonusheal * 0.01f * heal));
        handlehealth();
    }
    public void castheal(float heal)
    {
        health += heal;
        handlehealth();
    }
    private void handlehealth()
    {
        if (health > maxhealth)
        {
            health = maxhealth;
        }
        Statics.charcurrenthealth[charnumber] = health;
        healthUImanager.healthupdate(playerhpuislot, health, maxhealth);
    }
}
// static kann von jedem anderen script aufgerufen werden (classname+voidname)
// string kann mit texten verbunden werden. Kann z.b einen text umänder
// array: sind []. in diesen klammern können mehrere values bzw. zahlen zusammen gerechtnet werden

