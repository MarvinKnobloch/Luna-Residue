using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Healthuimanager : MonoBehaviour
{
    [SerializeField] internal Expmanager handlesecondexp;

    private int[] chars = new int[4];
    [SerializeField] private GameObject[] playerui;
    [SerializeField] private TextMeshProUGUI[] playernames;
    [SerializeField] private Image[] healthbars;
    [SerializeField] private TextMeshProUGUI[] healthtexts;

    public void sethealthbars()
    {
        chars[0] = Statics.currentfirstchar;
        chars[1] = Statics.currentsecondchar;
        chars[2] = Statics.currentthirdchar;
        chars[3] = Statics.currentforthchar;
        for (int i = 0; i < chars.Length; i++)
        {
            if(chars[i] != -1)
            {
                playerui[i].SetActive(true);
                playernames[i].text = Statics.characternames[chars[i]];
                float hppercentage = Statics.charcurrenthealth[chars[i]] / Statics.charmaxhealth[chars[i]];
                healthbars[i].fillAmount = hppercentage;
                healthtexts[i].text = "HP " + Statics.charcurrenthealth[chars[i]] + " / " + Statics.charmaxhealth[chars[i]];
            }
            else
            {
                playerui[i].SetActive(false);
            }
        }
    }
    public void healthupdate(int slot, float health, float maxhealth)
    {
        float addhp = healthbars[slot].fillAmount;
        float hppercentage = health / maxhealth;
        if (addhp > hppercentage)
        {
            healthbars[slot].fillAmount = hppercentage;
        }
        if (addhp < hppercentage)
        {
            healthbars[slot].fillAmount = hppercentage;
        }
        healthtexts[slot].text = "HP " + health + " / " + maxhealth;
    }

    public void switchtomain()
    {
        playernames[0].text = Statics.characternames[chars[0]];
        float hppercentage = Statics.charcurrenthealth[chars[0]] / Statics.charmaxhealth[chars[0]];
        healthbars[0].fillAmount = hppercentage;
        healthtexts[0].text = "HP " + Statics.charcurrenthealth[chars[0]] + " / " + Statics.charmaxhealth[chars[0]];

        playernames[1].text = Statics.characternames[chars[1]];
        float percentage = Statics.charcurrenthealth[chars[1]] / Statics.charmaxhealth[chars[1]];
        healthbars[1].fillAmount = percentage;
        healthtexts[1].text = "HP " + Statics.charcurrenthealth[chars[1]] + " / " + Statics.charmaxhealth[chars[1]];
    }
    public void switchtosecond()
    {
        playernames[0].text = Statics.characternames[chars[1]];
        float hppercentage = Statics.charcurrenthealth[chars[1]] / Statics.charmaxhealth[chars[1]];
        healthbars[0].fillAmount = hppercentage;
        healthtexts[0].text = "HP " + Statics.charcurrenthealth[chars[1]] + " / " + Statics.charmaxhealth[chars[1]];

        playernames[1].text = Statics.characternames[chars[0]];
        float percentage = Statics.charcurrenthealth[chars[0]] / Statics.charmaxhealth[chars[0]];
        healthbars[1].fillAmount = percentage;
        healthtexts[1].text = "HP " + Statics.charcurrenthealth[chars[0]] + " / " + Statics.charmaxhealth[chars[0]];
    }
    public void hpupdateafterlvlup(int charnumber, int slot)
    {
        float hppercentage = Statics.charcurrenthealth[charnumber] / Statics.charmaxhealth[charnumber];
        healthbars[slot].fillAmount = hppercentage;
        healthtexts[slot].text = "HP " + Statics.charcurrenthealth[charnumber] + " / " + Statics.charmaxhealth[charnumber];
    }
}
