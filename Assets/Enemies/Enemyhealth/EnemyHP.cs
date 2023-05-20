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

    [NonSerialized] public string enemyname;
    public float currenthealth;
    public float maxhealth;
    [NonSerialized] public int sizeofenemy;
    public int enemylvl;
    [SerializeField] private int addenemylvl;
    [NonSerialized] public float finaldmg;

    private CapsuleCollider capsulecollider;
    private BoxCollider boxcollider;
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
    [NonSerialized] public bool enemyisdead;

    private int golddropamount;

    private Color dmgtextcolor;
    [SerializeField] private int[] playerhits = { 0, 0, 0 };
    private int mosthits;
    private int playerwithmosthits;
    [NonSerialized] public int currentplayerwithmosthits;
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
        boxcollider = GetComponent<BoxCollider>();
        enemymovement = GetComponent<Enemymovement>();
        enemyheight = (capsulecollider.height * transform.localScale.y) + 0.4f;
        enemyname = enemyvalues.enemyname;
        enemylvl = enemyvalues.enemylvl + addenemylvl;
        sizeofenemy = enemyvalues.enemysize;
        maxhealth = Mathf.Round(enemyvalues.basehealth + (enemyvalues.basehealth * Statics.enemyhealthpercantageadded * enemylvl));
        if(enemymovement != null) enemymovement.basedmg = Globalplayercalculations.calculateenemydmg(enemyvalues.basedmg, enemylvl);
        enemyfocusbargameobject = enemyfocusdebuffbar.transform.parent.gameObject;

        ColorUtility.TryParseHtmlString("#CD5003", out dmgtextcolor);

        if (LoadCharmanager.Overallmainchar != null)
        {
            if (Vector3.Distance(LoadCharmanager.Overallmainchar.transform.position, transform.position) < 20)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnEnable()
    {
        enemycalculatedmg.enemyscript = this;
        enemyisdead = false;
        currenthealth = maxhealth;
        capsulecollider.enabled = true;
        boxcollider.enabled = true;
        resetdebuff();
    }
    private void OnDisable()
    {
        removefromcanvas();                       //bei port wird der baractivate nicht getriggert
    }
    public void takeplayerdamage(float damage, int dmgtype, bool crit)
    {
        if (enemyisdead == false)
        {
            if (dmgtype == 0) finaldmg = damage;
            else if (dmgtype == 1) enemycalculatedmg.downdmg(damage);
            else if (dmgtype == 2) enemycalculatedmg.middmg(damage);
            else if (dmgtype == 3) enemycalculatedmg.updmg(damage);
            if (crit == true) Floatingnumberscontroller.floatingnumberscontroller.activatenumbers(this.gameObject, finaldmg, dmgtextcolor);
            else Floatingnumberscontroller.floatingnumberscontroller.activatenumbers(this.gameObject, finaldmg, Color.yellow);
            currenthealth -= finaldmg;
            afterdmgtaken();
        }
    }
    public void takeelementaldamage(float damage)
    {
        Floatingnumberscontroller.floatingnumberscontroller.activatenumbers(this.gameObject, damage, Color.blue);
        currenthealth -= damage;
        afterdmgtaken();
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
                capsulecollider.enabled = false;
                boxcollider.enabled = false;
                removefromcanvas();
                Infightcontroller.infightenemylists.Remove(transform.gameObject);
                int enemycount = Infightcontroller.infightenemylists.Count;
                Statics.currentenemyspecialcd = Statics.enemyspecialcd + (enemycount * 2);
                if (enemycount == 0)
                {
                    Infightcontroller.instance.savegameoverposi(GetComponent<Enemymovement>().spawnpostion);
                }
                if (Movescript.lockontarget != null)
                {
                    unmarktarget();
                    enemyisnotfocustarget();
                    LoadCharmanager.Overallmainchar.GetComponent<Movescript>().lockonchangeafterdeath();
                }
                Infightcontroller.instance.checkifinfight();
                supporttargetdied?.Invoke();
                LoadCharmanager.expmanager.gainexp(Mathf.Round(enemyvalues.expgain + (enemylvl * enemyvalues.expgain * 0.5f)));
                enemymovement.enemydied();
                if (gameObject.TryGetComponent(out Enemyisrewardobject enemyisrewardobject))
                {
                    enemyisrewardobject.checkforrewardcondition();
                }
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
        if(enemyisdead == false && gothealthbar == false)
        {
            gothealthbar = true;
            addhealthbar(this);                    //erstellt eine neu Healthbar mit verbindung zum Enemyhpscript;
            if (enemyincreasebasicdmg == true)
            {
                healthbar.debuffUI.SetActive(true);
                healthbar.debuffbar.color = Color.blue;
            }
            else if (enemydebuffcd == true)
            {
                healthbar.debuffUI.SetActive(true);
                healthbar.debuffbar.color = Color.yellow;
            }
        }
    }
    public void removefromcanvas()
    {
        if(healthbar != null)
        {
            healthbar = null;
            gothealthbar = false;
            removehealthbar(this);
        }
    }
    public void enemydebuffstart()
    {
        enemyincreasebasicdmg = true;
        enemydebuffcd = true;
        StartCoroutine("enemydebuff");
    }
    IEnumerator enemydebuff()
    {
        float debufftime = (float)Math.Round(UnityEngine.Random.Range(0.1f, 2.0f) + Statics.enemydebufftime, 1);
        debufftimer = debufftime;
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
                healthbar.debuffbar.fillAmount = debufftimer / debufftime;
            }
            if (isfocus == true)
            {
                enemyfocusdebuffbar.fillAmount = debufftimer / debufftime;
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
    private void resetdebuff()
    {
        StopCoroutine("enemydebuff");
        StopCoroutine("enemydebuffcdstart");
        enemyincreasebasicdmg = false;
        enemydebuffcd = false;
    }
    public void marktarget() => markcurrenttarget();
    public void unmarktarget()
    {
        if (gothealthbar == true) unmarkcurrenttarget();
    }
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
            hitstakensincelastaggrocheck = 0;
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
    public void fightstartsettraget()
    {
        mosthits = -1;
        playerwithmosthits = -1;
        playerhits[0] = UnityEngine.Random.Range(0, 2);
        if (LoadCharmanager.Overallthirdchar != null) playerhits[1] = UnityEngine.Random.Range(0, 2);
        else playerhits[1] = -11;
        if (LoadCharmanager.Overallforthchar != null) playerhits[2] = UnityEngine.Random.Range(0, 2);
        else playerhits[2] = -11;
        hitstakensincelastaggrocheck = 4;
        healthbar.currenttargetimage.gameObject.SetActive(true);
        currentplayerwithmosthits = -1;
        setnewtarget();
    }
    private void setnewtarget()
    {
        for (int i = 0; i < playerhits.Length; i++)
        {
            if (playerhits[i] > mosthits)
            {
                mosthits = playerhits[i];
                playerwithmosthits = i;
            }
        }
        if (playerwithmosthits == currentplayerwithmosthits) return;
        else if (playerwithmosthits == 0)
        {
            enemymovement.currenttarget = LoadCharmanager.Overallmainchar;
            if(Statics.currentactiveplayer == 0) healthbar.targetupdate(Statics.currentfirstchar);
            else healthbar.targetupdate(Statics.currentsecondchar);
            currentplayerwithmosthits = playerwithmosthits;
        }
        else if (playerwithmosthits == 1)
        {
            enemymovement.currenttarget = LoadCharmanager.Overallthirdchar;
            healthbar.targetupdate(Statics.currentthirdchar);
            currentplayerwithmosthits = playerwithmosthits;
        }
        else if (playerwithmosthits == 2)
        {
            enemymovement.currenttarget = LoadCharmanager.Overallforthchar;
            healthbar.targetupdate(Statics.currentforthchar);
            currentplayerwithmosthits = playerwithmosthits;
        }
        
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
        if(playerhits[player] < 1)
        {
            playerhits[player] = 1;
        }
    }
    private void dropitems()
    {
        if(enemyvalues.gold != null)
        {
            if (enemyvalues.golddropamount <= 2) golddropamount = 3;
            else golddropamount = enemyvalues.golddropamount;
            golddropamount = Mathf.RoundToInt(golddropamount * (enemylvl * 0.5f) + UnityEngine.Random.Range(0, Mathf.RoundToInt(enemylvl * 0.5f)));
            GameObject enemygolddrop = Instantiate(enemyvalues.gold, transform.position + Vector3.up, transform.rotation);
            enemygolddrop.GetComponent<Golditemcontroller>().golddropamount = golddropamount;
        }

        foreach (Enemydrops obj in enemyvalues.enemydrops)
        {
            int randomnumber = UnityEngine.Random.Range(0, 100);
            if (randomnumber <= obj.itemdropchance)
            {
                Instantiate(obj.itemtodrop, transform.position + Vector3.up, transform.rotation);
            }
        }
    }
}