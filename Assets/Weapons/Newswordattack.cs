using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newswordattack : MonoBehaviour
{
    private SpielerSteu controlls;
    private Animator animator;
    private Movescript movementscript;
    private SwordController swordcontroller;

    public bool root;
    private float basicattackcd;
    public int combochain;

    public bool readattackinput;
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

    //weaponswitch
    private float checkforenemyonswitchrange = 3f;
    public LayerMask weaponswitchlayer;

    void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        movementscript = GetComponent<Movescript>();
        animator = GetComponent<Animator>();
        swordcontroller = GetComponent<SwordController>();
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
                case Attackstate.attackchain:
                    attackchaininput();
                    break;
                case Attackstate.weaponswitch:
                    break;
                default:
                    break;
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
                movementscript.gravitation = 0f;
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
    private void attackchaininput()
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
    public void swordgroundbasci2end()
    {
        if (readattackinput == true) groundattackchainend();
        else
        {
            attackestate = Attackstate.attackchain;
            if (down) movementscript.ChangeAnimationState(grounddownstate);
            else if (mid) movementscript.ChangeAnimationState(groundmidstate);
            else if (up) movementscript.ChangeAnimationState(groundupstate);
        }
    }
    public void swordgrounddownend()
    {
        if (readattackinput == true) groundattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
    }
    public void swordgroundmidend()
    {
        if (readattackinput == true) groundattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
    }
    public void swordgrounduproot()
    {
        movementscript.attackonceair = false;
        root = true;
        movementscript.state = Movescript.State.Airattack;
        movementscript.graviti = 0f;
        movementscript.gravitation = 0f;
    }
    public void swordgroundupend()
    {
        root = false;
        if (readattackinput == true) airattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(airbasic1state);
        }
    }

    public void airattackchainend()
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
    public void swordairdownroot()
    {
        root = true;
    }
    public void swordairdownend()
    {
        root = false;
        if (readattackinput == true) groundattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
    }
    public void swordairmidend()
    {
        if (readattackinput == true) airattackchainend();
        else
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(airbasic1state);
        }
    }
    public void swordairupend()
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
            movementscript.state = Movescript.State.Airattack;
            //movementscript.charactercontroller.stepOffset = 0;
            movementscript.ChangeAnimationState(swordswitchstate);
        }
        else
        {
            attackestate = Attackstate.waitforattack;
        }
    }
    public void swordweaponswitchend()
    {
        attackestate = Attackstate.waitforattack;
        movementscript.state = Movescript.State.Ground;
        Statics.otheraction = false;
    }
}
