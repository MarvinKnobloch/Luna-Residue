using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fistattack : MonoBehaviour
{
    private Movescript movementscript;
    private Fistcontroller fistcontroller;
    private Healingscript healingscript;
    private EleAbilities eleAbilities;

    private SpielerSteu Steuerung;
    private Animator animator;
    private bool root;
    private bool basic;
    private bool basic2;
    private bool attackend;
    private bool basicairattack;
    private bool stayairattack;


    //animationstate
    const string attack1state = "Basic";
    const string basicair1state = "Air1";
    const string dashstate = "Fistdash";
    const string kickstate = "Kick";
    const string fistswitchstate = "Fistweaponswitch";

    public bool input;
    private bool down;
    private bool mid;
    private bool up;

    //Cooldowns
    private float basicattackcd;
    public int combochain;
    private bool basicchain;
    private bool air3downintobasic;
    private bool basicairchain;

    //weaponswitch
    private bool checkforweaponswitch;
    private float checkforenemyonswitchrange = 3f;
    public LayerMask weaponswitchlayer;

    void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
        animator = GetComponent<Animator>();
        movementscript = GetComponent<Movescript>();
        fistcontroller = GetComponent<Fistcontroller>();
        healingscript = GetComponent<Healingscript>();
        eleAbilities = GetComponent<EleAbilities>();
    }

    private void OnEnable()
    {
        Steuerung.Enable();
        basic = false;
        root = false;
        movementscript.currentstate = null;
        basicattackcd = 1f;
        fistcontroller.enabled = true;
        combochain = 0;
        input = false;
        StartCoroutine(startfistweaponswitch());
    }
    private void OnDisable()
    {
        CancelInvoke();
        fistcontroller.enabled = false;
    }
    void Update()
    {
        basicattackcd += Time.deltaTime;

        if (LoadCharmanager.disableattackbuttons == false)
        {
            if (movementscript.amBoden == true)
            {
                if (Steuerung.Player.Attack1.WasPressedThisFrame() && basicattackcd > 1f && Statics.otheraction == false)
                {
                    movementscript.state = Movescript.State.Groundattack;
                    Statics.otheraction = true;
                    movementscript.ChangeAnimationState(attack1state);
                    basic = false;
                    attackend = false;
                    resetfaustani();
                }
                if (Steuerung.Player.Attack1.WasPressedThisFrame() && basicchain == true && combochain <= 1)
                {
                    input = false;
                    basicchain = false;
                }

                if (basic == true && Steuerung.Player.Attack2.WasPressedThisFrame())
                {
                    input = false;
                    basic = false;
                }
                if (basic2 == true && Steuerung.Player.Attack2.WasPressedThisFrame())
                {
                    input = false;
                    basic2 = false;
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
            }

            if (movementscript.inderluft == true)
            {
                if (movementscript.attack3intoair == true && Steuerung.Player.Attack1.WasPressedThisFrame())
                {
                    movementscript.state = Movescript.State.Airattack;
                    movementscript.amBoden = false;
                    movementscript.attack3intoair = false;
                    input = false;
                }
                if (Steuerung.Player.Attack1.WasPressedThisFrame() && air3downintobasic == true && combochain <= 1)
                {
                    air3downintobasic = false;
                    input = false;
                }

                if (Steuerung.Player.Attack1.WasPressedThisFrame() && movementscript.airattackminheight == true && movementscript.attackonceair == true && Statics.otheraction == false && Statics.infight == true)
                {
                    movementscript.state = Movescript.State.Airattack;
                    movementscript.graviti = 0f;
                    movementscript.gravitation = 0f;
                    Statics.otheraction = true;
                    movementscript.attackonceair = false;
                    basicairattack = false;
                    stayairattack = false;
                    input = false;
                    movementscript.ChangeAnimationState(basicair1state);
                    resetfaustani();
                }
                if (Steuerung.Player.Attack1.WasPressedThisFrame() && movementscript.airattackminheight == true && basicairchain == true && combochain <= 1)
                {
                    basicairchain = false;
                    input = false;
                }

                if (movementscript.amBoden == false)
                {
                    if (basicairattack == true && Steuerung.Player.Attack2.WasPressedThisFrame())
                    {
                        movementscript.airattackminheight = true;
                        basicairattack = false;
                        input = false;
                    }
                    if (stayairattack == true && Steuerung.Player.Attack1.WasPressedThisFrame())
                    {
                        stayairattack = false;
                        combochain++;
                        input = false;
                        down = true;
                        mid = false;
                        up = false;
                    }
                    if (stayairattack == true && Steuerung.Player.Attack2.WasPressedThisFrame())
                    {
                        stayairattack = false;
                        combochain++;
                        input = false;
                        down = false;
                        mid = true;
                        up = false;
                    }
                    if (stayairattack == true && Steuerung.Player.Attack3.WasPressedThisFrame())
                    {
                        stayairattack = false;
                        combochain++;
                        input = false;
                        down = false;
                        mid = false;
                        up = true;
                    }
                }
            }
            if (Statics.dazestunstart == true)
            {
                Statics.dazestunstart = false;
                Statics.otheraction = true;
                Statics.dash = false;
                root = false;
                CancelInvoke();
                resetfaustairstates();
                resetfaustani();
                healingscript.resethealvalues();
                eleAbilities.resetelementalmovementvalues();
                eleAbilities.icelanceiscanceled();
                input = false;
                combochain = 0;
                movementscript.Charrig.enabled = false;
                if(Statics.enemyspezialtimescale == false)
                {
                    Time.timeScale = Statics.normalgamespeed;
                    Time.fixedDeltaTime = Statics.normaltimedelta;
                }
                movementscript.bowair3intoground = false;
            }

            if (Steuerung.Player.Dash.WasPerformedThisFrame() && Statics.dashcdmissingtime > Statics.dashcost && Statics.dash == false)
            {
                movementscript.state = Movescript.State.Beforedash;
                Statics.otheraction = true;
                Statics.dash = true;
                GlobalCD.startdashcd();
                root = true;
                CancelInvoke();
                resetfaustairstates();
                resetfaustani();
                healingscript.resethealvalues();
                eleAbilities.resetelementalmovementvalues();
                eleAbilities.icelanceiscanceled();
                input = false;
                combochain = 0;
                movementscript.graviti = 0;
                movementscript.gravitation = 0f;
                Invoke("dash", 0.05f);
            }
            /*if (Steuerung.Player.Kick.WasPerformedThisFrame() && Statics.kickcdbool == false && Statics.dash == false)
            {
                Movementscript.state = Movescript.State.DashKick;
                Statics.otheraction = true;
                Statics.kick = true;
                GlobalCD.startkickcd();
                GetComponent<Healingscript>().healanzeige.SetActive(false);
                resetfaustairstates();
                resetfaustani();
                input = false;
                combochain = 0;
                Movementscript.runter = 0;
                Movementscript.gravitation = 0f;
                Movementscript.ChangeAnimationStateInstant(kickstate);
                root = false;
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
        movementscript.state = Movescript.State.DashKick;
        movementscript.ChangeAnimationStateInstant(dashstate);
    }
    private void fistdashend()
    {
        root = false;
        Statics.otheraction = false;
        GlobalCD.startresetdash();
        movementscript.state = Movescript.State.Actionintoair;
    }
    private void fistkickhitboxcheck()
    {
        Collider[] kickcol = Physics.OverlapSphere(transform.position, 4f, LayerMask.GetMask("Meleehitbox"));                                               // colliderpos, größe(muss nur im programm angegeben werden wenn keine hitbox erstellt worden ist, rotation, layer
        foreach (Collider kickhit in kickcol)
        {
            if (kickhit.gameObject.GetComponent<Checkforhitbox>())
            {
                kickhit.gameObject.GetComponentInParent<Enemymovement>().hardattackinterrupt();
            }
        }
    }
    private void faustbasictrue()
    {
        input = true;
        basic = true;
        animator.SetBool("attack1", false);
    }
    private void faustbasicend()
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
    private void faustbasic2true()
    {
        input = true;
        basic2 = true;
        animator.SetBool("attack2", false);
    }
    private void faustbasic2end()
    {
        basic2 = false;
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
            animator.SetBool("attack3", true);
        }
    }
    private void faustbasic3true()
    {
        input = true;
        attackend = true;
        animator.SetBool("attack3", false);
    }
    private void faustbasic3end()
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
    private void fistchain()
    {
        input = true;
        basicchain = true;
        animator.SetBool("attackdown", false);
        animator.SetBool("attackmid", false);
    }
    private void fistdownend()
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
    private void fistmidend()
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
    private void faustuproot()
    {
        movementscript.attackonceair = false;
        root = true;
        movementscript.state = Movescript.State.Airattack;
        movementscript.graviti = 0f;
        movementscript.gravitation = 0f;
        movementscript.amBoden = false;
        movementscript.inderluft = true;
    }
    private void faustuptrue()
    {
        input = true;
        movementscript.attack3intoair = true;
        animator.SetBool("attackup", false);
    }
    private void faustupend()
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
            animator.SetBool("air1", true);
            basicairattack = false;
            stayairattack = false;
        }
    }
    private void faustbasicairtrue()
    {
        input = true;
        basicairattack = true;
        animator.SetBool("air1", false);
    }
    private void faustbasicairend()
    {
        basicairattack = false;
        if (input == true)
        {
            input = false;
            movementscript.state = Movescript.State.Actionintoair;
            Statics.otheraction = false;
            combochain = 0;
        }
        else
        {
            animator.SetBool("air2", true);
        }
    }
    private void fauststayair()
    {
        input = true;
        stayairattack = true;
        animator.SetBool("air2", false);
    }
    private void fauststayairend()
    {
        stayairattack = false;
        if (input == true)
        {
            input = false;
            movementscript.state = Movescript.State.Actionintoair;
            Statics.otheraction = false;
            combochain = 0;
        }
        if (down)
        {
            animator.SetBool("air3down", true);
        }
        if (mid)
        {
            animator.SetBool("air3mid", true);
        }
        if (up)
        {
            animator.SetBool("air3up", true);
        }
    }
    private void fistair3downroot()
    {
        root = true;
    }
    private void fistairintoground()
    {
        animator.SetBool("air3down", false);
        air3downintobasic = true;
        input = true;
    }
    private void fistair3togroundend()
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
            animator.SetBool("attack1", true);
            movementscript.state = Movescript.State.Groundattack;
        }
        else
        {
            input = true;
        }
        if (input == true)
        {
            input = false;
            Statics.otheraction = false;
            combochain = 0;
            basicattackcd = 0.5f;
            movementscript.state = Movescript.State.Actionintoair;
        }
    }
    private void fistairchain()
    {
        input = true;
        basicairchain = true;
        animator.SetBool("air3mid", false);
        animator.SetBool("air3up", false);
    }
    private void fistair3midend()
    {
        basicairchain = false;
        if (input == true)
        {
            input = false;
            movementscript.state = Movescript.State.Actionintoair;
            Statics.otheraction = false;
            combochain = 0;
        }
        else
        {
            animator.SetBool("air1", true);
        }
    }
    private void fistair3upend()
    {
        basicairchain = false;
        if (input == true)
        {
            input = false;
            animator.SetBool("air3up", false);
            movementscript.state = Movescript.State.Actionintoair;
            Statics.otheraction = false;
            combochain = 0;
        }
        else
        {
            animator.SetBool("air1", true);
        }
    }
    IEnumerator startfistweaponswitch()
    {
        yield return null;
        fistweaponswitch();
    }
    private void fistweaponswitch()
    {
        checkforweaponswitch = Physics.CheckSphere(transform.position, checkforenemyonswitchrange, weaponswitchlayer);
        if (checkforweaponswitch == true && Statics.otheraction == false)
        {
            Statics.otheraction = true;
            movementscript.state = Movescript.State.Groundattack;
            movementscript.charactercontroller.stepOffset = 0;
            movementscript.ChangeAnimationState(fistswitchstate);
        }
    }
    private void fistweaponswitchend()
    {
        movementscript.state = Movescript.State.Air;
        Statics.otheraction = false;
    }
    private void resetfaustairstates()
    {
        basicairattack = false;
        stayairattack = false;
        air3downintobasic = false;
        basicairchain = false;
        movementscript.attack3intoair = false;
    }
    private void resetfaustani()
    {
        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
        animator.SetBool("attack3", false);
        animator.SetBool("attackdown", false);
        animator.SetBool("attackmid", false);
        animator.SetBool("attackup", false);
        animator.SetBool("air1", false);
        animator.SetBool("air2", false);
        animator.SetBool("air3down", false);
        animator.SetBool("air3mid", false);
        animator.SetBool("air3up", false);
    }
}



