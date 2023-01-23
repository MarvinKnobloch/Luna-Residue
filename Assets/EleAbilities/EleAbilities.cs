using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;


public class EleAbilities : MonoBehaviour
{
    [SerializeField] internal Movescript Movementscript;
    private SpielerSteu Steuerung;

    const string firedashstartstate = "Firedashstart";
    const string waterpushbackstate = "Waterpushback";
    const string waterintoairstate = "Waterintoair";
    const string waterkickstate = "Waterkick";
    const string naturethendrilstate = "Naturethendril";
    const string icelanceidlestate = "Icelance";
    const string lightbackstabstartstate = "Lightbackstabstart";
    const string lightbackstabendstate = "Lightbackstabend";
    const string stormchainlightningstate = "Stormchainlightning";
    const string darkportalstate = "Darkportalstart";
    const string earthslidechargestate = "Earthslidecharge";

    public int currentelement;
    public int newelement;
    public int combospell;
    private Color spezialbackgroundcolor;
    public Text spezialtext;
    public Image spezialbackground;

    public GameObject charmanager;
    private Manamanager manacontroller;
    private float basicmanacosts = 3f;
    private float watermovementmanacost = 2f;

    public GameObject icelance1;
    public GameObject icelance2;
    public GameObject icelance3;
    public float icelancespeed;
    public bool starticelancemovement;

    public LayerMask Layerhitbox;
    public GameObject damagetext;

