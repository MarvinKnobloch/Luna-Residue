using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthUImanager : MonoBehaviour
{
    [SerializeField] internal Expmanager handlesecondexp;

    public Image mainbar;
    public Image secondbar;
    public Image thirdcharbar;
    public Image forthcharbar;
    public Image mainexpbar;
    public Image secondexpbar;
    public Image thirdcharexp;
    public Image forthcharexp;
    public TextMeshProUGUI maincharhp;
    public TextMeshProUGUI secondcharhp;
    public TextMeshProUGUI thirdcharhp;
    public TextMeshProUGUI forthcharhp;
    public Text mainname;
    public Text secondname;
    public Text thirdcharname;
    public Text forthcharname;
    private GameObject mainchar;
    private GameObject secondchar;
    private GameObject thirdchar;
    private GameObject forthchar;

    public GameObject char3anzeige;
    public GameObject char4anzeige;

    public float secondcharhealth;
    private float secondcharmaxhealth;
    private GameObject expsavechar;

    public void sethealthbars()
    { 
        mainchar = LoadCharmanager.Overallmainchar;
        secondchar = LoadCharmanager.Overallsecondchar;

        expsavechar = mainchar;

        mainchar.GetComponent<SpielerHP>().Healthbar = mainbar;
        mainchar.GetComponent<SpielerHP>().healthText = maincharhp;
        mainname.text = mainchar.GetComponent<SpielerHP>().playername.text;

        float percantage = Statics.charcurrenthealth[Statics.currentfirstchar] / Statics.charmaxhealth[Statics.currentfirstchar];
        mainbar.fillAmount = percantage;
        maincharhp.text = "HP " + Statics.charcurrenthealth[Statics.currentfirstchar] + " / " + Statics.charmaxhealth[Statics.currentfirstchar];

        secondchar.GetComponent<SpielerHP>().Healthbar = mainbar;
        secondchar.GetComponent<SpielerHP>().healthText = maincharhp;

        secondcharhealth = secondchar.GetComponent<SpielerHP>().health;
        secondcharmaxhealth = secondchar.GetComponent<SpielerHP>().maxhealth;
        float hFraction = secondcharhealth / secondcharmaxhealth;
        secondbar.fillAmount = hFraction;
        secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
        secondname.text = secondchar.GetComponent<SpielerHP>().playername.text;

        if (LoadCharmanager.Overallthirdchar != null)
        {
            thirdchar = LoadCharmanager.Overallthirdchar;
            thirdchar.GetComponent<SpielerHP>().Healthbar = thirdcharbar;
            thirdchar.GetComponent<SpielerHP>().healthText = thirdcharhp;
            thirdcharname.text = thirdchar.GetComponent<SpielerHP>().playername.text;
            float hppercentage = Statics.charcurrenthealth[Statics.currentthirdchar] / Statics.charmaxhealth[Statics.currentthirdchar];
            thirdcharbar.fillAmount = hppercentage;
            thirdcharhp.text = "HP " + Statics.charcurrenthealth[Statics.currentthirdchar] + " / " + Statics.charmaxhealth[Statics.currentthirdchar];
            char3anzeige.SetActive(true);
        }
        else
        {
            char3anzeige.SetActive(false);
        }

        if (LoadCharmanager.Overallforthchar != null)
        {
            forthchar = LoadCharmanager.Overallforthchar;
            forthchar.GetComponent<SpielerHP>().Healthbar = forthcharbar;
            forthchar.GetComponent<SpielerHP>().healthText = forthcharhp;
            forthcharname.text = forthchar.GetComponent<SpielerHP>().playername.text;
            float hppercentage = Statics.charcurrenthealth[Statics.currentforthchar] / Statics.charmaxhealth[Statics.currentforthchar];
            forthcharbar.fillAmount = hppercentage;
            forthcharhp.text = "HP " + Statics.charcurrenthealth[Statics.currentforthchar] + " / " + Statics.charmaxhealth[Statics.currentforthchar];
            char4anzeige.SetActive(true);
        }
        else
        {
            char4anzeige.SetActive(false);
        }
    }

    public void switchtomain()
    {
        mainname.text = mainchar.GetComponent<SpielerHP>().playername.text;
        mainchar.GetComponent<SpielerHP>().UpdatehealthUI();

        secondcharhealth = secondchar.GetComponent<SpielerHP>().health;
        secondcharmaxhealth = secondchar.GetComponent<SpielerHP>().maxhealth;
        float hFraction = secondcharhealth / secondcharmaxhealth;
        secondbar.fillAmount = hFraction;
        secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
        secondname.text = secondchar.GetComponent<SpielerHP>().playername.text;
    }
    public void switchtosecond()
    {
        mainname.text = secondchar.GetComponent<SpielerHP>().playername.text;
        secondchar.GetComponent<SpielerHP>().UpdatehealthUI();

        secondcharhealth = mainchar.GetComponent<SpielerHP>().health;
        secondcharmaxhealth = mainchar.GetComponent<SpielerHP>().maxhealth;
        float hFraction = secondcharhealth / secondcharmaxhealth;
        secondbar.fillAmount = hFraction;
        secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
        secondname.text = mainchar.GetComponent<SpielerHP>().playername.text;

    }
    public void hpupdateafterlvlup(int charnumber, int slot)
    {
        float hFraction = Statics.charcurrenthealth[charnumber] / Statics.charmaxhealth[charnumber];
        if (slot == 1)
        {
            mainbar.fillAmount = hFraction;
            maincharhp.text = "HP " + Statics.charcurrenthealth[charnumber] + " / " + Statics.charmaxhealth[charnumber];
        }
        else if(slot == 2)
        {
            secondbar.fillAmount = hFraction;
            secondcharhp.text = "HP " + Statics.charcurrenthealth[charnumber] + " / " + Statics.charmaxhealth[charnumber];
        }
        else if (slot == 3)
        {
            thirdcharbar.fillAmount = hFraction;
            thirdcharhp.text = "HP " + Statics.charcurrenthealth[charnumber] + " / " + Statics.charmaxhealth[charnumber];
        }
        else if (slot == 4)
        {
            forthcharbar.fillAmount = hFraction;
            forthcharhp.text = "HP " + Statics.charcurrenthealth[charnumber] + " / " + Statics.charmaxhealth[charnumber];
        }
    }
}
