using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fistattack : MonoBehaviour
{
    private SpielerSteu controlls;
    private Animator animator;
    private Movescript movementscript;
    private Fistcontroller fistcontroller;
    private Healingscript healingscript;
    private EleAbilities eleAbilities;

    private bool root;
    private float basicattackcd;

    private bool readattackinput;
    private bool down;
    private bool mid;
    private bool up;

    //animations
    const string groundbasic1state = "Fistbasic";
    const string groundbasic2state = "Fistbasic2";
    const string groundbasic3state = "Fistbasic3";
    const string grounddownstate = "Fistdownend";
    const string groundmidstate = "Fistmidend";
    const string groundupstate = "Fistupend";
    const string airbasic1state = "Fistair1";
    const string airbasic2state = "Fistair2";
    const string airdownstate = "Fistair3down";
    const string airmidstate = "Fistair3mid";
    const string airupstate = "Fistair3up";
    const string swordswitchstate = "Fistweaponswitch";
    const string dashstate = "Fistdash";

    //weaponswitch
    private float checkforenemyonswitchrange = 3f;
    public LayerMask weaponswitchlayer;

    void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        movementscript = GetComponent<Movescript>();
        animator = GetComponent<Animator>();
        fistcontroller = GetComponent<Fistcontroller>();
        healingscript = GetComponent<Healingscript>();
        eleAbilities = GetComponent<EleAbilities>();
    }
    private void OnEnable()
    {
        controlls.Enable();
        root = false;
        movementscript.currentstate = null;
        basicattackcd = 1f;
        fistcontroller.enabled = true;
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
        attack3,
        attackchain,
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
                case Attackstate.attack3:
                    attack3input();
                    break;
                case Attackstate.attackchain:
                    attackchaininput();
                    break;
                case Attackstate.weaponswitch:
                    break;
                default:
                    break;
            }

            if (controlls.Player.Dash.WasPerformedThisFrame() && Statics.dashcdmissingtime > Statics.dashcost && Statics.dash == false)
            {
                movementscript.state = Movescript.State.Beforedash;                  //damit man beim angreifen noch die Richtung bestimmen kann
                Statics.dash = true;
                Statics.playeriframes = true;
                resetvalues();
                root = true;
                GlobalCD.startdashcd();
                movementscript.graviti = 0;
                Invoke("dash", 0.05f);                                             //damit man beim angreifen noch die Richtung bestimmen kann
            }
        }
        if (Statics.resetvaluesondeathorstun == true)                                //reset alles values bei stun
        {
            Statics.resetvaluesondeathorstun = false;
            Statics.playeriframes = false;
            resetvalues();
            root = false;
            movementscript.Charrig.enabled = false;
            if (Statics.enemyspezialtimescale == false)
            {
                Time.timeScale = Statics.normalgamespeed;
                Time.fixedDeltaTime = Statics.normaltimedelta;
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
                movementscript.state = Movescript.State.Meleegroundattack;
                attackestate = Attackstate.attack1;
                Statics.otheraction = true;
                movementscript.ChangeAnimationState(groundbasic1state);
                readattackinput = false;
                movementscript.attackcombochain = 0;
            }
        }
        else if (movementscript.airattackminheight == true)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame() && movementscript.attackonceair == true && Statics.otheraction == false)// && Statics.infight == true)
            {
                eleAbilities.stopignorelayers();
                movementscript.state = Movescript.State.Meleeairattack;
                attackestate = Attackstate.attack1;
                movementscript.graviti = 0f;
                Statics.otheraction = true;
                movementscript.attackonceair = false;
                readattackinput = false;
                movementscript.ChangeAnimationState(airbasic1state);
                movementscript.attackcombochain = 0;
            }
        }
    }
    private void attack1input()
    {
        if (readattackinput == true)
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
            if (controlls.Player.Attack2.WasPressedThisFrame())
            {
                readattackinput = false;
            }
        }
    }
    private void attack3input()
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                movementscript.attackcombochain++;
                down = true;
                readattackinput = false;
            }
            else if (controlls.Player.Attack2.WasPressedThisFrame())
            {
                movementscript.attackcombochain++;
                mid = true;
                readattackinput = false;
            }
            else if (controlls.Player.Attack3.WasPressedThisFrame())
            {
                movementscript.attackcombochain++;
                up = true;
                readattackinput = false;
            }
        }
    }
    private void attackchaininput()
    {
        if (readattackinput == true)
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
        CancelInvoke();
        healingscript.resethealvalues();
        eleAbilities.stopignorelayers();
        eleAbilities.icelanceiscanceled();
    }
    private void dash()
    {
        movementscript.state = Movescript.State.Empty;
        movementscript.ChangeAnimationStateInstant(dashstate);
    }
    private void fistdashend()
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
    private void fistgroundattackchainend()
    {
        readattackinput = false;
        movementscript.switchtogroundstate();
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = false;
        movementscript.attackcombochain = 0;
        basicattackcd = 0;
    }
    private void fistgroundbasicend()
    {
        if (movementscript.state != Movescript.State.Meleegroundattack) return;                   //sonst kommt der call von der animation noch durch obwohl schon die animation gewechselt würde
        if (readattackinput == true) fistgroundattackchainend();
        else
        {
            down = false;
            mid = false;
            up = false;
            attackestate = Attackstate.attack2;
            movementscript.ChangeAnimationState(groundbasic2state);
        }
    }
    private void fistgroundbasic2end()
    {
        if (movementscript.state != Movescript.State.Meleegroundattack) return;
        if (readattackinput == true) fistgroundattackchainend();
        else
        {
            attackestate = Attackstate.attack3;
            movementscript.ChangeAnimationState(groundbasic3state);
        }
    }
    private void fistgroundbasic3end()
    {
        if (movementscript.state != Movescript.State.Meleegroundattack) return;
        if (readattackinput == true) fistgroundattackchainend();
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
                attackestate = Attackstate.attackchain;
                movementscript.ChangeAnimationState(groundupstate);
            }
        }
    }
    private void fiststaygroundend()
    {
        if (movementscript.state != Movescript.State.Meleegroundattack) return;
        if (readattackinput == false && movementscript.attackcombochain < 2)
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
        else fistgroundattackchainend();
    }
    private void fistgrounduproot()
    {
        if (movementscript.state != Movescript.State.Meleegroundattack) return;
        movementscript.attackonceair = false;
        root = true;
        movementscript.state = Movescript.State.Meleeairattack;
        movementscript.graviti = 0f;
    }
    private void fistgroundupend()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        root = false;
        if (readattackinput == true) fistairattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(airbasic1state);
        }
    }

    private void fistairattackchainend()
    {
        readattackinput = false;
        movementscript.switchtoairstate();
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = false;
        movementscript.attackcombochain = 0;
        basicattackcd = 0;
    }
    private void fistbasicairend()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        if (readattackinput == true) fistairattackchainend();
        else
        {
            down = false;
            mid = false;
            up = false;
            attackestate = Attackstate.attack3;
            movementscript.ChangeAnimationState(airbasic2state);
        }
    }
    private void fistairbasic2end()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        if (readattackinput == true) fistairattackchainend();
        else
        {
            attackestate = Attackstate.attackchain;
            if (down) movementscript.ChangeAnimationState(airdownstate);
            else if (mid) movementscript.ChangeAnimationState(airmidstate);
            else if (up) movementscript.ChangeAnimationState(airupstate);
        }
    }
    private void fistairdownroot()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        root = true;
        eleAbilities.ignorelayers();
    }
    private void fistairdownend()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        eleAbilities.stopignorelayers();
        root = false;
        if (readattackinput == false && movementscript.attackcombochain < 2)
        {
            attackestate = Attackstate.attack1;
            movementscript.graviti = -0.5f;
            movementscript.state = Movescript.State.Meleegroundattack;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
        else fistgroundattackchainend();
    }
    private void fiststayairend()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        if (readattackinput == false && movementscript.attackcombochain < 2)
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(airbasic1state);
        }
        else fistairattackchainend();
    }
    IEnumerator startweaponswitch()
    {
        yield return null;
        fistweaponswitch();
    }
    private void fistweaponswitch()
    {
        if (Physics.CheckSphere(transform.position, checkforenemyonswitchrange, weaponswitchlayer))
        {
            Statics.otheraction = true;
            movementscript.graviti = -5;
            movementscript.state = Movescript.State.Meleeairattack;
            movementscript.ChangeAnimationState(swordswitchstate);
        }
        else
        {
            attackestate = Attackstate.waitforattack;
        }
    }
    private void fistweaponswitchend()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        attackestate = Attackstate.waitforattack;
        movementscript.switchtogroundstate();
        Statics.otheraction = false;
    }
}