    void Awake()
    {
        manacontroller = charmanager.GetComponent<Manamanager>();
        newelement = 0;
        currentelement = 0;
        Statics.currentelementstate = 0;
        combospell = 0;
        Steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        icelance1.SetActive(false);
        icelance2.SetActive(false);
        icelance3.SetActive(false);
        Steuerung.Enable();
        newelement = Statics.currentelementstate;
        combospell = Statics.currentcombospell;
    }
    private void OnDisable()
    {
        Statics.currentcombospell = combospell;
        Statics.currentelementstate = newelement;
    }
    void Update()
    {
        if (Statics.otheraction == false && LoadCharmanager.gameispaused == false)
        {
            if (Manamanager.mana >= basicmanacosts)
            {
                if (Steuerung.Player.Ability1.WasPerformedThisFrame())
                {
                    if (Statics.currentactiveplayer == 0 && Statics.spellnumbers[0] < 24)                // 24 = anzahl der vorhanden Spells
                    {
                        choosespell(Statics.spellnumbers[0]);
                    }
                    if (Statics.currentactiveplayer == 1 && Statics.spellnumbers[2] < 24)
                    {
                        choosespell(Statics.spellnumbers[2]);
                    }
                }
                if (Steuerung.Player.Ability2.WasPerformedThisFrame())
                {
                    if (Statics.currentactiveplayer == 0 && Statics.spellnumbers[1] < 24)
                    {
                        choosespell(Statics.spellnumbers[1]);
                    }
                    if (Statics.currentactiveplayer == 1 && Statics.spellnumbers[3] < 24)
                    {
                        choosespell(Statics.spellnumbers[3]);
                    }
                }
                if (Steuerung.Player.Ability3.WasPerformedThisFrame() && Statics.spellnumbers[4] < 24)
                {
                    choosespell(Statics.spellnumbers[4]);
                }

                if (Steuerung.Player.Ability4.WasPerformedThisFrame() && Statics.spellnumbers[5] < 24)
                {
                    choosespell(Statics.spellnumbers[5]);
                }

                if (Steuerung.Player.Ability56.WasPerformedThisFrame())
                {
                    float z = Steuerung.Player.Ability56.ReadValue<float>();
                    if (z > 1 && Statics.spellnumbers[6] < 24)
                    {
                        choosespell(Statics.spellnumbers[6]);
                    }
                    if (z < -1 && Statics.spellnumbers[7] < 24)
                    {
                        choosespell(Statics.spellnumbers[7]);
                    }
                }
                if (Steuerung.Player.Spezial.WasPerformedThisFrame())
                {
                    choosecombospell(combospell);
                    manacontroller.Managemana(-3);
                }
            }
        }
    }
    private void endabilities()
    {
        StopAllCoroutines();
        Movementscript.state = Movescript.State.Air;
        Statics.otheraction = false;
    }
    public void ignorelayers()
    {
        Physics.IgnoreLayerCollision(6, 6);
        Physics.IgnoreLayerCollision(8, 6);
        Physics.IgnoreLayerCollision(15, 6);
    }
    public void stopignorelayers()
    {
        Physics.IgnoreLayerCollision(1, 6, false);            //wegen memorypuzzle
        Physics.IgnoreLayerCollision(6, 6, false);
        Physics.IgnoreLayerCollision(8, 6, false);
        Physics.IgnoreLayerCollision(15, 6, false);
    }
    private void fire1()
    {
        manacontroller.Managemana(-basicmanacosts);
        Statics.otheraction = true;
        Movementscript.state = Movescript.State.Firedashstart;
        Movementscript.ChangeAnimationState(firedashstartstate);
        Movementscript.graviti = 0f;
        if (Movescript.lockontarget != null)
        {
            Transform target = Movescript.lockontarget;
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
        }
        ColorUtility.TryParseHtmlString("#A41D1D", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkfirestate(1);
    }
    

    private void fire2()
    {
        Debug.Log("fire2");
        ColorUtility.TryParseHtmlString("#A41D1D", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkfirestate(1);
    }
    private void fire3()
    {
        Debug.Log("fire3");
        ColorUtility.TryParseHtmlString("#A41D1D", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkfirestate(1);
    }
    private void checkfirestate(int newvalue)
    {
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setspezialnull();
                break;
            case 2:
                setspezialnull();
                break;
            case 3:
                setburn();
                break;
            case 4:
                setspezialnull();
                break;
            case 5:
                setspezialnull();
                break;
            case 6:
                setfirestorm();
                break;
            case 7:
                setexplosion();
                break;
            case 8:
                setmeteor();
                break;
        }
    }
    private void water1()
    {
        if (Movescript.lockontarget != null)
        {
            Transform target = Movescript.lockontarget;
            Ray ray = new Ray(this.transform.position + Vector3.up, Vector3.down);
            if (Vector3.Distance(transform.position, target.transform.position) < 8f)
            {
                manacontroller.Managemana(-watermovementmanacost);
                Statics.otheraction = true;
                Movementscript.state = Movescript.State.Waterpushback;
                Movementscript.ChangeAnimationState(waterpushbackstate);
                Movementscript.graviti = 0f;
                Vector3 lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos);
            }
            else if (Physics.Raycast(ray, out RaycastHit hit, 4f))
            {
                manacontroller.Managemana(-watermovementmanacost);
                Statics.otheraction = true;
                Movementscript.state = Movescript.State.Waterintoair;
                Movementscript.ChangeAnimationState(waterintoairstate);
                Movementscript.graviti = 0f;
                Vector3 lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos);
            }
            else
            {
                manacontroller.Managemana(-watermovementmanacost);
                Statics.otheraction = true;
                Movementscript.state = Movescript.State.Waterkickend;
                Movementscript.ChangeAnimationState(waterkickstate);
                Movementscript.graviti = 0f;
                Vector3 lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos);
            }
            ignorelayers();
            ColorUtility.TryParseHtmlString("#1A19C5", out spezialbackgroundcolor);
            spezialbackground.color = spezialbackgroundcolor;
            checkwaterstate(2);
        }
    }
    private void water2()
    {
        Debug.Log("water2");
        ColorUtility.TryParseHtmlString("#1A19C5", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkwaterstate(2);
    }
    private void water3()
    {
        Debug.Log("water3");
        ColorUtility.TryParseHtmlString("#1A19C5", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkwaterstate(2);
    }
    private void checkwaterstate(int newvalue)
    {
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setspezialnull();
                break;
            case 2:
                setspezialnull();
                break;
            case 3:
                setposioncloud();
                break;
            case 4:
                seticerain();
                break;
            case 5:
                setspezialnull();
                break;
            case 6:
                setwaterdome();
                break;
            case 7:
                setspezialnull();
                break;
            case 8:
                setgeysir();
                break;
        }
    }
    private void nature1()
    {
        if (Movescript.lockontarget != null)
        {
            manacontroller.Managemana(-basicmanacosts);
            Statics.otheraction = true;
            Movementscript.state = Movescript.State.Naturethendril;
            Movementscript.ChangeAnimationState(naturethendrilstate);
            ignorelayers();
            Movementscript.graviti = 0f;
            Transform target = Movescript.lockontarget;
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);

            int dmgdealed = 10;
            target.gameObject.GetComponentInChildren<EnemyHP>().takeplayerdamage(dmgdealed, 0, false);

            ColorUtility.TryParseHtmlString("#1D9028", out spezialbackgroundcolor);
            spezialbackground.color = spezialbackgroundcolor;
            checknaturestate(3);
        }
    }
    private void nature2()
    {
        Debug.Log("nature2");
        ColorUtility.TryParseHtmlString("#1D9028", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checknaturestate(3);
    }
    private void nature3()
    {
        Debug.Log("nature3");
        ColorUtility.TryParseHtmlString("#1D9028", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checknaturestate(3);
    }
    private void checknaturestate(int newvalue)
    {
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setburn();
                break;
            case 2:
                setposioncloud();
                break;
            case 3:
                setspezialnull();
                break;
            case 4:
                setspezialnull();
                break;
            case 5:
                setsunflair();
                break;
            case 6:
                setspezialnull();
                break;
            case 7:
                setspezialnull();
                break;
            case 8:
                setthrontendril();
                break;
        }
    }
    private void ice1()
    {
        if (Movescript.lockontarget != null)
        {
            manacontroller.Managemana(-basicmanacosts);
            Statics.otheraction = true;
            Movementscript.state = Movescript.State.Icelancestart;
            Movementscript.ChangeAnimationState(icelanceidlestate);
            ignorelayers();
            Movementscript.graviti = 0f;
            Transform target = Movescript.lockontarget;
            if (Vector3.Distance(transform.position, target.transform.position) < 2f)
            {
                Vector3 lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
            }
            icelance1.transform.position = LoadCharmanager.Overallmainchar.transform.position + (transform.right * -2) + Vector3.up;
            icelance1.transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - icelance1.transform.position, Vector3.up);
            icelance2.transform.position = LoadCharmanager.Overallmainchar.transform.position + (transform.right * 2) + Vector3.up;
            icelance2.transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - icelance2.transform.position, Vector3.up);
            icelance3.transform.position = LoadCharmanager.Overallmainchar.transform.position;
            icelance3.transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - icelance3.transform.position, Vector3.up);
            icelance1.SetActive(true);
            icelance2.SetActive(true);
            icelance3.SetActive(true);
            ColorUtility.TryParseHtmlString("#219C93", out spezialbackgroundcolor);
            spezialbackground.color = spezialbackgroundcolor;
            checkicestate(4);
        }
    }
    public void starticelance3movement()
    {
        StartCoroutine("icelance3movement");
    }
    IEnumerator icelance3movement()
    {
        starticelancemovement = false;
        Transform target = Movescript.lockontarget;
        if (target != null)
        {
            while (true)
            {
                icelance3.transform.position = Vector3.MoveTowards(icelance3.transform.position, icelance3.transform.position + Vector3.up, 1 * Time.deltaTime);
                if (starticelancemovement == true)
                {
                    starticelancemovement = false;
                    StopCoroutine("icelance3movement");
                }
                if (Movescript.lockontarget == null)
                {
                    StopCoroutine("shootfirsticelance");
                    icelance1.SetActive(false);
                    StopCoroutine("shootsecondicelance");
                    icelance2.SetActive(false);
                    StopCoroutine("icelance3movement");
                    icelance3.SetActive(false);
                }
                yield return null;
            }
        }
    }
    public void startfirsticelance()
    {
        StartCoroutine("shootfirsticelance");
    }

    IEnumerator shootfirsticelance()
    {
        if (Movescript.lockontarget != null)
        {
            Transform target = Movescript.lockontarget;
            if (target != null)
            {
                while (true)
                {
                    if (target != null)
                    {
                        icelance1.transform.position = Vector3.MoveTowards(icelance1.transform.position, target.position, Movementscript.icelancespeed * Time.deltaTime);
                        if (Vector3.Distance(icelance1.transform.position, target.position) < 0.1f)
                        {
                            icelancedmg();
                            icelance1.SetActive(false);
                            StopCoroutine("shootfirsticelance");
                        }
                    }
                    else
                    {
                        target = Movescript.lockontarget;
                    }
                    yield return null;
                }
            }
            else
            {
                icelance1.SetActive(false);
                StopCoroutine("shootfirsticelance");
            }
        }
        else
        {
            icelance1.SetActive(false);
            StopCoroutine("shootfirsticelance");
        }
    }
    public void startsecondicelance()
    {
        StartCoroutine("shootsecondicelance");
    }
    IEnumerator shootsecondicelance()
    {
        if (Movescript.lockontarget != null)
        {
            Transform target = Movescript.lockontarget;
            if (target != null)
            {
                while (true)
                {
                    if (target != null)
                    {                   
                        icelance2.transform.position = Vector3.MoveTowards(icelance2.transform.position, target.position, Movementscript.icelancespeed * Time.deltaTime);
                        if (Vector3.Distance(icelance2.transform.position, target.position) < 0.1f)
                        {
                            icelancedmg();
                            icelance2.SetActive(false);
                            StopCoroutine("shootsecondicelance");
                        }
                    }
                    else
                    {
                        target = Movescript.lockontarget;
                        //icelance2.SetActive(false);
                        //StopCoroutine("shootsecondicelance");
                    }
                    yield return null;
                }
            }
            else
            {
                icelance2.SetActive(false);
                StopCoroutine("shootsecondicelance");
            }
        }
        else
        {
            icelance2.SetActive(false);
            StopCoroutine("shootsecondicelance");
        }
    }
    public void startthirdicelance()
    {
        starticelancemovement = true;
        StartCoroutine("shootthirdicelance");
    }
    IEnumerator shootthirdicelance()
    {
        if (Movescript.lockontarget != null)
        {
            Transform target = Movescript.lockontarget;
            if (target != null)
            {
                while (true)
                {
                    if (target != null)
                    {
                        icelance3.transform.position = Vector3.MoveTowards(icelance3.transform.position, target.position, Movementscript.icelancespeed * Time.deltaTime);
                        if (Vector3.Distance(icelance3.transform.position, target.position) < 0.1f)
                        {
                            icelancedmg();
                            icelance3.SetActive(false);
                            StopCoroutine("shootthirdicelance");
                        }
                    }
                    else
                    {
                        target = Movescript.lockontarget;
                    }
                    yield return null;
                }
            }
            else
            {
                icelance3.SetActive(false);
                StopCoroutine("shootthirdicelance");
            }
        }
        else
        {
            icelance3.SetActive(false);
            StopCoroutine("shootthirdicelance");
        }
    }
    private void icelancedmg()
    {
        Collider[] cols = Physics.OverlapSphere(Movescript.lockontarget.position, 2f, Layerhitbox);
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }
        }
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    float dmg = 10;
                    enemyscript.dmgonce = true;
                    enemyscript.takeplayerdamage(dmg, 0, false);
                }
            }
        }
    }
    public void icelanceiscanceled()
    {
        icelance1.SetActive(false);
        icelance2.SetActive(false);
        icelance3.SetActive(false);
        StopCoroutine("shootfirsticelance");
        StopCoroutine("shootsecondicelance");
        StopCoroutine("shootthirdicelance");
    }
    private void ice2()
    {
        Debug.Log("ice2");
        ColorUtility.TryParseHtmlString("#219C93", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkicestate(4);
    }
    private void ice3()
    {
        Debug.Log("ice3");
        ColorUtility.TryParseHtmlString("#219C93", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkicestate(4);
    }
    private void checkicestate(int newvalue)
    {
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setspezialnull();
                break;
            case 2:
                seticerain();
                break;
            case 3:
                setspezialnull();
                break;
            case 4:
                setspezialnull();
                break;
            case 5:
                seticelightballs();
                break;
            case 6:
                setspezialnull();
                break;
            case 7:
                seticegravity();
                break;
            case 8:
                setgainthammer();
                break;
        }
    }
    private void light1()
    {
        Transform target = Movescript.lockontarget;
        if (target != null)
        {
            manacontroller.Managemana(-basicmanacosts);
            Statics.otheraction = true;
            Movementscript.state = Movescript.State.Empty;
            Movementscript.ChangeAnimationState(lightbackstabstartstate);
            Movementscript.graviti = 0;
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
            ColorUtility.TryParseHtmlString("#F3C200", out spezialbackgroundcolor);
            spezialbackground.color = spezialbackgroundcolor;
            checklightstate(5);
        }
    }
    private void lightbackstabend()
    {
        Transform target = Movescript.lockontarget;
        if (target != null)
        {
            transform.position = target.transform.position + (transform.forward * 2);
            //transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
            {
                transform.position = hit.point;
            }
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
            Movementscript.ChangeAnimationState(lightbackstabendstate);
        }
        else
        {
            endabilities();
        }
    }
    private void ligthbackstabdmg()
    {
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 3f, Layerhitbox);
            foreach (Collider Enemyhit in cols)
            {
                if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                {
                    enemyscript.dmgonce = false;
                }
            }
            foreach (Collider Enemyhit in cols)
            {
                if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                {
                    if (enemyscript.dmgonce == false)
                    {
                        enemyscript.dmgonce = true;
                        float dmg = 10;
                        enemyscript.takeplayerdamage(dmg, 0, false);
                    }

                }
            }
        }
    }
    private void light2()
    {
        Debug.Log("light2");
        ColorUtility.TryParseHtmlString("#F3C200", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checklightstate(5);
    }
    private void light3()
    {
        Debug.Log("light3");
        ColorUtility.TryParseHtmlString("#F3C200", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checklightstate(5);
    }
    private void checklightstate(int newvalue)
    {
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setspezialnull();
                break;
            case 2:
                setspezialnull();
                break;
            case 3:
                setsunflair();
                break;
            case 4:
                seticelightballs();
                break;
            case 5:
                setspezialnull();
                break;
            case 6:
                setlightning();
                break;
            case 7:
                setblackhole();
                break;
            case 8:
                setspezialnull();
                break;
        }
    }
    private void storm1()
    {
        Transform target = Movescript.lockontarget;
        if (target != null)
        {
            manacontroller.Managemana(-basicmanacosts);
            Statics.otheraction = true;
            Movementscript.lightningfirsttarget = null;
            Movementscript.ligthningsecondtarget = null;
            Movementscript.lightningthirdtarget = null;
            Movementscript.lightningfirsttarget = target;
            Movementscript.currentlightningtraget = target;
            Movementscript.state = Movescript.State.Stormchainligthning;
            ignorelayers();
            Movementscript.ChangeAnimationState(stormchainlightningstate);
            Movementscript.graviti = 0;
            transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - transform.position, Vector3.up);
            ColorUtility.TryParseHtmlString("#5F138E", out spezialbackgroundcolor);
            spezialbackground.color = spezialbackgroundcolor;
            checkstormstate(6);
        }
    }
    private void storm2()
    {
        Debug.Log("storm2");
        ColorUtility.TryParseHtmlString("#5F138E", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkstormstate(6);
    }
    private void storm3()
    {
        Debug.Log("storm3");
        ColorUtility.TryParseHtmlString("#5F138E", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkstormstate(6);
    }
    private void checkstormstate(int newvalue)
    {
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setfirestorm();
                break;
            case 2:
                setwaterdome();
                break;
            case 3:
                setspezialnull();
                break;
            case 4:
                setspezialnull();
                break;
            case 5:
                setlightning();
                break;
            case 6:
                setspezialnull();
                break;
            case 7:
                setdarkmateriabeam();
                break;
            case 8:
                setspezialnull();
                break;
        }
    }
    private void dark1()
    {
        Transform target = Movescript.lockontarget;
        if (target != null)
        {
            manacontroller.Managemana(-basicmanacosts);
            Statics.otheraction = true;
            Movementscript.state = Movescript.State.Empty;
            Movementscript.ChangeAnimationState(darkportalstate);
            ignorelayers();
            Movementscript.graviti = 0;
            transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - transform.position, Vector3.up);
            ColorUtility.TryParseHtmlString("#1D1414", out spezialbackgroundcolor);
            spezialbackground.color = spezialbackgroundcolor;
            checkdarkstate(7);
        }
    }
    private void dark2()
    {
        Debug.Log("dark2");
        ColorUtility.TryParseHtmlString("#1D1414", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkdarkstate(7);
    }
    private void dark3()
    {
        Debug.Log("dark3");
        ColorUtility.TryParseHtmlString("#1D1414", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkdarkstate(7);
    }
    private void checkdarkstate(int newvalue)
    {
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setexplosion();
                break;
            case 2:
                setspezialnull();
                break;
            case 3:
                setspezialnull();
                break;
            case 4:
                seticegravity();
                break;
            case 5:
                setblackhole();
                break;
            case 6:
                setdarkmateriabeam();
                break;
            case 7:
                setspezialnull();
                break;
            case 8:
                setspezialnull();
                break;
        }
    }
    private void earth1()
    {
        if (Movescript.lockontarget != null)
        {
            Movementscript.graviti = 0f;
            manacontroller.Managemana(-basicmanacosts);
            Statics.otheraction = true;
            Movementscript.state = Movescript.State.Empty;
            Movementscript.ChangeAnimationState(earthslidechargestate);
            ignorelayers();
            Transform target = Movescript.lockontarget;
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
            ColorUtility.TryParseHtmlString("#4B2E11", out spezialbackgroundcolor);
            spezialbackground.color = spezialbackgroundcolor;
            checkearthstate(8);
        }
    }
    private void earth2()
    {
        Debug.Log("earth2");
        ColorUtility.TryParseHtmlString("#4B2E11", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkearthstate(8);
    }
    private void earth3()
    {
        Debug.Log("earth3");
        ColorUtility.TryParseHtmlString("#4B2E11", out spezialbackgroundcolor);
        spezialbackground.color = spezialbackgroundcolor;
        checkearthstate(8);
    }
    private void checkearthstate(int newvalue)
    {
        currentelement = newelement;
        newelement = newvalue;
        switch (currentelement)
        {
            case 0:
                break;
            case 1:
                setmeteor();
                break;
            case 2:
                setgeysir();
                break;
            case 3:
                thorntendril();
                break;
            case 4:
                setgainthammer();
                break;
            case 5:
                setspezialnull();
                break;
            case 6:
                setspezialnull();
                break;
            case 7:
                setspezialnull();
                break;
            case 8:
                setspezialnull();
                break;
        }
    }
    void choosespell(int number)
    {
        switch (number)
        {
            case 0:
                fire1();
                break;
            case 1:
                fire2();
                break;
            case 2:
                fire3();
                break;
            case 3:
                water1();
                break;
            case 4:
                water2();
                break;
            case 5:
                water3();
                break;
            case 6:
                nature1();
                break;
            case 7:
                nature2();
                break;
            case 8:
                nature3();
                break;
            case 9:
                ice1();
                break;
            case 10:
                ice2();
                break;
            case 11:
                ice3();
                break;
            case 12:
                light1();
                break;
            case 13:
                light2();
                break;
            case 14:
                light3();
                break;
            case 15:
                storm1();
                break;
            case 16:
                storm2();
                break;
            case 17:
                storm3();
                break;
            case 18:
                dark1();
                break;
            case 19:
                dark2();
                break;
            case 20:
                dark3();
                break;
            case 21:
                earth1();
                break;
            case 22:
                earth2();
                break;
            case 23:
                earth3();
                break;
        }
    }
    void choosecombospell(int number)
    {
        switch (number)
        {
            case 0:
                break;
            case 1:
                firestorm();
                break;
            case 2:
                burn();
                break;
            case 3:
                waterdome();
                break;
            case 4:
                geysir();
                break;
            case 5:
                thorntendril();
                break;
            case 6:
                posioncloud();
                break;
            case 7:
                icerain();
                break;
            case 8:
                icegravity();
                break;
            case 9:
                sunflair();
                break;
            case 10:
                icelightballs();
                break;
            case 11:
                lightning();
                break;
            case 12:
                darkmateriabeam();
                break;
            case 13:
                blackhole();
                break;
            case 14:
                explosion();
                break;
            case 15:
                gainthammer();
                break;
            case 16:
                meteor();
                break;
        }
    }
    private void setspezialnull()
    {
        spezialtext.text = "fail";
        combospell = 0;
    }
    private void firestorm()
    {
        Debug.Log("firestorm");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setfirestorm()
    {
        spezialtext.text = "Firestorm";
        combospell = 1;
    }
    private void burn()
    {
        Debug.Log("burn");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setburn()
    {
        spezialtext.text = "Burn";
        combospell = 2;
    }
    private void waterdome()
    {
        Debug.Log("waterdome");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setwaterdome()
    {
        spezialtext.text = "Waterdome";
        combospell = 3;
    }
    private void geysir()
    {
        Debug.Log("geysir");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setgeysir()
    {
        spezialtext.text = "Geysir";
        combospell = 4;
    }
    private void thorntendril()
    {
        Debug.Log("thorntendril");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setthrontendril()
    {
        spezialtext.text = "Thorntendril";
        combospell = 5;
    }
    private void posioncloud()
    {
        Debug.Log("posioncloud");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setposioncloud()
    {
        spezialtext.text = "poisoncload";
        combospell = 6;
    }
    private void icerain()
    {
        Debug.Log("icerain");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void seticerain()
    {
        spezialtext.text = "Icerain";
        combospell = 7;
    }
    private void icegravity()
    {
        Debug.Log("icegravity");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void seticegravity()
    {
        spezialtext.text = "Icegravity";
        combospell = 8;
    }
    private void sunflair()
    {
        Debug.Log("sunflair");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setsunflair()
    {
        spezialtext.text = "Sunflair";
        combospell = 9;
    }
    private void icelightballs()
    {
        Debug.Log("icelightballs");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void seticelightballs()
    {
        spezialtext.text = "Icelightballs";
        combospell = 10;
    }
    private void lightning()
    {
        Debug.Log("lightning");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setlightning()
    {
        spezialtext.text = "Lightning";
        combospell = 11;
    }
    private void darkmateriabeam()
    {
        Debug.Log("darkmateriabeam");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setdarkmateriabeam()
    {
        spezialtext.text = "Darkmateriabeam";
        combospell = 12;
    }
    private void blackhole()
    {
        Debug.Log("blackhole");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setblackhole()
    {
        spezialtext.text = "Blackhole";
        combospell = 13;
    }
    private void explosion()
    {
        Debug.Log("explosion");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setexplosion()
    {
        spezialtext.text = "Explosion";
        combospell = 14;
    }
    private void gainthammer()
    {
        Debug.Log("gainthammer");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setgainthammer()
    {
        spezialtext.text = "Gainthammer";
        combospell = 15;
    }
    private void meteor()
    {
        Debug.Log("meteor");
        currentelement = 0;
        newelement = 0;
        spezialbackground.color = Color.white;
        spezialtext.text = "fail";
    }
    private void setmeteor()
    {
        spezialtext.text = "Meteor";
        combospell = 16;
    }
}

/*if (Steuerung.Player.Ability3.WasPerformedThisFrame())
{
    choosespell(Statics.thirdability1);
    manacontroller.Managemana(-3);
}

if (Steuerung.Player.Ability4.WasPerformedThisFrame())
{
    choosespell(Statics.thirdability2);
    manacontroller.Managemana(-3);
}*/
