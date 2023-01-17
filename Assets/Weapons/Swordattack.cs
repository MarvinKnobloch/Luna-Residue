using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordattack : MonoBehaviour
{
    private SpielerSteu controlls;
    private Animator animator;
    private Movescript movementscript;
    private SwordController swordcontroller;
    private Healingscript healingscript;
    private EleAbilities eleAbilities;

    private bool root;
    private float basicattackcd;
    public int combochain;

    private bool readattackinput;
    private bool down;
    private bool mid;
    private bool up;

    //animations
    const string groundbasic1state = "Basic";
    const string groundbasic2state = "Swordbasic2";
    const string grounddownstate = "Sworddownend";
    const string groundmidstate = "Swordmidend";
    const string groundupstate = "Swordupend";
    const string airbasic1state = "Air1";
    const string airbasic2state = "Air2";
    const string airdownstate = "SWair3down";
    const string airmidstate = "SWair3mid";
    const string airupstate = "SWair3up";
    const string swordswitchstate = "Swordweaponswitch";
    const string dashstate = "Sworddash";

    //weaponswitch
    private float checkforenemyonswitchrange = 3f;
    public LayerMask weaponswitchlayer;

    void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        movementscript = GetComponent<Movescript>();
        animator = GetComponent<Animator>();
        swordcontroller = GetComponent<SwordController>();
        healingscript = GetComponent<Healingscript>();
        eleAbilities = GetComponent<EleAbilities>();
    }
    private void OnEnable()
    {
        controlls.Enable();
        root = false;
        movementscript.currentstate = null;
        basicattackcd = 1f;
        swordcontroller.enabled = true;
        combochain = 0;
        readattackinput = false;
        attackestate = Attackstate.weaponswitch;
        StartCoroutine(startweaponswitch());
    }

    private Attackstate attackestate;
    enum Attackstate
    {
        waitforattack,
        attack1,
        attack2,
        attackchain,
        groundintoair,                                // eine zusätzliche chain bei ground into air
        weaponswitch,
    }
    void Update()
    {
        if (LoadCharmanager.disableattackbuttons == false)
        {
            switch (attackestate)
            {
                case Attackstate.waitforattack:
                    waitforattackinput();
                    break;
                case Attackstate.attack1:
                    attack1input();
                    break;
                case Attackstate.attack2:
                    attack2input();
                    break;
                case Attackstate.attackchain:
                    airintogroundinput();
                    break;
                case Attackstate.groundintoair:
                    groundintoairinput();
                    break;
                case Attackstate.weaponswitch:
                    break;
                default:
                    break;
            }
            if (Statics.dazestunstart == true)                                //reset alles values bei stun
            {
                Statics.dazestunstart = false;
                Statics.playeriframes = false;
                resetvalues();
                movementscript.Charrig.enabled = false;
                if (Statics.enemyspezialtimescale == false)
                {
                    Time.timeScale = Statics.normalgamespeed;
                    Time.fixedDeltaTime = Statics.normaltimedelta;
                }
            }
            if (controlls.Player.Dash.WasPerformedThisFrame() && Statics.dashcdmissingtime > Statics.dashcost && Statics.dash == false)
            {
                movementscript.state = Movescript.State.Beforedash;                  //damit man beim angreifen noch die Richtung bestimmen kann
                Statics.dash = true;
                Statics.playeriframes = true;
                resetvalues();
                GlobalCD.startdashcd();
                movementscript.graviti = 0;
                Invoke("dash", 0.05f);                                             //damit man beim angreifen noch die Richtung bestimmen kann
            }
        }
    }

    private void waitforattackinput()
    {
        basicattackcd += Time.deltaTime;
        if (movementscript.state == Movescript.State.Ground)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame() && basicattackcd > 0.5f && Statics.otheraction == false)
            {
                movementscript.state = Movescript.State.Groundattack;
                attackestate = Attackstate.attack1;
                Statics.otheraction = true;
                movementscript.ChangeAnimationState(groundbasic1state);
                readattackinput = false;
                combochain = 0;
            }
        }
        else if (movementscript.state == Movescript.State.Air)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame() && movementscript.airattackminheight == true && movementscript.attackonceair == true && Statics.otheraction == false)// && Statics.infight == true)
            {
                movementscript.state = Movescript.State.Airattack;
                attackestate = Attackstate.attack1;
                movementscript.graviti = 0f;
                Statics.otheraction = true;
                movementscript.attackonceair = false;
                combochain = 0;
                readattackinput = false;
                movementscript.ChangeAnimationState(airbasic1state);
            }
        }
    }
    private void attack1input()
    {
        if(readattackinput == true)
        {
            if (controlls.Player.Attack2.WasPressedThisFrame())
            {
                readattackinput = false;
            }
        }
    }
    private void attack2input()
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                combochain++;
                down = true;
                readattackinput = false;
            }
            else if (controlls.Player.Attack2.WasPressedThisFrame())
            {
                combochain++;
                mid = true;
                readattackinput = false;
            }
            else if (controlls.Player.Attack3.WasPressedThisFrame())
            {
                combochain++;
                up = true;
                readattackinput = false;
            }
        }
    }
    private void groundintoairinput()
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                readattackinput = false;
            }
        }
    }
    private void airintogroundinput()
    {
        if (readattackinput == true && combochain < 2)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                readattackinput = false;
            }
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

    private void resetvalues()
    {
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = true;
        root = true;
        CancelInvoke();
        healingscript.resethealvalues();
        eleAbilities.resetelementalmovementvalues();
        eleAbilities.icelanceiscanceled();
    }
    private void dash()
    {
        movementscript.state = Movescript.State.Empty;
        movementscript.ChangeAnimationStateInstant(dashstate);
    }
    private void sworddashend()
    {
        root = false;
        Statics.playeriframes = false;
        Statics.otheraction = false;
        GlobalCD.startresetdash();
        movementscript.switchtoairstate();
    }
    private void setinputtotrue()
    {
        readattackinput = true;
    }
    private void groundattackchainend()
    {
        readattackinput = false;
        movementscript.switchtogroundstate();
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = false;
        combochain = 0;
        basicattackcd = 0;
    }
    private void swordgroundbasicend()
    {
        if (readattackinput == true) groundattackchainend();
        else
        {
            down = false;
            mid = false;
            up = false;
            attackestate = Attackstate.attack2;
            movementscript.ChangeAnimationState(groundbasic2state);
        }
    }
    private void swordgroundbasci2end()
    {
        if (readattackinput == true) groundattackchainend();
        else
        {
            if (down == true)
            {
                attackestate = Attackstate.attackchain;
                if (down) movementscript.ChangeAnimationState(grounddownstate);
            }
            else if (mid == true)
            {
                attackestate = Attackstate.attackchain;
                movementscript.ChangeAnimationState(groundmidstate);
            }
            else if (up == true)
            {
                attackestate = Attackstate.groundintoair;
                movementscript.ChangeAnimationState(groundupstate);
            }
        }
    }
    private void swordstaygroundend()
    {
        if (readattackinput == true) groundattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
    }
    private void swordgrounduproot()
    {
        movementscript.attackonceair = false;
        root = true;
        movementscript.state = Movescript.State.Airattack;
        movementscript.graviti = 0f;
    }
    private void swordgroundupend()
    {
        root = false;
        if (readattackinput == true) airattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(airbasic1state);
        }
    }

    private void airattackchainend()
    {
        readattackinput = false;
        movementscript.switchtoairstate();
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = false;
        combochain = 0;
        basicattackcd = 0;
    }
    private void swordbasicairend()
    {
        if (readattackinput == true) airattackchainend();
        else
        {
            down = false;
            mid = false;
            up = false;
            attackestate = Attackstate.attack2;
            movementscript.ChangeAnimationState(airbasic2state);
        }
    }
    private void swordairbasic2end()
    {
        if (readattackinput == true) airattackchainend();
        else
        {
            attackestate = Attackstate.attackchain;
            if (down) movementscript.ChangeAnimationState(airdownstate);
            else if (mid) movementscript.ChangeAnimationState(airmidstate);
            else if (up) movementscript.ChangeAnimationState(airupstate);
        }
    }
    private void swordairdownroot()
    {
        root = true;
    }
    private void swordairdownend()
    {
        root = false;
        if (readattackinput == true) groundattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.graviti = -0.5f;
            movementscript.state = Movescript.State.Groundattack;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
    }
    private void swordstayairend()
    {
        if (readattackinput == true) airattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(airbasic1state);
        }
    }
    IEnumerator startweaponswitch()
    {
        yield return null;
        swordweaponswitch();
    }
    private void swordweaponswitch()
    {
        if (Physics.CheckSphere(transform.position, checkforenemyonswitchrange, weaponswitchlayer))
        {
            Statics.otheraction = true;
            movementscript.graviti = 0;
            movementscript.state = Movescript.State.Airattack;
            movementscript.ChangeAnimationState(swordswitchstate);
        }
        else
        {
            attackestate = Attackstate.waitforattack;
        }
    }
    private void swordweaponswitchend()
    {
        attackestate = Attackstate.waitforattack;
        movementscript.switchtogroundstate();
        Statics.otheraction = false;
    }
}
