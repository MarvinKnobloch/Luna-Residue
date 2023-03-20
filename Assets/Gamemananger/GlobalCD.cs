using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalCD : MonoBehaviour
{
    static MonoBehaviour instance;

    [SerializeField] private Image dashcdUI;
    [SerializeField] private TextMeshProUGUI dashcdtext;
    private float resetdashtime;

    [SerializeField] private Image healcdUI;
    [SerializeField] private TextMeshProUGUI healcdtext;

    [SerializeField] private Image weaponswitchcdimage;
    [SerializeField] private TextMeshProUGUI weaponswitchcdtext;
    [SerializeField] private Image weaponbuffduration�mage;
    [SerializeField] private Text weaponbuffdurationtext;

    [SerializeField] private Image charswitchcd�mage;
    [SerializeField] private TextMeshProUGUI charswitchcdtext;
    [SerializeField] private Image charbuffdurationimage;
    [SerializeField] private Text charbuffdurationtext;

    public static int currentweaponswitchchar;
    public static int currentcharswitchchar;


    [SerializeField] private GameObject weaponswitchbuffimage;
    [SerializeField] private GameObject charswitchbuffimage;

    private bool dashcounterisrunning;
    private bool charswitchbuffisrunning;

    private float currentmissingweaponbufftime;
    private float currentmissingcharbufftime;

    void Awake()
    {
        Statics.dashcdbool = false;
        Statics.charswitchbool = false;
        Statics.healcdbool = false;
        Statics.otheraction = false;
        instance = this;
        healcdtext.text = "";
        dashcdtext.text = "";
        weaponswitchcdtext.text = "";
        weaponbuffdurationtext.text = "";
        charswitchcdtext.text = "";
        Statics.healmissingtime = 7f;
        Statics.dashcdmissingtime = Statics.dashcd;
        charswitchbuffimage.SetActive(false);
    }

    public static void starthealingcd(int rescd)
    {
        Statics.healcd = Statics.presethealcd + rescd;
        instance.StartCoroutine("healingcd");
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
    public static void startsupportresurrectioncd()
    {
        instance.StartCoroutine("supportresurrection");
    }
    public static void stopsupportresurrectioncd()
    {
        instance.StopCoroutine("supportresurrection");
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
        weaponswitchcdimage.fillAmount = 0;
        Statics.weaponswitchmissingtime = 0;

        while (true)
        {
            Statics.weaponswitchmissingtime += Time.deltaTime;
            weaponswitchcdimage.fillAmount = Statics.weaponswitchmissingtime / Statics.weaponswitchcd;
            weaponswitchcdtext.text = Mathf.RoundToInt(Statics.weaponswitchcd - Statics.weaponswitchmissingtime).ToString();

            if (Statics.weaponswitchmissingtime >= Statics.weaponswitchcd)
            {
                Statics.weapsonswitchbool = false;
                weaponswitchcdtext.text = "";
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
            weaponbuffduration�mage.fillAmount = Statics.weaponswitchbuffmissingtime / currentmissingweaponbufftime;
            weaponbuffdurationtext.text = Mathf.RoundToInt(Statics.weaponswitchbuffmissingtime).ToString();

            if (Statics.weaponswitchbuffmissingtime <= 0)
            {
                Statics.weaponswitchbuff = 0;
                weaponswitchbuffimage.SetActive(false);
                StopCoroutine("weaponswitchbuff");
            }
            yield return null;
        }
    }
    IEnumerator charswitchcd()         // wird im Main/Secondcharwechsel gecalled
    {
        Statics.charswitchbool = true;
        charswitchcd�mage.fillAmount = 0;
        Statics.charswitchmissingtime = 0;

        Statics.characterswitchbuff = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().charswitchbuff;
        charbuffdurationimage.fillAmount = 0;
        while (true)
        {
            Statics.charswitchmissingtime += Time.deltaTime;
            charswitchcd�mage.fillAmount = Statics.charswitchmissingtime / Statics.charswitchcd;
            charswitchcdtext.text = Mathf.RoundToInt(Statics.charswitchcd - Statics.charswitchmissingtime).ToString();

            if (Statics.charswitchmissingtime >= Statics.charswitchcd)
            {
                Statics.charswitchbool = false;
                charswitchcdtext.text = "";
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
            charbuffdurationimage.fillAmount = Statics.charswitchbuffmissingtime / currentmissingcharbufftime;
            charbuffdurationtext.text = Mathf.RoundToInt(Statics.charswitchbuffmissingtime).ToString();

            if (Statics.charswitchbuffmissingtime <= 0)
            {
                Statics.characterswitchbuff = 0;
                charswitchbuffimage.SetActive(false);
                charswitchbuffisrunning = false;
                StopCoroutine("charswitchbuff");
            }
            yield return null;
        }
    }
    IEnumerator supportresurrection()
    {
        int randomnumber = Random.Range(1, 3);
        yield return new WaitForSeconds(Statics.infightresurrectcd + randomnumber);
        Statics.supportcanresurrect = true;
        StopCoroutine("supportresurrection()");
    }
}
