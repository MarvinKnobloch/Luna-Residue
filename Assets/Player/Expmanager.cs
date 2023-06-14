using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Expmanager : MonoBehaviour
{
    private Healthuimanager healthUImanager;

    [SerializeField] private TextMeshProUGUI grouplvltext;
    [SerializeField] private Image expbar;
    [SerializeField] private Image finalexpbar;
    [SerializeField] private TextMeshProUGUI groupexp;

    private float currentpercentage;
    private int finalbarlvl;

    [Range(1f, 300f)]
    public float flatexpnumber = 220;                                     // erhöht die benötigte exp für jedes lvl gleich, um so niederiger die zahl um so weniger exp braucht man zum lvln
    [Range(2f, 4f)]
    public float expmultiplier = 2;                                       // je höher die zahl um so steiler die kurve
    [Range(7f, 14f)]
    public float expdivision = 14;                                        // je höher die zahl um so flacher die kurve

    [SerializeField] private GameObject lvlupeffect;

    void Awake()
    {
        healthUImanager = GetComponent<Healthuimanager>();
    }
    private void OnEnable()
    {
        finalbarlvl = Statics.charcurrentlvl;
        StopCoroutine("fillbar");
        currentpercentage = Statics.charcurrentexp / Statics.charrequiredexp;
        expbar.fillAmount = currentpercentage;
        finalexpbar.fillAmount = currentpercentage;
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
            StopCoroutine("fillbar");
            StartCoroutine("fillbar");
            grouplvltext.text = "Group Level " + Statics.charcurrentlvl;
            groupexp.text = Statics.charcurrentexp + "/" + Statics.charrequiredexp;
        }      
    }
    IEnumerator fillbar()
    {
        if (finalbarlvl < Statics.charcurrentlvl) expbar.fillAmount = 1;
        currentpercentage = Statics.charcurrentexp / Statics.charrequiredexp;
        while (true)
        {
            if (finalexpbar.fillAmount < 1)
            {
                finalexpbar.fillAmount += 0.005f;
            }

            else 
            {
                finalbarlvl++;
                finalexpbar.fillAmount = 0; 
            }
            if (finalbarlvl >= Statics.charcurrentlvl)
            {
                expbar.fillAmount = currentpercentage;
                if (finalexpbar.fillAmount >= currentpercentage)
                {
                    finalexpbar.fillAmount = currentpercentage;
                    StopCoroutine("fillbar");
                }
            }
            yield return new WaitForSeconds(0.03f);
        }

    }
    private void levelup()
    {
        Statics.charcurrentlvl++;
        lvlupattackdmgupdate();
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
        lvlupeffect.SetActive(true);
    }
    private void lvlupattackdmgupdate()
    {
        for (int i = 0; i < Statics.charattack.Length; i++)
        {
            Statics.charattack[i]++;
        }
        LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().updateattributes();
        if (LoadCharmanager.Overallsecondchar != null) LoadCharmanager.Overallsecondchar.GetComponent<Attributecontroller>().updateattributes();
        if (LoadCharmanager.Overallthirdchar != null) LoadCharmanager.Overallthirdchar.GetComponent<Attributecontroller>().updateattributes();
        if (LoadCharmanager.Overallforthchar != null) LoadCharmanager.Overallforthchar.GetComponent<Attributecontroller>().updateattributes();
        if (TryGetComponent(out LoadCharmanager loadCharmanager))
        {
            loadCharmanager.weaponscriptupdate();
        }
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
