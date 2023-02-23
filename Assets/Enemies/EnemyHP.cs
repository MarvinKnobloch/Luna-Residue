using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private Enemyvalues enemyvalues;
    private Enemycalculatedmg enemycalculatedmg = new Enemycalculatedmg();
    private Enemymovement enemymovement;
    public GameObject damagetext;

    [NonSerialized] public string enemyname;
    public float currenthealth;
    public float maxhealth;
    [NonSerialized] public int sizeofenemy;
    public int enemylvl;
    [SerializeField] private int addenemylvl;
    [NonSerialized] public float finaldmg;

    private CapsuleCollider capsulecollider;
    [NonSerialized] public float enemyheight;

    [NonSerialized] public bool enemyincreasebasicdmg;
    [NonSerialized] public bool enemydebuffcd;
    [NonSerialized] public float debufftimer;
    [NonSerialized] public float debuffcdtimer;
    [NonSerialized] public float debufffillamount;

    [NonSerialized] public Enemyhealthbar healthbar;
    private bool gothealthbar;
    [NonSerialized] public bool isfocus;
    [SerializeField] private GameObject enemyfocusbargameobject;
    [SerializeField] private Image enemyfocusdebuffbar;

    //public static bool enemygethit;
    private bool enemyisdead;
    [NonSerialized] public bool dmgonce;                           // da ich zwei collider hab und dadurch 2mal dmg gemacht wird, wird es momentan mit einem bool gecheckt ( hab noch keine besser lösung gefunden

    public Enemydrops[] itemdroplist;

    [SerializeField] private int[] playerhits = { 0, 0, 0 };
    private int mosthits;
    private int playerwithmosthits;
    private int hitstakensincelastaggrocheck;

    public static event Action supporttargetdied;

    public static event Action<EnemyHP> addhealthbar;
    public static event Action<EnemyHP> removehealthbar;

    public static event Action<EnemyHP> setfocustarget;
    public static event Action<EnemyHP> deselectfocustarget;

    public event Action<float> healthpctchanged;
    public event Action<float, float> focustargetuihptext;


    public event Action markcurrenttarget;
    public event Action unmarkcurrenttarget;

    private void Awake()
    {
        capsulecollider = GetComponent<CapsuleCollider>();
        enemymovement = GetComponent<Enemymovement>();
        enemyheight = (capsulecollider.height * transform.localScale.y) + 0.4f;
        enemyname = enemyvalues.enemyname;
        enemylvl = enemyvalues.enemylvl + addenemylvl;
        maxhealth = Mathf.Round(enemyvalues.basehealth + (enemyvalues.basehealth / 10 * enemylvl));
        currenthealth = Mathf.Clamp(currenthealth, 0, maxhealth);
        currenthealth = maxhealth;
        sizeofenemy = enemyvalues.enemysize;
        enemymovement.basedmg = enemyvalues.basedmg + enemylvl;
        enemyfocusbargameobject = enemyfocusdebuffbar.transform.parent.gameObject;
    }
    private void OnEnable()
    {
        enemycalculatedmg.enemyscript = this;
        enemyisdead = false;
        currenthealth = maxhealth;
        mosthits = 0;
        playerwithmosthits = 0;
        hitstakensincelastaggrocheck = 5;
        for (int i = 0; i < playerhits.Length; i++)
        {
            playerhits[i] = 0;
        }
    }
    public void takeplayerdamage(float damage, int dmgtype , bool crit)                                               
    {
        if (gameObject.GetComponent<Miniadd>())
        {
            currenthealth -= damage;
            if (currenthealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (enemyisdead == false)
            {
                if (dmgtype == 0) finaldmg = damage;
                else if (dmgtype == 1) enemycalculatedmg.downdmg(damage);
                else if (dmgtype == 2) enemycalculatedmg.middmg(damage);
                else if (dmgtype == 3) enemycalculatedmg.updmg(damage);
                var showtext = Instantiate(damagetext, transform.position, Quaternion.identity);
                showtext.GetComponent<TextMeshPro>().text = finaldmg.ToString();
                if (crit == true)
                {
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
                currenthealth -= finaldmg;
                afterdmgtaken();
            }
        }    
    }
    public void takesupportdmg(float dmg)
    {
        currenthealth -= dmg;
        afterdmgtaken();
    }
    private void afterdmgtaken()
    {
        if(enemyisdead == false)
        {
            if (currenthealth >= maxhealth)
            {
                currenthealth = maxhealth;
            }
            if (gothealthbar == true)
            {
                float currenthealthpct = currenthealth / maxhealth;
                healthpctchanged(currenthealthpct);
            }
            if (isfocus == true)
            {
                focustargetuihptext(currenthealth, maxhealth);
            }
            if (currenthealth <= 0)
            {
                enemyisdead = true;
                currenthealth = 0;
                removefromcanvas();
                Infightcontroller.infightenemylists.Remove(transform.gameObject);
                int enemycount = Infightcontroller.infightenemylists.Count;
                Statics.currentenemyspecialcd = Statics.enemyspecialcd + enemycount;
                if (enemycount == 0)
                {
                    Statics.gameoverposi = LoadCharmanager.Overallmainchar.transform.position;
                    Statics.gameoverrota = LoadCharmanager.Overallmainchar.transform.rotation;
                    Statics.gameovercam = LoadCharmanager.savecamvalueX;
                }
                if (Movescript.lockontarget != null)
                {
                    unmarktarget();
                    enemyisnotfocustarget();
                    LoadCharmanager.Overallmainchar.GetComponent<Movescript>().lockonfindclostesttarget();
                }
                Infightcontroller.checkifinfight();
                supporttargetdied?.Invoke();
                enemymovement.enemydied();
                Invoke("enemydied", 3);
            }
        }
    }
    private void enemydied()
    {
        gameObject.SetActive(false);
        dropitems();
    }
    public void enemyheal(float heal)
    {
        currenthealth += heal;
        if (currenthealth >= maxhealth)
        {
            currenthealth = maxhealth;
        }
        if (gothealthbar == true)
        {
            float currenthealthpct = currenthealth / maxhealth;
            healthpctchanged(currenthealthpct);
        }
        if (isfocus == true)
        {
            focustargetuihptext(currenthealth, maxhealth);
        }
    }
    public void addtocanvas()
    {
        gothealthbar = true;
        addhealthbar(this);     //erstellt eine neu Healthbar mit verbindung zum Enemyhpscript;

    }
    public void removefromcanvas()
    {
        healthbar = null;
        gothealthbar = false;
        removehealthbar(this);
    }
    public void enemydebuffstart()
    {
        enemyincreasebasicdmg = true;
        enemydebuffcd = true;
        StartCoroutine("enemydebuff");
    }
    IEnumerator enemydebuff()
    {
        debufftimer = Statics.enemydebufftime;
        if(gothealthbar == true)
        {
            healthbar.debuffUI.SetActive(true);
            healthbar.debuffbar.color = Color.blue;
        }
        if (isfocus == true)
        {
            enemyfocusbargameobject.SetActive(true);
        }
        while (true)
        {
            debufftimer -= Time.deltaTime;
            if (gothealthbar == true)
            {
                healthbar.debuffbar.fillAmount = debufftimer / Statics.enemydebufftime;
            }
            if (isfocus == true)
            {
                enemyfocusdebuffbar.fillAmount = debufftimer / Statics.enemydebufftime;
                enemyfocusdebuffbar.color = Color.blue;
            }

            if (debufftimer <= 0)
            {
                enemyincreasebasicdmg = false;
                StopCoroutine("enemydebuff");
                StartCoroutine("enemydebuffcdstart");
            }
            yield return null;
        }
    }
    IEnumerator enemydebuffcdstart()
    {
        debuffcdtimer = Statics.enemydebufftime;
        if (gothealthbar == true)
        {
            healthbar.debuffUI.SetActive(true);
            healthbar.debuffbar.color = Color.yellow;
        }
        while (true)
        {
            debuffcdtimer -= Time.deltaTime;
            if (gothealthbar == true)
            {
                healthbar.debuffbar.fillAmount = debuffcdtimer / Statics.enemydebufftime;
            }
            if (isfocus == true)
            {
                enemyfocusdebuffbar.fillAmount = debuffcdtimer / Statics.enemydebufftime;
                enemyfocusdebuffbar.color = Color.yellow;
            }

            if (debuffcdtimer <= 0)
            {
                enemydebuffcd = false;
                if (gothealthbar == true)
                {
                    healthbar.debuffUI.SetActive(false);
                    if (isfocus == true)
                    {
                        enemyfocusbargameobject.SetActive(false);
                    }
                }
                StopCoroutine("enemydebuffcdstart");
            }
            yield return null;
        }
    }
    public void marktarget() => markcurrenttarget();
    public void unmarktarget() => unmarkcurrenttarget();

    public void enemyisfocustarget()
    {
        isfocus = true;
        setfocustarget(this);
        if(enemydebuffcd == true)
        {
            enemyfocusbargameobject.SetActive(true);
        }
    }

    public void enemyisnotfocustarget()
    {
        isfocus = false;
        deselectfocustarget(this);
    }

    public void tookdmgfrom(int player, int hitamount)
    {
        hitstakensincelastaggrocheck++;
        if(player == 1) playerhits[0] += hitamount;
        else if(player == 3) playerhits[1] += hitamount;
        else if(player == 4) playerhits[2] += hitamount;
        if(hitstakensincelastaggrocheck >= 5)
        {
            setnewtarget();
        }
    }
    public void resetplayerhits()
    {
        mosthits = -1;
        playerwithmosthits = 0;
        hitstakensincelastaggrocheck = 5;
        for (int i = 0; i < playerhits.Length; i++)
        {
            playerhits[i] = 0;
        }
    }

    private void setnewtarget()
    {
        hitstakensincelastaggrocheck = 0;
        for (int i = 0; i < playerhits.Length; i++)
        {
            if (playerhits[i] > mosthits)
            {
                mosthits = playerhits[i];
                playerwithmosthits = i;
            }
        }
        if(playerwithmosthits == 0) enemymovement.currenttarget = LoadCharmanager.Overallmainchar;
        else if (playerwithmosthits == 1) enemymovement.currenttarget = LoadCharmanager.Overallthirdchar;
        else if (playerwithmosthits == 2) enemymovement.currenttarget = LoadCharmanager.Overallforthchar;
    }
    public void newtargetonplayerdeath(int player)
    {
        mosthits = -1;
        playerhits[player] = -10;
        setnewtarget();
    }
    public void playerisresurrected(int player)
    {
        playerhits[player] = mosthits - 10;
    }
    private void dropitems()
    {
        foreach (Enemydrops obj in itemdroplist)
        {
            int randomnumber = UnityEngine.Random.Range(0, 100);
            if (randomnumber <= obj.itemdropchance)
            {
                Instantiate(obj.itemtodrop, transform.position, transform.rotation);
            }
        }
    }
}
[Serializable]
public class Enemydrops
{
    [SerializeField] public GameObject itemtodrop;
    [SerializeField] public int itemdropchance;
}