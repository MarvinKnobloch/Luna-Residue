using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowattack : MonoBehaviour
{
    private Movescript movementscript;
    private Healingscript healingscript;
    private EleAbilities eleAbilities;
    private SpielerSteu controlls;
    private Animator animator;

    public GameObject playerarrow;

    private bool root;
    private float basicattackcd;
    //public int combochain;
    private int bowaircount;

    private bool readattackinput;
    private bool down;
    private bool mid;
    private bool up;

    //castarrow
    public GameObject singlearrow;
    private float basicarrowdmg = 5;
    private float singleendarrowdmg = 8;
    private float singledoublearrowdmg = 5;

    public GameObject aoearrow;
    private float aoearrowdmg = 7;

    public GameObject puzzlearrow;

    public Transform Arrowlaunchposi;
    public Transform Camtransform;
    public LayerMask Arrowraycastlayer;
    public LayerMask Puzzlelayer;

    //animations
    const string groundbasic1state = "Bowbasic";
    const string groundbasic2state = "Bowbasic2";
    const string grounddownstate = "Bowdownend";
    const string groundmidstate = "Bowmidend";
    const string groundupstate = "Bowupend";
    const string bowairchargestate = "Bowaircharge";
    const string bowairholdstate = "Bowairhold";
    const string bowairreleasestate = "Bowairrelease";
    const string airdownstate = "Bowairdown";
    const string airmidstate = "Bowairmid";
    const string airupstate = "Bowairup";
    const string bowbackflipstate = "Bowweaponswitchbackflip";
    const string slowmochargeup = "Slowmocharge";
    const string slowmoshoot = "Slowmoshoot";
    const string slowmofastcharge = "Slowmofastcharge";
    const string dashstate = "Bowdash";
    const string starthookstate = "Bowhookcharge";
    const string hookshotstate = "Bowhookshot";
    const string chargestate = "Chargearrow";

    //weaponswitch
    private float checkforenemyonswitchrange = 4f;
    public LayerMask weaponswitchlayer;
    private float slowmopercentage = 0.3f;

    void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        animator = GetComponent<Animator>();
        movementscript = GetComponent<Movescript>();
        healingscript = GetComponent<Healingscript>();
        eleAbilities = GetComponent<EleAbilities>();
    }
    private void OnEnable()
    {
        controlls.Enable();
        root = false;
        movementscript.currentstate = null;
        basicattackcd = 1f;
        bowaircount = 0;
        readattackinput = false;
        attackestate = Attackstate.weaponswitch;
        StartCoroutine(startweaponswitch());
    }
    private void OnDisable()
    {
        CancelInvoke();
        playerarrow.SetActive(false);
    }
    private Attackstate attackestate;
    enum Attackstate
    {
        waitforattack,
        attack1,
        attack2,
        groundattackchain,
        airattackchain,
        bowairattack,
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
                case Attackstate.bowairattack:
                    bowairbasicinput();
                    break;
                case Attackstate.groundattackchain:
                    groundattackchaininput();
                    break;
                case Attackstate.airattackchain:
                    break;
                case Attackstate.groundintoair:
                    groundintoairinput();
                    break;
                case Attackstate.weaponswitch:
                    shotweaponswitcharrow();                                //gleicher input, damit ich nicht noch mehr schreiben muss
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
            if (Movescript.lockontarget != null && controlls.Player.Attack4.WasPressedThisFrame() && Statics.otheraction == false)           // bowhookshot
            {
                if (Vector3.Distance(transform.position, Movescript.lockontarget.position) > 2f)
                {
                    movementscript.state = Movescript.State.Empty;
                    attackestate = Attackstate.waitforattack;
                    movementscript.graviti = 0f;
                    Statics.otheraction = true;
                    transform.rotation = Quaternion.LookRotation(Movescript.lockontarget.transform.position - transform.position, Vector3.up);
                    movementscript.ChangeAnimationState(starthookstate);
                }
            }
            if (movementscript.state == Movescript.State.Ground)                               //out of Combat aim
            {
                if (Statics.infight == false && controlls.Player.Attack4.WasPressedThisFrame() && Statics.otheraction == false)
                {
                    Statics.otheraction = true;
                    movementscript.ChangeAnimationState(chargestate);
                    movementscript.switchtooutofcombataim();
                }
            }
        }
    }
    private void waitforattackinput()                   //input für attackchainstart
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
                movementscript.attackcombochain = 0;
            }
        }
        else if (movementscript.state == Movescript.State.Air)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame() && movementscript.airattackminheight == true && movementscript.attackonceair == true && Statics.otheraction == false)// && Statics.infight == true)
            {
                movementscript.switchtoattackaimstate();
                attackestate = Attackstate.bowairattack;
                movementscript.graviti = 0f;       
                Statics.otheraction = true;
                bowaircount = 0;
                movementscript.attackonceair = false;
                movementscript.attackcombochain = 0;
                readattackinput = false;
                movementscript.ChangeAnimationState(bowairchargestate);
            }
        }
    }
    private void attack1input()                                //basic2groundinput
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack2.WasPressedThisFrame())
            { 
                readattackinput = false; 
            }
        }
    }
    private void bowairbasicinput()                         //Air aim input
    {
        if (readattackinput == true)
        {
            if (bowaircount <= 1)
            {
                if (controlls.Player.Attack1.WasPressedThisFrame())
                {
                    bowaircount++;
                    shotairbasicarrow();
                    movementscript.ChangeAnimationStateInstant(bowairreleasestate);
                    readattackinput = false;
                }
            }
            else
            {
                if (controlls.Player.Attack1.WasPressedThisFrame())
                {
                    movementscript.attackcombochain++;
                    movementscript.ChangeAnimationState(airdownstate);
                    readattackinput = false;
                }
                else if (controlls.Player.Attack2.WasPressedThisFrame())
                {
                    movementscript.attackcombochain++;
                    bowaircount = 0;
                    shotairmidarrow();
                    movementscript.ChangeAnimationState(airmidstate);
                    readattackinput = false;
                }
                else if (controlls.Player.Attack3.WasPressedThisFrame())
                {
                    movementscript.attackcombochain++;
                    bowaircount = 0;
                    shotairuparrow();
                    movementscript.ChangeAnimationState(airupstate);
                    readattackinput = false;
                }
            }
        }
    }
    private void attack2input()                   //groundend input
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
    private void groundattackchaininput()
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                readattackinput = false;
            }
        }
    }
    private void airattackchaininput()
    {
        if (readattackinput == true && movementscript.attackcombochain < 2)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                attackestate = Attackstate.bowairattack;
                readattackinput = false;
                movementscript.ChangeAnimationStateInstant(bowairchargestate);
            }
        }
    }
    private void shotweaponswitcharrow()
    {
        if (readattackinput == true)
        {
            if (controlls.Player.Attack1.WasPressedThisFrame())
            {
                weaponswitchshootarrow();
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
        if (Statics.enemyspezialtimescale == false)
        {
            Time.timeScale = Statics.normalgamespeed;
            Time.fixedDeltaTime = Statics.normaltimedelta;
        }
        if (movementscript.Cam2.gameObject.activeSelf == true)
        {
            movementscript.disableaimcam();
        }
        movementscript.gravitation = movementscript.normalgravition;
        playerarrow.SetActive(false);
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = true;
        root = true;
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
    private void bowdashend()
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
        playerarrow.SetActive(false);
        readattackinput = false;
        movementscript.switchtogroundstate();
        attackestate = Attackstate.waitforattack;
        Statics.otheraction = false;
        movementscript.attackcombochain = 0;
        basicattackcd = 0;
    }
    private void bowgroundbasicend()
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
    private void bowgroundbasci2end()
    {
        if (readattackinput == true) groundattackchainend();
        else
        {
            if (down == true)
            {
                attackestate = Attackstate.groundattackchain;
                if (down) movementscript.ChangeAnimationState(grounddownstate);
            }
            else if (mid == true)
            {
                attackestate = Attackstate.groundattackchain;
                movementscript.ChangeAnimationState(groundmidstate);
            }
            else if (up == true)
            {
                attackestate = Attackstate.groundintoair;
                movementscript.ChangeAnimationState(groundupstate);
            }
        }
    }
    private void bowstaygroundend()
    {
        if (readattackinput == false && movementscript.attackcombochain < 2)
        {
            attackestate = Attackstate.attack1;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
        else groundattackchainend();
    }
    private void bowgrounduproot()
    {
        bowaircount = 0;
        movementscript.attackonceair = false;
        root = true;
        movementscript.state = Movescript.State.Empty;
        movementscript.graviti = 0f;
    }
    private void bowgroundupend()
    {
        root = false;
        if (readattackinput == true) airattackchainend();
        else
        {
            movementscript.switchtoattackaimstate();
            attackestate = Attackstate.bowairattack;
            movementscript.ChangeAnimationStateInstant(bowairchargestate);
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
        movementscript.disableaimcam();
        playerarrow.SetActive(false);
    }
    private void bowaircharge() => movementscript.ChangeAnimationState(bowairchargestate);

    private void bowairhold()
    {
        readattackinput = true;
        movementscript.ChangeAnimationState(bowairholdstate);
    }
    private void checkforaircombo()
    {
        if (movementscript.attackcombochain < 2) movementscript.ChangeAnimationState(bowairchargestate);
        else airattackchainend();
    }
    private void startbowair3intoground()
    {
        root = true;
        attackestate = Attackstate.groundattackchain;
        movementscript.playeraim.aimend();
        playerarrow.SetActive(false);
        if (Movescript.lockontarget == null)
        {
            movementscript.playerlockon.lookfortarget();
        }
    }
    private void bowairdownend()
    {
        root = false;
        if (readattackinput == false && movementscript.attackcombochain < 2)
        {
            attackestate = Attackstate.attack1;
            movementscript.graviti = -0.5f;
            movementscript.state = Movescript.State.Groundattack;
            movementscript.ChangeAnimationState(groundbasic1state);
        }
        else groundattackchainend();
    }
    IEnumerator startweaponswitch()
    {
        yield return null;
        bowweaponswitch();
    }
    private void bowweaponswitch()
    {
        if (Physics.CheckSphere(transform.position, checkforenemyonswitchrange, weaponswitchlayer))
        {
            Statics.otheraction = true;
            movementscript.attackonceair = false;
            movementscript.state = Movescript.State.Bowweaponswitch;
            attackestate = Attackstate.weaponswitch;
            root = true;
            movementscript.ChangeAnimationState(bowbackflipstate);
        }
        else
        {
            attackestate = Attackstate.waitforattack;
        }
    }
    private void bowswitchslowmotion()
    {
        movementscript.switchtoattackaimstate();
        root = false;
        movementscript.ChangeAnimationState(slowmochargeup);
        Time.timeScale = slowmopercentage;
        Time.fixedDeltaTime = Statics.normaltimedelta * slowmopercentage;
        movementscript.graviti = -1.5f;
        Invoke("bowweaponswitchattackend", 1.5f);
    }
    private void bowweaponswitchattackend()
    {
        Statics.otheraction = false;
        movementscript.disableaimcam();
        playerarrow.SetActive(false);
        Time.timeScale = Statics.normalgamespeed;
        Time.fixedDeltaTime = Statics.normaltimedelta;
        movementscript.switchtoairstate();
        attackestate = Attackstate.waitforattack;
    }
    private void weaponswitchshootarrow()
    {
        shotairbasicarrow();
        playerarrow.SetActive(false);
        movementscript.ChangeAnimationState(slowmoshoot);
    }
    private void slowmofastercharge()
    {
        movementscript.ChangeAnimationState(slowmofastcharge);
    }
    private void hookshotstart()
    {
        movementscript.ChangeAnimationState(hookshotstate);
        movementscript.state = Movescript.State.Bowhookshot;
    }
    private void pullarrow() => playerarrow.SetActive(true);
    private void arrowfalse() => playerarrow.SetActive(false);

    private void shotsinglearrow(float dmg, int type)
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 arrowrotation = (Movescript.lockontarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = Instantiate(singlearrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Arrow.GetComponent<Singlearrow>().Arrowtarget = Movescript.lockontarget.gameObject;
            Arrow.GetComponent<Singlearrow>().setarrowvalues(dmg, type);
            //Singlegroundarrow arrowcontroller = Arrow.GetComponent<Singlegroundarrow>();
            //arrowcontroller.Arrowtarget = null; // Movescript.lockontarget.gameObject;
            arrowfalse();
        }
    }
    private void shotbasicgroundarrow() => shotsinglearrow(basicarrowdmg, 0);
    private void shotgroundmid() => shotsinglearrow(singledoublearrowdmg, 2);
    private void shotgroundup() => shotsinglearrow(singleendarrowdmg, 3);
    private void shotairdown() => shotsinglearrow(singleendarrowdmg, 1);
    private void shothookarrow() => shotsinglearrow(basicarrowdmg, 0);

    private void shotaoearrow(float dmg, int radius, int type)
    {
        if (Movescript.lockontarget != null)
        {
            Vector3 arrowrotation = (Movescript.lockontarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = Instantiate(aoearrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Arrow.GetComponent<Aoearrow>().Arrowtarget = Movescript.lockontarget.gameObject;
            Arrow.GetComponent<Aoearrow>().setarrowvalues(dmg, radius, type);
            arrowfalse();
        }
    }
    private void shotgrounddown() => shotaoearrow(aoearrowdmg, 3, 1);

    private void shotsinglearrowwhileaim(float dmg, int type)
    {
        if (Physics.Raycast(Camtransform.position, Camtransform.forward, out RaycastHit hit, Mathf.Infinity, Arrowraycastlayer, QueryTriggerInteraction.Ignore))
        {
            Vector3 arrowrotation = (hit.point - Arrowlaunchposi.position).normalized;
            GameObject Arrow = Instantiate(singlearrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Arrow.GetComponent<Singlearrow>().Arrowtarget = hit.transform.gameObject;
            Arrow.GetComponent<Singlearrow>().setarrowvalues(dmg, type);
            //SingleAirarrow arrowcontroller = Arrow.GetComponent<SingleAirarrow>();
            //arrowcontroller.arrowziel = hit.point;
            //arrowcontroller.Arrowtarget = hit.transform;
            //arrowcontroller.hit = true;
        }
        else
        {
            Debug.Log("arrow does not hit");
        }
    }
    private void shotairbasicarrow() => shotsinglearrowwhileaim(basicarrowdmg, 0);
    private void shotairmidarrow() => shotsinglearrowwhileaim(singleendarrowdmg, 2);

    private void shotaoearrowwhileaim(float dmg, float radius, int type)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camtransform.position, Camtransform.forward, out hit, Mathf.Infinity, Arrowraycastlayer, QueryTriggerInteraction.Ignore))
        {
            Vector3 arrowrotation = (hit.point - Arrowlaunchposi.position).normalized;
            GameObject Arrow = Instantiate(aoearrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Arrow.GetComponent<Aoearrow>().Arrowtarget = hit.transform.gameObject;
            Arrow.GetComponent<Aoearrow>().setarrowvalues(dmg,radius, type);
        }
        else
        {
            Debug.Log("arrow does not hit");
        }
    }
    private void shotairuparrow() => shotaoearrowwhileaim(aoearrowdmg, 3 , 3);

    private void shotpuzzlearrow()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camtransform.position, Camtransform.forward, out hit, Mathf.Infinity, Arrowraycastlayer, QueryTriggerInteraction.Ignore))
        {
            Vector3 arrowrotation = (hit.point - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(puzzlearrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Puzzlearrow arrowcontroller = Arrow.GetComponent<Puzzlearrow>();
            arrowcontroller.arrowziel = hit.point;
            arrowcontroller.hit = true;
        }
    }

}
