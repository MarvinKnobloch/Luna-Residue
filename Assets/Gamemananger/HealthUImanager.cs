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
        mainchar.GetComponent<SpielerHP>().UpdatehealthUI();

        secondchar.GetComponent<SpielerHP>().Healthbar = mainbar;
        secondchar.GetComponent<SpielerHP>().healthText = maincharhp;

        secondcharhealth = secondchar.GetComponent<SpielerHP>().health;
        secondcharmaxhealth = secondchar.GetComponent<SpielerHP>().maxhealth;
        float hFraction = secondcharhealth / secondcharmaxhealth;
        secondbar.fillAmount = hFraction;
        secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
        secondname.text = secondchar.GetComponent<SpielerHP>().playername.text;

        if (LoadCharmanager.Overallthirdchar != null)                                                          //(PlayerPrefs.GetInt("Thirdcharindex") <= 5)
        {
            thirdchar = LoadCharmanager.Overallthirdchar;
            thirdchar.GetComponent<SpielerHP>().Healthbar = thirdcharbar;
            thirdchar.GetComponent<SpielerHP>().healthText = thirdcharhp;
            thirdcharname.text = thirdchar.GetComponent<SpielerHP>().playername.text;
            thirdchar.GetComponent<SpielerHP>().UpdatehealthUI();
            char3anzeige.SetActive(true);
        }
        else
        {
            char3anzeige.SetActive(false);
        }

        if (LoadCharmanager.Overallforthchar != null)                  //(PlayerPrefs.GetInt("Forthcharindex") <= 5)
        {
            forthchar = LoadCharmanager.Overallforthchar;
            forthchar.GetComponent<SpielerHP>().Healthbar = forthcharbar;
            forthchar.GetComponent<SpielerHP>().healthText = forthcharhp;
            forthcharname.text = forthchar.GetComponent<SpielerHP>().playername.text;
            forthchar.GetComponent<SpielerHP>().UpdatehealthUI();
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
    public void seconcharlvlupdate()
    {
        if (Statics.currentactiveplayer == 0)
        {
            if (secondchar.GetComponent<Attributecontroller>().guardhpbuff == true)
            {
                secondchar.GetComponent<SpielerHP>().maxhealth += Statics.guardbonushpeachlvl;
                Statics.charmaxhealth[Statics.currentsecondchar] = secondchar.GetComponent<SpielerHP>().maxhealth;
                secondcharhealth = secondchar.GetComponent<SpielerHP>().health;
                secondcharmaxhealth = secondchar.GetComponent<SpielerHP>().maxhealth;
                float hFraction = secondcharhealth / secondcharmaxhealth;
                secondbar.fillAmount = hFraction;
                secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
            }
        }
        else if (Statics.currentactiveplayer == 1)
        {
            if (mainchar.GetComponent<Attributecontroller>().guardhpbuff == true)
            {
                mainchar.GetComponent<SpielerHP>().maxhealth += Statics.guardbonushpeachlvl;
                Statics.charmaxhealth[Statics.currentfirstchar] = mainchar.GetComponent<SpielerHP>().maxhealth;
                secondcharhealth = mainchar.GetComponent<SpielerHP>().health;
                secondcharmaxhealth = mainchar.GetComponent<SpielerHP>().maxhealth;
                float hFraction = secondcharhealth / secondcharmaxhealth;
                secondbar.fillAmount = hFraction;
                secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
            }
        }
    }
}

    /*public void secondcharhpupdate()
    {
        if (Statics.currentactiveplayer == 0)
        {
            secondcharhealth = secondchar.GetComponent<SpielerHP>().health;
            secondcharmaxhealth = secondchar.GetComponent<SpielerHP>().maxhealth;
            float hFraction = secondcharhealth / secondcharmaxhealth;
            secondbar.fillAmount = hFraction;
            secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
        }
        else if (Statics.currentactiveplayer == 1)
        {
            secondcharhealth = mainchar.GetComponent<SpielerHP>().health;
            secondcharmaxhealth = mainchar.GetComponent<SpielerHP>().maxhealth;
            float hFraction = secondcharhealth / secondcharmaxhealth;
            secondbar.fillAmount = hFraction;
            secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
        }
    }
}*/


/*expsavechar = mainchar;
mainchar = secondchar;
secondchar = expsavechar;

secondcharhealth = secondchar.GetComponent<SpielerHP>().health;
secondcharmaxhealth = secondchar.GetComponent<SpielerHP>().maxhealth;
float hFraction = secondcharhealth / secondcharmaxhealth;
secondbar.fillAmount = hFraction;
secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
secondname.text = secondchar.GetComponent<SpielerHP>().playername.text;

mainchar.GetComponent<SpielerHP>().Healthbar = mainbar;
mainchar.GetComponent<SpielerHP>().healthText = maincharhp;
mainname.text = mainchar.GetComponent<SpielerHP>().playername.text;
mainchar.GetComponent<SpielerHP>().UpdatehealthUI();*/


/*expsavechar = mainchar;
mainchar = secondchar;
secondchar = expsavechar;

secondcharhealth = secondchar.GetComponent<SpielerHP>().health;
secondcharmaxhealth = secondchar.GetComponent<SpielerHP>().maxhealth;
float hFraction = secondcharhealth / secondcharmaxhealth;
secondbar.fillAmount = hFraction;
secondcharhp.text = "HP " + secondcharhealth + " / " + secondcharmaxhealth;
secondname.text = secondchar.GetComponent<SpielerHP>().playername.text;

mainchar.GetComponent<SpielerHP>().Healthbar = mainbar;
mainchar.GetComponent<SpielerHP>().healthText = maincharhp;
mainname.text = mainchar.GetComponent<SpielerHP>().playername.text;
mainchar.GetComponent<SpielerHP>().UpdatehealthUI();*/
