using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalCD : MonoBehaviour
{
    public static GlobalCD instance;

    [SerializeField] private Image dashcdUI;
    [SerializeField] private TextMeshProUGUI dashcdtext;
    private float resetdashtime;

    [SerializeField] private Image healcdUI;
    [SerializeField] private TextMeshProUGUI healcdtext;

    [SerializeField] private Image weaponswitchcdimage;
    [SerializeField] private TextMeshProUGUI weaponswitchcdtext;
    [SerializeField] private Image weaponbuffdurationîmage;
    [SerializeField] private Text weaponbuffdurationtext;

    [SerializeField] private Image charswitchcdimage;
    [SerializeField] private TextMeshProUGUI charswitchcdtext;
    [SerializeField] private Image charbuffdurationimage;
    [SerializeField] private Text charbuffdurationtext;

    public static int currentweaponswitchchar;
    public static int currentcharswitchchar;


    [SerializeField] private GameObject weaponswitchbuffimage;
    [SerializeField] private GameObject charswitchbuffimage;

    private bool dashcounterisrunning;
    private bool charswitchbuffisrunning;
    private bool supportrezzcdisrunning;

    private float currentmissingweaponbufftime;
    private float currentmissingcharbufftime;

    [SerializeField] private TextMeshProUGUI watertimer;
    private int watertimerdisplay;
    public bool water1movement;
    private Coroutine test;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Statics.dashcdbool = false;
        Statics.charswitchbool = false;
        Statics.healcdbool = false;
        Statics.otheraction = false;
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
        instance.StopCoroutine("weaponcd");
        instance.StartCoroutine("charswitchbuff");
        instance.StartCoroutine("charswitchcd");
    }
    public static void startsupportresurrectioncd()
    {
        if (instance.supportrezzcdisrunning == true || Statics.supportcanresurrect == true) return;
        else instance.StartCoroutine("supportresurrection");
    }
    public static void stopsupportresurrectioncd()
    {
        instance.supportrezzcdisrunning = false;
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
            //dashcdtext.text = Mathf.RoundToInt(Statics.dashcd - Statics.dashcdmissingtime).ToString();

            if (Statics.dashcdmissingtime >= Statics.dashcd)
            {
                Statics.dashcdbool = false;
                //dashcdtext.text = "";
                StopCoroutine("dashcd");
                dashcounterisrunning = false;
            }
            yield return null;
        }
       
    }
    IEnumerator resetdash()
    {
        Statics.bonusiframes = true;
        resetdashtime = 0;
        while (true)
        {
            resetdashtime += Time.deltaTime;
            if(resetdashtime >= 0.15)
            {
                Statics.dash = false;
                StartCoroutine("bonusiframes");
                StopCoroutine("resetdash");
            }
            yield return null;
        }
    }
    IEnumerator bonusiframes()                   //bonusiframes sind für battlemonk, kann man vll auch noch wo anderst einsetzten
    {
        yield return new WaitForSeconds(0.05f);
        Statics.bonusiframes = false;
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
            weaponbuffdurationîmage.fillAmount = Statics.weaponswitchbuffmissingtime / currentmissingweaponbufftime;
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
        Statics.weapsonswitchbool = false;
        weaponswitchcdimage.fillAmount = 1;
        weaponswitchcdtext.text = "";

        Statics.charswitchbool = true;
        charswitchcdimage.fillAmount = 0;
        Statics.charswitchmissingtime = 0;

        Statics.characterswitchbuff = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().charswitchbuff;
        charbuffdurationimage.fillAmount = 0;
        while (true)
        {
            Statics.charswitchmissingtime += Time.deltaTime;
            charswitchcdimage.fillAmount = Statics.charswitchmissingtime / Statics.charswitchcd;
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
        supportrezzcdisrunning = true;
        int randomnumber = Random.Range(1, 3);
        yield return new WaitForSeconds(Statics.infightresurrectcd * 2 + randomnumber);
        Statics.supportcanresurrect = true;
        supportrezzcdisrunning = false;
        StopCoroutine("supportresurrection");
    }
    public void watermovementtimer()
    {
        water1movement = true;
        watertimerdisplay = 4;
        watertimer.gameObject.SetActive(true);
        watertimer.text = watertimerdisplay.ToString();
        test = StartCoroutine(startwatermovementtimer());
    }
    IEnumerator startwatermovementtimer()
    {
        while(watertimerdisplay > 1)
        {
            watertimerdisplay--;
            watertimer.text = watertimerdisplay.ToString();
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Haööp");
        watertimer.gameObject.SetActive(false);
        water1movement = false;
    }
    public void stopwatermovementtimer()
    {
        StopCoroutine(test);
        watertimer.gameObject.SetActive(false);
        water1movement = false;
    }
}
