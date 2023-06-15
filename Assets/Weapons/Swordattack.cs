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
    public LayerMask weaponswitchlayer;

    //audio
    [SerializeField] private Playersounds playersounds;

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
        Statics.dash = false;
        movementscript.currentstate = null;
        basicattackcd = 1f;
        swordcontroller.enabled = true;
        readattackinput = false;
        eleAbilities.stopignorelayers();
        swordweaponswitch();
    }

    [SerializeField] private Attackstate attackestate;
    enum Attackstate
    {
        waitforattack,
        attack1,
        attack2,
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
                    switchtowaitforattack();
                    break;
                case Attackstate.attack2:
                    attack2input();
                    switchtowaitforattack();
                    break;
                case Attackstate.attackchain:
                    attackchaininput();
                    switchtowaitforattack();
                    break;
                case Attackstate.weaponswitch:
                    switchtowaitforattack();
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
        if (Statics.resetvaluesondeathorstun == true)
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
    private void switchtowaitforattack()          //schutz gegen attackstate stuck(zb. bei meleeattackgroundcheck -> slidewalls/switchtoair)
    {
        if (movementscript.state == Movescript.State.Ground)
        {
            readattackinput = false;
            attackestate = Attackstate.waitforattack;
            Statics.otheraction = false;
            movementscript.attackcombochain = 0;
            basicattackcd = 0;
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
        else if (movementscript.airattackminheight == true)      //(movementscript.state == Movescript.State.Air || movementscript.state == Movescript.State.Upwards)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame() && movementscript.attackonceair == true && Statics.otheraction == false && Statics.infight == true)
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
        LoadCharmanager.interaction = false;
        Statics.otheraction = true;
        CancelInvoke();
        healingscript.resethealvalues();
        eleAbilities.stopignorelayers();
        eleAbilities.resetelevalues();
    }
    private void dash()
    {
        Physics.IgnoreLayerCollision(11, 6);
        Physics.IgnoreLayerCollision(8, 6);
        movementscript.state = Movescript.State.Empty;
        movementscript.ChangeAnimationStateInstant(dashstate);
    }
    private void sworddashend()
    {
        root = false;
        GlobalCD.startresetdash();
        Statics.playeriframes = false;
        Statics.otheraction = false;
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
        movementscript.attackcombochain = 0;
        basicattackcd = 0;
    }
    private void swordgroundbasicend()
    {
        if (movementscript.state != Movescript.State.Meleegroundattack) return;                   //sonst kommt der call von der animation noch durch obwohl schon die animation gewechselt würde
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
        if (movementscript.state != Movescript.State.Meleegroundattack) return;
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
                attackestate = Attackstate.attackchain;
                movementscript.ChangeAnimationState(groundupstate);
            }
        }
    }
    private void swordstaygroundend()
    {
        if (movementscript.state != Movescript.State.Meleegroundattack) return;
        if (readattackinput == false && movementscript.attackcombochain < 2)
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
        else groundattackchainend();
    }
    private void swordgrounduproot()
    {
        if (movementscript.state != Movescript.State.Meleegroundattack) return;
        movementscript.attackonceair = false;
        root = true;
        movementscript.state = Movescript.State.Meleeairattack;
        movementscript.graviti = 0f;
    }
    private void swordgroundupend()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
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
        movementscript.attackcombochain = 0;
        basicattackcd = 0;
    }
    private void swordbasicairend()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
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
        if (movementscript.state != Movescript.State.Meleeairattack) return;
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
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        root = true;
        eleAbilities.ignorelayers();
    }
    private void swordairdownend()
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
        else groundattackchainend();
    }
    private void swordstayairend()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        if (readattackinput == false && movementscript.attackcombochain < 2)
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(airbasic1state);
        }
        else airattackchainend();
    }
    private void swordweaponswitch()
    {
        if (Physics.CheckSphere(transform.position, Statics.weaponswitchattackrange, weaponswitchlayer))
        {
            Statics.otheraction = true;
            movementscript.attackonceair = true;
            attackestate = Attackstate.weaponswitch;
            movementscript.state = Movescript.State.Meleeairattack;
            movementscript.graviti = 0;
            movementscript.ChangeAnimationState(swordswitchstate);
        }
        else
        {
            Statics.otheraction = false;
            movementscript.switchtoairstate();
            attackestate = Attackstate.waitforattack;
        }
    }
    private void swordweaponswitchend()
    {
        if (movementscript.state != Movescript.State.Meleeairattack) return;
        Statics.otheraction = false;
        movementscript.switchtoairstate();
        attackestate = Attackstate.waitforattack;
    }
}
