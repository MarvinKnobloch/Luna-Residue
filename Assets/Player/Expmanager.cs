using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expmanager : MonoBehaviour
{
    public int charlvl;
    public float currentexp;
    public float requiredexp;

    private SpielerSteu Steuerung;
    [SerializeField] private Attributecontroller[] attributecontroller;
    private HealthUImanager healthUImanager;

    public Image[] expbar;

    [Range(1f, 300f)]
    public float flatexpnumber = 200;                                     // erhöht die benötigte exp für jedes lvl gleich, um so niederiger die zahl um so weniger exp braucht man zum lvln
    [Range(2f, 4f)]
    public float expmultiplier = 2;                                       // je höher die zahl um so steiler die kurve
    [Range(7f, 14f)]
    public float expdivision = 14;                                        // je höher die zahl um so flacher die kurve


    void Awake()
    {
        Steuerung = new SpielerSteu();
        healthUImanager = GetComponent<HealthUImanager>();
        charlvl = Statics.charcurrentlvl;
        currentexp = Statics.charcurrentexp;
        requiredexp = Statics.charrequiredexp;
    }
    private void OnEnable()
    {
        Steuerung.Enable();
        //requiredexp = calculaterequiredexp();
        updateexpbar();
    }

    void Update()
    {
        if (Steuerung.Player.GButton.WasPerformedThisFrame())
        {
            gainexp(200);
        }
    }
    public void updateexpbar()
    {
        float levelprozent = currentexp / requiredexp;
        foreach(Image bar in expbar)
        {
            float currentexpbar = bar.fillAmount;
            if (currentexpbar < levelprozent)
            {
                bar.fillAmount = levelprozent;
            }
            if (currentexpbar > levelprozent)
            {
                bar.fillAmount = levelprozent;
            }
        }
        if (currentexp >= requiredexp)
        {
            levelup();
        }
        else
        {
            Statics.charcurrentlvl = charlvl;
            Statics.charcurrentexp = currentexp;
            Statics.charrequiredexp = requiredexp;
        }

    }
    public void gainexp(float expgained)
    {
        currentexp += expgained;
        updateexpbar();
    }
    public void levelup()
    {
        charlvl++;
        if(Statics.currentactiveplayer == 0)
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

        foreach (Image bar in expbar)
        {
            bar.fillAmount = 0f;
        }
        currentexp = Mathf.RoundToInt(currentexp - requiredexp);
        requiredexp = calculaterequiredexp();
        updateexpbar();
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
        for (int levelcycle = 1; levelcycle <= charlvl; levelcycle++)
        {
            expperlevel += (int)Mathf.Floor(levelcycle + flatexpnumber * Mathf.Pow(expmultiplier, levelcycle / expdivision));
        }
        return expperlevel / 4;
    }
}
