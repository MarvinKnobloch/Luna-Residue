using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;


public class EleAbilities : MonoBehaviour
{
    //animation time = ca. 0.9sec
    [SerializeField] internal Movescript Movementscript;
    private SpielerSteu controlls;

    const string firedashstartstate = "Firedashstart";
    const string waterpushbackstate = "Waterpushback";
    const string waterkickstate = "Waterkick";
    const string naturethendrilstate = "Naturethendril";
    const string icelanceidlestate = "Icelance";
    const string lightbackstabstartstate = "Lightbackstabstart";
    const string lightbackstabendstate = "Lightbackstabend";
    const string stormchainlightningstate = "Stormchainlightning";
    const string darkportalstate = "Darkportalstart";
    const string earthslidechargestate = "Earthslidecharge";

    public GameObject charmanager;
    private Manamanager manacontroller;
    private float basicmanacosts = 3f;

    [SerializeField] private GameObject[] spellposis;
    private GameObject water1posi;
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
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        icelance1.SetActive(false);
        icelance2.SetActive(false);
        icelance3.SetActive(false);
        controlls.Enable();
    }
    void Update()
    {
        if (Statics.otheraction == false && Statics.infight == true)
        {
            if (Manamanager.mana >= basicmanacosts)
            {
                if (controlls.Player.Ability1.WasPerformedThisFrame())
                {
                    if (Statics.currentactiveplayer == 0 && Statics.spellnumbers[0] != -1)
                    {
                        water1posi = spellposis[0];
                        Movementscript.spelltimer = 0;
                        choosespell(Statics.spellnumbers[0]);
                    }
                    if (Statics.currentactiveplayer == 1 && Statics.spellnumbers[2] != -1)
                    {
                        water1posi = spellposis[0];
                        Movementscript.spelltimer = 0;
                        choosespell(Statics.spellnumbers[2]);
                    }
                }
                if (controlls.Player.Ability2.WasPerformedThisFrame())
                {
                    if (Statics.currentactiveplayer == 0 && Statics.spellnumbers[1] != -1)
                    {
                        water1posi = spellposis[1];
                        Movementscript.spelltimer = 0;
                        choosespell(Statics.spellnumbers[1]);
                    }
                    if (Statics.currentactiveplayer == 1 && Statics.spellnumbers[3] != -1)
                    {
                        water1posi = spellposis[1];
                        Movementscript.spelltimer = 0;
                        choosespell(Statics.spellnumbers[3]);
                    }
                }
                if (controlls.Player.Ability3.WasPerformedThisFrame() && Statics.spellnumbers[4] != -1)
                {
                    water1posi = spellposis[2];
                    Movementscript.spelltimer = 0;
                    choosespell(Statics.spellnumbers[4]);
                }

                if (controlls.Player.Ability4.WasPerformedThisFrame() && Statics.spellnumbers[5] != -1)
                {
                    water1posi = spellposis[3];
                    Movementscript.spelltimer = 0;
                    choosespell(Statics.spellnumbers[5]);
                }

                if (controlls.Player.Ability56.WasPerformedThisFrame())
                {
                    Movementscript.spelltimer = 0;
                    float z = controlls.Player.Ability56.ReadValue<float>();
                    if (z > 1 && Statics.spellnumbers[6] != -1)
                    {
                        water1posi = spellposis[4];
                        choosespell(Statics.spellnumbers[6]);
                    }
                    if (z < -1 && Statics.spellnumbers[7] != -1)
                    {
                        water1posi = spellposis[5];
                        choosespell(Statics.spellnumbers[7]);
                    }
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
        Physics.IgnoreLayerCollision(11, 6);
        Physics.IgnoreLayerCollision(8, 6);
        Physics.IgnoreLayerCollision(15, 6);
    }
    public void stopignorelayers()
    {
        Physics.IgnoreLayerCollision(19, 6, false);            //wegen memorypuzzle
        Physics.IgnoreLayerCollision(11, 6, false);
        Physics.IgnoreLayerCollision(8, 6, false);
        Physics.IgnoreLayerCollision(15, 6, false);
    }
    public void overlapssphereeledmg(GameObject dmgposi, float radius, float dmg)
    {
        Collider[] cols = Physics.OverlapSphere(dmgposi.transform.position, radius, Layerhitbox, QueryTriggerInteraction.Ignore);
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.takeelementaldamage(dmg);
            }
        }
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
        Elespezial.instance.checkfirestate(1);
    }


    private void fire2()
    {
        Debug.Log("fire2");
        Elespezial.instance.checkfirestate(1);
    }
    private void fire3()
    {
        Debug.Log("fire3");
        Elespezial.instance.checkfirestate(1);
    }
    private void water1()
    {
        if (Movescript.lockontarget != null)
        {
            Transform target = Movescript.lockontarget;
            if (GlobalCD.instance.water1movement == false)
            {
                GlobalCD.instance.watermovementtimer(water1posi);
                manacontroller.Managemana(-basicmanacosts);
                Elespezial.instance.checkwaterstate(2);
            }
            else
            {
                GlobalCD.instance.stopwatermovementtimer();
            }
            if (Vector3.Distance(transform.position, target.transform.position) < 7f)
            {
                Statics.otheraction = true;
                Movementscript.waterpushbacktime = 0;
                Movementscript.state = Movescript.State.Waterpushback;
                Movementscript.ChangeAnimationState(waterpushbackstate);
                Movementscript.graviti = 0f;
                Vector3 lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos);
            }
            else
            {
                Statics.otheraction = true;
                Movementscript.state = Movescript.State.Waterkick;
                Movementscript.ChangeAnimationState(waterkickstate);
                Movementscript.graviti = 0f;
                Vector3 lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos);
            }
            ignorelayers();
        }
    }
    private void water2()
    {
        Debug.Log("water2");
        Elespezial.instance.checkwaterstate(2);
    }
    private void water3()
    {
        Debug.Log("water3");
        Elespezial.instance.checkwaterstate(2);
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
            Elespezial.instance.checknaturestate(3);
        }
    }
    private void nature2()
    {
        Debug.Log("nature2");
        Elespezial.instance.checknaturestate(3);
    }
    private void nature3()
    {
        Debug.Log("nature3");
        Elespezial.instance.checknaturestate(3);
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
            Elespezial.instance.checkicestate(4);
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
        overlapssphereeledmg(Movescript.lockontarget.gameObject, 2, 7);
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
        Elespezial.instance.checkicestate(4);
    }
    private void ice3()
    {
        Debug.Log("ice3");
        Elespezial.instance.checkicestate(4);
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
            Elespezial.instance.checklightstate(5);
        }
    }
    private void lightbackstabend()
    {
        if (Movementscript.state != Movescript.State.Empty) return;
        Transform target = Movescript.lockontarget;
        if (target != null)
        {
            transform.position = target.transform.position + (transform.forward * 2) + Vector3.up;
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
        overlapssphereeledmg(transform.gameObject, 3, 18);
    }
    private void light2()
    {
        Debug.Log("light2");
        Elespezial.instance.checklightstate(5);
    }
    private void light3()
    {
        Debug.Log("light3");
        Elespezial.instance.checklightstate(5);
    }
    private void storm1()
    {
        Transform target = Movescript.lockontarget;
        if (target != null)
        {
            manacontroller.Managemana(-basicmanacosts);
            Statics.otheraction = true;
            Movementscript.lightningspeed = 10;
            Movementscript.lightningfirsttarget = null;
            Movementscript.ligthningsecondtarget = null;
            Movementscript.lightningthirdtarget = null;
            Movementscript.lightningfirsttarget = target;
            Movementscript.currentlightningtarget = target;
            Movementscript.state = Movescript.State.Stormchainligthning;
            ignorelayers();
            Movementscript.ChangeAnimationState(stormchainlightningstate);
            Movementscript.graviti = 0;
            transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - transform.position, Vector3.up);
            Elespezial.instance.checkstormstate(6);
        }
    }
    private void storm2()
    {
        Debug.Log("storm2");
        Elespezial.instance.checkstormstate(6);
    }
    private void storm3()
    {
        Debug.Log("storm3");
        Elespezial.instance.checkstormstate(6);
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
            Elespezial.instance.checkdarkstate(7);
        }
    }
    private void dark2()
    {
        Debug.Log("dark2");
        Elespezial.instance.checkdarkstate(7);
    }
    private void dark3()
    {
        Debug.Log("dark3");
        Elespezial.instance.checkdarkstate(7);
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
            Elespezial.instance.checkearthstate(8);
        }
    }
    private void earth2()
    {
        Debug.Log("earth2");
        Elespezial.instance.checkearthstate(8);
    }
    private void earth3()
    {
        Debug.Log("earth3");
        Elespezial.instance.checkearthstate(8);
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
