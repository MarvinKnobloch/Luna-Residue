using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalCD : MonoBehaviour
{
    static MonoBehaviour instance;
    public Image healcdUI;
    public Text healcdtext;

    public Image dashcdUI;
    public Text dashcdtext;

    private float resetdashtime;
    public static int currentweaponswitchchar;
    public static int currentcharswitchchar;

    public Image weaponswitchcdUI;
    public Text weaponswitchtext;

    public Image weaponswitchbuffUI;
    public Text weaponswitchbufftext;

    [SerializeField] private GameObject weaponswitchbuffimage;
    [SerializeField] private GameObject charswitchbuffimage;
    public Image charwechselbuffUI;
    public Text charwechselbufftext;
    public Image charwechselcdUI;
    public Text charwechseltext;

    private bool dashcounterisrunning;
    private bool charswitchbuffisrunning;

    private float currentmissingweaponbufftime;
    private float currentmissingcharbufftime;
    void Awake()
    {
        Statics.dashcdbool = false;
        Statics.charswitchbool = false;
        Statics.healcdbool = true;
        Statics.characterswitchbuff = 100;
        Statics.weaponswitchbuff = 100;
        Statics.otheraction = false;
        instance = this;
        healcdtext.text = "";
        dashcdtext.text = "";
        weaponswitchtext.text = "";
        weaponswitchbufftext.text = "";
        charwechseltext.text = "";
        Statics.healmissingtime = 7f;
        Statics.dashcdmissingtime = Statics.dashcd;
        charswitchbuffimage.SetActive(false);
        onswitchhealingcd();
    }

    public static void starthealingcd()
    {
        instance.StartCoroutine("healingcd");
    }
    public static void onswitchhealingcd()
    {
        instance.StartCoroutine("healingcdonchange");
    }
    public static void startdashcd()
    {
        instance.StartCoroutine("dashcd"); 
    }
    public static void startresetdash()
    {
        instance.StartCoroutine("resetdash");
    }
    public static void startweaponswitchcd()
    {
        instance.StartCoroutine("weaponcd");
    }
    public static void startweaponswitchbuff(int currentchar)
    {
        currentweaponswitchchar = currentchar;
        instance.StopCoroutine("weaponswitchbuff");
        instance.StartCoroutine("weaponswitchbuff");
    }
    public static void startcharswitch()
    {
        instance.StartCoroutine("charswitchbuff");
        instance.StartCoroutine("charswitchcd");
    }
    IEnumerator healingcd()
    {
        Statics.healcdbool = true;
        healcdUI.fillAmount = 0;
        Statics.healmissingtime = 0;

        while (true)
        {
            Statics.healmissingtime += Time.deltaTime;
            healcdUI.fillAmount = Statics.healmissingtime / Statics.healcd;
            healcdtext.text = Mathf.RoundToInt(Statics.healcd - Statics.healmissingtime).ToString();

            if (Statics.healmissingtime >= Statics.healcd)
            {
                healcdtext.text = "";
                Statics.healcdbool = false;
                StopCoroutine("healingcd");
            }
            yield return null;
        }
    }
    IEnumerator healingcdonchange()
    {
        Statics.healcdbool = true;
        while (true)
        {
            Statics.healmissingtime += Time.deltaTime;
            healcdUI.fillAmount = Statics.healmissingtime / Statics.healcd;
            healcdtext.text = Mathf.RoundToInt(Statics.healcd - Statics.healmissingtime).ToString();

            if (Statics.healmissingtime >= Statics.healcd)
            {
                healcdtext.text = "";
                Statics.healcdbool = false;
                StopCoroutine("healingcdonchange");
            }
            yield return null;
        }
    }
    IEnumerator dashcd()
    {
        if(dashcounterisrunning == true)
        {
            StopCoroutine("dashcd");
            dashcounterisrunning = false;
            StartCoroutine("dashcd");
        }
        else
        {
            Statics.dashcdbool = true;
            Statics.dashcdmissingtime -= Statics.dashcost;
            dashcounterisrunning = true;
        }

        while (true)
        {
            Statics.dashcdmissingtime += Time.deltaTime;
            dashcdUI.fillAmount = Statics.dashcdmissingtime / Statics.dashcd;
            dashcdtext.text = Mathf.RoundToInt(Statics.dashcd - Statics.dashcdmissingtime).ToString();

            if (Statics.dashcdmissingtime >= Statics.dashcd)
            {
                Statics.dashcdbool = false;
                dashcdtext.text = "";
                StopCoroutine("dashcd");
                dashcounterisrunning = false;
            }
            yield return null;
        }
       
    }
    IEnumerator resetdash()
    {
        resetdashtime = 0;
        while (true)
        {
            resetdashtime += Time.deltaTime;
            if(resetdashtime >= 0.15)
            {
                Statics.dash = false;
                StopCoroutine("resetdash");
            }
            yield return null;
        }
    }
    IEnumerator weaponcd()
    {
        Statics.weapsonswitchbool = true;
        weaponswitchcdUI.fillAmount = 0;
        Statics.weaponswitchmissingtime = 0;

        while (true)
        {
            Statics.weaponswitchmissingtime += Time.deltaTime;
            weaponswitchcdUI.fillAmount = Statics.weaponswitchmissingtime / Statics.weaponswitchcd;
            weaponswitchtext.text = Mathf.RoundToInt(Statics.weaponswitchcd - Statics.weaponswitchmissingtime).ToString();

            if (Statics.weaponswitchmissingtime >= Statics.weaponswitchcd)
            {
                Statics.weapsonswitchbool = false;
                weaponswitchtext.text = "";
                StopCoroutine("weaponcd");
            }
            yield return null;
        }
    }
    IEnumerator weaponswitchbuff()
    {
        weaponswitchbuffimage.SetActive(true);       
        Statics.weaponswitchbuffmissingtime += Statics.charweaponbuffduration[currentweaponswitchchar];
        currentmissingweaponbufftime = Statics.weaponswitchbuffmissingtime;
        
        while (true)
        {
            Statics.weaponswitchbuffmissingtime -= Time.deltaTime;
            weaponswitchbuffUI.fillAmount = Statics.weaponswitchbuffmissingtime / currentmissingweaponbufftime;
            weaponswitchbufftext.text = Mathf.RoundToInt(Statics.weaponswitchbuffmissingtime).ToString();

            if (Statics.weaponswitchbuffmissingtime <= 0)
            {
                Statics.weaponswitchbuff = 100;
                weaponswitchbuffimage.SetActive(false);
                StopCoroutine("weaponswitchbuff");
            }
            yield return null;
        }
    }
    IEnumerator charswitchcd()         // wird im Main/Secondcharwechsel gecalled
    {
        Statics.charswitchbool = true;
        charwechselcdUI.fillAmount = 0;
        Statics.charswitchmissingtime = 0;

        Statics.characterswitchbuff = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().charswitchbuff;
        charwechselbuffUI.fillAmount = 0;
        while (true)
        {
            Statics.charswitchmissingtime += Time.deltaTime;
            charwechselcdUI.fillAmount = Statics.charswitchmissingtime / Statics.charswitchcd;
            charwechseltext.text = Mathf.RoundToInt(Statics.charswitchcd - Statics.charswitchmissingtime).ToString();

            if (Statics.charswitchmissingtime >= Statics.charswitchcd)
            {
                Statics.charswitchbool = false;
                charwechseltext.text = "";
                StopCoroutine("charswitchcd");
            }
            yield return null;
        }
    }
    IEnumerator charswitchbuff()
    {
        charswitchbuffimage.SetActive(true);
        if (charswitchbuffisrunning == true)
        {
            StopCoroutine("charswitchbuff");
            charswitchbuffisrunning = false;
            StartCoroutine("charswitchbuff");
        }
        else
        {
            Statics.charswitchbuffmissingtime = Statics.charswitchbuffduration[currentcharswitchchar];
            charswitchbuffisrunning = true;
            currentmissingcharbufftime = Statics.charswitchbuffmissingtime;
        }
        while (true)
        {
            Statics.charswitchbuffmissingtime -= Time.deltaTime;
            charwechselbuffUI.fillAmount = Statics.charswitchbuffmissingtime / currentmissingcharbufftime;
            charwechselbufftext.text = Mathf.RoundToInt(Statics.charswitchbuffmissingtime).ToString();

            if (Statics.charswitchbuffmissingtime <= 0)
            {
                Statics.characterswitchbuff = 100;
                charswitchbuffimage.SetActive(false);
                charswitchbuffisrunning = false;
                StopCoroutine("charswitchbuff");
            }
            yield return null;
        }
    }
}
