using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Playerhp : MonoBehaviour
{
    [SerializeField] private int charnumber;
    public float health;
    public float maxhealth;
    [NonSerialized] public bool playerisdead;
    public int playerhpuislot;
    [SerializeField] HealthUImanager healthUImanager;
    private Attributecontroller attributecontroller;
    [SerializeField] private MonoBehaviour charstatemachine;

    void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>(); 
    }
    private void OnEnable()
    {
        if(health > 0)
        {
            playerisdead = false;
        }
    }
    public void TakeDamage(float damage)
    {
        if (Statics.playeriframes == true && gameObject == LoadCharmanager.Overallmainchar)
        {
            return;
        }

        float dmgtodeal = Mathf.Round(damage - ((Statics.groupstonedefensebonus + attributecontroller.stoneclassdmgreduction + (attributecontroller.defense / 40)) * 0.01f * damage));
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
    public void playerisresurrected(float healamount)
    {
        playerisdead = false;
        castheal(healamount * 2);
        if (playerhpuislot == 0)           //0 ist immer mainchar
        {
            //gameObject.GetComponent<Movescript>();
        }
        else
        {
            gameObject.GetComponent<Supportmovement>().supportresurrected();
            foreach (GameObject obj in Infightcontroller.infightenemylists)
            {
                obj.GetComponent<EnemyHP>().playerisresurrected(playerhpuislot - 1);
            }
        }
    }
    private void handlehealth()
    {

        if (health > maxhealth)
        {
            health = maxhealth;
        }
        else if(health <= 0)
        {
            health = 0;
            if(playerisdead == false)
            {
                playerisdead = true;
                if (playerhpuislot == 0)           //0 ist immer mainchar
                {
                    //gameObject.GetComponent<Movescript>();
                }
                else
                {
                    gameObject.GetComponent<Supportmovement>().dying();
                    foreach(GameObject obj in Infightcontroller.infightenemylists)
                    {
                        obj.GetComponent<EnemyHP>().newtargetonplayerdeath(playerhpuislot -1);
                    }
                }
            }
        }
        Statics.charcurrenthealth[charnumber] = health;
        healthUImanager.healthupdate(playerhpuislot, health, maxhealth);
    }
}

