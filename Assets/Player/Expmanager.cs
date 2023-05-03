using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Expmanager : MonoBehaviour
{
    private SpielerSteu controlls;
    private Healthuimanager healthUImanager;

    [SerializeField] private Image expbar;
    [SerializeField] private TextMeshProUGUI exptext;

    [Range(1f, 300f)]
    public float flatexpnumber = 220;                                     // erhöht die benötigte exp für jedes lvl gleich, um so niederiger die zahl um so weniger exp braucht man zum lvln
    [Range(2f, 4f)]
    public float expmultiplier = 2;                                       // je höher die zahl um so steiler die kurve
    [Range(7f, 14f)]
    public float expdivision = 14;                                        // je höher die zahl um so flacher die kurve


    void Awake()
    {
        controlls = new SpielerSteu();
        healthUImanager = GetComponent<Healthuimanager>();
    }
    private void OnEnable()
    {
        controlls.Enable();
        updateexpbar();
    }
    public void gainexp(float expgained)
    {
        Statics.charcurrentexp += expgained;
        updateexpbar();
    }
    private void updateexpbar()
    {
        if (Statics.charcurrentexp >= Statics.charrequiredexp)
        {
            levelup();
            updateexpbar();
        }
        else
        {
            float levelpercentage = Statics.charcurrentexp / Statics.charrequiredexp;
            float currentexp = expbar.fillAmount;
            if (currentexp < levelpercentage)
            {
                expbar.fillAmount = levelpercentage;
            }
            if (currentexp > levelpercentage)
            {
                expbar.fillAmount = levelpercentage;
            }
            exptext.text = "Group Level " + Statics.charcurrentlvl;
        }      
    }
    private void levelup()
    {
        Statics.charcurrentlvl++;
        if (Statics.currentactiveplayer == 0)
        {
            checkforguard(Statics.currentfirstchar, 0);
            checkforguard(Statics.currentsecondchar, 1);
        }
        else
        {
            checkforguard(Statics.currentfirstchar, 1);
            checkforguard(Statics.currentsecondchar, 0);
        }
        checkforguard(Statics.currentthirdchar, 2);
        checkforguard(Statics.currentforthchar, 3);

        expbar.fillAmount = 0f;
        Statics.charcurrentexp = Mathf.RoundToInt(Statics.charcurrentexp - Statics.charrequiredexp);
        Statics.charrequiredexp = calculaterequiredexp();
    }
    private void checkforguard(int charnumber, int charposi)
    {
        if(charnumber != -1)
        {
            if (Statics.characterclassroll[charnumber] == 1)
            {
                Statics.charmaxhealth[charnumber] += Statics.guardbonushpeachlvl;
                healthUImanager.hpupdateafterlvlup(charnumber, charposi);
            }
        }
    }
    private int calculaterequiredexp()                                       // formel ist aus einem Video
    {
        int expperlevel = 0;
        for (int levelcycle = 1; levelcycle <= Statics.charcurrentlvl; levelcycle++)
        {
            expperlevel += (int)Mathf.Floor(levelcycle + flatexpnumber * Mathf.Pow(expmultiplier, levelcycle / expdivision));
        }
        return expperlevel / 4;
    }
}
