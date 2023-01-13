using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Bowattack : MonoBehaviour
{
    private Movescript movementscript;
    private AimScript aimscript;
    private Healingscript healingscript;
    private EleAbilities eleAbilities;

    private SpielerSteu Steuerung;
    private Animator animator;
    private bool root;
    private bool basic;
    private bool attackend;
    private bool basicairattack;
    private bool secondairattack;
    private bool thirdairattack;
    private bool checkairchainstate;
    private bool air3state;
    private float slowmogravition = 0.5f;

    public int combochain;
    private bool basicchain;
    private bool air3downintobasic;

    public bool input;
    private bool down;
    private bool mid;
    private bool up;

    private float basicattackcd;

    //weaponswitch
    private bool checkforweaponswitch;
    private float checkforenemyonswitch = 3f;
    public LayerMask weaponswitchlayer;
    public float slowmotionpercentage;
    public bool weaponswitchattack;
    public GameObject prefabarrow;

    //animationstates
    const string animationbowswitch = "Boweaponchange";
    const string slowmochargeup = "Slowmocharge";
    const string slowmoshoot = "Slowmoshoot";
    const string slowmofastcharge = "Slowmofastcharge";
    const string attack1state = "Basic";
    const string basicair1state = "Air1";
    const string dashstate = "Bowdash";
    const string kickstate = "Kick";
    const string fallstate = "Fall";
    const string starthookstate = "Bowhookcharge";
    const string chargestate = "Chargearrow";

    //castarrow
    public GameObject basicgroundarrow;
    public GameObject grounddownarrow;
    public GameObject groundmidarrow;
    public GameObject grounduparrow;
    public GameObject basicairarrow;
    public GameObject airdownarrow;
    public GameObject airmidarrow;
    public GameObject airuparrow;
    public GameObject hookshotarrow;
    public GameObject puzzlearrow;
    public Transform Arrowlaunchposi;
    private float maxtravelrangeifnothit = 50;
    public Transform Kamerarichtung;
    public LayerMask Arrowraycastlayer;
    public LayerMask Puzzlelayer;

    public GameObject charmanager;

    void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
        animator = GetComponent<Animator>();
        movementscript = GetComponent<Movescript>();
        aimscript = GetComponent<AimScript>();
        healingscript = GetComponent<Healingscript>();
        eleAbilities = GetComponent<EleAbilities>();
    }

    private void OnEnable()
    {
        Steuerung.Enable();
        movementscript.currentstate = null;
        basic = false;
        root = false;
        basicattackcd = 1f;
        weaponswitchattack = false;
        prefabarrow.SetActive(false);
        aimscript.enabled = true;
        StartCoroutine(startweaponswitch());
        combochain = 0;
    }
    private void OnDisable()
    {
        CancelInvoke();
        aimscript.enabled = false;
        prefabarrow.SetActive(false);
    }
    private void Start()
    {
        aimscript.enabled = true;
    }
    void Update()
    {
        basicattackcd += Time.deltaTime;
        if (LoadCharmanager.disableattackbuttons == false)
        {
            if (movementscript.amBoden == true)
            {
                if (Steuerung.Player.Attack1.WasPressedThisFrame() && basicattackcd > 0.7f && aimscript.Charotation == false && Statics.otheraction == false)
                {
                    movementscript.state = Movescript.State.BowGroundattack;
                    Statics.otheraction = true;
                    movementscript.ChangeAnimationState(attack1state);
                    basic = false;
                    attackend = false;
                    resetbowani();
                }
                if (Steuerung.Player.Attack1.WasPressedThisFrame() && basicchain == true && combochain <= 1)               //Groundchain Ground3 into Ground1
                {
                    input = false;
                    basicchain = false;
                }
                if (basic == true && Steuerung.Player.Attack2.WasPressedThisFrame())
                {
                    input = false;
                    basic = false;
                }

                if (attackend == true && Steuerung.Player.Attack1.WasPerformedThisFrame())
                {
                    input = false;
                    attackend = false;
                    combochain++;
                    down = true;
                    mid = false;
                    up = false;
                }
                if (attackend == true && Steuerung.Player.Attack2.WasPerformedThisFrame())
                {
                    input = false;
                    attackend = false;
                    combochain++;
                    down = false;
                    mid = true;
                    up = false;
                }
                if (attackend == true && Steuerung.Player.Attack3.WasPerformedThisFrame())
                {
                    input = false;
                    attackend = false;
                    combochain++;
                    down = false;
                    mid = false;
                    up = true;
                }
                if (Steuerung.Player.Attack4.WasPressedThisFrame() && Statics.otheraction == false && Statics.infight == false)
                {
                    movementscript.activaterig = false;
                    movementscript.fullcharge = false;
                    Statics.otheraction = true;
                    movementscript.state = Movescript.State.Bowcharge;
                    transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
                    aimscript.virtualcam = true;
                    movementscript.ChangeAnimationState(chargestate);
                }
            }
            if (Steuerung.Player.Attack4.WasPressedThisFrame() && Statics.otheraction == false && Statics.infight == true)           // bowhookshot
            {
                if (Movescript.lockontarget != null)
                {
                    if (Vector3.Distance(transform.position, Movescript.lockontarget.position) > 5f)
                    {
                        movementscript.gravitation = 0f;
                        movementscript.graviti = 0f;
                        Statics.otheraction = true;
                        movementscript.ChangeAnimationState(starthookstate);
                    }
                }
            }

            if (Steuerung.Player.Attack1.WasPressedThisFrame() && air3downintobasic == true && combochain <= 1)          //Air3down into ground
            {
                movementscript.state = Movescript.State.Airattack;
                air3downintobasic = false;
                input = false;
            }

            if (movementscript.inderluft == true)
            {
                if (weaponswitchattack == true && Steuerung.Player.Attack1.WasPerformedThisFrame())                //attack bei weaponswitch
                {
                    slowattack2();
                    weaponswitchattack = false;
                }

                if (movementscript.attack3intoair == true && Steuerung.Player.Attack1.WasPressedThisFrame())                 //Attackchain von Atttack3down into Air1
                {
                    movementscript.state = Movescript.State.Airattack;
                    movementscript.amBoden = false;
                    movementscript.attack3intoair = false;
                    input = false;
                }

                if (Steuerung.Player.Attack1.WasPressedThisFrame() && movementscript.airattackminheight == true && movementscript.attackonceair == true && Statics.otheraction == false && Statics.infight == true)
                {
                    Statics.otheraction = true;
                    bowairfirstattack();
                }

                if (basicairattack == true && Steuerung.Player.Attack1.WasPerformedThisFrame())                             //Attackchain von Air3 into Air1
                {
                    CancelInvoke();
                    basicairattack = false;
                    air3state = false;
                    animator.SetBool("airhold", false);
                    animator.SetBool("airrelease", true);
                    shotairbasicarrow();
                    checkairchainstate = true;
                }
                if (secondairattack == true && Steuerung.Player.Attack2.WasPerformedThisFrame())
                {
                    CancelInvoke();
                    animator.SetBool("airhold", false);
                    animator.SetBool("airrelease", true);
                    shotairbasicarrow();
                    secondairattack = false;
                    checkairchainstate = false;
                    air3state = true;
                }
                if (thirdairattack == true && Steuerung.Player.Attack1.WasPerformedThisFrame())
                {
                    thirdairattack = false;
                    CancelInvoke();                                                 // wegen airattackchainfail
                    animator.SetBool("airhold", false);
                    animator.SetBool("air3down", true);
                    combochain++;
                    startbowair3intoground();
                }
                if (thirdairattack == true && Steuerung.Player.Attack2.WasPerformedThisFrame())
                {
                    thirdairattack = false;
                    CancelInvoke();                                                 // wegen airattackchainfail
                    animator.SetBool("airhold", false);
                    animator.SetBool("air3mid", true);
                    combochain++;
                    shotairmidarrow();
                }
                if (thirdairattack == true && Steuerung.Player.Attack3.WasPerformedThisFrame())
                {
                    thirdairattack = false;
                    CancelInvoke();                                               // wegen airattackchainfail
                    animator.SetBool("airhold", false);
                    animator.SetBool("air3up", true);
                    combochain++;
                    shotairuparrow();
                }
            }
            if (Statics.dazestunstart == true)
            {
                Statics.dazestunstart = false;
                Statics.otheraction = true;
                Statics.dash = false;
                resetbowairstates();
                resetbowani();
                healingscript.resethealvalues();
                eleAbilities.resetelementalmovementvalues();
                eleAbilities.icelanceiscanceled();
                root = false;
                CancelInvoke();
                input = false;
                combochain = 0;
                movementscript.Charrig.enabled = false;
                if (Statics.enemyspezialtimescale == false)
                {
                    Time.timeScale = Statics.normalgamespeed;
                    Time.fixedDeltaTime = Statics.normaltimedelta;
                }
                movementscript.bowair3intoground = false;
                prefabarrow.SetActive(false);
                weaponswitchattack = false;
                if (aimscript.virtualcam == true)
                {
                    aimscript.aimend();
                }
            }

            if (Steuerung.Player.Dash.WasPerformedThisFrame() && Statics.dashcdmissingtime > Statics.dashcost && Statics.dash == false)
            {
                movementscript.state = Movescript.State.Beforedash;
                Statics.otheraction = true;
                Statics.dash = true;
                GlobalCD.startdashcd();
                resetbowairstates();
                resetbowani();
                healingscript.resethealvalues();
                eleAbilities.resetelementalmovementvalues();
                eleAbilities.icelanceiscanceled();
                root = true;
                CancelInvoke();
                input = false;
                combochain = 0;
                Time.timeScale = Statics.normalgamespeed;
                Time.fixedDeltaTime = Statics.normaltimedelta;
                movementscript.Charrig.enabled = false;
                movementscript.bowair3intoground = false;
                prefabarrow.SetActive(false);
                weaponswitchattack = false;
                if (aimscript.virtualcam == true)
                {
                    aimscript.aimend();
                }
                movementscript.graviti = 0f;
                movementscript.gravitation = 0f;
                Invoke("dash", 0.05f);
            }
            /*if (Steuerung.Player.Kick.WasPerformedThisFrame() && Statics.kickcdbool == false && Statics.dash == false)
            {
                Movementscript.state = Movescript.State.DashKick;
                Statics.otheraction = true;
                Statics.kick = true;
                GlobalCD.startkickcd();
                resetbowairstates();
                resetbowani();
                prefabarrow.SetActive(false);
                GetComponent<Healingscript>().healanzeige.SetActive(false);
                CancelInvoke();
                root = false;
                input = false;
                combochain = 0;
                Movementscript.Charrig.enabled = false;
                Time.timeScale = Statics.normalgamespeed;
                Time.fixedDeltaTime = Statics.normaltimedelta;
                Movementscript.bowair3intoground = false;
                weaponswitchattack = false;
                if (aimscript.virtualcam == true)
                {
                    aimscript.aimend();
                }
                Movementscript.runter = 0f;
                Movementscript.gravitation = 0f;
                Movementscript.ChangeAnimationStateInstant(kickstate);
            }*/
        }
    }

    private void OnAnimatorMove()
    {
        if (root == true)
        {
            Vector3 velocity = animator.deltaPosition;
            movementscript.charactercontroller.Move(velocity);
        }
    }
    private void dash()
    {
        movementscript.state = Movescript.State.Dash;
        movementscript.ChangeAnimationStateInstant(dashstate);
    }
    private void bowdashend()
    {     
        root = false;
        Statics.otheraction = false;
        GlobalCD.startresetdash();
        movementscript.state = Movescript.State.Actionintoair;
    }
    private void bowkickhitboxcheck()
    {
        Collider[] kickcol = Physics.OverlapSphere(transform.position, 0f, LayerMask.GetMask("Meleehitbox"));                                               // colliderpos, größe(muss nur im programm angegeben werden wenn keine hitbox erstellt worden ist, rotation, layer
        foreach (Collider kickhit in kickcol)
        {
            if (kickhit.gameObject.GetComponent<Checkforhitbox>())
            {
                kickhit.gameObject.GetComponentInParent<Enemymovement>().hardattackinterrupt();
            }
        }
    }
    private void bowbasictrue()
    {
        input = true;
        basic = true;
        animator.SetBool("attack1", false);
    }
    private void bowbasicend()
    {
        basic = false;
        if (input == true)
        {
            input = false;
            movementscript.state = Movescript.State.Ground;
            Statics.otheraction = false;
            combochain = 0;
            basicattackcd = 0.5f;
        }
        else
        {
            animator.SetBool("attack2", true);
        }
    }
    private void bowbasic2true()
    {
        input = true;
        attackend = true;
        animator.SetBool("attack2", false);
    }
    private void bowbasic2end()
    {
        attackend = false;
        if (input == true)
        {
            input = false;
            movementscript.state = Movescript.State.Ground;
            Statics.otheraction = false;
            combochain = 0;
            basicattackcd = 0.5f;
        }
        if (down)
        {
            animator.SetBool("attackdown", true);
        }
        if (mid)
        {
            animator.SetBool("attackmid", true);
        }
        if (up)
        {
            animator.SetBool("attackup", true);
        }
    }
    private void hookarrow()
    {
        shothookarrow();
    }
    private void bowchain()
    {
        input = true;
        basicchain = true;
        animator.SetBool("attackdown", false);
        animator.SetBool("attackmid", false);
    }
    private void bowdownend()
    {
        basicchain = false;
        if (input == true)
        {
            input = false;
            movementscript.state = Movescript.State.Ground;
            Statics.otheraction = false;
            combochain = 0;
            basicattackcd = 0.5f;
        }
        else
        {
            animator.SetBool("attack1", true);
        }
    }
    private void bowmidend()
    {
        basicchain = false;
        if (input == true)
        {
            input = false;
            movementscript.state = Movescript.State.Ground;
            Statics.otheraction = false;
            combochain = 0;
            basicattackcd = 0.5f;
        }
        else
        {
            animator.SetBool("attack1", true);
        }
    }
    private void bowuproot()
    {
        movementscript.attackonceair = false;
        root = true;
        movementscript.state = Movescript.State.BowAirattack;
        movementscript.graviti = 0f;
        movementscript.gravitation = 0f;
        movementscript.amBoden = false;
        movementscript.inderluft = true;
    }
    private void bowuptrue()
    {
        input = true;
        movementscript.attack3intoair = true;
        animator.SetBool("attackup", false);
    }
    private void bowupend()
    {
        basicchain = false;
        movementscript.attack3intoair = false;
        root = false;
        if (input == true)
        {
            input = false;
            movementscript.state = Movescript.State.Actionintoair;
            Statics.otheraction = false;
            combochain = 0;
        }
        else
        {
            bowairfirstattack();
        }
    }
    private void bowairfirstattack()
    {
        movementscript.state = Movescript.State.BowAirattack;
        movementscript.attackonceair = false;        // wegen attack spam
        movementscript.gravitation = 0f;
        movementscript.graviti = 0f;
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        aimscript.virtualcam = true;
        resetbowairstates();
        movementscript.ChangeAnimationState(basicair1state);
    }
    private void bowairchainfirst()
    {
        animator.SetBool("air3mid", false);
        animator.SetBool("air3up", false);
        if (combochain <= 1)
        {
            animator.SetBool("aircharge", true);
            checkairchainstate = false;
            air3state = false;
            resetbowairstates();
        }
        else
        {
            airattackchainfail();
        }
    }
    /*private void bowair1()
    {
        basicairattack = false;
        checkairchainstate = false;
        air3state = false;
        animator.SetBool("aircharge", false);
        Movementscript.moveattackcheck = true;
        animator.SetBool("fallafterattack", false);
    }*/
    private void bowairhold()
    {
        animator.SetBool("airhold", true);
        animator.SetBool("aircharge", false);
    }
    private void bowairinput()
    {
        if (checkairchainstate == false && air3state == false)                            // BowAirhold animationSpeed ist auf 0.2, weil sonst die Animation geloopt wird und der Invoke mehrmals called
        {
            basicairattack = true;
        }
        if (checkairchainstate == true)
        {
            secondairattack = true;
        }
        if (air3state == true)
        {
            thirdairattack = true;
            air3state = false;
        }
        Invoke("airattackchainfail", 0.7f);
    }
    private void cancelbowairinput()
    {

    }
    private void startbowair3intoground()
    {
        aimscript.virtualcam = false;
        aimscript.aimend();
        prefabarrow.SetActive(false);
        if(Movescript.lockontarget == null)
        {
            Movescript.lockoncheck = true;
            movementscript.cancellockon = true;
            movementscript.bowair3intoground = true;                            // für autolockon
        }
    }
    private void bowair3downroot()
    {
        root = true;
        movementscript.bowair3intoground = false;
    }
    private void bowairintogroundtrue()
    {
        animator.SetBool("air3down", false);
        air3downintobasic = true;
        input = true;
    }
    private void bowair3togroundend()
    {
        root = false;
        air3downintobasic = false;
        movementscript.gravitation = 4f;
        movementscript.graviti = -0.5f;

        Ray ray = new Ray(this.transform.position + Vector3.up * 0.3f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.4f) && input == false)
        {
            movementscript.charactercontroller.stepOffset = 0.2f;
            movementscript.attackonceair = true;
            movementscript.amBoden = true;
            movementscript.inderluft = false;
            movementscript.airattackminheight = false;
            movementscript.currentstate = null;                                        //airchain, Aircharge muss resetet werden
            animator.SetBool("attack1", true);
            movementscript.state = Movescript.State.BowGroundattack;
        }
        else
        {
            input = true;
        }
        if (input == true)
        {
            input = false;
            Statics.otheraction = false;           
            basicattackcd = 0.5f;
            combochain = 0;
            movementscript.state = Movescript.State.Actionintoair;
        }
    }
    private void airattackchainfail()
    {
        aimscript.virtualcam = false;
        aimscript.aimend();
        animator.SetBool("air3mid", false);
        animator.SetBool("air3up", false);
        animator.SetBool("airhold", false);
        prefabarrow.SetActive(false);
        basicairattack = false;
        secondairattack = false;
        thirdairattack = false;
        air3state = false;
        movementscript.state = Movescript.State.Actionintoair;
        combochain = 0;
        Statics.otheraction = false;
    }
    private void bowcharge()
    {
        animator.SetBool("airrelease", false);
        animator.SetBool("aircharge", true);
    }
    IEnumerator startweaponswitch()
    {
        yield return null;
        bowweaponswitch();
    }
    private void bowweaponswitch()
    {
        checkforweaponswitch = Physics.CheckSphere(transform.position, checkforenemyonswitch, weaponswitchlayer);
        if (checkforweaponswitch == true && Statics.otheraction == false)
        {
            Statics.otheraction = true;
            movementscript.attackonceair = false;
            movementscript.state = Movescript.State.Bowweaponswitch;
            movementscript.gravitation = 0;
            movementscript.charactercontroller.stepOffset = 0;
            movementscript.amBoden = false;
            movementscript.inderluft = true;
            root = true;
            movementscript.ChangeAnimationState(animationbowswitch);
            resetbowairstates();
            resetbowani();
        }
        else
        {
            movementscript.state = Movescript.State.Airintoground;
            resetbowairstates();
            resetbowani();
        }
    }
    private void bowswitchslowmotion()
    {
        aimscript.virtualcam = true;
        root = false;
        movementscript.ChangeAnimationState(slowmochargeup);
        Time.timeScale = slowmotionpercentage;
        Time.fixedDeltaTime = Statics.normaltimedelta * slowmotionpercentage;
        movementscript.graviti = 0f;
        movementscript.gravitation = slowmogravition;
        Invoke("bowweaponswitchattackend", 1.5f);
    }
    private void slowattack2()
    {
        shotairbasicarrow();
        prefabarrow.SetActive(false);
        movementscript.ChangeAnimationState(slowmoshoot);
    }
    private void slowmofastercharge()
    {
        movementscript.ChangeAnimationState(slowmofastcharge);
    }
    private void slowmoattacktrue()
    {

        weaponswitchattack = true;
    }
    private void bowweaponswitchattackend()
    {
        Statics.otheraction = false;
        aimscript.virtualcam = false;
        aimscript.aimend();
        prefabarrow.SetActive(false);
        movementscript.attackonceair = false;
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        weaponswitchattack = false;
        movementscript.state = Movescript.State.Actionintoair;
        movementscript.ChangeAnimationStateInstant(fallstate);
    }
    private void hookshotstart()
    {
        movementscript.state = Movescript.State.Bowhookshot;
    }
    private void pullarrow()
    {
        prefabarrow.SetActive(true);
    }
    private void arrowfalse()
    {
        prefabarrow.SetActive(false);
    }
    private void shotbasicgroundarrow()
    {
        if (Movescript.lockoncheck == true)
        {
            Vector3 arrowrotation = (Movescript.lockontarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(basicgroundarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Singlegroundarrow arrowcontroller = Arrow.GetComponent<Singlegroundarrow>();
            arrowcontroller.Arrowtarget = Movescript.lockontarget.transform;
        }
    }
    private void shotgrounddown()
    {
        if (Movescript.lockoncheck == true)
        {
            Vector3 arrowrotation = (Movescript.lockontarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(grounddownarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Grounddownarrow arrowcontroller = Arrow.GetComponent<Grounddownarrow>();
            //arrowcontroller.arrowziel = Movescript.lockontarget.transform.position;
            arrowcontroller.Arrowtarget = Movescript.lockontarget.transform;
        }
    }
    private void shotgroundmid()
    {
        if (Movescript.lockoncheck == true)
        {
            Vector3 arrowrotation = (Movescript.lockontarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(groundmidarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Groundmidarrow arrowcontroller = Arrow.GetComponent<Groundmidarrow>();
            arrowcontroller.Arrowtarget = Movescript.lockontarget.transform;
        }
    }
    private void shotgroundup()
    {
        if (Movescript.lockoncheck == true)
        {
            Vector3 arrowrotation = (Movescript.lockontarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(grounduparrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Grounduparrow arrowcontroller = Arrow.GetComponent<Grounduparrow>();
            arrowcontroller.Arrowtarget = Movescript.lockontarget.transform;
        }
    }
    private void shotairbasicarrow()
    {
        RaycastHit hit;
        if (Physics.Raycast(Kamerarichtung.position, Kamerarichtung.forward, out hit, 500f, Arrowraycastlayer, QueryTriggerInteraction.Ignore))
        {
            Vector3 arrowrotation = (hit.point - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(basicairarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            SingleAirarrow arrowcontroller = Arrow.GetComponent<SingleAirarrow>();
            arrowcontroller.arrowziel = hit.point;
            arrowcontroller.Arrowtarget = hit.transform;
            arrowcontroller.hit = true;
        }
        else
        {
            Vector3 arrowrotation = (Kamerarichtung.forward).normalized;
            GameObject Arrow = GameObject.Instantiate(basicairarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            SingleAirarrow arrowcontroller = Arrow.GetComponent<SingleAirarrow>();
            arrowcontroller.arrowziel = Kamerarichtung.position + Kamerarichtung.forward * maxtravelrangeifnothit;
            arrowcontroller.hit = false;
        }
    }
    private void shotairdown()
    {
        if (Movescript.lockoncheck == true)
        {
            Vector3 arrowrotation = (Movescript.lockontarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(airdownarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Airdownarrow arrowcontroller = Arrow.GetComponent<Airdownarrow>();
            arrowcontroller.Arrowtarget = Movescript.lockontarget.transform;
        }
    }
    private void shotairmidarrow()
    {
        RaycastHit hit;
        if (Physics.Raycast(Kamerarichtung.position, Kamerarichtung.forward, out hit, 500f, Arrowraycastlayer, QueryTriggerInteraction.Ignore))
        {
            Vector3 arrowrotation = (hit.point - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(airmidarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Airmidarrow arrowcontroller = Arrow.GetComponent<Airmidarrow>();
            arrowcontroller.arrowziel = hit.point;
            arrowcontroller.Arrowtarget = hit.transform;
            arrowcontroller.hit = true;
        }
        else
        {
            Vector3 arrowrotation = (Kamerarichtung.forward).normalized;
            GameObject Arrow = GameObject.Instantiate(airmidarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Airmidarrow arrowcontroller = Arrow.GetComponent<Airmidarrow>();
            arrowcontroller.arrowziel = Kamerarichtung.position + Kamerarichtung.forward * maxtravelrangeifnothit;
            arrowcontroller.hit = false;
        }
    }
    private void shotairuparrow()
    {
        RaycastHit hit;
        if (Physics.Raycast(Kamerarichtung.position, Kamerarichtung.forward, out hit, 500f, Arrowraycastlayer, QueryTriggerInteraction.Ignore))
        {
            Vector3 arrowrotation = (hit.point - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(airuparrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Airuparrow arrowcontroller = Arrow.GetComponent<Airuparrow>();
            arrowcontroller.arrowziel = hit.point;
            arrowcontroller.Arrowtarget = hit.transform;
            arrowcontroller.hit = true;
        }
        else
        {
            Vector3 arrowrotation = (Kamerarichtung.forward).normalized;
            GameObject Arrow = GameObject.Instantiate(airuparrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Airuparrow arrowcontroller = Arrow.GetComponent<Airuparrow>();
            arrowcontroller.arrowziel = Kamerarichtung.position + Kamerarichtung.forward * maxtravelrangeifnothit;
            arrowcontroller.hit = false;
        }
    }
    private void shothookarrow()
    {
        if (Movescript.lockoncheck == true)
        {
            Vector3 arrowrotation = (Movescript.lockontarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(hookshotarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Hookshotarrow arrowcontroller = Arrow.GetComponent<Hookshotarrow>();
            arrowcontroller.Arrowtarget = Movescript.lockontarget.transform;
        }
    }
    private void shotpuzzlearrow()
    {
        RaycastHit hit;
        if (Physics.Raycast(Kamerarichtung.position, Kamerarichtung.forward, out hit, 500f, Arrowraycastlayer, QueryTriggerInteraction.Ignore))
        {
            Vector3 arrowrotation = (hit.point - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(puzzlearrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Puzzlearrow arrowcontroller = Arrow.GetComponent<Puzzlearrow>();
            arrowcontroller.arrowziel = hit.point;
            //arrowcontroller.Arrowtarget = hit.transform;
            arrowcontroller.hit = true;
        }
    }
    private void resetbowairstates()
    {
        basicairattack = false;
        secondairattack = false;
        thirdairattack = false;
        checkairchainstate = false;
        air3state = false;
        movementscript.bowair3intoground = false;
        movementscript.attack3intoair = false;
    }
    private void resetbowani()
    {
        animator.SetBool("airhold", false);
        animator.SetBool("aircharge", false);
        animator.SetBool("airrelease", false);
        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
        animator.SetBool("attackdown", false);
        animator.SetBool("attackmid", false);
        animator.SetBool("attackup", false);
        animator.SetBool("air3down", false);
        animator.SetBool("air3mid", false);
        animator.SetBool("air3up", false);
    }
}

//Ray ray = new Ray(transform.position + Vector3.up, transform.forward);                          //Gizmo aktivieren
//Debug.DrawRay(ray.origin, ray.direction * 3f, Color.green);