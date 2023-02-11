using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpielerHP : MonoBehaviour
{
    public float health;
    public float maxhealth;
    public Image Healthbar;
    public TextMeshProUGUI healthText;
    public Text playername;
    private Attributecontroller attributecontroller;

    void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>(); 
    }
    public void UpdatehealthUI()                      //wird am anfange im attribuecontroller gecalled                             
    {
        if(health > maxhealth)
        {
            health = maxhealth;
        }
        float fillhp = Healthbar.fillAmount;
        float hFraction = health / maxhealth;
        if (fillhp > hFraction)
        {
            Healthbar.fillAmount = hFraction;
        }
        if (fillhp < hFraction)
        {
            Healthbar.fillAmount = hFraction;
        }
        healthText.text = "HP " + health + " / " + maxhealth;
    }
    public void TakeDamage(float damage)
    {
        if (Statics.playeriframes == true && gameObject == LoadCharmanager.Overallmainchar)
        {
            return;
        }

        float dmgtodeal = Mathf.Round(damage - ((((Statics.groupstonedefensebonus + attributecontroller.stoneclassdmgreduction) * 0.01f) + attributecontroller.defense / 40) * damage));
        health -= dmgtodeal;                                                             
        if (health > maxhealth)
        {
            health = maxhealth;
        }
        UpdatehealthUI();
    }
    public void playerheal(float heal)
    {
        health += Mathf.Round(heal + (Statics.groupstonehealbonus + attributecontroller.stoneclassbonusheal * 0.01f * heal));
        if (health > maxhealth)
        {
            health = maxhealth;
        }
        UpdatehealthUI();
    }
    public void castheal(float heal)
    {
        health += heal;
        if (health > maxhealth)
        {
            health = maxhealth;
        }
        UpdatehealthUI();
    }
}
// static kann von jedem anderen script aufgerufen werden (classname+voidname)
// string kann mit texten verbunden werden. Kann z.b einen text umänder
// array: sind []. in diesen klammern können mehrere values bzw. zahlen zusammen gerechtnet werden

